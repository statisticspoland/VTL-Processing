namespace StatisticsPoland.VtlProcessing.Core.Tests.Utilities
{
    using StatisticsPoland.VtlProcessing.Core.Infrastructure.Interfaces;
    using StatisticsPoland.VtlProcessing.Core.Models;
    using StatisticsPoland.VtlProcessing.Core.Models.Interfaces;
    using StatisticsPoland.VtlProcessing.Core.Models.Types;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Factory of testing expressions.
    /// </summary>
    public static class TestExprFactory
    {
        /// <summary>
        /// Gets the expression by testing data type.
        /// </summary>
        /// <param name="type">The testing data type.</param>
        /// <returns>The expression.</returns>
        public static IExpression GetExpression(TestExprType type)
        {
            return TestExprFactory.GetExpression(true, type);
        }

        /// <summary>
        /// Gets the expression by testing data types.
        /// </summary>
        /// <param name="types">The testing data types.</param>
        /// <returns>The expression.</returns>
        public static IExpression GetExpression(params TestExprType[] types)
        {
            return TestExprFactory.GetExpression(false, types);
        }

        /// <summary>
        /// Gets the arithmetic operator expression with a given numeric testing data type.
        /// </summary>
        /// <param name="exprFactory">The expression factory.</param>
        /// <param name="type">The numeric testing data type.</param>
        /// <returns>The expression.</returns>
        public static IExpression GetArithmeticExpr(IExpressionFactory exprFactory, TestExprType type)
        {
            IExpression arithmeticExpr = exprFactory.GetExpression("+", ExpressionFactoryNameTarget.OperatorSymbol);

            switch (type)
            {
                case TestExprType.Integer:
                    arithmeticExpr.AddOperand("ds_1", TestExprFactory.GetExpression(TestExprType.Integer));
                    arithmeticExpr.AddOperand("ds_2", TestExprFactory.GetExpression(TestExprType.Integer));
                    break;
                case TestExprType.Number:
                    arithmeticExpr.AddOperand("ds_1", TestExprFactory.GetExpression(TestExprType.Number));
                    arithmeticExpr.AddOperand("ds_2", TestExprFactory.GetExpression(TestExprType.Number));
                    break;
                case TestExprType.MixedIntNumDataset:
                    arithmeticExpr.AddOperand("ds_1", TestExprFactory.GetExpression(TestExprType.Integer));
                    arithmeticExpr.AddOperand("ds_2", TestExprFactory.GetExpression(TestExprType.Number));
                    break;
                default: throw new Exception();
            }

            arithmeticExpr.Structure = arithmeticExpr.OperatorDefinition.GetOutputStructure(arithmeticExpr);

            return arithmeticExpr;
        }

        /// <summary>
        /// Folds expression from given expressions.
        /// </summary>
        /// <param name="expressions">The expressions.</param>
        /// <returns>The expression.</returns>
        public static IExpression FoldExpression(params IExpression[] expressions)
        {
            return TestExprFactory.FoldExpression(expressions.ToList());
        }

        /// <summary>
        /// Folds expression from given expressions.
        /// </summary>
        /// <param name="expressions">The expressions.</param>
        /// <returns>The expression.</returns>
        public static IExpression FoldExpression(ICollection<IExpression> expressions)
        {
            IExpression expr = ModelResolvers.ExprResolver();

            for (int i = 0; i < expressions.ToArray().Length; i++)
            {
                expr.AddOperand($"ds_{i + 1}", expressions.ToArray()[i]);
            }

            return expr;
        }

        /// <summary>
        /// Gets dataset expression by a given components colection.
        /// </summary>
        /// <param name="components">The components collection.</param>
        /// <returns>The expression.</returns>
        public static IExpression GetDatasetExpr(params KeyValuePair<ComponentType, BasicDataType>[] components)
        {
            string componentName = string.Empty;
            IExpression expr = ModelResolvers.ExprResolver();
            expr.Structure = ModelResolvers.DsResolver();

            for (int i = 0; i < components.Length; i++)
            {
                switch (components[i].Key)
                {
                    case ComponentType.Identifier:
                        componentName = "Id" + (expr.Structure.Identifiers.Count + 1);
                        expr.Structure.Identifiers.Add(new StructureComponent(components[i].Value, componentName));
                        break;
                    case ComponentType.Measure:
                        componentName = "Me" + (expr.Structure.Measures.Count + 1);
                        expr.Structure.Measures.Add(new StructureComponent(components[i].Value, componentName));
                        break;
                    case ComponentType.NonViralAttribute:
                        componentName = "At" + (expr.Structure.NonViralAttributes.Count + expr.Structure.ViralAttributes.Count + 1);
                        expr.Structure.NonViralAttributes.Add(new StructureComponent(components[i].Value, componentName));
                        break;
                    case ComponentType.ViralAttribute:
                        componentName = "At" + (expr.Structure.NonViralAttributes.Count + expr.Structure.ViralAttributes.Count + 1);
                        expr.Structure.ViralAttributes.Add(new StructureComponent(components[i].Value, componentName));
                        break;
                    default: break;
                }
            }

            return expr;
        }

        /// <summary>
        /// Gets the expression by testing data types.
        /// </summary>
        /// <param name="noChildren">Specifies if returning expression has to have descendant expressions.</param>
        /// <param name="types">The testing data types.</param>
        /// <returns>The expression.</returns>
        private static IExpression GetExpression(bool noChildren, params TestExprType[] types)
        {
            if (noChildren && types.Length != 1) throw new Exception();

            IExpression expr = ModelResolvers.ExprResolver();

            for (int i = 0; i < types.Length; i++)
            {
                IExpression operand = ModelResolvers.ExprResolver();
                switch (types[i])
                {
                    case TestExprType.IntsDataset:
                        operand =
                            TestExprFactory.GetDatasetExpr(
                                new KeyValuePair<ComponentType, BasicDataType>(ComponentType.Identifier, BasicDataType.Integer),
                                new KeyValuePair<ComponentType, BasicDataType>(ComponentType.Measure, BasicDataType.Integer),
                                new KeyValuePair<ComponentType, BasicDataType>(ComponentType.Measure, BasicDataType.Integer));
                        break;
                    case TestExprType.NumbersDataset:
                        operand =
                            TestExprFactory.GetDatasetExpr(
                                new KeyValuePair<ComponentType, BasicDataType>(ComponentType.Identifier, BasicDataType.Integer),
                                new KeyValuePair<ComponentType, BasicDataType>(ComponentType.Measure, BasicDataType.Number),
                                new KeyValuePair<ComponentType, BasicDataType>(ComponentType.Measure, BasicDataType.Number));
                        break;
                    case TestExprType.StringsDataset:
                        operand =
                            TestExprFactory.GetDatasetExpr(
                                new KeyValuePair<ComponentType, BasicDataType>(ComponentType.Identifier, BasicDataType.Integer),
                                new KeyValuePair<ComponentType, BasicDataType>(ComponentType.Measure, BasicDataType.String),
                                new KeyValuePair<ComponentType, BasicDataType>(ComponentType.Measure, BasicDataType.String));
                        break;
                    case TestExprType.BoolsDataset:
                        operand =
                            TestExprFactory.GetDatasetExpr(
                                new KeyValuePair<ComponentType, BasicDataType>(ComponentType.Identifier, BasicDataType.Integer),
                                new KeyValuePair<ComponentType, BasicDataType>(ComponentType.Measure, BasicDataType.Boolean),
                                new KeyValuePair<ComponentType, BasicDataType>(ComponentType.Measure, BasicDataType.Boolean));
                        break;
                    case TestExprType.TimesDataset:
                        operand =
                            TestExprFactory.GetDatasetExpr(
                                new KeyValuePair<ComponentType, BasicDataType>(ComponentType.Identifier, BasicDataType.Integer),
                                new KeyValuePair<ComponentType, BasicDataType>(ComponentType.Measure, BasicDataType.Time),
                                new KeyValuePair<ComponentType, BasicDataType>(ComponentType.Measure, BasicDataType.Time));
                        break;
                    case TestExprType.DatesDataset:
                        operand =
                            TestExprFactory.GetDatasetExpr(
                                new KeyValuePair<ComponentType, BasicDataType>(ComponentType.Identifier, BasicDataType.Integer),
                                new KeyValuePair<ComponentType, BasicDataType>(ComponentType.Measure, BasicDataType.Date),
                                new KeyValuePair<ComponentType, BasicDataType>(ComponentType.Measure, BasicDataType.Date));
                        break;
                    case TestExprType.TimePeriodsDataset:
                        operand =
                            TestExprFactory.GetDatasetExpr(
                                new KeyValuePair<ComponentType, BasicDataType>(ComponentType.Identifier, BasicDataType.Integer),
                                new KeyValuePair<ComponentType, BasicDataType>(ComponentType.Measure, BasicDataType.TimePeriod),
                                new KeyValuePair<ComponentType, BasicDataType>(ComponentType.Measure, BasicDataType.TimePeriod));
                        break;
                    case TestExprType.DurationsDataset:
                        operand =
                            TestExprFactory.GetDatasetExpr(
                                new KeyValuePair<ComponentType, BasicDataType>(ComponentType.Identifier, BasicDataType.Integer),
                                new KeyValuePair<ComponentType, BasicDataType>(ComponentType.Measure, BasicDataType.Duration),
                                new KeyValuePair<ComponentType, BasicDataType>(ComponentType.Measure, BasicDataType.Duration));
                        break;
                    case TestExprType.NonesDataset:
                        operand =
                            TestExprFactory.GetDatasetExpr(
                                new KeyValuePair<ComponentType, BasicDataType>(ComponentType.Identifier, BasicDataType.Integer),
                                new KeyValuePair<ComponentType, BasicDataType>(ComponentType.Measure, BasicDataType.None),
                                new KeyValuePair<ComponentType, BasicDataType>(ComponentType.Measure, BasicDataType.None));
                        break;
                    case TestExprType.MixedIntNumDataset:
                        operand =
                            TestExprFactory.GetDatasetExpr(
                                new KeyValuePair<ComponentType, BasicDataType>(ComponentType.Identifier, BasicDataType.Integer),
                                new KeyValuePair<ComponentType, BasicDataType>(ComponentType.Measure, BasicDataType.Integer),
                                new KeyValuePair<ComponentType, BasicDataType>(ComponentType.Measure, BasicDataType.Number));
                        break;
                    case TestExprType.MixedNumStrDataset:
                        operand =
                            TestExprFactory.GetDatasetExpr(
                                new KeyValuePair<ComponentType, BasicDataType>(ComponentType.Identifier, BasicDataType.Integer),
                                new KeyValuePair<ComponentType, BasicDataType>(ComponentType.Measure, BasicDataType.Number),
                                new KeyValuePair<ComponentType, BasicDataType>(ComponentType.Measure, BasicDataType.String));
                        break;
                    case TestExprType.MixedNoneIntDataset:
                        operand =
                            TestExprFactory.GetDatasetExpr(
                                new KeyValuePair<ComponentType, BasicDataType>(ComponentType.Identifier, BasicDataType.Integer),
                                new KeyValuePair<ComponentType, BasicDataType>(ComponentType.Measure, BasicDataType.None),
                                new KeyValuePair<ComponentType, BasicDataType>(ComponentType.Measure, BasicDataType.Integer));
                        break;
                    case TestExprType.MixedNoneNumDataset:
                        operand =
                            TestExprFactory.GetDatasetExpr(
                                new KeyValuePair<ComponentType, BasicDataType>(ComponentType.Identifier, BasicDataType.Integer),
                                new KeyValuePair<ComponentType, BasicDataType>(ComponentType.Measure, BasicDataType.None),
                                new KeyValuePair<ComponentType, BasicDataType>(ComponentType.Measure, BasicDataType.Number));
                        break;
                    case TestExprType.MixedNoneStrDataset:
                        operand =
                            TestExprFactory.GetDatasetExpr(
                                new KeyValuePair<ComponentType, BasicDataType>(ComponentType.Identifier, BasicDataType.Integer),
                                new KeyValuePair<ComponentType, BasicDataType>(ComponentType.Measure, BasicDataType.None),
                                new KeyValuePair<ComponentType, BasicDataType>(ComponentType.Measure, BasicDataType.String));
                        break;
                    case TestExprType.MixedNoneBoolDataset:
                        operand =
                            TestExprFactory.GetDatasetExpr(
                                new KeyValuePair<ComponentType, BasicDataType>(ComponentType.Identifier, BasicDataType.Integer),
                                new KeyValuePair<ComponentType, BasicDataType>(ComponentType.Measure, BasicDataType.None),
                                new KeyValuePair<ComponentType, BasicDataType>(ComponentType.Measure, BasicDataType.Boolean));
                        break;
                    case TestExprType.MixedNoneTimeDataset:
                        operand =
                            TestExprFactory.GetDatasetExpr(
                                new KeyValuePair<ComponentType, BasicDataType>(ComponentType.Identifier, BasicDataType.Integer),
                                new KeyValuePair<ComponentType, BasicDataType>(ComponentType.Measure, BasicDataType.None),
                                new KeyValuePair<ComponentType, BasicDataType>(ComponentType.Measure, BasicDataType.Time));
                        break;
                    case TestExprType.MixedNoneDateDataset:
                        operand =
                            TestExprFactory.GetDatasetExpr(
                                new KeyValuePair<ComponentType, BasicDataType>(ComponentType.Identifier, BasicDataType.Integer),
                                new KeyValuePair<ComponentType, BasicDataType>(ComponentType.Measure, BasicDataType.None),
                                new KeyValuePair<ComponentType, BasicDataType>(ComponentType.Measure, BasicDataType.Date));
                        break;
                    case TestExprType.MixedNoneTimePerDataset:
                        operand =
                            TestExprFactory.GetDatasetExpr(
                                new KeyValuePair<ComponentType, BasicDataType>(ComponentType.Identifier, BasicDataType.Integer),
                                new KeyValuePair<ComponentType, BasicDataType>(ComponentType.Measure, BasicDataType.None),
                                new KeyValuePair<ComponentType, BasicDataType>(ComponentType.Measure, BasicDataType.TimePeriod));
                        break;
                    case TestExprType.MixedNoneDurDataset:
                        operand =
                            TestExprFactory.GetDatasetExpr(
                                new KeyValuePair<ComponentType, BasicDataType>(ComponentType.Identifier, BasicDataType.Integer),
                                new KeyValuePair<ComponentType, BasicDataType>(ComponentType.Measure, BasicDataType.None),
                                new KeyValuePair<ComponentType, BasicDataType>(ComponentType.Measure, BasicDataType.Duration));
                        break;
                    default:
                        operand.Structure = ModelResolvers.DsResolver("const", ComponentType.Measure, (BasicDataType)types[i]);
                        break;
                }

                if (!noChildren) expr.AddOperand($"ds_{i + 1}", operand);
                else expr = operand;
            }

            return expr;
        }
    }
}
