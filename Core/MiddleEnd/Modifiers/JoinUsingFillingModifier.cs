namespace StatisticsPoland.VtlProcessing.Core.MiddleEnd.Modifiers
{
    using StatisticsPoland.VtlProcessing.Core.Infrastructure.JoinBuilder.Interfaces;
    using StatisticsPoland.VtlProcessing.Core.MiddleEnd.Modifiers.Interfaces;
    using StatisticsPoland.VtlProcessing.Core.Models;
    using StatisticsPoland.VtlProcessing.Core.Models.Interfaces;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Modifier putting "using" branches to "inner" and "left" "join" operator expressions that do not contain them.
    /// </summary>
    public class JoinUsingFillingModifier : ISchemaModifier
    {
        private readonly IJoinBuilder builder;

        /// <summary>
        /// Initializes a new instance of the <see cref="JoinUsingFillingModifier"/> class.
        /// </summary>
        /// <param name="builder">Builder for modifier.</param>
        public JoinUsingFillingModifier(IJoinBuilder builder)
        {
            this.builder = builder;
        }

        /// <summary>
        /// Performs putting "using" branches to "inner" and "left" "join" operator expressions that do not contain them.
        /// </summary>
        /// <param name="schema">The schema to modify.</param>
        public void Modify(ITransformationSchema schema)
        {
            foreach (AssignmentObject assignmentObject in schema.AssignmentObjects)
            {
                this.ModifyJoins(assignmentObject.Expression);
            }
        }


        /// <summary>
        /// Performs putting "using" branches to an "inner" or "left" "join" operator expression that do not contain them.
        /// </summary>
        /// <param name="expression">The expression to modify.</param>
        private void ModifyJoins(IExpression expression)
        {
            if (expression.OperatorSymbol == "join" && !expression.Operands.ContainsKey("using") && expression.OperatorDefinition.Keyword != "cross")
            {
                List<IExpression> branches = expression.OperandsCollection.ToList();

                branches.Insert(1, this.builder.BuildBranch("using", expression));
                expression.OperandsCollection = branches;

                if (expression.Operands.ContainsKey("apply")) this.ModifyJoins(expression.Operands["apply"]);
            }
            else
            {
                foreach (IExpression expr in expression.OperandsCollection)
                {
                    this.ModifyJoins(expr);
                }
            }
        }
    }
}