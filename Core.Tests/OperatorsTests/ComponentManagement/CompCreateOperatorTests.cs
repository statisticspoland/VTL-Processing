namespace StatisticsPoland.VtlProcessing.Core.Tests.OperatorsTests.ComponentManagement
{
    using Moq;
    using StatisticsPoland.VtlProcessing.Core.ErrorHandling;
    using StatisticsPoland.VtlProcessing.Core.Infrastructure.DependencyInjection;
    using StatisticsPoland.VtlProcessing.Core.MiddleEnd.Utilities;
    using StatisticsPoland.VtlProcessing.Core.Models;
    using StatisticsPoland.VtlProcessing.Core.Models.Interfaces;
    using StatisticsPoland.VtlProcessing.Core.Models.Types;
    using StatisticsPoland.VtlProcessing.Core.Operators;
    using StatisticsPoland.VtlProcessing.Core.Operators.Auxiliary;
    using StatisticsPoland.VtlProcessing.Core.Tests.Utilities;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Xunit;

    public class CompCreateOperatorTests
    {
        private readonly List<string> _operators;
        private readonly OperatorResolver _opResolver;

        public CompCreateOperatorTests()
        {
            Mock<OperatorResolver> opResolverMock = new Mock<OperatorResolver>();
            opResolverMock.Setup(o => o("calcExpr")).Returns(() => { return new CalcExprOperator(ModelResolvers.DsResolver); });
            opResolverMock.Setup(o => o("comp")).Returns(new ComponentOperator(ModelResolvers.DsResolver, new ComponentTypeInference(ModelResolvers.DsResolver)));
            opResolverMock.Setup(o => o("join")).Returns(new JoinOperator(ModelResolvers.DsResolver));

            this._opResolver = opResolverMock.Object;

            this._operators = new List<string>() { "calcExpr" };
        }

        private StructureComponent[] GetComponentsByCompType(IDataStructure dataStructure, ComponentType compType)
        {
            switch (compType)
            {
                case ComponentType.Identifier: return dataStructure.Identifiers.ToArray();
                case ComponentType.Measure: return dataStructure.Measures.ToArray();
                case ComponentType.NonViralAttribute: return dataStructure.NonViralAttributes.ToArray();
                case ComponentType.ViralAttribute: return dataStructure.ViralAttributes.ToArray();
                default: return null;
            }
        }
        
        private void StartTest(bool arg2_isComp, string opSymbol, ComponentType compType, TestExprType[] dataTypes)
        {
            IExpression expr1 = TestExprFactory.GetExpression(dataTypes[0]);
            IExpression expr2 = TestExprFactory.GetExpression(dataTypes[1]);

            expr1.ExpressionText = expr1.Structure.Components[0].ComponentName = "Component";
            expr1.OperatorDefinition = ModelResolvers.OperatorResolver("comp");

            if (arg2_isComp)
            {
                expr2.ExpressionText = expr2.Structure.Components[0].ComponentName = "Component";
                expr2.OperatorDefinition = ModelResolvers.OperatorResolver("comp");
            }

            IExpression compCreateExpr = TestExprFactory.FoldExpression(expr1, expr2);
            compCreateExpr.OperatorDefinition = ModelResolvers.OperatorResolver(opSymbol);

            compCreateExpr.OperatorDefinition.Keyword = this.GetKeywordByCompType(compType);
            compCreateExpr.Structure = compCreateExpr.OperatorDefinition.GetOutputStructure(compCreateExpr);
            StructureComponent[] components = this.GetComponentsByCompType(compCreateExpr.Structure, compType);

            for (int i = 0; i < compCreateExpr.Structure.Components.Count; i++)
            {
                Assert.True(compCreateExpr.Structure.Components[i].ComponentName == expr1.Structure.Components[i].ComponentName);
            }

            BasicDataType type = expr2.Structure.Components[0].ValueDomain.DataType;
            if (type == BasicDataType.None || (type == BasicDataType.Integer && expr1.Structure.Components[0].ValueDomain.DataType == BasicDataType.Number)) 
                type = expr1.Structure.Components[0].ValueDomain.DataType;

            Assert.Equal(components.Length, expr2.Structure.Components.Count);
            Assert.Equal(components[0].ValueDomain.DataType, type);
        }

        private string GetKeywordByCompType(ComponentType compType)
        {
            switch (compType)
            {
                case ComponentType.Identifier: return "identifier";
                case ComponentType.Measure: return "measure";
                case ComponentType.NonViralAttribute: return "attribute";
                case ComponentType.ViralAttribute: return "viral attribute";
                default: return null;
            }
        }

        [Theory]
        [InlineData(ComponentType.Identifier, TestExprType.None, TestExprType.Integer)]
        [InlineData(ComponentType.Identifier, TestExprType.None, TestExprType.Number)]
        [InlineData(ComponentType.Identifier, TestExprType.None, TestExprType.String)]
        [InlineData(ComponentType.Identifier, TestExprType.None, TestExprType.Boolean)]
        [InlineData(ComponentType.Identifier, TestExprType.None, TestExprType.Time)]
        [InlineData(ComponentType.Identifier, TestExprType.None, TestExprType.Date)]
        [InlineData(ComponentType.Identifier, TestExprType.None, TestExprType.TimePeriod)]
        [InlineData(ComponentType.Identifier, TestExprType.None, TestExprType.Duration)]
        [InlineData(ComponentType.Measure, TestExprType.Integer, TestExprType.Integer)]
        [InlineData(ComponentType.Measure, TestExprType.Integer, TestExprType.Number)]
        [InlineData(ComponentType.Measure, TestExprType.Integer, TestExprType.None)]
        [InlineData(ComponentType.Measure, TestExprType.Number, TestExprType.Number)]
        [InlineData(ComponentType.Measure, TestExprType.Number, TestExprType.Integer)]
        [InlineData(ComponentType.Measure, TestExprType.Number, TestExprType.None)]
        [InlineData(ComponentType.Measure, TestExprType.String, TestExprType.String)]
        [InlineData(ComponentType.Measure, TestExprType.String, TestExprType.None)]
        [InlineData(ComponentType.Measure, TestExprType.Boolean, TestExprType.Boolean)]
        [InlineData(ComponentType.Measure, TestExprType.Boolean, TestExprType.None)]
        [InlineData(ComponentType.Measure, TestExprType.Time, TestExprType.Time)]
        [InlineData(ComponentType.Measure, TestExprType.Time, TestExprType.None)]
        [InlineData(ComponentType.Measure, TestExprType.Date, TestExprType.Date)]
        [InlineData(ComponentType.Measure, TestExprType.Date, TestExprType.None)]
        [InlineData(ComponentType.Measure, TestExprType.TimePeriod, TestExprType.TimePeriod)]
        [InlineData(ComponentType.Measure, TestExprType.TimePeriod, TestExprType.None)]
        [InlineData(ComponentType.Measure, TestExprType.Duration, TestExprType.Duration)]
        [InlineData(ComponentType.Measure, TestExprType.Duration, TestExprType.None)]
        [InlineData(ComponentType.Measure, TestExprType.None, TestExprType.None)]
        [InlineData(ComponentType.Measure, TestExprType.None, TestExprType.Integer)]
        [InlineData(ComponentType.Measure, TestExprType.None, TestExprType.Number)]
        [InlineData(ComponentType.Measure, TestExprType.None, TestExprType.String)]
        [InlineData(ComponentType.Measure, TestExprType.None, TestExprType.Boolean)]
        [InlineData(ComponentType.Measure, TestExprType.None, TestExprType.Time)]
        [InlineData(ComponentType.Measure, TestExprType.None, TestExprType.Date)]
        [InlineData(ComponentType.Measure, TestExprType.None, TestExprType.TimePeriod)]
        [InlineData(ComponentType.Measure, TestExprType.None, TestExprType.Duration)]
        [InlineData(ComponentType.ViralAttribute, TestExprType.Integer, TestExprType.Integer)]
        [InlineData(ComponentType.ViralAttribute, TestExprType.Integer, TestExprType.Number)]
        [InlineData(ComponentType.ViralAttribute, TestExprType.Integer, TestExprType.None)]
        [InlineData(ComponentType.ViralAttribute, TestExprType.Number, TestExprType.Number)]
        [InlineData(ComponentType.ViralAttribute, TestExprType.Number, TestExprType.Integer)]
        [InlineData(ComponentType.ViralAttribute, TestExprType.Number, TestExprType.None)]
        [InlineData(ComponentType.ViralAttribute, TestExprType.String, TestExprType.String)]
        [InlineData(ComponentType.ViralAttribute, TestExprType.String, TestExprType.None)]
        [InlineData(ComponentType.ViralAttribute, TestExprType.Boolean, TestExprType.Boolean)]
        [InlineData(ComponentType.ViralAttribute, TestExprType.Boolean, TestExprType.None)]
        [InlineData(ComponentType.ViralAttribute, TestExprType.Time, TestExprType.Time)]
        [InlineData(ComponentType.ViralAttribute, TestExprType.Time, TestExprType.None)]
        [InlineData(ComponentType.ViralAttribute, TestExprType.Date, TestExprType.Date)]
        [InlineData(ComponentType.ViralAttribute, TestExprType.Date, TestExprType.None)]
        [InlineData(ComponentType.ViralAttribute, TestExprType.TimePeriod, TestExprType.TimePeriod)]
        [InlineData(ComponentType.ViralAttribute, TestExprType.TimePeriod, TestExprType.None)]
        [InlineData(ComponentType.ViralAttribute, TestExprType.Duration, TestExprType.Duration)]
        [InlineData(ComponentType.ViralAttribute, TestExprType.Duration, TestExprType.None)]
        [InlineData(ComponentType.ViralAttribute, TestExprType.None, TestExprType.None)]
        [InlineData(ComponentType.ViralAttribute, TestExprType.None, TestExprType.Integer)]
        [InlineData(ComponentType.ViralAttribute, TestExprType.None, TestExprType.Number)]
        [InlineData(ComponentType.ViralAttribute, TestExprType.None, TestExprType.String)]
        [InlineData(ComponentType.ViralAttribute, TestExprType.None, TestExprType.Boolean)]
        [InlineData(ComponentType.ViralAttribute, TestExprType.None, TestExprType.Time)]
        [InlineData(ComponentType.ViralAttribute, TestExprType.None, TestExprType.Date)]
        [InlineData(ComponentType.ViralAttribute, TestExprType.None, TestExprType.TimePeriod)]
        [InlineData(ComponentType.ViralAttribute, TestExprType.None, TestExprType.Duration)]
        [InlineData(ComponentType.NonViralAttribute, TestExprType.Integer, TestExprType.Integer)]
        [InlineData(ComponentType.NonViralAttribute, TestExprType.Integer, TestExprType.Number)]
        [InlineData(ComponentType.NonViralAttribute, TestExprType.Integer, TestExprType.None)]
        [InlineData(ComponentType.NonViralAttribute, TestExprType.Number, TestExprType.Number)]
        [InlineData(ComponentType.NonViralAttribute, TestExprType.Number, TestExprType.Integer)]
        [InlineData(ComponentType.NonViralAttribute, TestExprType.Number, TestExprType.None)]
        [InlineData(ComponentType.NonViralAttribute, TestExprType.String, TestExprType.String)]
        [InlineData(ComponentType.NonViralAttribute, TestExprType.String, TestExprType.None)]
        [InlineData(ComponentType.NonViralAttribute, TestExprType.Boolean, TestExprType.Boolean)]
        [InlineData(ComponentType.NonViralAttribute, TestExprType.Boolean, TestExprType.None)]
        [InlineData(ComponentType.NonViralAttribute, TestExprType.Time, TestExprType.Time)]
        [InlineData(ComponentType.NonViralAttribute, TestExprType.Time, TestExprType.None)]
        [InlineData(ComponentType.NonViralAttribute, TestExprType.Date, TestExprType.Date)]
        [InlineData(ComponentType.NonViralAttribute, TestExprType.Date, TestExprType.None)]
        [InlineData(ComponentType.NonViralAttribute, TestExprType.TimePeriod, TestExprType.TimePeriod)]
        [InlineData(ComponentType.NonViralAttribute, TestExprType.TimePeriod, TestExprType.None)]
        [InlineData(ComponentType.NonViralAttribute, TestExprType.Duration, TestExprType.Duration)]
        [InlineData(ComponentType.NonViralAttribute, TestExprType.Duration, TestExprType.None)]
        [InlineData(ComponentType.NonViralAttribute, TestExprType.None, TestExprType.None)]
        [InlineData(ComponentType.NonViralAttribute, TestExprType.None, TestExprType.Integer)]
        [InlineData(ComponentType.NonViralAttribute, TestExprType.None, TestExprType.Number)]
        [InlineData(ComponentType.NonViralAttribute, TestExprType.None, TestExprType.String)]
        [InlineData(ComponentType.NonViralAttribute, TestExprType.None, TestExprType.Boolean)]
        [InlineData(ComponentType.NonViralAttribute, TestExprType.None, TestExprType.Time)]
        [InlineData(ComponentType.NonViralAttribute, TestExprType.None, TestExprType.Date)]
        [InlineData(ComponentType.NonViralAttribute, TestExprType.None, TestExprType.TimePeriod)]
        [InlineData(ComponentType.NonViralAttribute, TestExprType.None, TestExprType.Duration)]
        public void GetOutputStructure_CorrectExpr_FirstCompStructure(ComponentType compType, params TestExprType[] dataTypes)
        {
            foreach (string opSymbol in this._operators)
            {
                this.StartTest(true, opSymbol, compType, dataTypes);
                this.StartTest(false, opSymbol, compType, dataTypes);
            }
        }

        [Fact]
        public void GetOutputStructure_IdWrongArgsExpr_ThrowsException()
        {
            TestExprType[][] correctCombs = new TestExprType[][] {
                new TestExprType[] { TestExprType.None, TestExprType.Integer },
                new TestExprType[] { TestExprType.None, TestExprType.Number },
                new TestExprType[] { TestExprType.None, TestExprType.String },
                new TestExprType[] { TestExprType.None, TestExprType.Boolean },
                new TestExprType[] { TestExprType.None, TestExprType.Time },
                new TestExprType[] { TestExprType.None, TestExprType.Date },
                new TestExprType[] { TestExprType.None, TestExprType.TimePeriod },
                new TestExprType[] { TestExprType.None, TestExprType.Duration }
            };

            TestExprType[][] combinations = Enum.GetValues(typeof(TestExprType)).Cast<TestExprType>().GetCombinations(2);
            TestExprType[][] wrongCombs = combinations.Without(correctCombs);

            foreach (string opSymbol in this._operators)
            {
                foreach (TestExprType[] wrongComb in wrongCombs)
                {
                    for (int i = 0; i < 2; i++)
                    {
                        IExpression compCreateExpr = TestExprFactory.GetExpression(wrongComb);
                        compCreateExpr.Operands["ds_1"].Structure.Identifiers.Add(compCreateExpr.Operands["ds_1"].Structure.Measures[0]);
                        compCreateExpr.Operands["ds_1"].Structure.Measures.Clear();

                        if (i == 1)
                        {
                            IExpression expr2 = compCreateExpr.Operands["ds_2"];
                            expr2.ExpressionText = "Me1";
                            expr2.OperatorDefinition = ModelResolvers.OperatorResolver("comp");
                        }

                        compCreateExpr.OperatorDefinition = ModelResolvers.OperatorResolver(opSymbol);
                        compCreateExpr.OperatorDefinition.Keyword = "identifier";

                        // Debug condition example: wrongComb[0] == TestExprType.Integer && wrongComb[1] == TestExprType.Integer
                        Assert.ThrowsAny<VtlOperatorError>(() => { compCreateExpr.OperatorDefinition.GetOutputStructure(compCreateExpr); });
                    }
                }   
            }
        }

        [Fact]
        public void GetOutputStructure_NoIdWrongArgsExpr_ThrowsException()
        {
            string[] keywords = new string[] { "measure", "attribute", "viral attribute" };

            TestExprType[][] correctCombs = new TestExprType[][] {
                new TestExprType[] { TestExprType.Integer, TestExprType.Integer },
                new TestExprType[] { TestExprType.Integer, TestExprType.Number },
                new TestExprType[] { TestExprType.Integer, TestExprType.None },
                new TestExprType[] { TestExprType.Number, TestExprType.Number },
                new TestExprType[] { TestExprType.Number, TestExprType.Integer },
                new TestExprType[] { TestExprType.Number, TestExprType.None },
                new TestExprType[] { TestExprType.String, TestExprType.String },
                new TestExprType[] { TestExprType.String, TestExprType.None },
                new TestExprType[] { TestExprType.Boolean, TestExprType.Boolean },
                new TestExprType[] { TestExprType.Boolean, TestExprType.None },
                new TestExprType[] { TestExprType.Time, TestExprType.Time },
                new TestExprType[] { TestExprType.Time, TestExprType.None },
                new TestExprType[] { TestExprType.Date, TestExprType.Date },
                new TestExprType[] { TestExprType.Date, TestExprType.None },
                new TestExprType[] { TestExprType.TimePeriod, TestExprType.TimePeriod },
                new TestExprType[] { TestExprType.TimePeriod, TestExprType.None },
                new TestExprType[] { TestExprType.Duration, TestExprType.Duration },
                new TestExprType[] { TestExprType.Duration, TestExprType.None },
                new TestExprType[] { TestExprType.None, TestExprType.None },
                new TestExprType[] { TestExprType.None, TestExprType.Integer },
                new TestExprType[] { TestExprType.None, TestExprType.Number },
                new TestExprType[] { TestExprType.None, TestExprType.String },
                new TestExprType[] { TestExprType.None, TestExprType.Boolean },
                new TestExprType[] { TestExprType.None, TestExprType.Time },
                new TestExprType[] { TestExprType.None, TestExprType.Date },
                new TestExprType[] { TestExprType.None, TestExprType.TimePeriod },
                new TestExprType[] { TestExprType.None, TestExprType.Duration }
            };

            TestExprType[][] combinations = Enum.GetValues(typeof(TestExprType)).Cast<TestExprType>().GetCombinations(2);
            TestExprType[][] wrongCombs = combinations.Without(correctCombs);

            foreach (string opSymbol in this._operators)
            {
                foreach (string keyword in keywords)
                {
                    foreach (TestExprType[] wrongComb in wrongCombs)
                    {
                        for (int i = 0; i < 2; i++)
                        {
                            IExpression compCreateExpr = TestExprFactory.GetExpression(wrongComb);
                            if (i == 1)
                            {
                                IExpression expr2 = compCreateExpr.Operands["ds_2"];
                                expr2.ExpressionText = "Me1";
                                expr2.OperatorDefinition = ModelResolvers.OperatorResolver("comp");
                            }

                            compCreateExpr.OperatorDefinition = ModelResolvers.OperatorResolver(opSymbol);
                            compCreateExpr.OperatorDefinition.Keyword = keyword;

                            // Debug condition example: wrongComb[0] == TestExprType.Integer && wrongComb[1] == TestExprType.Integer
                            Assert.ThrowsAny<VtlOperatorError>(() => { compCreateExpr.OperatorDefinition.GetOutputStructure(compCreateExpr); });
                        }
                    }
                }
            }
        }

        [Fact]
        public void GetOutputStructure_WrongOperatorKeyword_ThrowsException()
        {
            foreach (string opSymbol in this._operators)
            {
                IExpression compCreateExpr = TestExprFactory.GetExpression(TestExprType.Integer, TestExprType.Integer);
                compCreateExpr.OperatorDefinition = ModelResolvers.OperatorResolver(opSymbol);
                compCreateExpr.OperatorDefinition.Keyword = "wrong keyword";

                Assert.ThrowsAny<VtlOperatorError>(() => { compCreateExpr.OperatorDefinition.GetOutputStructure(compCreateExpr); });
            }
        }
    }
}