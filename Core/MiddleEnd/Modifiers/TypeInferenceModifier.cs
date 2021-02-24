namespace StatisticsPoland.VtlProcessing.Core.MiddleEnd.Modifiers
{
    using StatisticsPoland.VtlProcessing.Core.Infrastructure.DependencyInjection;
    using StatisticsPoland.VtlProcessing.Core.MiddleEnd.Modifiers.Interfaces;
    using StatisticsPoland.VtlProcessing.Core.Models;
    using StatisticsPoland.VtlProcessing.Core.Models.Interfaces;
    using System;
    using System.Linq;

    /// <summary>
    /// Modifier performing a type inference.
    /// </summary>
    public class TypeInferenceModifier : ISchemaModifier
    {
        private readonly OperatorResolver opResolver;
        private readonly IDataModel dataModel;

        /// <summary>
        /// Initializes a new instance of the <see cref="TypeInferenceModifier"/> class.
        /// </summary>
        /// <param name="opResolver">The operator resolver.</param>
        /// <param name="dataModel">The data model.</param>
        public TypeInferenceModifier(OperatorResolver opResolver, IDataModel dataModel)
        {
            this.opResolver = opResolver;
            this.dataModel = dataModel;
        }

        /// <summary>
        /// Performs a type inference for the schema.
        /// </summary>
        /// <param name="schema">The schema to modify.</param>
        public void Modify(ITransformationSchema schema)
        {
            foreach (IRuleset ruleset in schema.Rulesets)
            {
                foreach (IExpression expr in ruleset.RulesCollection)
                {
                    this.Infer(expr);
                }

                ruleset.InferStructure();
            }

            foreach (AssignmentObject assignmentObject in schema.AssignmentObjects)
            {
                this.Infer(assignmentObject.Expression);
                if (assignmentObject.IsPersistentAssignment) this.ValidatePersistentAssignment(assignmentObject.Expression);
            }
        }

        /// <summary>
        /// Inferes types for an expression.
        /// </summary>
        /// <param name="expr">The expression to infere types for.</param>
        private void Infer(IExpression expr)
        {
            foreach (IExpression op in expr.OperandsCollection)
            {
                this.Infer(op);
            }

            if (expr.OperatorSymbol == "get" && expr.CurrentJoinExpr != null && expr.GetFirstAncestorExpr("Alias") == null)
            {
                expr.ResultName = "Reference";
                expr.OperatorDefinition = this.opResolver("ref");
                expr.ReferenceExpression = expr.CurrentJoinExpr.Operands["ds"].OperandsCollection.FirstOrDefault(alias => alias.ParamSignature == expr.ExpressionText);
            }

            if (expr.OperatorSymbol != null && expr.Structure == null)
                expr.Structure = expr.OperatorDefinition.GetOutputStructure(expr);
        }

        /// <summary>
        /// Validates a persistent assignment expression.
        /// </summary>
        /// <param name="expr">The expression to validate.</param>
        private void ValidatePersistentAssignment(IExpression expr)
        {
            IDataStructure structure = null;
            try
            {
                structure = this.dataModel.GetDatasetStructure(expr.ResultName);

                string[] split = expr.ResultName.Split(@"\");
                if (split.Length == 1)
                    expr.ResultName = $@"{this.dataModel.DefaultNamespace}\{expr.ResultName}";
            }
            catch
            {
                if (structure == null) throw new Exception($"'{expr.ResultName}' doesn't exist in the data model.");
            }

            if (!structure.IsSupersetOf(expr.Structure, true, true)) throw new Exception("Structures of datasets don't match.");
        }
    }
}