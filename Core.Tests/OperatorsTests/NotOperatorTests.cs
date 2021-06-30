namespace StatisticsPoland.VtlProcessing.Core.Tests.OperatorsTests
{
    using StatisticsPoland.VtlProcessing.Core.ErrorHandling;
    using StatisticsPoland.VtlProcessing.Core.Infrastructure;
    using StatisticsPoland.VtlProcessing.Core.Models;
    using StatisticsPoland.VtlProcessing.Core.Models.Interfaces;
    using StatisticsPoland.VtlProcessing.Core.Models.Types;
    using StatisticsPoland.VtlProcessing.Core.Tests.Utilities;
    using System;
    using System.Linq;
    using Xunit;

    public class NotOperatorTests
    {
        [Theory]
        [InlineData(TestExprType.Boolean)]
        [InlineData(TestExprType.None)]
        public void GetOutputStructure_CorrectScalarExpr_BoolScalarStructure(TestExprType type)
        {
            IExpression notExpr = TestExprFactory.GetExpression(new TestExprType[] { type });
            notExpr.OperatorDefinition = ModelResolvers.OperatorResolver("not");

            IDataStructure dataStructure = notExpr.OperatorDefinition.GetOutputStructure(notExpr);

            Assert.True(TestExprFactory.GetExpression(TestExprType.Boolean).Structure.EqualsObj(dataStructure));
        }

        [Theory]
        [InlineData(TestExprType.BoolsDataset)]
        [InlineData(TestExprType.NonesDataset)]
        public void GetOutputStructure_CorrectOneMeasureDatasetExpr_OneMeasureBoolDatasetStructure(TestExprType type)
        {
            IExpression notExpr = TestExprFactory.GetExpression(new TestExprType[] { type });
            notExpr.OperatorDefinition = ModelResolvers.OperatorResolver("not");
            notExpr.Operands["ds_1"].Structure.Measures.RemoveAt(1);

            IDataStructure expectedStructure = notExpr.Operands["ds_1"].Structure.GetCopy();
            expectedStructure.Measures.Clear();
            expectedStructure.Measures.Add(new StructureComponent(BasicDataType.Boolean, "bool_var"));

            IDataStructure dataStructure = notExpr.OperatorDefinition.GetOutputStructure(notExpr);

            Assert.True(expectedStructure.EqualsObj(dataStructure));
        }

        [Theory]
        [InlineData(TestExprType.BoolsDataset)]
        [InlineData(TestExprType.NonesDataset)]
        public void GetOutputStructure_MultiMeasuresDatasetExpr_ThrowsException(TestExprType type)
        {
            IExpression notExpr = TestExprFactory.GetExpression(new TestExprType[] { type });
            notExpr.OperatorDefinition = ModelResolvers.OperatorResolver("not");

            Assert.ThrowsAny<VtlOperatorError>(() => { notExpr.OperatorDefinition.GetOutputStructure(notExpr); });
        }

        [Fact]
        public void GetOutputStructure_WrongArgExpr_ThrowsException()
        {
            TestExprType[][] correctCombs = new TestExprType[][] {
                new TestExprType[] { TestExprType.Boolean },
                new TestExprType[] { TestExprType.None },
                new TestExprType[] { TestExprType.BoolsDataset },
                new TestExprType[] { TestExprType.NonesDataset },
            };

            TestExprType[][] combinations = Enum.GetValues(typeof(TestExprType)).Cast<TestExprType>().GetCombinations(1);
            TestExprType[][] wrongCombs = combinations.Without(correctCombs);

            foreach (TestExprType[] wrongComb in wrongCombs.Where(wrongComb => (int)wrongComb[0] < 18)) // No mixed datasets
            {
                IExpression notExpr = TestExprFactory.GetExpression(wrongComb);
                notExpr.OperatorDefinition = ModelResolvers.OperatorResolver("not");
                if (notExpr.Operands["ds_1"].Structure.Measures.Count > 1)
                    notExpr.Operands["ds_1"].Structure.Measures.RemoveAt(1);

                // Debug condition example: wrongComb[0] == TestExprType.Integer
                Assert.ThrowsAny<VtlOperatorError>(() => { notExpr.OperatorDefinition.GetOutputStructure(notExpr); });
            }
        }
    }
}
