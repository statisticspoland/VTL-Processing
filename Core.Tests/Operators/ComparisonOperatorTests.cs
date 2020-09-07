namespace StatisticsPoland.VtlProcessing.Core.Tests.Operators
{
    using Moq;
    using StatisticsPoland.VtlProcessing.Core.ErrorHandling;
    using StatisticsPoland.VtlProcessing.Core.Infrastructure;
    using StatisticsPoland.VtlProcessing.Core.Infrastructure.DependencyInjection;
    using StatisticsPoland.VtlProcessing.Core.Infrastructure.Interfaces;
    using StatisticsPoland.VtlProcessing.Core.Models;
    using StatisticsPoland.VtlProcessing.Core.Models.Interfaces;
    using StatisticsPoland.VtlProcessing.Core.Models.Types;
    using StatisticsPoland.VtlProcessing.Core.Operators;
    using StatisticsPoland.VtlProcessing.Core.Tests.Utilities;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Xunit;

    public class ComparisonOperatorTests
    {
        private readonly List<string> operators;
        private readonly OperatorResolver opResolver;

        public ComparisonOperatorTests()
        {
            Mock<OperatorResolver> opResolverMock = new Mock<OperatorResolver>();
            Mock<IExpressionFactory> exprFacMock = new Mock<IExpressionFactory>();

            exprFacMock.Setup(o => o.GetExpression(It.IsAny<string>(), It.IsAny<ExpressionFactoryNameTarget>()))
                .Returns((string name, ExpressionFactoryNameTarget field) =>
                {
                    IExpression expr = ModelResolvers.ExprResolver();
                    if (field == ExpressionFactoryNameTarget.ResultName) expr.ResultName = name;
                    else expr.OperatorDefinition = opResolverMock.Object(name);
                    return expr;
                });
            exprFacMock.Setup(o => o.OperatorResolver).Returns(opResolverMock.Object);

            opResolverMock.Setup(o => o("=")).Returns(() => { return new ComparisonOperator(ModelResolvers.DsResolver, "="); });
            opResolverMock.Setup(o => o("<>")).Returns(() => { return new ComparisonOperator(ModelResolvers.DsResolver, "<>"); });
            opResolverMock.Setup(o => o("<")).Returns(() => { return new ComparisonOperator(ModelResolvers.DsResolver, "<"); });
            opResolverMock.Setup(o => o("<=")).Returns(() => { return new ComparisonOperator(ModelResolvers.DsResolver, "<="); });
            opResolverMock.Setup(o => o(">")).Returns(() => { return new ComparisonOperator(ModelResolvers.DsResolver, ">"); });
            opResolverMock.Setup(o => o(">=")).Returns(() => { return new ComparisonOperator(ModelResolvers.DsResolver, ">="); });

            this.opResolver = opResolverMock.Object;

            this.operators = new List<string>() { "=", "<>", "<", "<=", ">", ">=" };
        }

        [Theory]
        [InlineData(TestExprType.Integer, TestExprType.Integer)]
        [InlineData(TestExprType.Integer, TestExprType.Number)]
        [InlineData(TestExprType.Integer, TestExprType.None)]
        [InlineData(TestExprType.Number, TestExprType.Integer)]
        [InlineData(TestExprType.Number, TestExprType.Number)]
        [InlineData(TestExprType.Number, TestExprType.None)]
        [InlineData(TestExprType.String, TestExprType.String)]
        [InlineData(TestExprType.String, TestExprType.None)]
        [InlineData(TestExprType.Boolean, TestExprType.Boolean)]
        [InlineData(TestExprType.Boolean, TestExprType.None)]
        [InlineData(TestExprType.Time, TestExprType.Time)]
        [InlineData(TestExprType.Time, TestExprType.None)]
        [InlineData(TestExprType.Date, TestExprType.Date)]
        [InlineData(TestExprType.Date, TestExprType.None)]
        [InlineData(TestExprType.TimePeriod, TestExprType.TimePeriod)]
        [InlineData(TestExprType.TimePeriod, TestExprType.None)]
        [InlineData(TestExprType.Duration, TestExprType.Duration)]
        [InlineData(TestExprType.Duration, TestExprType.None)]
        [InlineData(TestExprType.None, TestExprType.None)]
        [InlineData(TestExprType.None, TestExprType.Integer)]
        [InlineData(TestExprType.None, TestExprType.Number)]
        [InlineData(TestExprType.None, TestExprType.String)]
        [InlineData(TestExprType.None, TestExprType.Boolean)]
        [InlineData(TestExprType.None, TestExprType.Time)]
        [InlineData(TestExprType.None, TestExprType.Date)]
        [InlineData(TestExprType.None, TestExprType.TimePeriod)]
        [InlineData(TestExprType.None, TestExprType.Duration)]
        public void GetOutputStructure_Correct2ScalarsArgsExpr_BoolScalarStructure(params TestExprType[] types)
        {
            foreach (string opSymbol in this.operators)
            {
                IExpression equalityExpr = TestExprFactory.GetExpression(types);
                equalityExpr.OperatorDefinition = this.opResolver(opSymbol);

                IDataStructure dataStructure = equalityExpr.OperatorDefinition.GetOutputStructure(equalityExpr);

                Assert.True(TestExprFactory.GetExpression(TestExprType.Boolean).Structure.EqualsObj(dataStructure));
            }
        }

        [Theory]
        [InlineData(TestExprType.Integer, TestExprType.IntsDataset)]
        [InlineData(TestExprType.Integer, TestExprType.NumbersDataset)]
        [InlineData(TestExprType.Integer, TestExprType.NonesDataset)]
        [InlineData(TestExprType.Number, TestExprType.IntsDataset)]
        [InlineData(TestExprType.Number, TestExprType.NumbersDataset)]
        [InlineData(TestExprType.Number, TestExprType.NonesDataset)]
        [InlineData(TestExprType.String, TestExprType.StringsDataset)]
        [InlineData(TestExprType.String, TestExprType.NonesDataset)]
        [InlineData(TestExprType.Boolean, TestExprType.BoolsDataset)]
        [InlineData(TestExprType.Boolean, TestExprType.NonesDataset)]
        [InlineData(TestExprType.Time, TestExprType.TimesDataset)]
        [InlineData(TestExprType.Time, TestExprType.NonesDataset)]
        [InlineData(TestExprType.Date, TestExprType.DatesDataset)]
        [InlineData(TestExprType.Date, TestExprType.NonesDataset)]
        [InlineData(TestExprType.TimePeriod, TestExprType.TimePeriodsDataset)]
        [InlineData(TestExprType.TimePeriod, TestExprType.NonesDataset)]
        [InlineData(TestExprType.Duration, TestExprType.DurationsDataset)]
        [InlineData(TestExprType.Duration, TestExprType.NonesDataset)]
        [InlineData(TestExprType.None, TestExprType.NonesDataset)]
        [InlineData(TestExprType.None, TestExprType.IntsDataset)]
        [InlineData(TestExprType.None, TestExprType.NumbersDataset)]
        [InlineData(TestExprType.None, TestExprType.StringsDataset)]
        [InlineData(TestExprType.None, TestExprType.BoolsDataset)]
        [InlineData(TestExprType.None, TestExprType.TimesDataset)]
        [InlineData(TestExprType.None, TestExprType.DatesDataset)]
        [InlineData(TestExprType.None, TestExprType.TimePeriodsDataset)]
        [InlineData(TestExprType.None, TestExprType.DurationsDataset)]
        public void GetOutputStructure_1CorrectScalar1OneMeasureDatasetArgsExpr_OneMeasureBoolStructure(params TestExprType[] types)
        {
            foreach (string opSymbol in this.operators)
            {
                IExpression equalityExpr = TestExprFactory.GetExpression(types);
                equalityExpr.OperatorDefinition = this.opResolver(opSymbol);
                equalityExpr.Operands["ds_2"].Structure.Measures.RemoveAt(1);

                IDataStructure expectedStructure = equalityExpr.Operands["ds_2"].Structure.GetCopy();
                expectedStructure.Measures.Clear();
                expectedStructure.Measures.Add(new StructureComponent(BasicDataType.Boolean, "bool_var"));

                IDataStructure dataStructure = equalityExpr.OperatorDefinition.GetOutputStructure(equalityExpr);

                Assert.True(expectedStructure.EqualsObj(dataStructure));
            }
        }

        [Theory]
        [InlineData(TestExprType.IntsDataset, TestExprType.IntsDataset)]
        [InlineData(TestExprType.IntsDataset, TestExprType.NumbersDataset)]
        [InlineData(TestExprType.IntsDataset, TestExprType.NonesDataset)]
        [InlineData(TestExprType.NumbersDataset, TestExprType.IntsDataset)]
        [InlineData(TestExprType.NumbersDataset, TestExprType.NumbersDataset)]
        [InlineData(TestExprType.NumbersDataset, TestExprType.NonesDataset)]
        [InlineData(TestExprType.StringsDataset, TestExprType.StringsDataset)]
        [InlineData(TestExprType.StringsDataset, TestExprType.NonesDataset)]
        [InlineData(TestExprType.BoolsDataset, TestExprType.BoolsDataset)]
        [InlineData(TestExprType.BoolsDataset, TestExprType.NonesDataset)]
        [InlineData(TestExprType.TimesDataset, TestExprType.TimesDataset)]
        [InlineData(TestExprType.TimesDataset, TestExprType.NonesDataset)]
        [InlineData(TestExprType.DatesDataset, TestExprType.DatesDataset)]
        [InlineData(TestExprType.DatesDataset, TestExprType.NonesDataset)]
        [InlineData(TestExprType.TimePeriodsDataset, TestExprType.TimePeriodsDataset)]
        [InlineData(TestExprType.TimePeriodsDataset, TestExprType.NonesDataset)]
        [InlineData(TestExprType.DurationsDataset, TestExprType.DurationsDataset)]
        [InlineData(TestExprType.DurationsDataset, TestExprType.NonesDataset)]
        [InlineData(TestExprType.NonesDataset, TestExprType.NonesDataset)]
        [InlineData(TestExprType.NonesDataset, TestExprType.IntsDataset)]
        [InlineData(TestExprType.NonesDataset, TestExprType.NumbersDataset)]
        [InlineData(TestExprType.NonesDataset, TestExprType.StringsDataset)]
        [InlineData(TestExprType.NonesDataset, TestExprType.BoolsDataset)]
        [InlineData(TestExprType.NonesDataset, TestExprType.TimesDataset)]
        [InlineData(TestExprType.NonesDataset, TestExprType.DatesDataset)]
        [InlineData(TestExprType.NonesDataset, TestExprType.TimePeriodsDataset)]
        [InlineData(TestExprType.NonesDataset, TestExprType.DurationsDataset)]
        public void GetOutputStructure_2OneMeasureDatasetsArgsExpr_OneMeasureBoolStructure(params TestExprType[] types)
        {
            foreach (string opSymbol in this.operators)
            {
                IExpression equalityExpr = TestExprFactory.GetExpression(types);
                equalityExpr.OperatorDefinition = this.opResolver(opSymbol);
                equalityExpr.Operands["ds_1"].Structure.Measures.RemoveAt(1);
                equalityExpr.Operands["ds_2"].Structure.Measures.RemoveAt(1);

                IDataStructure expectedStructure = equalityExpr.Operands["ds_1"].Structure.GetCopy();
                expectedStructure.Measures.Clear();
                expectedStructure.Measures.Add(new StructureComponent(BasicDataType.Boolean, "bool_var"));

                IDataStructure dataStructure = equalityExpr.OperatorDefinition.GetOutputStructure(equalityExpr);

                Assert.True(expectedStructure.EqualsObj(dataStructure));
            }
        }

        [Theory]
        [InlineData(TestExprType.IntsDataset, TestExprType.Integer)]
        [InlineData(TestExprType.IntsDataset, TestExprType.Number)]
        [InlineData(TestExprType.IntsDataset, TestExprType.None)]
        [InlineData(TestExprType.NumbersDataset, TestExprType.Integer)]
        [InlineData(TestExprType.NumbersDataset, TestExprType.Number)]
        [InlineData(TestExprType.NumbersDataset, TestExprType.None)]
        [InlineData(TestExprType.StringsDataset, TestExprType.String)]
        [InlineData(TestExprType.StringsDataset, TestExprType.None)]
        [InlineData(TestExprType.BoolsDataset, TestExprType.Boolean)]
        [InlineData(TestExprType.BoolsDataset, TestExprType.None)]
        [InlineData(TestExprType.TimesDataset, TestExprType.Time)]
        [InlineData(TestExprType.TimesDataset, TestExprType.None)]
        [InlineData(TestExprType.DatesDataset, TestExprType.Date)]
        [InlineData(TestExprType.DatesDataset, TestExprType.None)]
        [InlineData(TestExprType.TimePeriodsDataset, TestExprType.TimePeriod)]
        [InlineData(TestExprType.TimePeriodsDataset, TestExprType.None)]
        [InlineData(TestExprType.DurationsDataset, TestExprType.Duration)]
        [InlineData(TestExprType.DurationsDataset, TestExprType.None)]
        [InlineData(TestExprType.NonesDataset, TestExprType.None)]
        [InlineData(TestExprType.NonesDataset, TestExprType.Integer)]
        [InlineData(TestExprType.NonesDataset, TestExprType.Number)]
        [InlineData(TestExprType.NonesDataset, TestExprType.String)]
        [InlineData(TestExprType.NonesDataset, TestExprType.Boolean)]
        [InlineData(TestExprType.NonesDataset, TestExprType.Time)]
        [InlineData(TestExprType.NonesDataset, TestExprType.Date)]
        [InlineData(TestExprType.NonesDataset, TestExprType.TimePeriod)]
        [InlineData(TestExprType.NonesDataset, TestExprType.Duration)]
        public void GetOutputStructure_1OneMeasureDataset1CorrectScalarArgsExpr_OneMeasureBoolStructure(params TestExprType[] types)
        {
            foreach (string opSymbol in this.operators)
            {
                IExpression equalityExpr = TestExprFactory.GetExpression(types);
                equalityExpr.OperatorDefinition = this.opResolver(opSymbol);
                equalityExpr.Operands["ds_1"].Structure.Measures.RemoveAt(1);

                IDataStructure expectedStructure = equalityExpr.Operands["ds_1"].Structure.GetCopy();
                expectedStructure.Measures.Clear();
                expectedStructure.Measures.Add(new StructureComponent(BasicDataType.Boolean, "bool_var"));

                IDataStructure dataStructure = equalityExpr.OperatorDefinition.GetOutputStructure(equalityExpr);

                Assert.True(expectedStructure.EqualsObj(dataStructure));
            }
        }

        [Theory]
        [InlineData(TestExprType.Integer, TestExprType.IntsDataset)]
        [InlineData(TestExprType.Integer, TestExprType.NumbersDataset)]
        [InlineData(TestExprType.Integer, TestExprType.NonesDataset)]
        [InlineData(TestExprType.Number, TestExprType.IntsDataset)]
        [InlineData(TestExprType.Number, TestExprType.NumbersDataset)]
        [InlineData(TestExprType.Number, TestExprType.NonesDataset)]
        [InlineData(TestExprType.String, TestExprType.StringsDataset)]
        [InlineData(TestExprType.String, TestExprType.NonesDataset)]
        [InlineData(TestExprType.Boolean, TestExprType.BoolsDataset)]
        [InlineData(TestExprType.Boolean, TestExprType.NonesDataset)]
        [InlineData(TestExprType.Time, TestExprType.TimesDataset)]
        [InlineData(TestExprType.Time, TestExprType.NonesDataset)]
        [InlineData(TestExprType.Date, TestExprType.DatesDataset)]
        [InlineData(TestExprType.Date, TestExprType.NonesDataset)]
        [InlineData(TestExprType.TimePeriod, TestExprType.TimePeriodsDataset)]
        [InlineData(TestExprType.TimePeriod, TestExprType.NonesDataset)]
        [InlineData(TestExprType.Duration, TestExprType.DurationsDataset)]
        [InlineData(TestExprType.Duration, TestExprType.NonesDataset)]
        [InlineData(TestExprType.None, TestExprType.NonesDataset)]
        [InlineData(TestExprType.None, TestExprType.IntsDataset)]
        [InlineData(TestExprType.None, TestExprType.NumbersDataset)]
        [InlineData(TestExprType.None, TestExprType.StringsDataset)]
        [InlineData(TestExprType.None, TestExprType.BoolsDataset)]
        [InlineData(TestExprType.None, TestExprType.TimesDataset)]
        [InlineData(TestExprType.None, TestExprType.DatesDataset)]
        [InlineData(TestExprType.None, TestExprType.TimePeriodsDataset)]
        [InlineData(TestExprType.None, TestExprType.DurationsDataset)]
        public void GetOutputStructure_1CorrectScalar1MultiMeasuresDatasetArgsExpr_ThrowsException(params TestExprType[] types)
        {
            foreach (string opSymbol in this.operators)
            {
                IExpression equalityExpr = TestExprFactory.GetExpression(types);
                equalityExpr.OperatorDefinition = this.opResolver(opSymbol);

                Assert.ThrowsAny<VtlOperatorError>(() => { equalityExpr.OperatorDefinition.GetOutputStructure(equalityExpr); });
            }
        }

        [Theory]
        [InlineData(TestExprType.IntsDataset, TestExprType.IntsDataset)]
        [InlineData(TestExprType.IntsDataset, TestExprType.NumbersDataset)]
        [InlineData(TestExprType.IntsDataset, TestExprType.NonesDataset)]
        [InlineData(TestExprType.NumbersDataset, TestExprType.IntsDataset)]
        [InlineData(TestExprType.NumbersDataset, TestExprType.NumbersDataset)]
        [InlineData(TestExprType.NumbersDataset, TestExprType.NonesDataset)]
        [InlineData(TestExprType.StringsDataset, TestExprType.StringsDataset)]
        [InlineData(TestExprType.StringsDataset, TestExprType.NonesDataset)]
        [InlineData(TestExprType.BoolsDataset, TestExprType.BoolsDataset)]
        [InlineData(TestExprType.BoolsDataset, TestExprType.NonesDataset)]
        [InlineData(TestExprType.TimesDataset, TestExprType.TimesDataset)]
        [InlineData(TestExprType.TimesDataset, TestExprType.NonesDataset)]
        [InlineData(TestExprType.DatesDataset, TestExprType.DatesDataset)]
        [InlineData(TestExprType.DatesDataset, TestExprType.NonesDataset)]
        [InlineData(TestExprType.TimePeriodsDataset, TestExprType.TimePeriodsDataset)]
        [InlineData(TestExprType.TimePeriodsDataset, TestExprType.NonesDataset)]
        [InlineData(TestExprType.DurationsDataset, TestExprType.DurationsDataset)]
        [InlineData(TestExprType.DurationsDataset, TestExprType.NonesDataset)]
        [InlineData(TestExprType.NonesDataset, TestExprType.NonesDataset)]
        [InlineData(TestExprType.NonesDataset, TestExprType.IntsDataset)]
        [InlineData(TestExprType.NonesDataset, TestExprType.NumbersDataset)]
        [InlineData(TestExprType.NonesDataset, TestExprType.StringsDataset)]
        [InlineData(TestExprType.NonesDataset, TestExprType.BoolsDataset)]
        [InlineData(TestExprType.NonesDataset, TestExprType.TimesDataset)]
        [InlineData(TestExprType.NonesDataset, TestExprType.DatesDataset)]
        [InlineData(TestExprType.NonesDataset, TestExprType.TimePeriodsDataset)]
        [InlineData(TestExprType.NonesDataset, TestExprType.DurationsDataset)]
        public void GetOutputStructure_2MultiMeasuresDatasetsArgsExpr_ThrowsException(params TestExprType[] types)
        {
            foreach (string opSymbol in this.operators)
            {
                IExpression equalityExpr = TestExprFactory.GetExpression(types);
                equalityExpr.OperatorDefinition = this.opResolver(opSymbol);

                Assert.ThrowsAny<VtlOperatorError>(() => { equalityExpr.OperatorDefinition.GetOutputStructure(equalityExpr); });
            }
        }

        [Theory]
        [InlineData(TestExprType.IntsDataset, TestExprType.Integer)]
        [InlineData(TestExprType.IntsDataset, TestExprType.Number)]
        [InlineData(TestExprType.IntsDataset, TestExprType.None)]
        [InlineData(TestExprType.NumbersDataset, TestExprType.Integer)]
        [InlineData(TestExprType.NumbersDataset, TestExprType.Number)]
        [InlineData(TestExprType.NumbersDataset, TestExprType.None)]
        [InlineData(TestExprType.StringsDataset, TestExprType.String)]
        [InlineData(TestExprType.StringsDataset, TestExprType.None)]
        [InlineData(TestExprType.BoolsDataset, TestExprType.Boolean)]
        [InlineData(TestExprType.BoolsDataset, TestExprType.None)]
        [InlineData(TestExprType.TimesDataset, TestExprType.Time)]
        [InlineData(TestExprType.TimesDataset, TestExprType.None)]
        [InlineData(TestExprType.DatesDataset, TestExprType.Date)]
        [InlineData(TestExprType.DatesDataset, TestExprType.None)]
        [InlineData(TestExprType.TimePeriodsDataset, TestExprType.TimePeriod)]
        [InlineData(TestExprType.TimePeriodsDataset, TestExprType.None)]
        [InlineData(TestExprType.DurationsDataset, TestExprType.Duration)]
        [InlineData(TestExprType.DurationsDataset, TestExprType.None)]
        [InlineData(TestExprType.NonesDataset, TestExprType.None)]
        [InlineData(TestExprType.NonesDataset, TestExprType.Integer)]
        [InlineData(TestExprType.NonesDataset, TestExprType.Number)]
        [InlineData(TestExprType.NonesDataset, TestExprType.String)]
        [InlineData(TestExprType.NonesDataset, TestExprType.Boolean)]
        [InlineData(TestExprType.NonesDataset, TestExprType.Time)]
        [InlineData(TestExprType.NonesDataset, TestExprType.Date)]
        [InlineData(TestExprType.NonesDataset, TestExprType.TimePeriod)]
        [InlineData(TestExprType.NonesDataset, TestExprType.Duration)]
        public void GetOutputStructure_1MultiMeasuresDataset1CorrectScalarArgsExpr_ThrowsException(params TestExprType[] types)
        {
            foreach (string opSymbol in this.operators)
            {
                IExpression equalityExpr = TestExprFactory.GetExpression(types);
                equalityExpr.OperatorDefinition = this.opResolver(opSymbol);

                Assert.ThrowsAny<VtlOperatorError>(() => { equalityExpr.OperatorDefinition.GetOutputStructure(equalityExpr); });
            }
        }

        [Fact]
        public void GetOutputStructure_WrongArgsExpr_ThrowsException()
        {
            TestExprType[][] correctCombs = new TestExprType[][] {
                new TestExprType[] { TestExprType.Integer, TestExprType.Integer },
                new TestExprType[] { TestExprType.Integer, TestExprType.Number },
                new TestExprType[] { TestExprType.Integer, TestExprType.None },
                new TestExprType[] { TestExprType.Number, TestExprType.Integer },
                new TestExprType[] { TestExprType.Number, TestExprType.Number },
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
                new TestExprType[] { TestExprType.None, TestExprType.Duration },
                new TestExprType[] { TestExprType.Integer, TestExprType.IntsDataset },
                new TestExprType[] { TestExprType.Integer, TestExprType.NumbersDataset },
                new TestExprType[] { TestExprType.Integer, TestExprType.NonesDataset },
                new TestExprType[] { TestExprType.Number, TestExprType.IntsDataset },
                new TestExprType[] { TestExprType.Number, TestExprType.NumbersDataset },
                new TestExprType[] { TestExprType.Number, TestExprType.NonesDataset },
                new TestExprType[] { TestExprType.String, TestExprType.StringsDataset },
                new TestExprType[] { TestExprType.String, TestExprType.NonesDataset },
                new TestExprType[] { TestExprType.Boolean, TestExprType.BoolsDataset },
                new TestExprType[] { TestExprType.Boolean, TestExprType.NonesDataset },
                new TestExprType[] { TestExprType.Time, TestExprType.TimesDataset },
                new TestExprType[] { TestExprType.Time, TestExprType.NonesDataset },
                new TestExprType[] { TestExprType.Date, TestExprType.DatesDataset },
                new TestExprType[] { TestExprType.Date, TestExprType.NonesDataset },
                new TestExprType[] { TestExprType.TimePeriod, TestExprType.TimePeriodsDataset },
                new TestExprType[] { TestExprType.TimePeriod, TestExprType.NonesDataset },
                new TestExprType[] { TestExprType.Duration, TestExprType.DurationsDataset },
                new TestExprType[] { TestExprType.Duration, TestExprType.NonesDataset },
                new TestExprType[] { TestExprType.None, TestExprType.NonesDataset },
                new TestExprType[] { TestExprType.None, TestExprType.IntsDataset },
                new TestExprType[] { TestExprType.None, TestExprType.NumbersDataset },
                new TestExprType[] { TestExprType.None, TestExprType.StringsDataset },
                new TestExprType[] { TestExprType.None, TestExprType.BoolsDataset },
                new TestExprType[] { TestExprType.None, TestExprType.TimesDataset },
                new TestExprType[] { TestExprType.None, TestExprType.DatesDataset },
                new TestExprType[] { TestExprType.None, TestExprType.TimePeriodsDataset },
                new TestExprType[] { TestExprType.None, TestExprType.DurationsDataset },
                new TestExprType[] { TestExprType.IntsDataset, TestExprType.IntsDataset },
                new TestExprType[] { TestExprType.IntsDataset, TestExprType.NumbersDataset },
                new TestExprType[] { TestExprType.IntsDataset, TestExprType.NonesDataset },
                new TestExprType[] { TestExprType.NumbersDataset, TestExprType.IntsDataset },
                new TestExprType[] { TestExprType.NumbersDataset, TestExprType.NumbersDataset },
                new TestExprType[] { TestExprType.NumbersDataset, TestExprType.NonesDataset },
                new TestExprType[] { TestExprType.StringsDataset, TestExprType.StringsDataset },
                new TestExprType[] { TestExprType.StringsDataset, TestExprType.NonesDataset },
                new TestExprType[] { TestExprType.BoolsDataset, TestExprType.BoolsDataset },
                new TestExprType[] { TestExprType.BoolsDataset, TestExprType.NonesDataset },
                new TestExprType[] { TestExprType.TimesDataset, TestExprType.TimesDataset },
                new TestExprType[] { TestExprType.TimesDataset, TestExprType.NonesDataset },
                new TestExprType[] { TestExprType.DatesDataset, TestExprType.DatesDataset },
                new TestExprType[] { TestExprType.DatesDataset, TestExprType.NonesDataset },
                new TestExprType[] { TestExprType.TimePeriodsDataset, TestExprType.TimePeriodsDataset },
                new TestExprType[] { TestExprType.TimePeriodsDataset, TestExprType.NonesDataset },
                new TestExprType[] { TestExprType.DurationsDataset, TestExprType.DurationsDataset },
                new TestExprType[] { TestExprType.DurationsDataset, TestExprType.NonesDataset },
                new TestExprType[] { TestExprType.NonesDataset, TestExprType.NonesDataset },
                new TestExprType[] { TestExprType.NonesDataset, TestExprType.IntsDataset },
                new TestExprType[] { TestExprType.NonesDataset, TestExprType.NumbersDataset },
                new TestExprType[] { TestExprType.NonesDataset, TestExprType.StringsDataset },
                new TestExprType[] { TestExprType.NonesDataset, TestExprType.BoolsDataset },
                new TestExprType[] { TestExprType.NonesDataset, TestExprType.TimesDataset },
                new TestExprType[] { TestExprType.NonesDataset, TestExprType.DatesDataset },
                new TestExprType[] { TestExprType.NonesDataset, TestExprType.TimePeriodsDataset },
                new TestExprType[] { TestExprType.NonesDataset, TestExprType.DurationsDataset },
                new TestExprType[] { TestExprType.IntsDataset, TestExprType.Integer },
                new TestExprType[] { TestExprType.IntsDataset, TestExprType.Number },
                new TestExprType[] { TestExprType.IntsDataset, TestExprType.None },
                new TestExprType[] { TestExprType.NumbersDataset, TestExprType.Integer },
                new TestExprType[] { TestExprType.NumbersDataset, TestExprType.Number },
                new TestExprType[] { TestExprType.NumbersDataset, TestExprType.None },
                new TestExprType[] { TestExprType.StringsDataset, TestExprType.String },
                new TestExprType[] { TestExprType.StringsDataset, TestExprType.None },
                new TestExprType[] { TestExprType.BoolsDataset, TestExprType.Boolean },
                new TestExprType[] { TestExprType.BoolsDataset, TestExprType.None },
                new TestExprType[] { TestExprType.TimesDataset, TestExprType.Time },
                new TestExprType[] { TestExprType.TimesDataset, TestExprType.None },
                new TestExprType[] { TestExprType.DatesDataset, TestExprType.Date },
                new TestExprType[] { TestExprType.DatesDataset, TestExprType.None },
                new TestExprType[] { TestExprType.TimePeriodsDataset, TestExprType.TimePeriod },
                new TestExprType[] { TestExprType.TimePeriodsDataset, TestExprType.None },
                new TestExprType[] { TestExprType.DurationsDataset, TestExprType.Duration },
                new TestExprType[] { TestExprType.DurationsDataset, TestExprType.None },
                new TestExprType[] { TestExprType.NonesDataset, TestExprType.None },
                new TestExprType[] { TestExprType.NonesDataset, TestExprType.Integer },
                new TestExprType[] { TestExprType.NonesDataset, TestExprType.Number },
                new TestExprType[] { TestExprType.NonesDataset, TestExprType.String },
                new TestExprType[] { TestExprType.NonesDataset, TestExprType.Boolean },
                new TestExprType[] { TestExprType.NonesDataset, TestExprType.Time },
                new TestExprType[] { TestExprType.NonesDataset, TestExprType.Date },
                new TestExprType[] { TestExprType.NonesDataset, TestExprType.TimePeriod },
                new TestExprType[] { TestExprType.NonesDataset, TestExprType.Duration }
            };                       

            TestExprType[][] combinations = Enum.GetValues(typeof(TestExprType)).Cast<TestExprType>().GetCombinations(2);
            TestExprType[][] wrongCombs = combinations.Without(correctCombs);

            foreach (string opSymbol in this.operators)
            {
                foreach (TestExprType[] wrongComb in wrongCombs.Where(wrongComb => (int)wrongComb[0] < 18 && (int)wrongComb[1] < 18)) // No mixed datasets
                {
                    IExpression equalityExpr = TestExprFactory.GetExpression(wrongComb);
                    equalityExpr.OperatorDefinition = this.opResolver(opSymbol);

                    if (equalityExpr.Operands["ds_1"].Structure.Measures.Count > 1)
                        equalityExpr.Operands["ds_1"].Structure.Measures.RemoveAt(1);

                    if (equalityExpr.Operands["ds_2"].Structure.Measures.Count > 1)
                        equalityExpr.Operands["ds_2"].Structure.Measures.RemoveAt(1);

                    // Debug condition example: wrongComb[0] == TestExprType.Integer && wrongComb[1] == TestExprType.Integer
                    Assert.ThrowsAny<VtlOperatorError>(() => { equalityExpr.OperatorDefinition.GetOutputStructure(equalityExpr); });
                }
            }
        }
    }
}
