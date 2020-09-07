namespace StatisticsPoland.VtlProcessing.Core.Tests.Operators
{
    using Moq;
    using StatisticsPoland.VtlProcessing.Core.ErrorHandling;
    using StatisticsPoland.VtlProcessing.Core.Infrastructure;
    using StatisticsPoland.VtlProcessing.Core.Infrastructure.DependencyInjection;
    using StatisticsPoland.VtlProcessing.Core.Infrastructure.Interfaces;
    using StatisticsPoland.VtlProcessing.Core.Models;
    using StatisticsPoland.VtlProcessing.Core.Models.Interfaces;
    using StatisticsPoland.VtlProcessing.Core.Models.Types;
    using StatisticsPoland.VtlProcessing.Core.Operators;
    using StatisticsPoland.VtlProcessing.Core.Tests.Utilities;
    using System;
    using System.Linq;
    using Xunit;

    public class IsNullOperatorTests
    {
        private readonly OperatorResolver opResolver;

        public IsNullOperatorTests()
        {
            Mock<OperatorResolver> opResolverMock = new Mock<OperatorResolver>();
            Mock<IExpressionFactory> exprFacMock = new Mock<IExpressionFactory>();

            exprFacMock.Setup(o => o.GetExpression(It.IsAny<string>(), It.IsAny<ExpressionFactoryNameTarget>()))
                .Returns((string name, ExpressionFactoryNameTarget field) =>
                {
                    IExpression expr = ModelResolvers.ExprResolver();
                    if (field == ExpressionFactoryNameTarget.ResultName) expr.ResultName = name;
                    else expr.OperatorDefinition = opResolverMock.Object(name);
                    return expr;
                });
            exprFacMock.Setup(o => o.OperatorResolver).Returns(opResolverMock.Object);

            opResolverMock.Setup(o => o("isnull")).Returns(() => { return new IsNullOperator(ModelResolvers.DsResolver); });

            this.opResolver = opResolverMock.Object;
        }

        [Theory]
        [InlineData(TestExprType.Integer)]
        [InlineData(TestExprType.Number)]
        [InlineData(TestExprType.String)]
        [InlineData(TestExprType.Boolean)]
        [InlineData(TestExprType.Time)]
        [InlineData(TestExprType.Date)]
        [InlineData(TestExprType.TimePeriod)]
        [InlineData(TestExprType.Duration)]
        [InlineData(TestExprType.None)]
        public void GetOutputStructure_ScalarExpr_BoolScalarStructure(TestExprType type)
        {
            IExpression isNullExpr = TestExprFactory.GetExpression(new TestExprType[] { type });
            isNullExpr.OperatorDefinition = this.opResolver("isnull");

            IDataStructure dataStructure = isNullExpr.OperatorDefinition.GetOutputStructure(isNullExpr);

            Assert.True(TestExprFactory.GetExpression(TestExprType.Boolean).Structure.EqualsObj(dataStructure));
        }

        [Theory]
        [InlineData(TestExprType.IntsDataset)]
        [InlineData(TestExprType.NumbersDataset)]
        [InlineData(TestExprType.StringsDataset)]
        [InlineData(TestExprType.BoolsDataset)]
        [InlineData(TestExprType.TimesDataset)]
        [InlineData(TestExprType.DatesDataset)]
        [InlineData(TestExprType.TimePeriodsDataset)]
        [InlineData(TestExprType.DurationsDataset)]
        [InlineData(TestExprType.NonesDataset)]
        [InlineData(TestExprType.MixedIntNumDataset)]
        [InlineData(TestExprType.MixedNumStrDataset)]
        [InlineData(TestExprType.MixedNoneIntDataset)]
        [InlineData(TestExprType.MixedNoneNumDataset)]
        [InlineData(TestExprType.MixedNoneStrDataset)]
        [InlineData(TestExprType.MixedNoneBoolDataset)]
        [InlineData(TestExprType.MixedNoneTimeDataset)]
        [InlineData(TestExprType.MixedNoneDateDataset)]
        [InlineData(TestExprType.MixedNoneTimePerDataset)]
        [InlineData(TestExprType.MixedNoneDurDataset)]
        public void GetOutputStructure_OneMeasureDatasetExpr_OneMeasureBoolStructure(TestExprType type)
        {
            IExpression isNullExpr = TestExprFactory.GetExpression(new TestExprType[] { type });
            isNullExpr.OperatorDefinition = this.opResolver("isnull");
            isNullExpr.Operands["ds_1"].Structure.Measures.RemoveAt(1);

            IDataStructure expectedStructure = isNullExpr.Operands["ds_1"].Structure.GetCopy();
            expectedStructure.Measures.Clear();
            expectedStructure.Measures.Add(new StructureComponent(BasicDataType.Boolean, "bool_var"));

            IDataStructure dataStructure = isNullExpr.OperatorDefinition.GetOutputStructure(isNullExpr);

            Assert.True(expectedStructure.EqualsObj(dataStructure));
        }

        [Fact]
        public void GetOutputStructure_MultiMeasuresDatasetExpr_ThrowsException()
        {
            TestExprType[][] scalars = new TestExprType[][] {
                new TestExprType[] { TestExprType.Integer },
                new TestExprType[] { TestExprType.Number },
                new TestExprType[] { TestExprType.String },
                new TestExprType[] { TestExprType.Boolean },
                new TestExprType[] { TestExprType.Time },
                new TestExprType[] { TestExprType.Date },
                new TestExprType[] { TestExprType.TimePeriod },
                new TestExprType[] { TestExprType.Duration },
                new TestExprType[] { TestExprType.None }
            };

            TestExprType[][] types = Enum.GetValues(typeof(TestExprType)).Cast<TestExprType>().GetCombinations(1);
            TestExprType[][] datasets = types.Without(scalars);

            foreach (TestExprType[] dataset in datasets)
            {
                IExpression isNullExpr = TestExprFactory.GetExpression(dataset);
                isNullExpr.OperatorDefinition = this.opResolver("isnull");

                // Debug condition example: dataset[0] == TestExprType.IntsDataset
                Assert.ThrowsAny<VtlOperatorError>(() => { isNullExpr.OperatorDefinition.GetOutputStructure(isNullExpr); });
            }
        }
    }
}
