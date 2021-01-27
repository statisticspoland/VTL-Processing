namespace StatisticsPoland.VtlProcessing.Core.Tests.OperatorsTests
{
    using StatisticsPoland.VtlProcessing.Core.ErrorHandling;
    using StatisticsPoland.VtlProcessing.Core.Infrastructure;
    using StatisticsPoland.VtlProcessing.Core.Models.Interfaces;
    using StatisticsPoland.VtlProcessing.Core.Tests.Utilities;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Xunit;

    public class SetOperatorTests
    {
        private readonly List<string> operators;

        public SetOperatorTests()
        {
            this.operators = new List<string>() { "union", "intersect", "setdiff", "symdiff" };
        }

        [Fact]
        public void GetOutputStructure_CorrectDatasetsExpr_DataStructure()
        {
            TestExprType[][] combinations = Enum.GetValues(typeof(TestExprType)).Cast<TestExprType>().GetCombinations(2);
            TestExprType[][] datasetCombs = combinations.Where(comb => (int)comb[0] >= 9 && (int)comb[1] >= 9).ToArray();

            foreach (TestExprType[] datasetComb in datasetCombs.Where(comb => comb[0] == comb[1]))
            {
                foreach (string opSymbol in this.operators)
                {
                    IExpression setExpr = ModelResolvers.ExprResolver();
                    setExpr.OperatorDefinition = ModelResolvers.OperatorResolver(opSymbol);

                    setExpr.AddOperand("ds_1", TestExprFactory.GetExpression(datasetComb[0]));
                    setExpr.AddOperand("ds_2", TestExprFactory.GetExpression(datasetComb[1]));

                    IDataStructure dataStructure = setExpr.OperatorDefinition.GetOutputStructure(setExpr);

                    Assert.True(setExpr.Operands["ds_1"].Structure.EqualsObj(dataStructure));
                }
            }
        }

        [Fact]
        public void GetOutputStructure_WrongDatasetsExpr_ThrowsException()
        {
            TestExprType[][] combinations = Enum.GetValues(typeof(TestExprType)).Cast<TestExprType>().GetCombinations(2);
            TestExprType[][] datasetCombs = combinations.Where(comb => (int)comb[0] >= 9 && (int)comb[1] >= 9).ToArray();

            foreach (TestExprType[] datasetComb in datasetCombs.Where(comb => comb[0] != comb[1]))
            {
                foreach (string opSymbol in this.operators)
                {
                    IExpression setExpr = ModelResolvers.ExprResolver();
                    setExpr.OperatorDefinition = ModelResolvers.OperatorResolver(opSymbol);

                    setExpr.AddOperand("ds_1", TestExprFactory.GetExpression(datasetComb[0]));
                    setExpr.AddOperand("ds_2", TestExprFactory.GetExpression(datasetComb[1]));

                    Assert.ThrowsAny<VtlOperatorError>(() => { setExpr.OperatorDefinition.GetOutputStructure(setExpr); });
                }
            }
        }

        [Fact]
        public void GetOutputStructure_DatasetScalarExpr_ThrowsException()
        {
            TestExprType[][] combinations = Enum.GetValues(typeof(TestExprType)).Cast<TestExprType>().GetCombinations(2);
            TestExprType[][] mixedCombs = combinations.Where(comb => (int)comb[0] >= 9 && (int)comb[1] < 9).ToArray();

            foreach (TestExprType[] mixedComb in mixedCombs)
            {
                foreach (string opSymbol in this.operators)
                {
                    IExpression setExpr = ModelResolvers.ExprResolver();
                    setExpr.OperatorDefinition = ModelResolvers.OperatorResolver(opSymbol);

                    setExpr.AddOperand("ds_1", TestExprFactory.GetExpression(mixedComb[0]));
                    setExpr.AddOperand("ds_2", TestExprFactory.GetExpression(mixedComb[1]));

                    Assert.ThrowsAny<VtlOperatorError>(() => { setExpr.OperatorDefinition.GetOutputStructure(setExpr); });
                }
            }
        }

        [Fact]
        public void GetOutputStructure_ScalarDatasetExpr_ThrowsException()
        {
            TestExprType[][] combinations = Enum.GetValues(typeof(TestExprType)).Cast<TestExprType>().GetCombinations(2);
            TestExprType[][] mixedCombs = combinations.Where(comb => (int)comb[0] < 9 && (int)comb[1] >= 9).ToArray();

            foreach (TestExprType[] mixedComb in mixedCombs)
            {
                foreach (string opSymbol in this.operators)
                {
                    IExpression setExpr = ModelResolvers.ExprResolver();
                    setExpr.OperatorDefinition = ModelResolvers.OperatorResolver(opSymbol);

                    setExpr.AddOperand("ds_1", TestExprFactory.GetExpression(mixedComb[0]));
                    setExpr.AddOperand("ds_2", TestExprFactory.GetExpression(mixedComb[1]));

                    Assert.ThrowsAny<VtlOperatorError>(() => { setExpr.OperatorDefinition.GetOutputStructure(setExpr); });
                }
            }
        }

        [Fact]
        public void GetOutputStructure_ScalarsExpr_ThrowsException()
        {
            TestExprType[][] combinations = Enum.GetValues(typeof(TestExprType)).Cast<TestExprType>().GetCombinations(2);
            TestExprType[][] scalarCombs = combinations.Where(comb => (int)comb[0] < 9 && (int)comb[1] < 9).ToArray();

            foreach (TestExprType[] scalarComb in scalarCombs)
            {
                foreach (string opSymbol in this.operators)
                {
                    IExpression setExpr = ModelResolvers.ExprResolver();
                    setExpr.OperatorDefinition = ModelResolvers.OperatorResolver(opSymbol);

                    setExpr.AddOperand("ds_1", TestExprFactory.GetExpression(scalarComb[0]));
                    setExpr.AddOperand("ds_2", TestExprFactory.GetExpression(scalarComb[1]));

                    Assert.ThrowsAny<VtlOperatorError>(() => { setExpr.OperatorDefinition.GetOutputStructure(setExpr); });
                }
            }
        }
    }
}
