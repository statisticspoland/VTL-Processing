namespace StatisticsPoland.VtlProcessing.Core.Tests.InfrastructureTests.JoinBuilder.JoinBranches
{
    using StatisticsPoland.VtlProcessing.Core.Infrastructure.JoinBuilder.JoinBranches;
    using StatisticsPoland.VtlProcessing.Core.Models;
    using StatisticsPoland.VtlProcessing.Core.Models.Interfaces;
    using StatisticsPoland.VtlProcessing.Core.Models.Types;
    using StatisticsPoland.VtlProcessing.Core.Tests.Utilities;
    using Xunit;

    public partial class UsingBranchTests
    {
        [Fact]
        public void Build_Expr_Expr()
        {
            UsingBranch usingBranch = new UsingBranch(this.exprFac);

            IExpression expr = TestExprFactory.GetExpression("join", ExpressionFactoryNameTarget.OperatorSymbol);
            IJoinExpression joinExpr = ModelResolvers.JoinExprResolver(expr);

            IExpression ds1Expr = TestExprFactory.GetExpression("get", ExpressionFactoryNameTarget.OperatorSymbol);
            ds1Expr.Structure = ModelResolvers.DsResolver();
            ds1Expr.Structure.Identifiers.Add(new StructureComponent(BasicDataType.Integer, "Id1"));
            ds1Expr.Structure.Identifiers.Add(new StructureComponent(BasicDataType.Boolean, "Id2"));

            IExpression ds2Expr = TestExprFactory.GetExpression("get", ExpressionFactoryNameTarget.OperatorSymbol);
            ds2Expr.Structure = ModelResolvers.DsResolver();
            ds2Expr.Structure.Identifiers.Add(new StructureComponent(BasicDataType.Integer, "Id1"));
            ds2Expr.Structure.Identifiers.Add(new StructureComponent(BasicDataType.Boolean, "Id2"));
            ds2Expr.Structure.Identifiers.Add(new StructureComponent(BasicDataType.String, "Id3"));

            IExpression dsBranch = ModelResolvers.ExprResolver();
            dsBranch.AddOperand("ds_1", ds1Expr);
            dsBranch.AddOperand("ds_2", ds2Expr);

            joinExpr.AddOperand("ds", dsBranch);

            IExpression expected = TestExprFactory.GetExpression("Using", ExpressionFactoryNameTarget.ResultName);
            expected.ParamSignature = "using";
            expected.ExpressionText = "using Id1, Id2";

            IExpression expected_1 = ModelResolvers.ExprResolver();
            IExpression expected_2 = ModelResolvers.ExprResolver();
            expected_1.ExpressionText = "Id1";
            expected_2.ExpressionText = "Id2";

            expected.AddOperand("Id1", expected_1);
            expected.AddOperand("Id2", expected_2);

            IExpression result = usingBranch.Build(joinExpr);

            Assert.Equal(expected.OperandsCollection.Count, result.OperandsCollection.Count);
            Assert.Equal(expected.Operands["Id1"].ExpressionText, result.Operands["Id1"].ExpressionText);
            Assert.Equal(expected.Operands["Id2"].ExpressionText, result.Operands["Id2"].ExpressionText);
            Assert.Equal(expected.ExpressionText, result.ExpressionText);
        }
    }
}
