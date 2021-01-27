namespace StatisticsPoland.VtlProcessing.Core.Tests.OperatorsTests
{
    using StatisticsPoland.VtlProcessing.Core.Infrastructure;
    using StatisticsPoland.VtlProcessing.Core.Models.Interfaces;
    using StatisticsPoland.VtlProcessing.Core.Models.Types;
    using StatisticsPoland.VtlProcessing.Core.Tests.Utilities;
    using System;
    using System.Collections.Generic;
    using Xunit;

    public class SubspaceOperatorTests
    {
        [Fact]
        public void GetOutputStructure_Correct1Operand()
        {
            IExpression expr = ModelResolvers.ExprResolver();
            expr.OperatorDefinition = ModelResolvers.OperatorResolver("sub");

            IExpression operand = TestExprFactory.GetDatasetExpr(new KeyValuePair<ComponentType, BasicDataType>(ComponentType.Identifier, BasicDataType.Integer));
            expr.AddOperand("ds_1", operand);

            IDataStructure dataStructure = expr.OperatorDefinition.GetOutputStructure(expr);

            Assert.True(operand.Structure.EqualsObj(dataStructure));
        }

        [Fact]
        public void GetOutputStructure_Correct2Operands()
        {
            IExpression expr = ModelResolvers.ExprResolver();
            expr.OperatorDefinition = ModelResolvers.OperatorResolver("sub");

            IExpression operand1 = TestExprFactory.GetDatasetExpr(new KeyValuePair<ComponentType, BasicDataType>(ComponentType.Identifier, BasicDataType.Integer));
            IExpression operand2 = TestExprFactory.GetDatasetExpr(new KeyValuePair<ComponentType, BasicDataType>(ComponentType.Identifier, BasicDataType.String));
            expr.AddOperand("ds_1", operand1);
            expr.AddOperand("ds_2", operand2);

            IDataStructure operandCollection = ModelResolvers.DsResolver();
            operandCollection.AddStructure(operand1.Structure);
            operandCollection.AddStructure(operand2.Structure);

            IDataStructure dataStructure = expr.OperatorDefinition.GetOutputStructure(expr);

            Assert.True(operandCollection.EqualsObj(dataStructure));
        }

        [Theory]
        [InlineData(ComponentType.Measure)]
        [InlineData(ComponentType.ViralAttribute)]
        [InlineData(ComponentType.NonViralAttribute)]
        public void GetOutputStructure_WrongComponentType(ComponentType wrongType)
        {
            IExpression expr = ModelResolvers.ExprResolver();
            expr.OperatorDefinition = ModelResolvers.OperatorResolver("sub");

            IExpression operand = TestExprFactory.GetDatasetExpr(new KeyValuePair<ComponentType, BasicDataType>(wrongType, BasicDataType.Integer));
            expr.AddOperand("ds_1", operand);

            Assert.ThrowsAny<Exception>(() => { expr.OperatorDefinition.GetOutputStructure(expr); });
        }
    }
}