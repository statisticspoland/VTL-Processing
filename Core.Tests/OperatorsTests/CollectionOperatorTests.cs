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

    public class CollectionOperatorTests
    {
        [Theory]
        [InlineData(TestExprType.Integer, TestExprType.Integer)]
        [InlineData(TestExprType.Integer, TestExprType.Number)]
        [InlineData(TestExprType.Integer, TestExprType.None)]
        [InlineData(TestExprType.Number, TestExprType.Number)]
        [InlineData(TestExprType.Number, TestExprType.Integer)]
        [InlineData(TestExprType.Number, TestExprType.None)]
        [InlineData(TestExprType.String, TestExprType.String)]
        [InlineData(TestExprType.String, TestExprType.None)]
        [InlineData(TestExprType.Boolean, TestExprType.Boolean)]
        [InlineData(TestExprType.Boolean, TestExprType.None)]
        [InlineData(TestExprType.Time, TestExprType.Time)]
        [InlineData(TestExprType.Time, TestExprType.None)]
        [InlineData(TestExprType.Date, TestExprType.Date)]
        [InlineData(TestExprType.Date, TestExprType.None)]
        [InlineData(TestExprType.TimePeriod, TestExprType.TimePeriod)]
        [InlineData(TestExprType.TimePeriod, TestExprType.None)]
        [InlineData(TestExprType.Duration, TestExprType.Duration)]
        [InlineData(TestExprType.Duration, TestExprType.None)]
        [InlineData(TestExprType.None, TestExprType.None)]
        [InlineData(TestExprType.None, TestExprType.Integer)]
        [InlineData(TestExprType.None, TestExprType.Number)]
        [InlineData(TestExprType.None, TestExprType.String)]
        [InlineData(TestExprType.None, TestExprType.Boolean)]
        [InlineData(TestExprType.None, TestExprType.Time)]
        [InlineData(TestExprType.None, TestExprType.Date)]
        [InlineData(TestExprType.None, TestExprType.TimePeriod)]
        [InlineData(TestExprType.None, TestExprType.Duration)]
        public void GetOutputStructure_CorrectScalars_DataStructure(params TestExprType[] types)
        {
            IExpression collectionExpr = TestExprFactory.GetExpression(types);
            collectionExpr.OperatorDefinition = ModelResolvers.OperatorResolver("collection");

            IDataStructure expected = ModelResolvers.DsResolver();
            expected.Measures.Add(new StructureComponent((BasicDataType)types[0]));
            expected.Measures.Add(new StructureComponent((BasicDataType)types[1]));

            IDataStructure dataStructure = collectionExpr.OperatorDefinition.GetOutputStructure(collectionExpr);

            Assert.True(expected.EqualsObj(dataStructure));
        }

        [Fact]
        public void GetOutputStructure_WrongArgsExpr_ThrowsException()
        {
            TestExprType[][] correctCombs = new TestExprType[][] {
                new TestExprType[] { TestExprType.Integer, TestExprType.Integer },
                new TestExprType[] { TestExprType.Integer, TestExprType.Number },
                new TestExprType[] { TestExprType.Integer, TestExprType.None },
                new TestExprType[] { TestExprType.Number, TestExprType.Number },
                new TestExprType[] { TestExprType.Number, TestExprType.Integer },
                new TestExprType[] { TestExprType.Number, TestExprType.None },
                new TestExprType[] { TestExprType.String, TestExprType.String },
                new TestExprType[] { TestExprType.String, TestExprType.None },
                new TestExprType[] { TestExprType.Boolean, TestExprType.Boolean },
                new TestExprType[] { TestExprType.Boolean, TestExprType.None },
                new TestExprType[] { TestExprType.Time, TestExprType.Time },
                new TestExprType[] { TestExprType.Time, TestExprType.None },
                new TestExprType[] { TestExprType.Date, TestExprType.Date },
                new TestExprType[] { TestExprType.Date, TestExprType.None },
                new TestExprType[] { TestExprType.TimePeriod, TestExprType.TimePeriod },
                new TestExprType[] { TestExprType.TimePeriod, TestExprType.None },
                new TestExprType[] { TestExprType.Duration, TestExprType.Duration },
                new TestExprType[] { TestExprType.Duration, TestExprType.None },
                new TestExprType[] { TestExprType.None, TestExprType.None },
                new TestExprType[] { TestExprType.None, TestExprType.Integer },
                new TestExprType[] { TestExprType.None, TestExprType.Number },
                new TestExprType[] { TestExprType.None, TestExprType.String },
                new TestExprType[] { TestExprType.None, TestExprType.Boolean },
                new TestExprType[] { TestExprType.None, TestExprType.Time },
                new TestExprType[] { TestExprType.None, TestExprType.Date },
                new TestExprType[] { TestExprType.None, TestExprType.TimePeriod },
                new TestExprType[] { TestExprType.None, TestExprType.Duration }
            };

            TestExprType[][] combinations = Enum.GetValues(typeof(TestExprType)).Cast<TestExprType>().GetCombinations(2);
            TestExprType[][] wrongCombs = combinations.Without(correctCombs);

            foreach (TestExprType[] wrongComb in wrongCombs)
            {
                IExpression collectionExpr = TestExprFactory.GetExpression(wrongComb);
                collectionExpr.OperatorDefinition = ModelResolvers.OperatorResolver("collection");

                // Debug condition example: wrongComb[0] == TestExprType.Integer && wrongComb[1] == TestExprType.Integer
                Assert.ThrowsAny<VtlOperatorError>(() => { collectionExpr.OperatorDefinition.GetOutputStructure(collectionExpr); });
            }
        }
    }
}
