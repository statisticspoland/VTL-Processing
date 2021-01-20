namespace StatisticsPoland.VtlProcessing.Core.Tests.InfrastructureTests.JoinBuilder
{
    using Moq;
    using StatisticsPoland.VtlProcessing.Core.Models.Interfaces;
    using StatisticsPoland.VtlProcessing.Core.Models.Types;
    using StatisticsPoland.VtlProcessing.Core.Operators;
    using StatisticsPoland.VtlProcessing.Core.Operators.Interfaces;
    using StatisticsPoland.VtlProcessing.Core.Tests.Utilities;
    using System.Collections.Generic;
    using System.Linq;
    using Xunit;

    public partial class ExpressionTextGeneratorTests
    {
        [Theory]
        [InlineData("+")]
        [InlineData("-")]
        [InlineData("*")]
        [InlineData("/")]
        [InlineData("||")]
        [InlineData("=")]
        [InlineData("<>")]
        [InlineData("<")]
        [InlineData("<=")]
        [InlineData(">")]
        [InlineData(">=")]
        [InlineData("and")]
        [InlineData("or")]
        [InlineData("xor")]
        [InlineData("in")]
        [InlineData("not_in")]
        [InlineData("#")]
        public void Generate_ConnectorOperator_CorrectText(string opSymbol)
        {
            IExpression expr = this.GetExpr(opSymbol);
            this.exprTextGenerator.Generate(expr);

            string expr1Text = expr.Operands["ds_1"].ExpressionText;
            string expr2Text = expr.Operands["ds_2"].ExpressionText;
            string separator = opSymbol != "#" ? " " : string.Empty;

            Assert.Equal($"{expr1Text}{separator}{opSymbol}{separator}{expr2Text}", expr.ExpressionText);
        }

        [Theory]
        [InlineData("not")]
        [InlineData("plus")]
        [InlineData("minus")]
        public void Generate_PrefixOperator_CorrectText(string opSymbol)
        {
            IExpression expr = this.GetExpr(opSymbol);
            this.exprTextGenerator.Generate(expr);

            string expr1Text = expr.Operands["ds_1"].ExpressionText;
            string separator = string.Empty;
            if (opSymbol != "not") opSymbol = opSymbol == "plus" ? "+" : "-";
            else separator = " ";

            Assert.Equal($"{opSymbol}{separator}{expr1Text}", expr.ExpressionText);
        }

        [Theory]
        [InlineData("calc")]
        [InlineData("keep")]
        [InlineData("drop")]
        [InlineData("rename")]
        [InlineData("sub")]
        public void Generate_DatasetClauseOperator_CorrectText(string opSymbol)
        {
            IExpression expr = this.GetExpr(opSymbol);
            this.exprTextGenerator.Generate(expr);

            string expr1Text = expr.Operands["ds_1"].ExpressionText;
            string expr2Text = expr.Operands["ds_2"].ExpressionText;
            string expr3Text = expr.Operands["ds_3"].ExpressionText;

            Assert.Equal($"{opSymbol} {expr1Text}, {expr2Text}, {expr3Text}", expr.ExpressionText);
        }

        [Theory]
        [InlineData("calcExpr")]
        [InlineData("renameExpr")]
        public void Generate_ClauseExpr_CorrectText(string opSymbol)
        {
            IExpression expr = this.GetExpr(opSymbol);
            this.exprTextGenerator.Generate(expr);

            string expr1Text = expr.Operands["ds_1"].ExpressionText;
            string expr2Text = expr.Operands["ds_2"].ExpressionText;
            string connector = opSymbol == "calcExpr" ? ":=" : "to";

            Assert.Equal($"{expr1Text} {connector} {expr2Text}", expr.ExpressionText);
        }

        [Theory]
        [InlineData("by")]
        [InlineData("except")]
        [InlineData("all")]
        public void Generate_GroupOperator_CorrectText(string opKeyword)
        {
            Mock<IOperatorDefinition> groupOpMock = new Mock<IOperatorDefinition>();
            groupOpMock.SetupGet(o => o.Keyword).Returns(opKeyword);

            IExpression expr = this.GetExpr("group");
            expr.OperatorDefinition = groupOpMock.Object;
            this.exprTextGenerator.Generate(expr);

            string expr1Text = expr.Operands["ds_1"].ExpressionText;
            string expr2Text = expr.Operands["ds_2"].ExpressionText;
            string expr3Text = expr.Operands["ds_3"].ExpressionText;

            Assert.Equal($"group {opKeyword} {expr1Text}, {expr2Text}, {expr3Text}", expr.ExpressionText);
        }

        [Fact]
        public void Generate_IfOperator_CorrectText()
        {
            for (int i = 0; i < 2; i++)
            {
                IExpression expr = this.GetExpr("if");
                expr.Operands.Clear();
                expr.Operands.Add("if", TestExprFactory.GetExpression("If", ExpressionFactoryNameTarget.ResultName));
                expr.Operands.Add("then", TestExprFactory.GetExpression("Then", ExpressionFactoryNameTarget.ResultName));
                
                string expr1Text = expr.Operands["if"].ExpressionText = "expr1";
                string expr2Text = expr.Operands["then"].ExpressionText = "expr2";

                string expected = $"{expr1Text} {expr2Text}";
                if (i == 1)
                {
                    expr.Operands.Add("else", TestExprFactory.GetExpression("Else", ExpressionFactoryNameTarget.ResultName));
                    string expr3Text = expr.Operands["else"].ExpressionText = "expr3";

                    expected += $" {expr3Text}";
                }

                this.exprTextGenerator.Generate(expr);

                Assert.Equal(expected, expr.ExpressionText);
            }
            
        }

        [Theory]
        [InlineData("If")]
        [InlineData("Then")]
        [InlineData("Else")]
        public void Generate_IfThenElseExpr_CorrectText(string resultName)
        {
            IExpression expr = TestExprFactory.GetExpression(resultName, ExpressionFactoryNameTarget.ResultName);
            expr.AddOperand("ds_1", ModelResolvers.ExprResolver());
            string exprText = expr.Operands["ds_1"].ExpressionText = "expr1";

            this.exprTextGenerator.Generate(expr);

            Assert.Equal($"{resultName.ToLower()} {exprText}", expr.ExpressionText);
        }

        [Fact]
        public void Generate_AggrAnalyticOperator_CorrectText()
        {
            List<string> operators = AggrFunctionOperator.Symbols.ToList();
            operators.AddRange(AnalyticFunctionOperator.Symbols);

            for (int i = 0; i < 3; i++)
            {
                foreach (string opSymbol in operators)
                {
                    IExpression expr = this.GetExpr(opSymbol);
                    string expected = $"{opSymbol}({expr.Operands["ds_1"].ExpressionText})";
                    
                    if (i == 1)
                    {
                        expr.Operands.Remove("ds_1");
                        expr.Operands["ds_2"].ResultName = "Alias";
                        expected = $"{opSymbol}({expr.Operands["ds_2"].ExpressionText})";
                    }
                    else if (i == 2)
                    {
                        expr.Operands.Clear();
                        expected = $"{opSymbol}()";
                    }

                    this.exprTextGenerator.Generate(expr);

                    Assert.Equal($"{expected}", expr.ExpressionText);
                }
            }
        }

        [Theory]
        [InlineData("get")]
        [InlineData("ref")]
        [InlineData("const")]
        [InlineData("comp")]
        [InlineData("join")]
        [InlineData("collection")]
        [InlineData("datasetClause")]
        [InlineData("subExpr")]
        [InlineData(null)]
        public void Generate_DoingNothingOperator_CorrectText(string opSymbol)
        {
            IExpression expr = this.GetExpr(opSymbol);
            expr.ExpressionText = "expr";

            string expected = expr.ExpressionText;

            this.exprTextGenerator.Generate(expr);

            Assert.Equal($"{expected}", expr.ExpressionText);
        }

        [Theory]
        [InlineData("between")]
        [InlineData("the_operator")]
        [InlineData("other")]
        [InlineData("test")]
        public void Generate_OtherOperators_CorrectText(string opSymbol)
        {
            IExpression expr = this.GetExpr(opSymbol);

            string expr1Text = expr.Operands["ds_1"].ExpressionText;
            string expr2Text = expr.Operands["ds_2"].ExpressionText;
            string expr3Text = expr.Operands["ds_3"].ExpressionText;

            this.exprTextGenerator.Generate(expr);

            Assert.Equal($"{opSymbol}({expr1Text}, {expr2Text}, {expr3Text})", expr.ExpressionText);
        }
    }
}
