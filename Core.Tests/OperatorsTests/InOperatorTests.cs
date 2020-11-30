namespace StatisticsPoland.VtlProcessing.Core.Tests.OperatorsTests
{
    using StatisticsPoland.VtlProcessing.Core.Infrastructure;
    using StatisticsPoland.VtlProcessing.Core.Models;
    using StatisticsPoland.VtlProcessing.Core.Models.Interfaces;
    using StatisticsPoland.VtlProcessing.Core.Models.Types;
    using StatisticsPoland.VtlProcessing.Core.Tests.Utilities;
    using System.Collections.Generic;
    using Xunit;

    public class InOperatorTests
    {
        private readonly List<string> operators;

        public InOperatorTests()
        { 
            this.operators = new List<string>() { "in", "not_in" };
        }

        [Fact]
        public void GetOutputStructure_CorrectScalarCollectionExpr_BoolScalarStructure()
        {
            foreach (string opSymbol in this.operators)
            {
                IExpression collectionExpr = ModelResolvers.ExprResolver();
                collectionExpr.Structure = ModelResolvers.DsResolver();
                collectionExpr.OperatorDefinition = ModelResolvers.OperatorResolver("collection");
                collectionExpr.Structure.Measures.Add(new StructureComponent(BasicDataType.Integer));
                collectionExpr.Structure.Measures.Add(new StructureComponent(BasicDataType.Number));
                collectionExpr.Structure.Measures.Add(new StructureComponent(BasicDataType.None));

                IExpression inExpr = ModelResolvers.ExprResolver();
                inExpr.AddOperand("ds_1", TestExprFactory.GetExpression(TestExprType.Number));
                inExpr.AddOperand("ds_2", collectionExpr);

                inExpr.OperatorDefinition = ModelResolvers.OperatorResolver(opSymbol);

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
                collectionExpr.OperatorDefinition = ModelResolvers.OperatorResolver("collection");
                collectionExpr.Structure.Measures.Add(new StructureComponent(BasicDataType.Integer));
                collectionExpr.Structure.Measures.Add(new StructureComponent(BasicDataType.Number));
                collectionExpr.Structure.Measures.Add(new StructureComponent(BasicDataType.None));

                IExpression inExpr = ModelResolvers.ExprResolver();
                inExpr.AddOperand("ds_1", TestExprFactory.GetExpression(TestExprType.NumbersDataset));
                inExpr.AddOperand("ds_2", collectionExpr);

                inExpr.Operands["ds_1"].Structure.Measures.RemoveAt(1);

                inExpr.OperatorDefinition = ModelResolvers.OperatorResolver(opSymbol);

                IExpression expected = TestExprFactory.GetExpression(TestExprType.BoolsDataset);
                expected.Structure.Measures.RemoveAt(1);

                IDataStructure dataStructure = inExpr.OperatorDefinition.GetOutputStructure(inExpr);

                Assert.True(expected.Structure.EqualsObj(dataStructure));
            }
        }
    }
}