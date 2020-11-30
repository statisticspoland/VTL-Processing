namespace Target.TSQL
{
    using Microsoft.Extensions.Logging;
    using StatisticsPoland.VtlProcessing.Core.BackEnd;
    using StatisticsPoland.VtlProcessing.Core.ErrorHandling;
    using StatisticsPoland.VtlProcessing.Core.Infrastructure.DependencyInjection;
    using StatisticsPoland.VtlProcessing.Core.Models;
    using StatisticsPoland.VtlProcessing.Core.Models.Interfaces;
    using System;
    using System.Text;
    using Target.TSQL.Infrastructure;
    using Target.TSQL.Infrastructure.Interfaces;
    using Target.TSQL.Preparers.Interfaces;

    /// <summary>
    /// The TSQL target renderer for the VTL 2.0 translation.
    /// </summary>
    public class TsqlTargetRenderer : ITargetRenderer
    {
        private readonly TransformationSchemaResolver schemaResolver;
        private readonly OperatorRendererResolver opRendererResolver;
        private readonly TemporaryTables tmpTables;
        private readonly IReferencesManager refs;
        private readonly IMapper mapper;
        private readonly ILogger<ITargetRenderer> logger;
        private readonly ITargetConfiguration conf;

        /// <summary>
        /// Initializes a new instance of the <see cref="TsqlTargetRenderer"/> class.
        /// </summary>
        /// <param name="schemaResolver">The transformation schema resolver.</param>
        /// <param name="opRendererResolver">The operator renderer resolver.</param>
        /// <param name="references">The references manager.</param>
        /// <param name="tmpTables">The temporary tables informations.</param>
        /// <param name="mapper">The objects names mapper.</param>
        /// <param name="configuration">The configuration of the target.</param>
        /// <param name="logger">The errors logger.</param>
        public TsqlTargetRenderer(
            TransformationSchemaResolver schemaResolver,
            OperatorRendererResolver opRendererResolver, 
            TemporaryTables tmpTables, 
            IReferencesManager references,
            IMapper mapper, 
            ITargetConfiguration configuration,
            ILogger<ITargetRenderer> logger = null)
        {
            this.schemaResolver = schemaResolver;
            this.opRendererResolver = opRendererResolver;
            this.tmpTables = tmpTables;
            this.refs = references;
            this.mapper = mapper;
            this.conf = configuration;
            this.logger = logger;
        }

        /// <summary>
        /// Gets the name of the target renderer.
        /// </summary>
        public string Name => "TSQL";

        public string Render(ITransformationSchema schema)
        {
            StringBuilder scriptSB = new StringBuilder();
            StringBuilder assignmentsSB = new StringBuilder();

            try
            {
                this.mapper.MapNames(schema);
                this.refs.TakeNonPersistentExprs(schema);            

                foreach (AssignmentObject assignmentObject in schema.AssignmentObjects)
                {
                    if (this.refs.ContainsExpression(assignmentObject.Expression)) 
                        assignmentsSB.AppendLine(this.refs.RenderNonPersistentExpr(assignmentObject.Expression)); // non-persistent assignment expressions rendering
                    else 
                        assignmentsSB.AppendLine(this.RenderPersistentExpr(assignmentObject.Expression)); // persistent assignment expressions rendering
                }

                if (this.conf.UseComments) scriptSB.AppendLine($"-- Script generated: {DateTime.Now}");
                scriptSB.AppendLine("BEGIN TRANSACTION\n");
                for (int i = 1; i <= this.tmpTables.Count; i++)
                {
                    scriptSB.AppendLine($"IF OBJECT_ID (N'tempdb..{this.tmpTables.Name}{i}', N'U') IS NOT NULL");
                    scriptSB.AppendLine($"DROP TABLE {this.tmpTables.Name}{i}\n");
                }

                scriptSB.AppendLine(this.refs.RenderDroppingOfTables());
                scriptSB.Append(assignmentsSB.ToString());
                scriptSB.AppendLine("COMMIT TRANSACTION");
                scriptSB.AppendLine("GO");
            }
            catch (VtlTargetError ex)
            {
                this.logger?.LogError(ex, ex.Message);
            }
            catch (Exception ex)
            {
                this.logger?.LogCritical(ex, ex.Message);
            }

            return scriptSB.ToString();
        }

        public string Render(IExpression expression)
        {
            this.mapper.MapNames(expression.ContainingSchema);
            this.refs.TakeNonPersistentExprs(this.schemaResolver()); // wyczyszczenie referencji

            return this.opRendererResolver(expression.OperatorSymbol).Render(expression);
        }

        /// <summary>
        /// Renders a TSQL translated code for the persistent assignment expression.
        /// </summary>
        /// <param name="expr">The expression.</param>
        /// <returns>The TSQL translated code.</returns>
        private string RenderPersistentExpr(IExpression expr)
        {
            StringBuilder sb = new StringBuilder();
            string resultName = this.conf.EnvMapper.Map(expr.ResultName); //expr.ParamSignature == "<root>" ? expr.ResultName : expr.ResultMappedName;
            string renderResult = this.opRendererResolver(expr.OperatorSymbol).Render(expr);

            if (this.conf.UseComments) sb.AppendLine($"-- Raw: {expr.ResultName} <- {expr.ExpressionText}");
            if (renderResult.Contains(resultName))
            {
                string tmp = $"{this.tmpTables.Name}{++this.tmpTables.Count}";

                sb.AppendLine($"SELECT * INTO {tmp} FROM (\n{renderResult}) AS t");
                sb.AppendLine($"DELETE FROM {resultName}");
                sb.AppendLine($"INSERT INTO {resultName} SELECT * FROM {tmp}");
            }
            else
            {
                sb.AppendLine($"DELETE FROM {resultName}");
                sb.AppendLine($"INSERT INTO {resultName} {renderResult}");
            }

            return sb.ToString();
        }
    }
}
