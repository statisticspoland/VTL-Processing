namespace StatisticsPoland.VtlProcessing.Core.Tests.OperatorsTests.ComponentManagement
{
    using Moq;
    using StatisticsPoland.VtlProcessing.Core.ErrorHandling;
    using StatisticsPoland.VtlProcessing.Core.Infrastructure;
    using StatisticsPoland.VtlProcessing.Core.Infrastructure.DependencyInjection;
    using StatisticsPoland.VtlProcessing.Core.Infrastructure.Interfaces;
    using StatisticsPoland.VtlProcessing.Core.Models.Interfaces;
    using StatisticsPoland.VtlProcessing.Core.Models.Types;
    using StatisticsPoland.VtlProcessing.Core.Operators;
    using StatisticsPoland.VtlProcessing.Core.Operators.Auxiliary;
    using StatisticsPoland.VtlProcessing.Core.Tests.Utilities;
    using System;
    using System.Linq;
    using Xunit;

    public class CompCollectorOperatorTests
    {
        private readonly IExpressionFactory exprFac;

        public CompCollectorOperatorTests()
        {
            Mock<IExpressionFactory> exprFactoryMock = new Mock<IExpressionFactory>();
            exprFactoryMock.SetupGet(e => e.OperatorResolver).Returns(ModelResolvers.OperatorResolver);
            exprFactoryMock.SetupGet(e => e.ExprResolver).Returns(ModelResolvers.ExprResolver);
            exprFactoryMock.Setup(e => e.GetExpression("group", ExpressionFactoryNameTarget.OperatorSymbol)).Returns(() =>
            {
                IExpression expr = ModelResolvers.ExprResolver();
                expr.OperatorDefinition = ModelResolvers.OperatorResolver("group");
                return expr;
            });
            exprFactoryMock.Setup(e => e.GetExpression("join", ExpressionFactoryNameTarget.OperatorSymbol)).Returns(() => 
            {
                IExpression expr = ModelResolvers.ExprResolver();
                expr.OperatorDefinition = ModelResolvers.OperatorResolver("join");
                expr.AddOperand("ds", ModelResolvers.ExprResolver());
                return expr;
            });

            this.exprFac = exprFactoryMock.Object;
        }

        [Theory]
        [InlineData("calc", TestExprType.Integer, TestExprType.Integer)]
        [InlineData("calc", TestExprType.Integer, TestExprType.Number)]
        [InlineData("calc", TestExprType.Integer, TestExprType.String)]
        [InlineData("calc", TestExprType.Integer, TestExprType.Boolean)]
        [InlineData("calc", TestExprType.Integer, TestExprType.Time)]
        [InlineData("calc", TestExprType.Integer, TestExprType.Date)]
        [InlineData("calc", TestExprType.Integer, TestExprType.TimePeriod)]
        [InlineData("calc", TestExprType.Number, TestExprType.Integer)]
        [InlineData("calc", TestExprType.Number, TestExprType.Number)]
        [InlineData("calc", TestExprType.Number, TestExprType.String)]
        [InlineData("calc", TestExprType.Number, TestExprType.Boolean)]
        [InlineData("calc", TestExprType.Number, TestExprType.Time)]
        [InlineData("calc", TestExprType.Number, TestExprType.Date)]
        [InlineData("calc", TestExprType.Number, TestExprType.TimePeriod)]
        [InlineData("calc", TestExprType.String, TestExprType.Integer)]
        [InlineData("calc", TestExprType.String, TestExprType.Number)]
        [InlineData("calc", TestExprType.String, TestExprType.String)]
        [InlineData("calc", TestExprType.String, TestExprType.Boolean)]
        [InlineData("calc", TestExprType.String, TestExprType.Time)]
        [InlineData("calc", TestExprType.String, TestExprType.Date)]
        [InlineData("calc", TestExprType.String, TestExprType.TimePeriod)]
        [InlineData("calc", TestExprType.Boolean, TestExprType.Integer)]
        [InlineData("calc", TestExprType.Boolean, TestExprType.Number)]
        [InlineData("calc", TestExprType.Boolean, TestExprType.String)]
        [InlineData("calc", TestExprType.Boolean, TestExprType.Boolean)]
        [InlineData("calc", TestExprType.Boolean, TestExprType.Time)]
        [InlineData("calc", TestExprType.Boolean, TestExprType.Date)]
        [InlineData("calc", TestExprType.Boolean, TestExprType.TimePeriod)]
        [InlineData("calc", TestExprType.Time, TestExprType.Integer)]
        [InlineData("calc", TestExprType.Time, TestExprType.Number)]
        [InlineData("calc", TestExprType.Time, TestExprType.String)]
        [InlineData("calc", TestExprType.Time, TestExprType.Boolean)]
        [InlineData("calc", TestExprType.Time, TestExprType.Time)]
        [InlineData("calc", TestExprType.Time, TestExprType.Date)]
        [InlineData("calc", TestExprType.Time, TestExprType.TimePeriod)]
        [InlineData("calc", TestExprType.Date, TestExprType.Integer)]
        [InlineData("calc", TestExprType.Date, TestExprType.Number)]
        [InlineData("calc", TestExprType.Date, TestExprType.String)]
        [InlineData("calc", TestExprType.Date, TestExprType.Boolean)]
        [InlineData("calc", TestExprType.Date, TestExprType.Time)]
        [InlineData("calc", TestExprType.Date, TestExprType.Date)]
        [InlineData("calc", TestExprType.Date, TestExprType.TimePeriod)]
        [InlineData("calc", TestExprType.TimePeriod, TestExprType.Integer)]
        [InlineData("calc", TestExprType.TimePeriod, TestExprType.Number)]
        [InlineData("calc", TestExprType.TimePeriod, TestExprType.String)]
        [InlineData("calc", TestExprType.TimePeriod, TestExprType.Boolean)]
        [InlineData("calc", TestExprType.TimePeriod, TestExprType.Time)]
        [InlineData("calc", TestExprType.TimePeriod, TestExprType.Date)]
        [InlineData("calc", TestExprType.TimePeriod, TestExprType.TimePeriod)]
        [InlineData("rename", TestExprType.Integer, TestExprType.Integer)]
        [InlineData("rename", TestExprType.Integer, TestExprType.Number)]
        [InlineData("rename", TestExprType.Integer, TestExprType.String)]
        [InlineData("rename", TestExprType.Integer, TestExprType.Boolean)]
        [InlineData("rename", TestExprType.Integer, TestExprType.Time)]
        [InlineData("rename", TestExprType.Integer, TestExprType.Date)]
        [InlineData("rename", TestExprType.Integer, TestExprType.TimePeriod)]
        [InlineData("rename", TestExprType.Number, TestExprType.Integer)]
        [InlineData("rename", TestExprType.Number, TestExprType.Number)]
        [InlineData("rename", TestExprType.Number, TestExprType.String)]
        [InlineData("rename", TestExprType.Number, TestExprType.Boolean)]
        [InlineData("rename", TestExprType.Number, TestExprType.Time)]
        [InlineData("rename", TestExprType.Number, TestExprType.Date)]
        [InlineData("rename", TestExprType.Number, TestExprType.TimePeriod)]
        [InlineData("rename", TestExprType.String, TestExprType.Integer)]
        [InlineData("rename", TestExprType.String, TestExprType.Number)]
        [InlineData("rename", TestExprType.String, TestExprType.String)]
        [InlineData("rename", TestExprType.String, TestExprType.Boolean)]
        [InlineData("rename", TestExprType.String, TestExprType.Time)]
        [InlineData("rename", TestExprType.String, TestExprType.Date)]
        [InlineData("rename", TestExprType.String, TestExprType.TimePeriod)]
        [InlineData("rename", TestExprType.Boolean, TestExprType.Integer)]
        [InlineData("rename", TestExprType.Boolean, TestExprType.Number)]
        [InlineData("rename", TestExprType.Boolean, TestExprType.String)]
        [InlineData("rename", TestExprType.Boolean, TestExprType.Boolean)]
        [InlineData("rename", TestExprType.Boolean, TestExprType.Time)]
        [InlineData("rename", TestExprType.Boolean, TestExprType.Date)]
        [InlineData("rename", TestExprType.Boolean, TestExprType.TimePeriod)]
        [InlineData("rename", TestExprType.Time, TestExprType.Integer)]
        [InlineData("rename", TestExprType.Time, TestExprType.Number)]
        [InlineData("rename", TestExprType.Time, TestExprType.String)]
        [InlineData("rename", TestExprType.Time, TestExprType.Boolean)]
        [InlineData("rename", TestExprType.Time, TestExprType.Time)]
        [InlineData("rename", TestExprType.Time, TestExprType.Date)]
        [InlineData("rename", TestExprType.Time, TestExprType.TimePeriod)]
        [InlineData("rename", TestExprType.Date, TestExprType.Integer)]
        [InlineData("rename", TestExprType.Date, TestExprType.Number)]
        [InlineData("rename", TestExprType.Date, TestExprType.String)]
        [InlineData("rename", TestExprType.Date, TestExprType.Boolean)]
        [InlineData("rename", TestExprType.Date, TestExprType.Time)]
        [InlineData("rename", TestExprType.Date, TestExprType.Date)]
        [InlineData("rename", TestExprType.Date, TestExprType.TimePeriod)]
        [InlineData("rename", TestExprType.TimePeriod, TestExprType.Integer)]
        [InlineData("rename", TestExprType.TimePeriod, TestExprType.Number)]
        [InlineData("rename", TestExprType.TimePeriod, TestExprType.String)]
        [InlineData("rename", TestExprType.TimePeriod, TestExprType.Boolean)]
        [InlineData("rename", TestExprType.TimePeriod, TestExprType.Time)]
        [InlineData("rename", TestExprType.TimePeriod, TestExprType.Date)]
        [InlineData("rename", TestExprType.TimePeriod, TestExprType.TimePeriod)]
        [InlineData("keep", TestExprType.Integer, TestExprType.Integer)]
        [InlineData("keep", TestExprType.Integer, TestExprType.Number)]
        [InlineData("keep", TestExprType.Integer, TestExprType.String)]
        [InlineData("keep", TestExprType.Integer, TestExprType.Boolean)]
        [InlineData("keep", TestExprType.Integer, TestExprType.Time)]
        [InlineData("keep", TestExprType.Integer, TestExprType.Date)]
        [InlineData("keep", TestExprType.Integer, TestExprType.TimePeriod)]
        [InlineData("keep", TestExprType.Number, TestExprType.Integer)]
        [InlineData("keep", TestExprType.Number, TestExprType.Number)]
        [InlineData("keep", TestExprType.Number, TestExprType.String)]
        [InlineData("keep", TestExprType.Number, TestExprType.Boolean)]
        [InlineData("keep", TestExprType.Number, TestExprType.Time)]
        [InlineData("keep", TestExprType.Number, TestExprType.Date)]
        [InlineData("keep", TestExprType.Number, TestExprType.TimePeriod)]
        [InlineData("keep", TestExprType.String, TestExprType.Integer)]
        [InlineData("keep", TestExprType.String, TestExprType.Number)]
        [InlineData("keep", TestExprType.String, TestExprType.String)]
        [InlineData("keep", TestExprType.String, TestExprType.Boolean)]
        [InlineData("keep", TestExprType.String, TestExprType.Time)]
        [InlineData("keep", TestExprType.String, TestExprType.Date)]
        [InlineData("keep", TestExprType.String, TestExprType.TimePeriod)]
        [InlineData("keep", TestExprType.Boolean, TestExprType.Integer)]
        [InlineData("keep", TestExprType.Boolean, TestExprType.Number)]
        [InlineData("keep", TestExprType.Boolean, TestExprType.String)]
        [InlineData("keep", TestExprType.Boolean, TestExprType.Boolean)]
        [InlineData("keep", TestExprType.Boolean, TestExprType.Time)]
        [InlineData("keep", TestExprType.Boolean, TestExprType.Date)]
        [InlineData("keep", TestExprType.Boolean, TestExprType.TimePeriod)]
        [InlineData("keep", TestExprType.Time, TestExprType.Integer)]
        [InlineData("keep", TestExprType.Time, TestExprType.Number)]
        [InlineData("keep", TestExprType.Time, TestExprType.String)]
        [InlineData("keep", TestExprType.Time, TestExprType.Boolean)]
        [InlineData("keep", TestExprType.Time, TestExprType.Time)]
        [InlineData("keep", TestExprType.Time, TestExprType.Date)]
        [InlineData("keep", TestExprType.Time, TestExprType.TimePeriod)]
        [InlineData("keep", TestExprType.Date, TestExprType.Integer)]
        [InlineData("keep", TestExprType.Date, TestExprType.Number)]
        [InlineData("keep", TestExprType.Date, TestExprType.String)]
        [InlineData("keep", TestExprType.Date, TestExprType.Boolean)]
        [InlineData("keep", TestExprType.Date, TestExprType.Time)]
        [InlineData("keep", TestExprType.Date, TestExprType.Date)]
        [InlineData("keep", TestExprType.Date, TestExprType.TimePeriod)]
        [InlineData("keep", TestExprType.TimePeriod, TestExprType.Integer)]
        [InlineData("keep", TestExprType.TimePeriod, TestExprType.Number)]
        [InlineData("keep", TestExprType.TimePeriod, TestExprType.String)]
        [InlineData("keep", TestExprType.TimePeriod, TestExprType.Boolean)]
        [InlineData("keep", TestExprType.TimePeriod, TestExprType.Time)]
        [InlineData("keep", TestExprType.TimePeriod, TestExprType.Date)]
        [InlineData("keep", TestExprType.TimePeriod, TestExprType.TimePeriod)]
        [InlineData("drop", TestExprType.Integer, TestExprType.Integer)]
        [InlineData("drop", TestExprType.Integer, TestExprType.Number)]
        [InlineData("drop", TestExprType.Integer, TestExprType.String)]
        [InlineData("drop", TestExprType.Integer, TestExprType.Boolean)]
        [InlineData("drop", TestExprType.Integer, TestExprType.Time)]
        [InlineData("drop", TestExprType.Integer, TestExprType.Date)]
        [InlineData("drop", TestExprType.Integer, TestExprType.TimePeriod)]
        [InlineData("drop", TestExprType.Number, TestExprType.Integer)]
        [InlineData("drop", TestExprType.Number, TestExprType.Number)]
        [InlineData("drop", TestExprType.Number, TestExprType.String)]
        [InlineData("drop", TestExprType.Number, TestExprType.Boolean)]
        [InlineData("drop", TestExprType.Number, TestExprType.Time)]
        [InlineData("drop", TestExprType.Number, TestExprType.Date)]
        [InlineData("drop", TestExprType.Number, TestExprType.TimePeriod)]
        [InlineData("drop", TestExprType.String, TestExprType.Integer)]
        [InlineData("drop", TestExprType.String, TestExprType.Number)]
        [InlineData("drop", TestExprType.String, TestExprType.String)]
        [InlineData("drop", TestExprType.String, TestExprType.Boolean)]
        [InlineData("drop", TestExprType.String, TestExprType.Time)]
        [InlineData("drop", TestExprType.String, TestExprType.Date)]
        [InlineData("drop", TestExprType.String, TestExprType.TimePeriod)]
        [InlineData("drop", TestExprType.Boolean, TestExprType.Integer)]
        [InlineData("drop", TestExprType.Boolean, TestExprType.Number)]
        [InlineData("drop", TestExprType.Boolean, TestExprType.String)]
        [InlineData("drop", TestExprType.Boolean, TestExprType.Boolean)]
        [InlineData("drop", TestExprType.Boolean, TestExprType.Time)]
        [InlineData("drop", TestExprType.Boolean, TestExprType.Date)]
        [InlineData("drop", TestExprType.Boolean, TestExprType.TimePeriod)]
        [InlineData("drop", TestExprType.Time, TestExprType.Integer)]
        [InlineData("drop", TestExprType.Time, TestExprType.Number)]
        [InlineData("drop", TestExprType.Time, TestExprType.String)]
        [InlineData("drop", TestExprType.Time, TestExprType.Boolean)]
        [InlineData("drop", TestExprType.Time, TestExprType.Time)]
        [InlineData("drop", TestExprType.Time, TestExprType.Date)]
        [InlineData("drop", TestExprType.Time, TestExprType.TimePeriod)]
        [InlineData("drop", TestExprType.Date, TestExprType.Integer)]
        [InlineData("drop", TestExprType.Date, TestExprType.Number)]
        [InlineData("drop", TestExprType.Date, TestExprType.String)]
        [InlineData("drop", TestExprType.Date, TestExprType.Boolean)]
        [InlineData("drop", TestExprType.Date, TestExprType.Time)]
        [InlineData("drop", TestExprType.Date, TestExprType.Date)]
        [InlineData("drop", TestExprType.Date, TestExprType.TimePeriod)]
        [InlineData("drop", TestExprType.TimePeriod, TestExprType.Integer)]
        [InlineData("drop", TestExprType.TimePeriod, TestExprType.Number)]
        [InlineData("drop", TestExprType.TimePeriod, TestExprType.String)]
        [InlineData("drop", TestExprType.TimePeriod, TestExprType.Boolean)]
        [InlineData("drop", TestExprType.TimePeriod, TestExprType.Time)]
        [InlineData("drop", TestExprType.TimePeriod, TestExprType.Date)]
        [InlineData("drop", TestExprType.TimePeriod, TestExprType.TimePeriod)]
        public void GetOutputStructure_2ScalarsExpr_DataStructure(string opSymbol, params TestExprType[] types)
        {
            IExpression compCreateExpr = TestExprFactory.GetExpression(types);
            compCreateExpr.OperatorDefinition = this.exprFac.OperatorResolver(opSymbol);
            compCreateExpr.Operands["ds_2"].Structure.Measures[0].ComponentName += "2";

            IJoinExpression joinExpr = ModelResolvers.JoinExprResolver(this.exprFac.GetExpression("join", ExpressionFactoryNameTarget.OperatorSymbol));
            joinExpr.AddOperand("ds_1", compCreateExpr);
            
            IDataStructure expectedStructure = compCreateExpr.Operands["ds_1"].Structure.GetCopy();
            expectedStructure.Measures.Add(compCreateExpr.Operands["ds_2"].Structure.GetCopy().Measures[0]);

            IDataStructure dataStructure = compCreateExpr.OperatorDefinition.GetOutputStructure(compCreateExpr);

            Assert.True(expectedStructure.EqualsObj(dataStructure));
        }

        [Theory]
        [InlineData(TestExprType.Integer, TestExprType.Integer)]
        [InlineData(TestExprType.Integer, TestExprType.Number)]
        [InlineData(TestExprType.Integer, TestExprType.String)]
        [InlineData(TestExprType.Integer, TestExprType.Boolean)]
        [InlineData(TestExprType.Integer, TestExprType.Time)]
        [InlineData(TestExprType.Integer, TestExprType.Date)]
        [InlineData(TestExprType.Integer, TestExprType.TimePeriod)]
        [InlineData(TestExprType.Number, TestExprType.Integer)]
        [InlineData(TestExprType.Number, TestExprType.Number)]
        [InlineData(TestExprType.Number, TestExprType.String)]
        [InlineData(TestExprType.Number, TestExprType.Boolean)]
        [InlineData(TestExprType.Number, TestExprType.Time)]
        [InlineData(TestExprType.Number, TestExprType.Date)]
        [InlineData(TestExprType.Number, TestExprType.TimePeriod)]
        [InlineData(TestExprType.String, TestExprType.Integer)]
        [InlineData(TestExprType.String, TestExprType.Number)]
        [InlineData(TestExprType.String, TestExprType.String)]
        [InlineData(TestExprType.String, TestExprType.Boolean)]
        [InlineData(TestExprType.String, TestExprType.Time)]
        [InlineData(TestExprType.String, TestExprType.Date)]
        [InlineData(TestExprType.String, TestExprType.TimePeriod)]
        [InlineData(TestExprType.Boolean, TestExprType.Integer)]
        [InlineData(TestExprType.Boolean, TestExprType.Number)]
        [InlineData(TestExprType.Boolean, TestExprType.String)]
        [InlineData(TestExprType.Boolean, TestExprType.Boolean)]
        [InlineData(TestExprType.Boolean, TestExprType.Time)]
        [InlineData(TestExprType.Boolean, TestExprType.Date)]
        [InlineData(TestExprType.Boolean, TestExprType.TimePeriod)]
        [InlineData(TestExprType.Time, TestExprType.Integer)]
        [InlineData(TestExprType.Time, TestExprType.Number)]
        [InlineData(TestExprType.Time, TestExprType.String)]
        [InlineData(TestExprType.Time, TestExprType.Boolean)]
        [InlineData(TestExprType.Time, TestExprType.Time)]
        [InlineData(TestExprType.Time, TestExprType.Date)]
        [InlineData(TestExprType.Time, TestExprType.TimePeriod)]
        [InlineData(TestExprType.Date, TestExprType.Integer)]
        [InlineData(TestExprType.Date, TestExprType.Number)]
        [InlineData(TestExprType.Date, TestExprType.String)]
        [InlineData(TestExprType.Date, TestExprType.Boolean)]
        [InlineData(TestExprType.Date, TestExprType.Time)]
        [InlineData(TestExprType.Date, TestExprType.Date)]
        [InlineData(TestExprType.Date, TestExprType.TimePeriod)]
        [InlineData(TestExprType.TimePeriod, TestExprType.Integer)]
        [InlineData(TestExprType.TimePeriod, TestExprType.Number)]
        [InlineData(TestExprType.TimePeriod, TestExprType.String)]
        [InlineData(TestExprType.TimePeriod, TestExprType.Boolean)]
        [InlineData(TestExprType.TimePeriod, TestExprType.Time)]
        [InlineData(TestExprType.TimePeriod, TestExprType.Date)]
        [InlineData(TestExprType.TimePeriod, TestExprType.TimePeriod)]
        public void GetOutputStructure_Group2ScalarsExpr_DataStructure(params TestExprType[] types)
        {
            IExpression compCreateExpr = this.exprFac.GetExpression("group", ExpressionFactoryNameTarget.OperatorSymbol);
            IExpression idExpr1 = ModelResolvers.ExprResolver();
            IExpression idExpr2 = ModelResolvers.ExprResolver();

            idExpr1.ExpressionText = "Id1";
            idExpr2.ExpressionText = "Id2";
            idExpr1.Structure = ModelResolvers.DsResolver("Id1", ComponentType.Identifier, (BasicDataType)types[0]);
            idExpr2.Structure = ModelResolvers.DsResolver("Id2", ComponentType.Identifier, (BasicDataType)types[1]);
            compCreateExpr.AddOperand("ds_1", idExpr1);
            compCreateExpr.AddOperand("ds_2", idExpr2);

            IJoinExpression joinExpr = ModelResolvers.JoinExprResolver(this.exprFac.GetExpression("join", ExpressionFactoryNameTarget.OperatorSymbol));
            joinExpr.AddOperand("ds_1", compCreateExpr);

            IDataStructure expectedStructure = compCreateExpr.Operands["ds_1"].Structure.GetCopy();
            expectedStructure.Identifiers.Add(compCreateExpr.Operands["ds_2"].Structure.GetCopy().Identifiers[0]);

            IDataStructure dataStructure = compCreateExpr.OperatorDefinition.GetOutputStructure(compCreateExpr);

            Assert.True(expectedStructure.EqualsObj(dataStructure));
        }

        [Theory]
        [InlineData("calc")]
        [InlineData("rename")]
        [InlineData("keep")]
        [InlineData("drop")]
        public void GetOutputStructure_NScalarsExpr_DataStructure(string opSymbol)
        {
            IJoinExpression joinExpr = ModelResolvers.JoinExprResolver(this.exprFac.GetExpression("join", ExpressionFactoryNameTarget.OperatorSymbol));

            for (int i = 3; i <= 5; i++) // very expensive
            {
                TestExprType[][] combinations = Enum.GetValues(typeof(TestExprType)).Cast<TestExprType>().Where(type => (int)type <= 8).GetCombinations(i);

                foreach (TestExprType[] combination in combinations)
                {
                    IExpression compCreateExpr = TestExprFactory.GetExpression(combination);
                    compCreateExpr.OperatorDefinition = this.exprFac.OperatorResolver(opSymbol);
                    joinExpr.AddOperand("ds_1", compCreateExpr);

                    IDataStructure expectedStructure = compCreateExpr.Operands["ds_1"].Structure.GetCopy();
                    for (int k = 2; k <= i; k++)
                    {
                        expectedStructure.AddStructure(compCreateExpr.Operands[$"ds_{k}"].Structure);
                    }

                    IDataStructure dataStructure = compCreateExpr.OperatorDefinition.GetOutputStructure(compCreateExpr);

                    Assert.True(expectedStructure.EqualsObj(dataStructure));
                }
            }
        }

        [Fact]
        public void GetOutputStructure_GroupNScalarsExpr_DataStructure()
        {
            IJoinExpression joinExpr = ModelResolvers.JoinExprResolver(this.exprFac.GetExpression("join", ExpressionFactoryNameTarget.OperatorSymbol));

            for (int i = 3; i <= 5; i++) // very expensive
            {
                TestExprType[][] combinations = Enum.GetValues(typeof(TestExprType)).Cast<TestExprType>().Where(type => (int)type <= 8).GetCombinations(i);

                foreach (TestExprType[] combination in combinations)
                {
                    IExpression compCreateExpr = this.exprFac.GetExpression("group", ExpressionFactoryNameTarget.OperatorSymbol);
                    joinExpr.AddOperand("ds_1", compCreateExpr);

                    IDataStructure expectedStructure = ModelResolvers.DsResolver();
                    for (int k = 2; k <= i; k++)
                    {
                        IExpression idExpr = ModelResolvers.ExprResolver();
                        idExpr.ExpressionText = $"Id{k}";
                        idExpr.Structure = ModelResolvers.DsResolver($"Id{k}", ComponentType.Identifier, (BasicDataType)combination[k - 1]);
                        compCreateExpr.AddOperand($"ds_{k}", idExpr);

                        expectedStructure.AddStructure(compCreateExpr.Operands[$"ds_{k}"].Structure);
                    }

                    IDataStructure dataStructure = compCreateExpr.OperatorDefinition.GetOutputStructure(compCreateExpr);

                    Assert.True(expectedStructure.EqualsObj(dataStructure));
                }
            }
        }             

        [Fact]
        public void GetOutputStructure_GroupNoId_ThrowsException()
        {
            TestExprType[][] wrongCombs = Enum.GetValues(typeof(TestExprType)).Cast<TestExprType>().Where(type => (int)type <= 8).GetCombinations(2);

            IJoinExpression joinExpr = ModelResolvers.JoinExprResolver(this.exprFac.GetExpression("join", ExpressionFactoryNameTarget.OperatorSymbol));

            foreach (TestExprType[] wrongComb in wrongCombs)
            {
                IExpression compCreateExpr = this.exprFac.GetExpression("group", ExpressionFactoryNameTarget.OperatorSymbol);
                IExpression idExpr1 = ModelResolvers.ExprResolver();
                IExpression idExpr2 = ModelResolvers.ExprResolver();

                idExpr1.ExpressionText = "Id1";
                idExpr2.ExpressionText = "Id2";
                idExpr1.Structure = ModelResolvers.DsResolver("Id1", ComponentType.Identifier, (BasicDataType)wrongComb[0]);
                idExpr2.Structure = ModelResolvers.DsResolver("Id2", ComponentType.Measure, (BasicDataType)wrongComb[1]);
                compCreateExpr.AddOperand("ds_1", idExpr1);
                compCreateExpr.AddOperand("ds_2", idExpr2);
                joinExpr.AddOperand("ds_1", compCreateExpr);

                // Debug condition example: wrongComb[0] == TestExprType.Integer && wrongComb[1] == TestExprType.Integer
                Assert.ThrowsAny<VtlOperatorError>(() => { compCreateExpr.OperatorDefinition.GetOutputStructure(compCreateExpr); });
            }
        }
    }
}
