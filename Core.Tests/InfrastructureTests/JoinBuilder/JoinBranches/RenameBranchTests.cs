namespace StatisticsPoland.VtlProcessing.Core.Tests.InfrastructureTests.JoinBuilder.JoinBranches
{
    using StatisticsPoland.VtlProcessing.Core.Infrastructure.JoinBuilder.JoinBranches;
    using StatisticsPoland.VtlProcessing.Core.Models.Interfaces;
    using StatisticsPoland.VtlProcessing.Core.Models.Types;
    using StatisticsPoland.VtlProcessing.Core.Tests.Utilities;
    using Xunit;

    public partial class RenameBranchTests
    {
        [Fact]
        public void Build_Expr_Expr()
        {
            RenameBranch renameBranch = new RenameBranch(this.exprFac, this.exprTextGenerator);

            IExpression expr = TestExprFactory.GetExpression("=", ExpressionFactoryNameTarget.OperatorSymbol);
            expr.LineNumber = 10;
            expr.Structure = ModelResolvers.DsResolver("Me1", ComponentType.Measure, BasicDataType.Boolean);
            expr.Structure.Measures[0].BaseComponentName = "Me1_v2";

            IExpression expected = TestExprFactory.GetExpression("rename", ExpressionFactoryNameTarget.OperatorSymbol);
            IExpression expected_1 = TestExprFactory.GetExpression("renameExpr", ExpressionFactoryNameTarget.OperatorSymbol);
            IExpression expected_1_1 = TestExprFactory.GetExpression("comp", ExpressionFactoryNameTarget.OperatorSymbol);
            IExpression expected_1_2 = TestExprFactory.GetExpression("comp", ExpressionFactoryNameTarget.OperatorSymbol);

            expected_1_1.ExpressionText = "Me1_v2";
            expected_1_2.ExpressionText = "Me1";

            expected.LineNumber = expr.LineNumber;
            expected_1.LineNumber = expr.LineNumber;
            expected_1_1.LineNumber = expr.LineNumber;
            expected_1_2.LineNumber = expr.LineNumber;

            expected.AddOperand("ds_1", expected_1);
            expected_1.AddOperand("ds_1", expected_1_1);
            expected_1.AddOperand("ds_2", expected_1_2);

            IExpression result = renameBranch.Build(expr);

            Assert.Equal(expected.OperandsCollection.Count, result.OperandsCollection.Count);
            Assert.Equal(expected.Operands["ds_1"].OperandsCollection.Count, result.Operands["ds_1"].OperandsCollection.Count);

            Assert.Equal(expected.OperatorSymbol, result.OperatorSymbol);
            Assert.Equal(expected.Operands["ds_1"].OperatorSymbol, result.Operands["ds_1"].OperatorSymbol);

            Assert.Equal(expected.Operands["ds_1"].Operands["ds_1"].OperatorSymbol, result.Operands["ds_1"].Operands["ds_1"].OperatorSymbol);
            Assert.Equal(expected.Operands["ds_1"].Operands["ds_2"].OperatorSymbol, result.Operands["ds_1"].Operands["ds_2"].OperatorSymbol);

            Assert.Equal(expected.Operands["ds_1"].Operands["ds_1"].ExpressionText, result.Operands["ds_1"].Operands["ds_1"].ExpressionText);
            Assert.Equal(expected.Operands["ds_1"].Operands["ds_2"].ExpressionText, result.Operands["ds_1"].Operands["ds_2"].ExpressionText);
            
            Assert.Equal(expected.LineNumber, result.LineNumber);
            Assert.Equal(expected.Operands["ds_1"].LineNumber, result.Operands["ds_1"].LineNumber);
            Assert.Equal(expected.Operands["ds_1"].Operands["ds_1"].LineNumber, result.Operands["ds_1"].Operands["ds_1"].LineNumber);
            Assert.Equal(expected.Operands["ds_1"].Operands["ds_2"].LineNumber, result.Operands["ds_1"].Operands["ds_2"].LineNumber);
        }
    }
}
