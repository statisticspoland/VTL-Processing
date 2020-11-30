namespace StatisticsPoland.VtlProcessing.Core.Operators.Auxiliary.ComponentManagement
{
    using StatisticsPoland.VtlProcessing.Core.ErrorHandling;
    using StatisticsPoland.VtlProcessing.Core.Infrastructure;
    using StatisticsPoland.VtlProcessing.Core.Infrastructure.DependencyInjection;
    using StatisticsPoland.VtlProcessing.Core.Infrastructure.Interfaces;
    using StatisticsPoland.VtlProcessing.Core.Models;
    using StatisticsPoland.VtlProcessing.Core.Models.Interfaces;
    using StatisticsPoland.VtlProcessing.Core.Models.Types;
    using StatisticsPoland.VtlProcessing.Core.Operators.Interfaces;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Definition of component version of any operator.
    /// </summary>
    public class JoinApplyMeasuresOperator : IJoinApplyMeasuresOperator
    {
        private readonly IExpressionFactory exprFac;
        private readonly DataStructureResolver dsResolver;

        /// <summary>
        /// Initialises a new instance of the <see cref="JoinApplyMeasuresOperator"/> class.
        /// </summary>
        /// <param name="exprFac">The expression factory.</param>
        /// <param name="dsResolver">The data structure resolver.</param>
        public JoinApplyMeasuresOperator(IExpressionFactory exprFac, DataStructureResolver dsResolver)
        {
            this.exprFac = exprFac;
            this.dsResolver = dsResolver;
        }

        public IDataStructure GetMeasuresStructure(IExpression expression)
        {
            IDataStructure measuresStructure = this.dsResolver();
            List<List<IExpression>> operands = new List<List<IExpression>>();

            foreach (IExpression expr in expression.OperandsCollection)
            {
                IDataStructure structure = expr.Structure;
                if (structure == null && expr.OperatorSymbol != null) 
                    structure = expr.Operands["ds_1"].Structure;

                operands.Add(new List<IExpression>());
                if (expr.IsApplyComponent)
                {
                    for (int i = 0; i < structure.Measures.Count; i++)
                    {
                        StructureComponent measure = structure.Measures[i];
                        IExpression operand = this.exprFac.GetExpression("Alias", ExpressionFactoryNameTarget.ResultName);

                        operand.Structure = this.dsResolver(measure.ComponentName, measure.ComponentType, measure.ValueDomain.DataType);
                        operands.Last().Add(operand);
                    }
                }
                else operands.Last().Add(expr);
            }

            List<IExpression> measuresExprList = operands.First(op => op.FirstOrDefault(o => o.ResultName == "Alias") != null);
            for (int i = 0; i < measuresExprList.Count; i++)
            {
                IExpression operatorExpr = this.exprFac.GetExpression(expression.OperatorSymbol, ExpressionFactoryNameTarget.OperatorSymbol);
                for (int j = 0; j < operands.Count; j++)
                {
                    IExpression operand = operands[j][0];
                    if (operands[j].Count > 1 && j != 0)
                    {
                        if (!this.AreExprListsEqual(operands[j], measuresExprList))
                            throw new VtlOperatorError(expression, expression.OperatorSymbol, "Aliased measures collections don't match each other.");

                        operand = operands[j][i];
                    }

                    operatorExpr.AddOperand($"ds_{j + 1}", operand);
                }

                if (operatorExpr.OperatorSymbol == "if")
                {
                    IExpression[] ifThenElseExprs = operatorExpr.OperandsCollection.ToArray();
                    operatorExpr.Operands.Clear();
                    operatorExpr.AddOperand("if", ifThenElseExprs[0]);
                    operatorExpr.AddOperand("then", ifThenElseExprs[1]);
                    operatorExpr.AddOperand("else", ifThenElseExprs[2]);
                    if (operatorExpr.Operands["if"].ResultName != "If") operatorExpr.Operands["if"].AddOperand("ds_1", ifThenElseExprs[0]);
                    if (operatorExpr.Operands["then"].ResultName != "Then") operatorExpr.Operands["then"].AddOperand("ds_1", ifThenElseExprs[1]);
                    if (operatorExpr.Operands["else"].ResultName != "Else") operatorExpr.Operands["else"].AddOperand("ds_1", ifThenElseExprs[2]);
                }

                IDataStructure operatorStructure = operatorExpr.OperatorDefinition.GetOutputStructure(operatorExpr);
                operatorStructure.Measures[0].BaseComponentName = measuresExprList[i].Structure.Measures[0].BaseComponentName;
                operatorStructure.Measures[0].ComponentName = measuresExprList[i].Structure.Measures[0].ComponentName;
                measuresStructure.AddStructure(operatorStructure);
            }

            return measuresStructure;
        }

        /// <summary>
        /// Specifies if given lists of expressions are equal.
        /// </summary>
        /// <param name="expr1">The first expression list.</param>
        /// <param name="expr2">The second expression list.</param>
        /// <returns>A value specyfing if given lists of expressions are equal.</returns>
        private bool AreExprListsEqual(List<IExpression> expr1, List<IExpression> expr2)
        {
            if (expr1.Count != expr2.Count) return false;
            for (int q = 0; q < expr2.Count; q++)
            {
                if (expr1[q].Structure.Measures[0].ComponentName != expr2[q].Structure.Measures[0].ComponentName ||
                    !expr1[q].Structure.Measures[0].ValueDomain.DataType.EqualsObj(expr2[q].Structure.Measures[0].ValueDomain.DataType))
                {
                    return false;
                }
            }

            return true;
        }
    }
}
