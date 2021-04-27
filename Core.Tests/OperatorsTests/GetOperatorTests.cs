namespace StatisticsPoland.VtlProcessing.Core.Tests.OperatorsTests
{
    using Moq;
    using StatisticsPoland.VtlProcessing.Core.Infrastructure;
    using StatisticsPoland.VtlProcessing.Core.Infrastructure.DependencyInjection;
    using StatisticsPoland.VtlProcessing.Core.Models.Interfaces;
    using StatisticsPoland.VtlProcessing.Core.Operators;
    using StatisticsPoland.VtlProcessing.Core.Tests.Utilities;
    using System;
    using Xunit;

    public class GetOperatorTests
    {
        private readonly OperatorResolver _opResolver;

        public GetOperatorTests()
        {
            Mock<IDataModel> dataModelMock = new Mock<IDataModel>();
            dataModelMock.Setup(o => o.GetDatasetStructure(It.IsAny<string>())).Returns((string dsName) => this.GetDataStructure(dsName));

            Mock<OperatorResolver> opResolverMock = new Mock<OperatorResolver>();
            opResolverMock.Setup(o => o("get")).Returns(() => { return new GetOperator(dataModelMock.Object); });

            this._opResolver = opResolverMock.Object;
        }

        private IDataStructure GetDataStructure(string dsName)
        {
            if (dsName != "IntsDataset") throw new Exception();
            IDataStructure ds = TestExprFactory.GetExpression(TestExprType.IntsDataset).Structure;
            ds.DatasetName = dsName;

            return ds;
        }

        [Fact]
        public void GetOutputStructure_CorrectExpr_DataStructure()
        {
            IExpression getExpr = ModelResolvers.ExprResolver();
            getExpr.ExpressionText = "IntsDataset";

            getExpr.OperatorDefinition = this._opResolver("get");

            IDataStructure dataStructure = getExpr.OperatorDefinition.GetOutputStructure(getExpr);

            Assert.True(TestExprFactory.GetExpression(TestExprType.IntsDataset).Structure.EqualsObj(dataStructure));
        }

        [Fact]
        public void GetOutputStructure_WrongExpr_ThrowsException()
        {
            IExpression getExpr = ModelResolvers.ExprResolver();
            getExpr.ExpressionText = "Dataset";

            getExpr.OperatorDefinition = this._opResolver("get");

            Assert.ThrowsAny<Exception>(() => { getExpr.OperatorDefinition.GetOutputStructure(getExpr); });
        }
    }
}
