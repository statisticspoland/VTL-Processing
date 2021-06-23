namespace StatisticsPoland.VtlProcessing.Core.Tests.OperatorsTests
{
    using StatisticsPoland.VtlProcessing.Core.ErrorHandling;
    using StatisticsPoland.VtlProcessing.Core.Infrastructure;
    using StatisticsPoland.VtlProcessing.Core.Models.Interfaces;
    using StatisticsPoland.VtlProcessing.Core.Tests.Utilities;
    using System;
    using System.Linq;
    using Xunit;

    public class MatchCharactersOperator
    {
        [Theory]
        [InlineData(TestExprType.StringsDataset, TestExprType.String)]
        [InlineData(TestExprType.StringsDataset, TestExprType.None)]
        [InlineData(TestExprType.NonesDataset, TestExprType.String)]
        [InlineData(TestExprType.NonesDataset, TestExprType.None)]
        public void GetOutputStructure_CorrectDatasetScalarExpr_BoolDatasetStructure(params TestExprType[] types)
        {
            IExpression stringExpr = TestExprFactory.GetExpression(types);
            stringExpr.OperatorDefinition = ModelResolvers.OperatorResolver("match_characters");

            IExpression expected = TestExprFactory.GetExpression(TestExprType.BoolsDataset);

            stringExpr.Operands["ds_1"].Structure.Measures.RemoveAt(1);
            expected.Structure.Measures.RemoveAt(1);

            IDataStructure dataStructure = stringExpr.OperatorDefinition.GetOutputStructure(stringExpr);

            Assert.True(expected.Structure.EqualsObj(dataStructure));
        }

        [Theory]
        [InlineData(TestExprType.String, TestExprType.String)]
        [InlineData(TestExprType.String, TestExprType.None)]
        [InlineData(TestExprType.None, TestExprType.String)]
        [InlineData(TestExprType.None, TestExprType.None)]
        public void GetOutputStructure_CorrectScalarsExpr_BoolScalarStructure(params TestExprType[] types)
        {
            IExpression stringExpr = TestExprFactory.GetExpression(types);
            stringExpr.OperatorDefinition = ModelResolvers.OperatorResolver("match_characters");

            IDataStructure dataStructure = stringExpr.OperatorDefinition.GetOutputStructure(stringExpr);

            Assert.True(TestExprFactory.GetExpression(TestExprType.Boolean).Structure.EqualsObj(dataStructure));
        }

        [Fact]
        public void GetOutputStructure_WrongArgsExpr_ThrowsException()
        {
            TestExprType[][] correctCombs = new TestExprType[][] {
                new TestExprType[] { TestExprType.String, TestExprType.String },
                new TestExprType[] { TestExprType.String, TestExprType.None },
                new TestExprType[] { TestExprType.None, TestExprType.String },
                new TestExprType[] { TestExprType.None, TestExprType.None },
                new TestExprType[] { TestExprType.StringsDataset, TestExprType.String },
                new TestExprType[] { TestExprType.StringsDataset, TestExprType.None },
                new TestExprType[] { TestExprType.NonesDataset, TestExprType.String },
                new TestExprType[] { TestExprType.NonesDataset, TestExprType.None }
            };

            TestExprType[][] combinations = Enum.GetValues(typeof(TestExprType)).Cast<TestExprType>().GetCombinations(2);
            TestExprType[][] wrongCombs = combinations.Without(correctCombs);

            foreach (TestExprType[] wrongComb in wrongCombs.Where(wrongComb => (int)wrongComb[0] < 18 && (int)wrongComb[1] < 18)) // No mixed datasets
            {
                IExpression stringExpr = TestExprFactory.GetExpression(wrongComb);
                stringExpr.OperatorDefinition = ModelResolvers.OperatorResolver("match_characters");

                if (!stringExpr.Operands["ds_1"].IsScalar) stringExpr.Operands["ds_1"].Structure.Measures.RemoveAt(1);
                if (!stringExpr.Operands["ds_2"].IsScalar) stringExpr.Operands["ds_2"].Structure.Measures.RemoveAt(1);

                // Debug condition example: wrongComb[0] == TestExprType.Integer && wrongComb[1] == TestExprType.Integer
                Assert.ThrowsAny<VtlOperatorError>(() => { stringExpr.OperatorDefinition.GetOutputStructure(stringExpr); });
            }
        }
    }
}
