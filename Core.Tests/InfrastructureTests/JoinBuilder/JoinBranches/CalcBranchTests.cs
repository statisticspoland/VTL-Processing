namespace StatisticsPoland.VtlProcessing.Core.Tests.InfrastructureTests.JoinBuilder.JoinBranches
{
    using StatisticsPoland.VtlProcessing.Core.Infrastructure.JoinBuilder.JoinBranches;
    using StatisticsPoland.VtlProcessing.Core.Models;
    using StatisticsPoland.VtlProcessing.Core.Models.Interfaces;
    using StatisticsPoland.VtlProcessing.Core.Models.Types;
    using StatisticsPoland.VtlProcessing.Core.Tests.Utilities;
    using Xunit;

    public partial class CalcBranchTests
    {
        [Theory]
        [InlineData("test")]
        [InlineData("count")]
        public void Build_Expr_Expr(string opSymbol)
        {
            CalcBranch calcBranch = new CalcBranch(this._exprFac, this._exprTextGenerator);

            IExpression operandExpr = ModelResolvers.ExprResolver();
            operandExpr.Structure = ModelResolvers.DsResolver();
            operandExpr.Structure.Measures.Add(new StructureComponent(BasicDataType.Integer, "Me1"));
            operandExpr.Structure.Measures.Add(new StructureComponent(BasicDataType.Integer, "Me2"));

            IExpression expr = this._exprFac.GetExpression(opSymbol, ExpressionFactoryNameTarget.OperatorSymbol); // local exprFac expected
            expr.AddOperand("ds_1", operandExpr);

            IExpression expected = TestExprFactory.GetExpression("calc", ExpressionFactoryNameTarget.OperatorSymbol);
            expected.OperatorDefinition.Keyword = "Aggr Built";

            for (int i = 0; i < (opSymbol == "test" ? 2 : 1); i++)
            {
                IExpression expected_sub = TestExprFactory.GetExpression("calcExpr", ExpressionFactoryNameTarget.OperatorSymbol);
                IExpression expected_sub_sub1 = TestExprFactory.GetExpression("comp", ExpressionFactoryNameTarget.OperatorSymbol);
                IExpression expected_sub_sub2 = this._exprFac.GetExpression(opSymbol, ExpressionFactoryNameTarget.OperatorSymbol); // local exprFac expected
                expected_sub_sub1.Structure = ModelResolvers.DsResolver(opSymbol == "test" ? $"Me{i + 1}" : "int_var", ComponentType.Measure, BasicDataType.Integer);
                expected_sub_sub2.Structure = ModelResolvers.DsResolver(opSymbol == "test" ? $"Me{i + 1}" : "int_var", ComponentType.Measure, BasicDataType.Integer);
                expected_sub.Operands.Add("ds_1", expected_sub_sub1);
                expected_sub.Operands.Add("ds_2", expected_sub_sub2);
            }
            
            IExpression result = calcBranch.Build(expr);

            if (opSymbol == "test")
            {
                Assert.Equal(2, result.OperandsCollection.Count); // calc
                Assert.Equal("calcExpr", result.Operands["ds_1"].OperatorSymbol);
                Assert.Equal("calcExpr", result.Operands["ds_2"].OperatorSymbol);

                Assert.Equal(2, result.Operands["ds_1"].OperandsCollection.Count); // calcExpr1
                Assert.Equal(2, result.Operands["ds_2"].OperandsCollection.Count); // calcExpr2
                Assert.Equal("comp", result.Operands["ds_1"].Operands["ds_1"].OperatorSymbol);
                Assert.Equal("comp", result.Operands["ds_2"].Operands["ds_1"].OperatorSymbol);
                Assert.Equal("Me1", result.Operands["ds_1"].Operands["ds_1"].ExpressionText);
                Assert.Equal("Me2", result.Operands["ds_2"].Operands["ds_1"].ExpressionText);

                Assert.Equal(0, result.Operands["ds_1"].Operands["ds_1"].OperandsCollection.Count); // component of calcExpr1
                Assert.Equal(1, result.Operands["ds_1"].Operands["ds_2"].OperandsCollection.Count); // aggrFunction of calcExpr1
                Assert.Equal(0, result.Operands["ds_2"].Operands["ds_1"].OperandsCollection.Count); // component of calcExpr2
                Assert.Equal(1, result.Operands["ds_2"].Operands["ds_2"].OperandsCollection.Count); // aggrFunction of calcExpr2
                Assert.Equal(opSymbol, result.Operands["ds_1"].Operands["ds_2"].OperatorSymbol);
                Assert.Equal(opSymbol, result.Operands["ds_2"].Operands["ds_2"].OperatorSymbol);
                Assert.Equal("Me1", result.Operands["ds_1"].Operands["ds_1"].ExpressionText);
                Assert.Equal("Me2", result.Operands["ds_2"].Operands["ds_1"].ExpressionText);
                Assert.Equal("Me1", result.Operands["ds_1"].Operands["ds_2"].Operands["ds_1"].ExpressionText); // component of aggrFunction of calcExpr1
                Assert.Equal("Me2", result.Operands["ds_2"].Operands["ds_2"].Operands["ds_1"].ExpressionText); // component of aggrFunction of calcExpr2
            }
            else
            {
                Assert.Equal(1, result.OperandsCollection.Count); // calc
                Assert.Equal("calcExpr", result.Operands["ds_1"].OperatorSymbol);

                Assert.Equal(2, result.Operands["ds_1"].OperandsCollection.Count); // calcExpr
                Assert.Equal("comp", result.Operands["ds_1"].Operands["ds_1"].OperatorSymbol);
                Assert.Equal("int_var", result.Operands["ds_1"].Operands["ds_1"].ExpressionText);

                Assert.Equal(0, result.Operands["ds_1"].Operands["ds_1"].OperandsCollection.Count); // component of calcExpr
                Assert.Equal(0, result.Operands["ds_1"].Operands["ds_2"].OperandsCollection.Count); // aggrFunction of calcExpr
                Assert.Equal(opSymbol, result.Operands["ds_1"].Operands["ds_2"].OperatorSymbol);
                Assert.Equal("int_var", result.Operands["ds_1"].Operands["ds_1"].ExpressionText);
            }
        }
    }
}
