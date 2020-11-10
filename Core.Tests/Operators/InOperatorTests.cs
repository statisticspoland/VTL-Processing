namespace StatisticsPoland.VtlProcessing.Core.Tests.Operators
{
    using Moq;
    using StatisticsPoland.VtlProcessing.Core.Infrastructure;
    using StatisticsPoland.VtlProcessing.Core.Infrastructure.DependencyInjection;
    using StatisticsPoland.VtlProcessing.Core.Infrastructure.Interfaces;
    using StatisticsPoland.VtlProcessing.Core.Models;
    using StatisticsPoland.VtlProcessing.Core.Models.Interfaces;
    using StatisticsPoland.VtlProcessing.Core.Models.Types;
    using StatisticsPoland.VtlProcessing.Core.Operators;
    using StatisticsPoland.VtlProcessing.Core.Operators.Auxiliary;
    using StatisticsPoland.VtlProcessing.Core.Operators.Auxiliary.ComponentManagement;
    using StatisticsPoland.VtlProcessing.Core.Tests.Utilities;
    using System.Collections.Generic;
    using Xunit;

    public class InOperatorTests
    {
        private readonly List<string> operators;
        private readonly OperatorResolver opResolver;

        public InOperatorTests()
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

            Mock<JoinApplyMeasuresOperator> joinApplyMeasuresOpMock = new Mock<JoinApplyMeasuresOperator>(exprFacMock.Object, ModelResolvers.DsResolver);

            opResolverMock.Setup(o => o("in")).Returns(() => { return new InOperator(joinApplyMeasuresOpMock.Object, ModelResolvers.DsResolver, "in"); });
            opResolverMock.Setup(o => o("not_in")).Returns(() => { return new InOperator(joinApplyMeasuresOpMock.Object, ModelResolvers.DsResolver, "not_in"); });
            opResolverMock.Setup(o => o("collection")).Returns(() => { return new CollectionOperator(ModelResolvers.DsResolver); });

            this.opResolver = opResolverMock.Object;

            this.operators = new List<string>() { "in", "not_in" };
        }

        [Fact]
        public void GetOutputStructure_CorrectScalarCollectionExpr_BoolScalarStructure()
        {
            foreach (string opSymbol in this.operators)
            {
                IExpression collectionExpr = ModelResolvers.ExprResolver();
                collectionExpr.Structure = ModelResolvers.DsResolver();
                collectionExpr.OperatorDefinition = this.opResolver("collection");
                collectionExpr.Structure.Measures.Add(new StructureComponent(BasicDataType.Integer));
                collectionExpr.Structure.Measures.Add(new StructureComponent(BasicDataType.Number));
                collectionExpr.Structure.Measures.Add(new StructureComponent(BasicDataType.None));

                IExpression inExpr = ModelResolvers.ExprResolver();
                inExpr.AddOperand("ds_1", TestExprFactory.GetExpression(TestExprType.Number));
                inExpr.AddOperand("ds_2", collectionExpr);

                inExpr.OperatorDefinition = this.opResolver(opSymbol);

                IDataStructure dataStructure = inExpr.OperatorDefinition.GetOutputStructure(inExpr);

                Assert.True(TestExprFactory.GetExpression(TestExprType.Boolean).Structure.EqualsObj(dataStructure));
            }
        }

        [Fact]
        public void GetOutputStructure_CorrectDatasetCollectionExpr_BoolScalarStructure()
        {
            foreach (string opSymbol in this.operators)
            {
                IExpression collectionExpr = ModelResolvers.ExprResolver();
                collectionExpr.Structure = ModelResolvers.DsResolver();
                collectionExpr.OperatorDefinition = this.opResolver("collection");
                collectionExpr.Structure.Measures.Add(new StructureComponent(BasicDataType.Integer));
                collectionExpr.Structure.Measures.Add(new StructureComponent(BasicDataType.Number));
                collectionExpr.Structure.Measures.Add(new StructureComponent(BasicDataType.None));

                IExpression inExpr = ModelResolvers.ExprResolver();
                inExpr.AddOperand("ds_1", TestExprFactory.GetExpression(TestExprType.NumbersDataset));
                inExpr.AddOperand("ds_2", collectionExpr);

                inExpr.Operands["ds_1"].Structure.Measures.RemoveAt(1);

                inExpr.OperatorDefinition = this.opResolver(opSymbol);

                IExpression expected = TestExprFactory.GetExpression(TestExprType.BoolsDataset);
                expected.Structure.Measures.RemoveAt(1);

                IDataStructure dataStructure = inExpr.OperatorDefinition.GetOutputStructure(inExpr);

                Assert.True(expected.Structure.EqualsObj(dataStructure));
            }
        }
    }
}