namespace StatisticsPoland.VtlProcessing.Core.Tests.OperatorsTests
{
    using StatisticsPoland.VtlProcessing.Core.ErrorHandling;
    using StatisticsPoland.VtlProcessing.Core.Infrastructure;
    using StatisticsPoland.VtlProcessing.Core.Models;
    using StatisticsPoland.VtlProcessing.Core.Models.Interfaces;
    using StatisticsPoland.VtlProcessing.Core.Models.Types;
    using StatisticsPoland.VtlProcessing.Core.Operators.Auxiliary;
    using StatisticsPoland.VtlProcessing.Core.Tests.Utilities;
    using System.Linq;
    using Xunit;

    public class PeriodIndicatorOperatorTests
    {
        [Fact]
        public void GetOutputStructure_CorrectScalarExpr_DurationScalarStructure()
        {
            IExpression periodIndicatorExpr = TestExprFactory.GetExpression("period_indicator", ExpressionFactoryNameTarget.OperatorSymbol);
            IExpression scalarExpr = TestExprFactory.GetExpression(TestExprType.TimePeriod);
            scalarExpr.OperatorDefinition = ModelResolvers.OperatorResolver("const");

            periodIndicatorExpr.AddOperand("ds_1", scalarExpr);

            IDataStructure dataStructure = periodIndicatorExpr.OperatorDefinition.GetOutputStructure(periodIndicatorExpr);

            Assert.True(dataStructure.IsSingleComponent);
            Assert.True(dataStructure.Measures.Count == 1);
            Assert.Equal(BasicDataType.Duration, dataStructure.Measures[0].ValueDomain.DataType);
        }

        [Theory]
        [InlineData(TestExprType.Integer)]
        [InlineData(TestExprType.Number)]
        [InlineData(TestExprType.String)]
        [InlineData(TestExprType.Boolean)]
        [InlineData(TestExprType.Time)]
        [InlineData(TestExprType.Date)]
        [InlineData(TestExprType.Duration)]
        public void GetOutputStructure_WrongScalarExpr_ThrowsException(TestExprType type)
        {
            IExpression periodIndicatorExpr = TestExprFactory.GetExpression("period_indicator", ExpressionFactoryNameTarget.OperatorSymbol);
            IExpression scalarExpr = TestExprFactory.GetExpression(type);
            scalarExpr.OperatorDefinition = ModelResolvers.OperatorResolver("const");

            periodIndicatorExpr.AddOperand("ds_1", scalarExpr);

            Assert.ThrowsAny<VtlOperatorError>(() => { periodIndicatorExpr.OperatorDefinition.GetOutputStructure(periodIndicatorExpr); });
        }

        [Fact]
        public void GetOutputStructure_CorrectDatasetParamExpr_DurationScalarStructure()
        {
            IExpression periodIndicatorExpr = TestExprFactory.GetExpression("period_indicator", ExpressionFactoryNameTarget.OperatorSymbol);
            IExpression datasetExpr = TestExprFactory.GetExpression("get", ExpressionFactoryNameTarget.OperatorSymbol);

            periodIndicatorExpr.AddOperand("ds_1", datasetExpr);

            datasetExpr.Structure = ModelResolvers.DsResolver();
            datasetExpr.Structure.Identifiers.Add(new StructureComponent(BasicDataType.Integer, "Id1"));
            datasetExpr.Structure.Identifiers.Add(new StructureComponent(BasicDataType.TimePeriod, "Id2"));
            datasetExpr.Structure.Measures.Add(new StructureComponent(BasicDataType.String, "Me1"));

            IDataStructure expectedStructure = datasetExpr.Structure.GetCopy();
            expectedStructure.Measures[0] = new StructureComponent(BasicDataType.Duration, "duration_var");

            IDataStructure dataStructure = periodIndicatorExpr.OperatorDefinition.GetOutputStructure(periodIndicatorExpr);

            Assert.True(expectedStructure.EqualsObj(dataStructure));
        }

        [Fact]
        public void GetOutputStructure_DoubleCompDatasetParamExpr_DurationScalarStructure()
        {
            IExpression periodIndicatorExpr = TestExprFactory.GetExpression("period_indicator", ExpressionFactoryNameTarget.OperatorSymbol);
            IExpression datasetExpr = TestExprFactory.GetExpression("get", ExpressionFactoryNameTarget.OperatorSymbol);

            periodIndicatorExpr.AddOperand("ds_1", datasetExpr);

            datasetExpr.Structure = ModelResolvers.DsResolver();
            datasetExpr.Structure.Identifiers.Add(new StructureComponent(BasicDataType.TimePeriod, "Id1"));
            datasetExpr.Structure.Identifiers.Add(new StructureComponent(BasicDataType.TimePeriod, "Id2"));
            datasetExpr.Structure.Measures.Add(new StructureComponent(BasicDataType.String, "Me1"));

            Assert.ThrowsAny<VtlOperatorError>(() => { periodIndicatorExpr.OperatorDefinition.GetOutputStructure(periodIndicatorExpr); });
        }

        [Theory]
        [InlineData(TestExprType.Integer)]
        [InlineData(TestExprType.Number)]
        [InlineData(TestExprType.String)]
        [InlineData(TestExprType.Boolean)]
        [InlineData(TestExprType.Time)]
        [InlineData(TestExprType.Date)]
        [InlineData(TestExprType.Duration)]
        public void GetOutputStructure_WrongDatasetParamExpr_ThrowsException(TestExprType type)
        {
            IExpression periodIndicatorExpr = TestExprFactory.GetExpression("period_indicator", ExpressionFactoryNameTarget.OperatorSymbol);
            IExpression datasetExpr = TestExprFactory.GetExpression("get", ExpressionFactoryNameTarget.OperatorSymbol);

            periodIndicatorExpr.AddOperand("ds_1", datasetExpr);

            datasetExpr.Structure = ModelResolvers.DsResolver();
            datasetExpr.Structure.Identifiers.Add(new StructureComponent(BasicDataType.Integer, "Id1"));
            datasetExpr.Structure.Identifiers.Add(new StructureComponent((BasicDataType)type, "Id2"));
            datasetExpr.Structure.Measures.Add(new StructureComponent(BasicDataType.String, "Me1"));

            Assert.ThrowsAny<VtlOperatorError>(() => { periodIndicatorExpr.OperatorDefinition.GetOutputStructure(periodIndicatorExpr); });
        }
    }
}
