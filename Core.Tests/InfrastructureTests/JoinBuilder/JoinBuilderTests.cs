namespace StatisticsPoland.VtlProcessing.Core.Tests.InfrastructureTests.JoinBuilder
{
    using StatisticsPoland.VtlProcessing.Core.ErrorHandling;
    using StatisticsPoland.VtlProcessing.Core.Infrastructure;
    using StatisticsPoland.VtlProcessing.Core.Infrastructure.JoinBuilder;
    using StatisticsPoland.VtlProcessing.Core.MiddleEnd.Utilities;
    using StatisticsPoland.VtlProcessing.Core.Models;
    using StatisticsPoland.VtlProcessing.Core.Models.Interfaces;
    using StatisticsPoland.VtlProcessing.Core.Models.Types;
    using StatisticsPoland.VtlProcessing.Core.Tests.Utilities;
    using System;
    using Xunit;

    public partial class JoinBuilderTests
    {
        [Fact]
        public void AddMainExpr_NonScalarConvertableToJoinExpr_AddsMainExpr()
        {
            JoinBuilder joinBuilder = new JoinBuilder(ModelResolvers.JoinExprResolver, this.joinBranches);

            foreach (string symbol in JoinOperators.Operators)
            {
                IExpression mainExpr = TestExprFactory.GetExpression(symbol, ExpressionFactoryNameTarget.OperatorSymbol);
                mainExpr.ResultName = "main";
                mainExpr.Structure = ModelResolvers.DsResolver();
                mainExpr.Structure.Identifiers.Add(new StructureComponent(BasicDataType.Integer));
                mainExpr.Structure.Measures.Add(new StructureComponent(BasicDataType.Integer));

                joinBuilder.AddMainExpr(mainExpr);

                Assert.Equal(mainExpr, joinBuilder.Branches["main"]);
                Assert.False(joinBuilder.IsCleared);
            }
        }

        [Fact]
        public void AddMainExpr_JoinExpr_AddsMainExpr()
        {
            JoinBuilder joinBuilder = new JoinBuilder(ModelResolvers.JoinExprResolver, this.joinBranches);

            IExpression mainExpr = TestExprFactory.GetExpression("join", ExpressionFactoryNameTarget.OperatorSymbol);
            mainExpr.ResultName = "main";
            mainExpr.Structure = ModelResolvers.DsResolver();
            mainExpr.Structure.Identifiers.Add(new StructureComponent(BasicDataType.Integer));
            mainExpr.Structure.Measures.Add(new StructureComponent(BasicDataType.Integer));

            joinBuilder.AddMainExpr(mainExpr);

            Assert.Equal(mainExpr.ResultName, joinBuilder.Branches["main"].ResultName);
            Assert.Equal(mainExpr.OperatorSymbol, joinBuilder.Branches["main"].OperatorSymbol);
            Assert.True(mainExpr.Structure.EqualsObj(joinBuilder.Branches["main"].Structure));
            Assert.False(joinBuilder.IsCleared);
        }

        [Fact]
        public void AddMainExpr_ScalarExpr_ThrowsException()
        {
            JoinBuilder joinBuilder = new JoinBuilder(ModelResolvers.JoinExprResolver, this.joinBranches);

            IExpression mainExpr = TestExprFactory.GetExpression("+", ExpressionFactoryNameTarget.OperatorSymbol);
            mainExpr.ResultName = "main";
            mainExpr.Structure = ModelResolvers.DsResolver();
            mainExpr.Structure.Measures.Add(new StructureComponent(BasicDataType.Integer));

            Assert.ThrowsAny<Exception>(() => { joinBuilder.AddMainExpr(mainExpr); });
        }

        [Fact]
        public void AddMainExpr_NotConvertableToJoinExpr_ThrowsException()
        {
            JoinBuilder joinBuilder = new JoinBuilder(ModelResolvers.JoinExprResolver, this.joinBranches);

            IExpression mainExpr = TestExprFactory.GetExpression("#", ExpressionFactoryNameTarget.OperatorSymbol);
            mainExpr.ResultName = "main";
            mainExpr.Structure = ModelResolvers.DsResolver();
            mainExpr.Structure.Identifiers.Add(new StructureComponent(BasicDataType.Integer));
            mainExpr.Structure.Measures.Add(new StructureComponent(BasicDataType.Integer));

            Assert.ThrowsAny<Exception>(() => { joinBuilder.AddMainExpr(mainExpr); });
        }

        [Fact]
        public void AddBranch_Expr_AddsBranch()
        {
            JoinBuilder joinBuilder = new JoinBuilder(ModelResolvers.JoinExprResolver, this.joinBranches);
            IExpression expr = ModelResolvers.ExprResolver();

            Assert.False(joinBuilder.Branches.ContainsKey("branch"));

            joinBuilder.AddBranch("branch", expr);

            Assert.True(joinBuilder.Branches.ContainsKey("branch"));
            Assert.Equal(expr, joinBuilder.Branches["branch"]);
            Assert.False(joinBuilder.IsCleared);
        }

        [Fact]
        public void Clear_ClearsBranches()
        {
            JoinBuilder joinBuilder = new JoinBuilder(ModelResolvers.JoinExprResolver, this.joinBranches);
            IExpression expr1 = ModelResolvers.ExprResolver();
            IExpression expr2 = ModelResolvers.ExprResolver();
            IExpression expr3 = ModelResolvers.ExprResolver();

            Assert.False(joinBuilder.Branches.ContainsKey("branch1"));
            Assert.False(joinBuilder.Branches.ContainsKey("branch2"));
            Assert.False(joinBuilder.Branches.ContainsKey("branch3"));

            joinBuilder.AddBranch("branch1", expr1);
            joinBuilder.AddBranch("branch2", expr2);
            joinBuilder.AddBranch("branch3", expr3);

            Assert.False(joinBuilder.IsCleared);

            joinBuilder.Clear();

            Assert.False(joinBuilder.Branches.ContainsKey("branch1"));
            Assert.False(joinBuilder.Branches.ContainsKey("branch2"));
            Assert.False(joinBuilder.Branches.ContainsKey("branch3"));
            Assert.True(joinBuilder.IsCleared);
        }

        [Theory]
        [InlineData("ds")]
        [InlineData("ds", "using")]
        [InlineData("ds", "filter")]
        [InlineData("ds", "apply")]
        [InlineData("ds", "calc")]
        [InlineData("ds", "aggr")]
        [InlineData("ds", "rename")]
        [InlineData("ds", "pivot")]
        [InlineData("ds", "unpivot")]
        [InlineData("ds", "subspace")]
        [InlineData("ds", "using", "filter")]
        [InlineData("ds", "using", "apply")]
        [InlineData("ds", "using", "calc")]
        [InlineData("ds", "using", "aggr")]
        [InlineData("ds", "using", "rename")]
        [InlineData("ds", "using", "pivot")]
        [InlineData("ds", "using", "unpivot")]
        [InlineData("ds", "using", "subspace")]
        [InlineData("ds", "filter", "apply")]
        [InlineData("ds", "filter", "calc")]
        [InlineData("ds", "filter", "aggr")]
        [InlineData("ds", "filter", "rename")]
        [InlineData("ds", "apply", "rename")]
        [InlineData("ds", "calc", "rename")]
        [InlineData("ds", "aggr", "rename")]
        [InlineData("ds", "using", "filter", "rename")]
        [InlineData("ds", "using", "apply", "rename")]
        [InlineData("ds", "using", "calc", "rename")]
        [InlineData("ds", "using", "aggr", "rename")]
        [InlineData("ds", "filter", "apply", "rename")]
        [InlineData("ds", "filter", "calc", "rename")]
        [InlineData("ds", "filter", "aggr", "rename")]
        [InlineData("ds", "using", "filter", "apply", "rename")]
        [InlineData("ds", "using", "filter", "calc", "rename")]
        [InlineData("ds", "using", "filter", "aggr", "rename")]
        public void Build_CorrectBranches_JoinExpr(params string[] branches)
        {
            JoinBuilder joinBuilder = new JoinBuilder(ModelResolvers.JoinExprResolver, this.joinBranches);
            joinBuilder.AddMainExpr(TestExprFactory.GetExpression("join", ExpressionFactoryNameTarget.OperatorSymbol));

            Assert.Empty(joinBuilder.Branches["main"].OperandsCollection);

            foreach (string branch in branches)
            {
                joinBuilder.AddBranch(branch, TestExprFactory.GetExpression("opt", ExpressionFactoryNameTarget.OperatorSymbol));
            }

            IJoinExpression result = joinBuilder.Build();

            Assert.Equal(branches.Length, joinBuilder.Branches["main"].OperandsCollection.Count);
            Assert.Equal(branches.Length, result.OperandsCollection.Count);

            foreach (string branch in branches)
            {
                Assert.True(joinBuilder.Branches["main"].Operands.ContainsKey(branch));
                Assert.True(result.Operands.ContainsKey(branch));
            }
        }

        [Theory]
        [InlineData("using")]
        [InlineData("filter")]
        [InlineData("apply")]
        [InlineData("calc")]
        [InlineData("aggr")]
        [InlineData("rename")]
        [InlineData("pivot")]
        [InlineData("unpivot")]
        [InlineData("subspace")]
        [InlineData("using", "filter")]
        [InlineData("using", "apply")]
        [InlineData("using", "calc")]
        [InlineData("using", "aggr")]
        [InlineData("using", "rename")]
        [InlineData("using", "pivot")]
        [InlineData("using", "unpivot")]
        [InlineData("using", "subspace")]
        [InlineData("filter", "apply")]
        [InlineData("filter", "calc")]
        [InlineData("filter", "aggr")]
        [InlineData("filter", "rename")]
        [InlineData("apply", "rename")]
        [InlineData("calc", "rename")]
        [InlineData("aggr", "rename")]
        [InlineData("using", "filter", "rename")]
        [InlineData("using", "apply", "rename")]
        [InlineData("using", "calc", "rename")]
        [InlineData("using", "aggr", "rename")]
        [InlineData("filter", "apply", "rename")]
        [InlineData("filter", "calc", "rename")]
        [InlineData("filter", "aggr", "rename")]
        [InlineData("using", "filter", "apply", "rename")]
        [InlineData("using", "filter", "calc", "rename")]
        [InlineData("using", "filter", "aggr", "rename")]
        public void Build_NoDsBranch_ThrowsException(params string[] branches)
        {
            JoinBuilder joinBuilder = new JoinBuilder(ModelResolvers.JoinExprResolver, this.joinBranches);
            joinBuilder.AddMainExpr(TestExprFactory.GetExpression("join", ExpressionFactoryNameTarget.OperatorSymbol));

            foreach (string branch in branches)
            {
                joinBuilder.AddBranch(branch, TestExprFactory.GetExpression("opt", ExpressionFactoryNameTarget.OperatorSymbol));
            }

            Assert.ThrowsAny<VtlOperatorError>(() => { joinBuilder.Build(); });
        }

        [Theory]
        [InlineData("ds", "apply", "calc")]
        [InlineData("ds", "apply", "aggr")]
        [InlineData("ds", "calc", "aggr")]
        [InlineData("ds", "keep", "drop")]
        [InlineData("ds", "pivot", "filter")]
        [InlineData("ds", "pivot", "apply")]
        [InlineData("ds", "pivot", "calc")]
        [InlineData("ds", "pivot", "aggr")]
        [InlineData("ds", "pivot", "rename")]
        [InlineData("ds", "pivot", "unpivot")]
        [InlineData("ds", "pivot", "subspace")]
        [InlineData("ds", "unpivot", "filter")]
        [InlineData("ds", "unpivot", "apply")]
        [InlineData("ds", "unpivot", "calc")]
        [InlineData("ds", "unpivot", "aggr")]
        [InlineData("ds", "unpivot", "rename")]
        [InlineData("ds", "unpivot", "pivot")]
        [InlineData("ds", "unpivot", "subspace")]
        [InlineData("ds", "subspace", "filter")]
        [InlineData("ds", "subspace", "apply")]
        [InlineData("ds", "subspace", "calc")]
        [InlineData("ds", "subspace", "aggr")]
        [InlineData("ds", "subspace", "rename")]
        [InlineData("ds", "subspace", "pivot")]
        [InlineData("ds", "subspace", "unpivot")]
        [InlineData("ds", "using", "apply", "calc")]
        [InlineData("ds", "using", "apply", "aggr")]
        [InlineData("ds", "using", "calc", "aggr")]
        [InlineData("ds", "using", "keep", "drop")]
        [InlineData("ds", "using", "pivot", "filter")]
        [InlineData("ds", "using", "pivot", "apply")]
        [InlineData("ds", "using", "pivot", "calc")]
        [InlineData("ds", "using", "pivot", "aggr")]
        [InlineData("ds", "using", "pivot", "rename")]
        [InlineData("ds", "using", "pivot", "unpivot")]
        [InlineData("ds", "using", "pivot", "subspace")]
        [InlineData("ds", "using", "unpivot", "filter")]
        [InlineData("ds", "using", "unpivot", "apply")]
        [InlineData("ds", "using", "unpivot", "calc")]
        [InlineData("ds", "using", "unpivot", "aggr")]
        [InlineData("ds", "using", "unpivot", "rename")]
        [InlineData("ds", "using", "unpivot", "pivot")]
        [InlineData("ds", "using", "unpivot", "subspace")]
        [InlineData("ds", "using", "subspace", "filter")]
        [InlineData("ds", "using", "subspace", "apply")]
        [InlineData("ds", "using", "subspace", "calc")]
        [InlineData("ds", "using", "subspace", "aggr")]
        [InlineData("ds", "using", "subspace", "rename")]
        [InlineData("ds", "using", "subspace", "pivot")]
        [InlineData("ds", "using", "subspace", "unpivot")]
        public void Build_WrongBranchesCombination_ThrowsException(params string[] branches)
        {
            JoinBuilder joinBuilder = new JoinBuilder(ModelResolvers.JoinExprResolver, this.joinBranches);
            joinBuilder.AddMainExpr(TestExprFactory.GetExpression("join", ExpressionFactoryNameTarget.OperatorSymbol));

            foreach (string branch in branches)
            {
                joinBuilder.AddBranch(branch, TestExprFactory.GetExpression("opt", ExpressionFactoryNameTarget.OperatorSymbol));
            }

            Assert.ThrowsAny<VtlOperatorError>(() => { joinBuilder.Build(); });
        }

        [Fact]
        public void Build_FullJoinWithUsinngBranch_ThrowsException()
        {
            JoinBuilder joinBuilder = new JoinBuilder(ModelResolvers.JoinExprResolver, this.joinBranches);
            joinBuilder.AddMainExpr(TestExprFactory.GetExpression("join", ExpressionFactoryNameTarget.OperatorSymbol));
            joinBuilder.AddBranch("ds", ModelResolvers.ExprResolver());
            joinBuilder.AddBranch("using", ModelResolvers.ExprResolver());
            joinBuilder.Branches["main"].OperatorDefinition.Keyword = "full";

            Assert.ThrowsAny<VtlOperatorError>(() => { joinBuilder.Build(); });
        }

        [Fact]
        public void Build_CrossJoinWithUsinngBranch_ThrowsException()
        {
            JoinBuilder joinBuilder = new JoinBuilder(ModelResolvers.JoinExprResolver, this.joinBranches);
            joinBuilder.AddMainExpr(TestExprFactory.GetExpression("join", ExpressionFactoryNameTarget.OperatorSymbol));
            joinBuilder.AddBranch("ds", ModelResolvers.ExprResolver());
            joinBuilder.AddBranch("using", ModelResolvers.ExprResolver());
            joinBuilder.Branches["main"].OperatorDefinition.Keyword = "cross";

            Assert.ThrowsAny<VtlOperatorError>(() => { joinBuilder.Build(); });
        }

        [Theory]
        [InlineData("branch1")]
        [InlineData("branch2")]
        public void BuildBranch_ExistingBranchBuilder_AddsBranch(string branch)
        {
            JoinBuilder joinBuilder = new JoinBuilder(ModelResolvers.JoinExprResolver, this.joinBranches);

            Assert.Empty(joinBuilder.Branches);

            IExpression result = joinBuilder.BuildBranch(branch, ModelResolvers.ExprResolver());

            Assert.True(joinBuilder.Branches.ContainsKey(branch));
            Assert.Equal(result, joinBuilder.Branches[branch]);
        }
    }
}
