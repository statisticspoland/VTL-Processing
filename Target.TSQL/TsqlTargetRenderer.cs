namespace StatisticsPoland.VtlProcessing.Target.TSQL
{
    using Microsoft.Extensions.Logging;
    using StatisticsPoland.VtlProcessing.Core.BackEnd;
    using StatisticsPoland.VtlProcessing.Core.ErrorHandling;
    using StatisticsPoland.VtlProcessing.Core.Infrastructure.DependencyInjection;
    using StatisticsPoland.VtlProcessing.Core.Models;
    using StatisticsPoland.VtlProcessing.Core.Models.Interfaces;
    using System;
    using System.Text;
    using Infrastructure;
    using Infrastructure.Interfaces;
    using Preparers.Interfaces;
    using StatisticsPoland.VtlProcessing.Core.Infrastructure.Interfaces;

    /// <summary>
    /// The TSQL target renderer for the VTL 2.0 translation.
    /// </summary>
    public class TsqlTargetRenderer : ITargetRenderer
    {
        private readonly TransformationSchemaResolver _schemaResolver;
        private readonly OperatorRendererResolver _opRendererResolver;
        private readonly TemporaryTables _tmpTables;
        private readonly IReferencesManager _refs;
        private readonly IMapper _mapper;
        private readonly ITargetConfiguration _conf;
        private readonly IEnvironmentMapper _envMapper;
        private readonly ILogger<ITargetRenderer> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="TsqlTargetRenderer"/> class.
        /// </summary>
        /// <param name="schemaResolver">The transformation schema resolver.</param>
        /// <param name="opRendererResolver">The operator renderer resolver.</param>
        /// <param name="references">The references manager.</param>
        /// <param name="tmpTables">The temporary tables informations.</param>
        /// <param name="mapper">The objects names mapper.</param>
        /// <param name="configuration">The configuration of the target.</param>
        /// <param name="envMapper">The environment names mapper.</param>
        /// <param name="logger">The errors logger.</param>
        public TsqlTargetRenderer(
            TransformationSchemaResolver schemaResolver,
            OperatorRendererResolver opRendererResolver, 
            TemporaryTables tmpTables, 
            IReferencesManager references,
            IMapper mapper, 
            ITargetConfiguration configuration,
            IEnvironmentMapper envMapper,
            ILogger<ITargetRenderer> logger = null)
        {
            this._schemaResolver = schemaResolver;
            this._opRendererResolver = opRendererResolver;
            this._tmpTables = tmpTables;
            this._refs = references;
            this._mapper = mapper;
            this._conf = configuration;
            this._envMapper = envMapper;
            this._logger = logger;
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
                this._mapper.MapNames(schema);
                this._refs.TakeNonPersistentExprs(schema);            

                foreach (AssignmentObject assignmentObject in schema.AssignmentObjects)
                {
                    if (this._refs.ContainsExpression(assignmentObject.Expression)) 
                        assignmentsSB.AppendLine(this._refs.RenderNonPersistentExpr(assignmentObject.Expression)); // non-persistent assignment expressions rendering
                    else 
                        assignmentsSB.AppendLine(this.RenderPersistentExpr(assignmentObject.Expression)); // persistent assignment expressions rendering
                }

                if (this._conf.UseComments) scriptSB.AppendLine($"-- Script generated: {DateTime.Now}");
                scriptSB.AppendLine("BEGIN TRANSACTION\n");
                for (int i = 1; i <= this._tmpTables.Count; i++)
                {
                    scriptSB.AppendLine($"IF OBJECT_ID (N'tempdb..{this._tmpTables.Name}{i}', N'U') IS NOT NULL");
                    scriptSB.AppendLine($"DROP TABLE {this._tmpTables.Name}{i}\n");
                }

                scriptSB.AppendLine(this._refs.RenderDroppingOfTables());
                scriptSB.Append(assignmentsSB.ToString());
                scriptSB.AppendLine("COMMIT TRANSACTION");
                scriptSB.AppendLine("GO");
            }
            catch (VtlTargetError ex)
            {
                this._logger?.LogError(ex, ex.Message);
            }
            catch (Exception ex)
            {
                this._logger?.LogCritical(ex, ex.Message);
            }

            return scriptSB.ToString();
        }

        public string Render(IExpression expression)
        {
            this._mapper.MapNames(expression.ContainingSchema);
            this._refs.TakeNonPersistentExprs(this._schemaResolver()); // wyczyszczenie referencji

            return this._opRendererResolver(expression.OperatorSymbol).Render(expression);
        }

        /// <summary>
        /// Renders a TSQL translated code for the persistent assignment expression.
        /// </summary>
        /// <param name="expr">The expression.</param>
        /// <returns>The TSQL translated code.</returns>
        private string RenderPersistentExpr(IExpression expr)
        {
            StringBuilder sb = new StringBuilder();
            string resultName = this._envMapper.Map(expr.ResultName); //expr.ParamSignature == "<root>" ? expr.ResultName : expr.ResultMappedName;
            string renderResult = this._opRendererResolver(expr.OperatorSymbol).Render(expr);

            if (this._conf.UseComments) sb.AppendLine($"-- Raw: {expr.ResultName} <- {expr.ExpressionText}");
            if (renderResult.Contains(resultName))
            {
                string tmp = $"{this._tmpTables.Name}{++this._tmpTables.Count}";

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
