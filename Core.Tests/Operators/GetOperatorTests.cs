namespace StatisticsPoland.VtlProcessing.Core.Tests.Operators
{
    using Moq;
    using StatisticsPoland.VtlProcessing.Core.Infrastructure;
    using StatisticsPoland.VtlProcessing.Core.Infrastructure.DependencyInjection;
    using StatisticsPoland.VtlProcessing.Core.Models;
    using StatisticsPoland.VtlProcessing.Core.Models.Interfaces;
    using StatisticsPoland.VtlProcessing.Core.Models.Logical;
    using StatisticsPoland.VtlProcessing.Core.Models.Types;
    using StatisticsPoland.VtlProcessing.Core.Operators;
    using System;
    using Xunit;

    public class GetOperatorTests
    {
        private readonly ExpressionResolver exprResolver;
        private readonly DataStructureResolver dsResolver;
        private readonly OperatorResolver opResolver;
        private readonly IDataModel dataModel;

        public GetOperatorTests()
        {
            Mock<IDataModel> dataModelMock = new Mock<IDataModel>();
            dataModelMock.Setup(o => o.GetDatasetStructure(It.IsAny<string>())).Returns((string dsName) => this.GetDataStructure(dsName));

            this.dataModel = dataModelMock.Object;

            Mock<ExpressionResolver> exprResolverMock = new Mock<ExpressionResolver>();
            exprResolverMock.Setup(o => o(null)).Returns(() => { return new Expression(); });
            exprResolverMock.Setup(o => o(It.IsNotNull<IExpression>())).Returns((IExpression parentExpr) => { return new Expression(parentExpr); });

            this.exprResolver = exprResolverMock.Object;

            Mock<DataStructureResolver> dsResolverMock = new Mock<DataStructureResolver>();
            dsResolverMock.Setup(o => o(null, null, null)).Returns(() => { return new DataStructure(); });
            dsResolverMock.Setup(o => o(It.IsNotNull<string>(), It.IsNotNull<ComponentType?>(), It.IsNotNull<BasicDataType?>()))
                .Returns((string compName, ComponentType? compType, BasicDataType? dataType) => { return new DataStructure(compName, (ComponentType)compType, (BasicDataType)dataType); });

            this.dsResolver = dsResolverMock.Object;

            Mock<OperatorResolver> opResolverMock = new Mock<OperatorResolver>();
            opResolverMock.Setup(o => o("get")).Returns(() => { return new GetOperator(this.dataModel); });

            this.opResolver = opResolverMock.Object;
        }

        private IDataStructure GetDataStructure(string dsName)
        {
            if (dsName != "IntsDataset") throw new Exception();
            IDataStructure ds = this.dsResolver("Id1", ComponentType.Identifier, BasicDataType.Integer);
            ds.Measures.Add(new StructureComponent(BasicDataType.Integer, "Me1", ComponentType.Measure));
            ds.DatasetName = dsName;

            return ds;
        }

        [Fact]
        public void GetOutputStructure_CorrectExpr_DataStructure()
        {
            IExpression getExpr = this.exprResolver();
            getExpr.ExpressionText = "IntsDataset";

            getExpr.OperatorDefinition = this.opResolver("get");

            IDataStructure expected = this.dsResolver("Id1", ComponentType.Identifier, BasicDataType.Integer);
            expected.Measures.Add(new StructureComponent(BasicDataType.Integer, "Me1", ComponentType.Measure));

            IDataStructure dataStructure = getExpr.OperatorDefinition.GetOutputStructure(getExpr);

            Assert.True(expected.EqualsObj(dataStructure));
        }

        [Fact]
        public void GetOutputStructure_WrongExpr_ThrowsException()
        {
            IExpression getExpr = this.exprResolver();
            getExpr.ExpressionText = "Dataset";

            getExpr.OperatorDefinition = this.opResolver("get");

            Assert.ThrowsAny<Exception>(() => { getExpr.OperatorDefinition.GetOutputStructure(getExpr); });
        }
    }
}
