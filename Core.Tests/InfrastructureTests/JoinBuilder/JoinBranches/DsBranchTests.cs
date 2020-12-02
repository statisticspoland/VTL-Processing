namespace StatisticsPoland.VtlProcessing.Core.Tests.InfrastructureTests.JoinBuilder.JoinBranches
{
    using StatisticsPoland.VtlProcessing.Core.Infrastructure;
    using StatisticsPoland.VtlProcessing.Core.Infrastructure.JoinBuilder.JoinBranches;
    using StatisticsPoland.VtlProcessing.Core.Models.Interfaces;
    using StatisticsPoland.VtlProcessing.Core.Models.Types;
    using StatisticsPoland.VtlProcessing.Core.Tests.Utilities;
    using System.Linq;
    using Xunit;

    public partial class DsBranchTests
    {

        [Theory]
        [InlineData("get", "const", "ref")]
        [InlineData("get", "join", "ref")]
        [InlineData("#", "-", "<>")]
        [InlineData("+", "-", "join", "calc", "test")]
        public void Build_Expr_Expr(params string[] opSymbols)
        {
            DsBranch dsBranch = new DsBranch(this.exprFac, this.exprTextGenerator);

            IExpression expr = ModelResolvers.ExprResolver();
            IExpression expected = ModelResolvers.ExprResolver();

            for (int i = 0; i < opSymbols.Length; i++)
            {
                IExpression subExpr = this.exprFac.GetExpression(opSymbols[i], ExpressionFactoryNameTarget.OperatorSymbol); // local expFactory expected
                subExpr.Structure = ModelResolvers.DsResolver();
                subExpr.ExpressionText = opSymbols[i];

                expr.AddOperand($"ds_{i}", subExpr);
                if (opSymbols[i].In("get", "ref", "#", "join")) expected.AddOperand($"ds_{i}", subExpr);
            }

            IExpression result = dsBranch.Build(expr);

            Assert.Equal(expected.OperandsCollection.Count, result.OperandsCollection.Count);
            
            foreach (string opSymbol in opSymbols.Where(opS => opS.In("get", "ref", "#", "join")))
            {
                Assert.True(result.OperandsCollection.Where(op => op.OperatorSymbol == opSymbol).Count() == 1);
            }
        }
    }
}
