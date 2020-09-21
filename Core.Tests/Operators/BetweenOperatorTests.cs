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
    using StatisticsPoland.VtlProcessing.Core.Operators.Auxiliary;
    using StatisticsPoland.VtlProcessing.Core.Operators.Interfaces;
    using StatisticsPoland.VtlProcessing.Core.Tests.Utilities;
    using System;
    using System.Linq;
    using Xunit;

    public class BetweenOperatorTests
    {
        private readonly OperatorResolver opResolver;

        public BetweenOperatorTests()
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

            IJoinApplyMeasuresOperator joinApplyMeasuresOp = new JoinApplyMeasuresOperator(
                exprFacMock.Object,
                ModelResolvers.DsResolver);

            opResolverMock.Setup(o => o("between")).Returns(() => { return new BetweenOperator(joinApplyMeasuresOp, ModelResolvers.DsResolver); });

            this.opResolver = opResolverMock.Object;
        }

        [Theory]
        [InlineData(TestExprType.Integer, TestExprType.Integer, TestExprType.Integer)]
        [InlineData(TestExprType.Integer, TestExprType.Integer, TestExprType.None)]
        [InlineData(TestExprType.Integer, TestExprType.None, TestExprType.Integer)]
        [InlineData(TestExprType.Integer, TestExprType.Number, TestExprType.Number)]
        [InlineData(TestExprType.Integer, TestExprType.Number, TestExprType.None)]
        [InlineData(TestExprType.Integer, TestExprType.None, TestExprType.Number)]
        [InlineData(TestExprType.Integer, TestExprType.None, TestExprType.None)]
        [InlineData(TestExprType.Integer, TestExprType.Integer, TestExprType.Number)]
        [InlineData(TestExprType.Integer, TestExprType.Number, TestExprType.Integer)]
        [InlineData(TestExprType.Number, TestExprType.Number, TestExprType.Number)]
        [InlineData(TestExprType.Number, TestExprType.Number, TestExprType.None)]
        [InlineData(TestExprType.Number, TestExprType.None, TestExprType.Number)]
        [InlineData(TestExprType.Number, TestExprType.Integer, TestExprType.Integer)]
        [InlineData(TestExprType.Number, TestExprType.Integer, TestExprType.None)]
        [InlineData(TestExprType.Number, TestExprType.None, TestExprType.Integer)]
        [InlineData(TestExprType.Number, TestExprType.None, TestExprType.None)]
        [InlineData(TestExprType.Number, TestExprType.Number, TestExprType.Integer)]
        [InlineData(TestExprType.Number, TestExprType.Integer, TestExprType.Number)]
        [InlineData(TestExprType.String, TestExprType.String, TestExprType.String)]
        [InlineData(TestExprType.String, TestExprType.String, TestExprType.None)]
        [InlineData(TestExprType.String, TestExprType.None, TestExprType.String)]
        [InlineData(TestExprType.String, TestExprType.None, TestExprType.None)]
        [InlineData(TestExprType.Boolean, TestExprType.Boolean, TestExprType.Boolean)]
        [InlineData(TestExprType.Boolean, TestExprType.Boolean, TestExprType.None)]
        [InlineData(TestExprType.Boolean, TestExprType.None, TestExprType.Boolean)]
        [InlineData(TestExprType.Boolean, TestExprType.None, TestExprType.None)]
        [InlineData(TestExprType.Time, TestExprType.Time, TestExprType.Time)]
        [InlineData(TestExprType.Time, TestExprType.Time, TestExprType.None)]
        [InlineData(TestExprType.Time, TestExprType.None, TestExprType.Time)]
        [InlineData(TestExprType.Time, TestExprType.None, TestExprType.None)]
        [InlineData(TestExprType.Date, TestExprType.Date, TestExprType.Date)]
        [InlineData(TestExprType.Date, TestExprType.Date, TestExprType.None)]
        [InlineData(TestExprType.Date, TestExprType.None, TestExprType.Date)]
        [InlineData(TestExprType.Date, TestExprType.None, TestExprType.None)]
        [InlineData(TestExprType.TimePeriod, TestExprType.TimePeriod, TestExprType.TimePeriod)]
        [InlineData(TestExprType.TimePeriod, TestExprType.TimePeriod, TestExprType.None)]
        [InlineData(TestExprType.TimePeriod, TestExprType.None, TestExprType.TimePeriod)]
        [InlineData(TestExprType.TimePeriod, TestExprType.None, TestExprType.None)]
        [InlineData(TestExprType.Duration, TestExprType.Duration, TestExprType.Duration)]
        [InlineData(TestExprType.Duration, TestExprType.Duration, TestExprType.None)]
        [InlineData(TestExprType.Duration, TestExprType.None, TestExprType.Duration)]
        [InlineData(TestExprType.Duration, TestExprType.None, TestExprType.None)]
        [InlineData(TestExprType.None, TestExprType.None, TestExprType.None)]
        [InlineData(TestExprType.None, TestExprType.None, TestExprType.Integer)]
        [InlineData(TestExprType.None, TestExprType.None, TestExprType.Number)]
        [InlineData(TestExprType.None, TestExprType.None, TestExprType.String)]
        [InlineData(TestExprType.None, TestExprType.None, TestExprType.Boolean)]
        [InlineData(TestExprType.None, TestExprType.None, TestExprType.Time)]
        [InlineData(TestExprType.None, TestExprType.None, TestExprType.Date)]
        [InlineData(TestExprType.None, TestExprType.None, TestExprType.TimePeriod)]
        [InlineData(TestExprType.None, TestExprType.None, TestExprType.Duration)]
        [InlineData(TestExprType.None, TestExprType.Integer, TestExprType.None)]
        [InlineData(TestExprType.None, TestExprType.Number, TestExprType.None)]
        [InlineData(TestExprType.None, TestExprType.String, TestExprType.None)]
        [InlineData(TestExprType.None, TestExprType.Boolean, TestExprType.None)]
        [InlineData(TestExprType.None, TestExprType.Time, TestExprType.None)]
        [InlineData(TestExprType.None, TestExprType.Date, TestExprType.None)]
        [InlineData(TestExprType.None, TestExprType.TimePeriod, TestExprType.None)]
        [InlineData(TestExprType.None, TestExprType.Duration, TestExprType.None)]
        [InlineData(TestExprType.None, TestExprType.Integer, TestExprType.Integer)]
        [InlineData(TestExprType.None, TestExprType.Integer, TestExprType.Number)]
        [InlineData(TestExprType.None, TestExprType.Number, TestExprType.Number)]
        [InlineData(TestExprType.None, TestExprType.Number, TestExprType.Integer)]
        [InlineData(TestExprType.None, TestExprType.String, TestExprType.String)]
        [InlineData(TestExprType.None, TestExprType.Boolean, TestExprType.Boolean)]
        [InlineData(TestExprType.None, TestExprType.Time, TestExprType.Time)]
        [InlineData(TestExprType.None, TestExprType.Date, TestExprType.Date)]
        [InlineData(TestExprType.None, TestExprType.TimePeriod, TestExprType.TimePeriod)]
        [InlineData(TestExprType.None, TestExprType.Duration, TestExprType.Duration)]
        public void GetOutputStructure_Correct3ScalarsArgsExpr_BoolScalarStructure(params TestExprType[] types)
        {
            IExpression betweenExpr = TestExprFactory.GetExpression(types);
            betweenExpr.OperatorDefinition = this.opResolver("between");

            IDataStructure dataStructure = betweenExpr.OperatorDefinition.GetOutputStructure(betweenExpr);

            Assert.Equal(BasicDataType.Boolean, dataStructure.Components.First().ValueDomain.DataType);
        }

        [Theory]
        [InlineData(TestExprType.IntsDataset, TestExprType.Integer, TestExprType.Integer)]
        [InlineData(TestExprType.IntsDataset, TestExprType.Integer, TestExprType.None)]
        [InlineData(TestExprType.IntsDataset, TestExprType.None, TestExprType.Integer)]
        [InlineData(TestExprType.IntsDataset, TestExprType.Number, TestExprType.Number)]
        [InlineData(TestExprType.IntsDataset, TestExprType.Number, TestExprType.None)]
        [InlineData(TestExprType.IntsDataset, TestExprType.None, TestExprType.Number)]
        [InlineData(TestExprType.IntsDataset, TestExprType.None, TestExprType.None)]
        [InlineData(TestExprType.IntsDataset, TestExprType.Integer, TestExprType.Number)]
        [InlineData(TestExprType.IntsDataset, TestExprType.Number, TestExprType.Integer)]
        [InlineData(TestExprType.NumbersDataset, TestExprType.Number, TestExprType.Number)]
        [InlineData(TestExprType.NumbersDataset, TestExprType.Number, TestExprType.None)]
        [InlineData(TestExprType.NumbersDataset, TestExprType.None, TestExprType.Number)]
        [InlineData(TestExprType.NumbersDataset, TestExprType.Integer, TestExprType.Integer)]
        [InlineData(TestExprType.NumbersDataset, TestExprType.Integer, TestExprType.None)]
        [InlineData(TestExprType.NumbersDataset, TestExprType.None, TestExprType.Integer)]
        [InlineData(TestExprType.NumbersDataset, TestExprType.None, TestExprType.None)]
        [InlineData(TestExprType.NumbersDataset, TestExprType.Number, TestExprType.Integer)]
        [InlineData(TestExprType.NumbersDataset, TestExprType.Integer, TestExprType.Number)]
        [InlineData(TestExprType.StringsDataset, TestExprType.String, TestExprType.String)]
        [InlineData(TestExprType.StringsDataset, TestExprType.String, TestExprType.None)]
        [InlineData(TestExprType.StringsDataset, TestExprType.None, TestExprType.String)]
        [InlineData(TestExprType.StringsDataset, TestExprType.None, TestExprType.None)]
        [InlineData(TestExprType.BoolsDataset, TestExprType.Boolean, TestExprType.Boolean)]
        [InlineData(TestExprType.BoolsDataset, TestExprType.Boolean, TestExprType.None)]
        [InlineData(TestExprType.BoolsDataset, TestExprType.None, TestExprType.Boolean)]
        [InlineData(TestExprType.BoolsDataset, TestExprType.None, TestExprType.None)]
        [InlineData(TestExprType.TimesDataset, TestExprType.Time, TestExprType.Time)]
        [InlineData(TestExprType.TimesDataset, TestExprType.Time, TestExprType.None)]
        [InlineData(TestExprType.TimesDataset, TestExprType.None, TestExprType.Time)]
        [InlineData(TestExprType.TimesDataset, TestExprType.None, TestExprType.None)]
        [InlineData(TestExprType.DatesDataset, TestExprType.Date, TestExprType.Date)]
        [InlineData(TestExprType.DatesDataset, TestExprType.Date, TestExprType.None)]
        [InlineData(TestExprType.DatesDataset, TestExprType.None, TestExprType.Date)]
        [InlineData(TestExprType.DatesDataset, TestExprType.None, TestExprType.None)]
        [InlineData(TestExprType.TimePeriodsDataset, TestExprType.TimePeriod, TestExprType.TimePeriod)]
        [InlineData(TestExprType.TimePeriodsDataset, TestExprType.TimePeriod, TestExprType.None)]
        [InlineData(TestExprType.TimePeriodsDataset, TestExprType.None, TestExprType.TimePeriod)]
        [InlineData(TestExprType.TimePeriodsDataset, TestExprType.None, TestExprType.None)]
        [InlineData(TestExprType.DurationsDataset, TestExprType.Duration, TestExprType.Duration)]
        [InlineData(TestExprType.DurationsDataset, TestExprType.Duration, TestExprType.None)]
        [InlineData(TestExprType.DurationsDataset, TestExprType.None, TestExprType.Duration)]
        [InlineData(TestExprType.DurationsDataset, TestExprType.None, TestExprType.None)]
        [InlineData(TestExprType.NonesDataset, TestExprType.None, TestExprType.None)]
        [InlineData(TestExprType.NonesDataset, TestExprType.None, TestExprType.Integer)]
        [InlineData(TestExprType.NonesDataset, TestExprType.None, TestExprType.Number)]
        [InlineData(TestExprType.NonesDataset, TestExprType.None, TestExprType.String)]
        [InlineData(TestExprType.NonesDataset, TestExprType.None, TestExprType.Boolean)]
        [InlineData(TestExprType.NonesDataset, TestExprType.None, TestExprType.Time)]
        [InlineData(TestExprType.NonesDataset, TestExprType.None, TestExprType.Date)]
        [InlineData(TestExprType.NonesDataset, TestExprType.None, TestExprType.TimePeriod)]
        [InlineData(TestExprType.NonesDataset, TestExprType.None, TestExprType.Duration)]
        [InlineData(TestExprType.NonesDataset, TestExprType.Integer, TestExprType.None)]
        [InlineData(TestExprType.NonesDataset, TestExprType.Number, TestExprType.None)]
        [InlineData(TestExprType.NonesDataset, TestExprType.String, TestExprType.None)]
        [InlineData(TestExprType.NonesDataset, TestExprType.Boolean, TestExprType.None)]
        [InlineData(TestExprType.NonesDataset, TestExprType.Time, TestExprType.None)]
        [InlineData(TestExprType.NonesDataset, TestExprType.Date, TestExprType.None)]
        [InlineData(TestExprType.NonesDataset, TestExprType.TimePeriod, TestExprType.None)]
        [InlineData(TestExprType.NonesDataset, TestExprType.Duration, TestExprType.None)]
        [InlineData(TestExprType.NonesDataset, TestExprType.Integer, TestExprType.Integer)]
        [InlineData(TestExprType.NonesDataset, TestExprType.Integer, TestExprType.Number)]
        [InlineData(TestExprType.NonesDataset, TestExprType.Number, TestExprType.Number)]
        [InlineData(TestExprType.NonesDataset, TestExprType.Number, TestExprType.Integer)]
        [InlineData(TestExprType.NonesDataset, TestExprType.String, TestExprType.String)]
        [InlineData(TestExprType.NonesDataset, TestExprType.Boolean, TestExprType.Boolean)]
        [InlineData(TestExprType.NonesDataset, TestExprType.Time, TestExprType.Time)]
        [InlineData(TestExprType.NonesDataset, TestExprType.Date, TestExprType.Date)]
        [InlineData(TestExprType.NonesDataset, TestExprType.TimePeriod, TestExprType.TimePeriod)]
        [InlineData(TestExprType.NonesDataset, TestExprType.Duration, TestExprType.Duration)]
        public void GetOutputStructure_1OneMeasureDataset2CorrectScalarsArgsExpr_OneMeasureBoolStructure(params TestExprType[] types)
        {
            IExpression betweenExpr = TestExprFactory.GetExpression(types);
            betweenExpr.OperatorDefinition = this.opResolver("between");
            betweenExpr.Operands["ds_1"].Structure.Measures.RemoveAt(1);

            IDataStructure expectedStructure = betweenExpr.Operands["ds_1"].Structure.GetCopy();
            expectedStructure.Measures.Clear();
            expectedStructure.Measures.Add(new StructureComponent(BasicDataType.Boolean, "bool_var"));

            IDataStructure dataStructure = betweenExpr.OperatorDefinition.GetOutputStructure(betweenExpr);

            Assert.True(expectedStructure.EqualsObj(dataStructure));
        }

        [Theory]
        [InlineData(TestExprType.IntsDataset, TestExprType.Integer, TestExprType.Integer)]
        [InlineData(TestExprType.IntsDataset, TestExprType.Integer, TestExprType.None)]
        [InlineData(TestExprType.IntsDataset, TestExprType.None, TestExprType.Integer)]
        [InlineData(TestExprType.IntsDataset, TestExprType.Number, TestExprType.Number)]
        [InlineData(TestExprType.IntsDataset, TestExprType.Number, TestExprType.None)]
        [InlineData(TestExprType.IntsDataset, TestExprType.None, TestExprType.Number)]
        [InlineData(TestExprType.IntsDataset, TestExprType.None, TestExprType.None)]
        [InlineData(TestExprType.IntsDataset, TestExprType.Integer, TestExprType.Number)]
        [InlineData(TestExprType.IntsDataset, TestExprType.Number, TestExprType.Integer)]
        [InlineData(TestExprType.NumbersDataset, TestExprType.Number, TestExprType.Number)]
        [InlineData(TestExprType.NumbersDataset, TestExprType.Number, TestExprType.None)]
        [InlineData(TestExprType.NumbersDataset, TestExprType.None, TestExprType.Number)]
        [InlineData(TestExprType.NumbersDataset, TestExprType.Integer, TestExprType.Integer)]
        [InlineData(TestExprType.NumbersDataset, TestExprType.Integer, TestExprType.None)]
        [InlineData(TestExprType.NumbersDataset, TestExprType.None, TestExprType.Integer)]
        [InlineData(TestExprType.NumbersDataset, TestExprType.None, TestExprType.None)]
        [InlineData(TestExprType.NumbersDataset, TestExprType.Number, TestExprType.Integer)]
        [InlineData(TestExprType.NumbersDataset, TestExprType.Integer, TestExprType.Number)]
        [InlineData(TestExprType.StringsDataset, TestExprType.String, TestExprType.String)]
        [InlineData(TestExprType.StringsDataset, TestExprType.String, TestExprType.None)]
        [InlineData(TestExprType.StringsDataset, TestExprType.None, TestExprType.String)]
        [InlineData(TestExprType.StringsDataset, TestExprType.None, TestExprType.None)]
        [InlineData(TestExprType.BoolsDataset, TestExprType.Boolean, TestExprType.Boolean)]
        [InlineData(TestExprType.BoolsDataset, TestExprType.Boolean, TestExprType.None)]
        [InlineData(TestExprType.BoolsDataset, TestExprType.None, TestExprType.Boolean)]
        [InlineData(TestExprType.BoolsDataset, TestExprType.None, TestExprType.None)]
        [InlineData(TestExprType.TimesDataset, TestExprType.Time, TestExprType.Time)]
        [InlineData(TestExprType.TimesDataset, TestExprType.Time, TestExprType.None)]
        [InlineData(TestExprType.TimesDataset, TestExprType.None, TestExprType.Time)]
        [InlineData(TestExprType.TimesDataset, TestExprType.None, TestExprType.None)]
        [InlineData(TestExprType.DatesDataset, TestExprType.Date, TestExprType.Date)]
        [InlineData(TestExprType.DatesDataset, TestExprType.Date, TestExprType.None)]
        [InlineData(TestExprType.DatesDataset, TestExprType.None, TestExprType.Date)]
        [InlineData(TestExprType.DatesDataset, TestExprType.None, TestExprType.None)]
        [InlineData(TestExprType.TimePeriodsDataset, TestExprType.TimePeriod, TestExprType.TimePeriod)]
        [InlineData(TestExprType.TimePeriodsDataset, TestExprType.TimePeriod, TestExprType.None)]
        [InlineData(TestExprType.TimePeriodsDataset, TestExprType.None, TestExprType.TimePeriod)]
        [InlineData(TestExprType.TimePeriodsDataset, TestExprType.None, TestExprType.None)]
        [InlineData(TestExprType.DurationsDataset, TestExprType.Duration, TestExprType.Duration)]
        [InlineData(TestExprType.DurationsDataset, TestExprType.Duration, TestExprType.None)]
        [InlineData(TestExprType.DurationsDataset, TestExprType.None, TestExprType.Duration)]
        [InlineData(TestExprType.DurationsDataset, TestExprType.None, TestExprType.None)]
        [InlineData(TestExprType.NonesDataset, TestExprType.None, TestExprType.None)]
        [InlineData(TestExprType.NonesDataset, TestExprType.None, TestExprType.Integer)]
        [InlineData(TestExprType.NonesDataset, TestExprType.None, TestExprType.Number)]
        [InlineData(TestExprType.NonesDataset, TestExprType.None, TestExprType.String)]
        [InlineData(TestExprType.NonesDataset, TestExprType.None, TestExprType.Boolean)]
        [InlineData(TestExprType.NonesDataset, TestExprType.None, TestExprType.Time)]
        [InlineData(TestExprType.NonesDataset, TestExprType.None, TestExprType.Date)]
        [InlineData(TestExprType.NonesDataset, TestExprType.None, TestExprType.TimePeriod)]
        [InlineData(TestExprType.NonesDataset, TestExprType.None, TestExprType.Duration)]
        [InlineData(TestExprType.NonesDataset, TestExprType.Integer, TestExprType.None)]
        [InlineData(TestExprType.NonesDataset, TestExprType.Number, TestExprType.None)]
        [InlineData(TestExprType.NonesDataset, TestExprType.String, TestExprType.None)]
        [InlineData(TestExprType.NonesDataset, TestExprType.Boolean, TestExprType.None)]
        [InlineData(TestExprType.NonesDataset, TestExprType.Time, TestExprType.None)]
        [InlineData(TestExprType.NonesDataset, TestExprType.Date, TestExprType.None)]
        [InlineData(TestExprType.NonesDataset, TestExprType.TimePeriod, TestExprType.None)]
        [InlineData(TestExprType.NonesDataset, TestExprType.Duration, TestExprType.None)]
        [InlineData(TestExprType.NonesDataset, TestExprType.Integer, TestExprType.Integer)]
        [InlineData(TestExprType.NonesDataset, TestExprType.Integer, TestExprType.Number)]
        [InlineData(TestExprType.NonesDataset, TestExprType.Number, TestExprType.Number)]
        [InlineData(TestExprType.NonesDataset, TestExprType.Number, TestExprType.Integer)]
        [InlineData(TestExprType.NonesDataset, TestExprType.String, TestExprType.String)]
        [InlineData(TestExprType.NonesDataset, TestExprType.Boolean, TestExprType.Boolean)]
        [InlineData(TestExprType.NonesDataset, TestExprType.Time, TestExprType.Time)]
        [InlineData(TestExprType.NonesDataset, TestExprType.Date, TestExprType.Date)]
        [InlineData(TestExprType.NonesDataset, TestExprType.TimePeriod, TestExprType.TimePeriod)]
        [InlineData(TestExprType.NonesDataset, TestExprType.Duration, TestExprType.Duration)]
        public void GetOutputStructure_1MultiMeasuresDataset2CorrectScalarsArgsExpr_ThrowsException(params TestExprType[] types)
        {
            IExpression betweenExpr = TestExprFactory.GetExpression(types);
            betweenExpr.OperatorDefinition = this.opResolver("between");

            Assert.ThrowsAny<VtlOperatorError>(() => { betweenExpr.OperatorDefinition.GetOutputStructure(betweenExpr); });
        }

        [Fact]
        public void GetOutputStructure_WrongArgsExpr_ThrowsException()
        {
            TestExprType[][] correctCombs = new TestExprType[][] {
                new TestExprType[] { TestExprType.Integer, TestExprType.Integer, TestExprType.Integer },
                new TestExprType[] { TestExprType.Integer, TestExprType.Integer, TestExprType.None },
                new TestExprType[] { TestExprType.Integer, TestExprType.None, TestExprType.Integer },
                new TestExprType[] { TestExprType.Integer, TestExprType.Number, TestExprType.Number },
                new TestExprType[] { TestExprType.Integer, TestExprType.Number, TestExprType.None },
                new TestExprType[] { TestExprType.Integer, TestExprType.None, TestExprType.Number },
                new TestExprType[] { TestExprType.Integer, TestExprType.None, TestExprType.None },
                new TestExprType[] { TestExprType.Integer, TestExprType.Integer, TestExprType.Number },
                new TestExprType[] { TestExprType.Integer, TestExprType.Number, TestExprType.Integer },
                new TestExprType[] { TestExprType.Number, TestExprType.Number, TestExprType.Number },
                new TestExprType[] { TestExprType.Number, TestExprType.Number, TestExprType.None },
                new TestExprType[] { TestExprType.Number, TestExprType.None, TestExprType.Number },
                new TestExprType[] { TestExprType.Number, TestExprType.Integer, TestExprType.Integer },
                new TestExprType[] { TestExprType.Number, TestExprType.Integer, TestExprType.None },
                new TestExprType[] { TestExprType.Number, TestExprType.None, TestExprType.Integer },
                new TestExprType[] { TestExprType.Number, TestExprType.None, TestExprType.None },
                new TestExprType[] { TestExprType.Number, TestExprType.Number, TestExprType.Integer },
                new TestExprType[] { TestExprType.Number, TestExprType.Integer, TestExprType.Number },
                new TestExprType[] { TestExprType.String, TestExprType.String, TestExprType.String },
                new TestExprType[] { TestExprType.String, TestExprType.String, TestExprType.None },
                new TestExprType[] { TestExprType.String, TestExprType.None, TestExprType.String },
                new TestExprType[] { TestExprType.String, TestExprType.None, TestExprType.None },
                new TestExprType[] { TestExprType.Boolean, TestExprType.Boolean, TestExprType.Boolean },
                new TestExprType[] { TestExprType.Boolean, TestExprType.Boolean, TestExprType.None },
                new TestExprType[] { TestExprType.Boolean, TestExprType.None, TestExprType.Boolean },
                new TestExprType[] { TestExprType.Boolean, TestExprType.None, TestExprType.None },
                new TestExprType[] { TestExprType.Time, TestExprType.Time, TestExprType.Time },
                new TestExprType[] { TestExprType.Time, TestExprType.Time, TestExprType.None },
                new TestExprType[] { TestExprType.Time, TestExprType.None, TestExprType.Time },
                new TestExprType[] { TestExprType.Time, TestExprType.None, TestExprType.None },
                new TestExprType[] { TestExprType.Date, TestExprType.Date, TestExprType.Date },
                new TestExprType[] { TestExprType.Date, TestExprType.Date, TestExprType.None },
                new TestExprType[] { TestExprType.Date, TestExprType.None, TestExprType.Date },
                new TestExprType[] { TestExprType.Date, TestExprType.None, TestExprType.None },
                new TestExprType[] { TestExprType.TimePeriod, TestExprType.TimePeriod, TestExprType.TimePeriod },
                new TestExprType[] { TestExprType.TimePeriod, TestExprType.TimePeriod, TestExprType.None },
                new TestExprType[] { TestExprType.TimePeriod, TestExprType.None, TestExprType.TimePeriod },
                new TestExprType[] { TestExprType.TimePeriod, TestExprType.None, TestExprType.None },
                new TestExprType[] { TestExprType.Duration, TestExprType.Duration, TestExprType.Duration },
                new TestExprType[] { TestExprType.Duration, TestExprType.Duration, TestExprType.None },
                new TestExprType[] { TestExprType.Duration, TestExprType.None, TestExprType.Duration },
                new TestExprType[] { TestExprType.Duration, TestExprType.None, TestExprType.None },
                new TestExprType[] { TestExprType.None, TestExprType.None, TestExprType.None },
                new TestExprType[] { TestExprType.None, TestExprType.None, TestExprType.Integer },
                new TestExprType[] { TestExprType.None, TestExprType.None, TestExprType.Number },
                new TestExprType[] { TestExprType.None, TestExprType.None, TestExprType.String },
                new TestExprType[] { TestExprType.None, TestExprType.None, TestExprType.Boolean },
                new TestExprType[] { TestExprType.None, TestExprType.None, TestExprType.Time },
                new TestExprType[] { TestExprType.None, TestExprType.None, TestExprType.Date },
                new TestExprType[] { TestExprType.None, TestExprType.None, TestExprType.TimePeriod },
                new TestExprType[] { TestExprType.None, TestExprType.None, TestExprType.Duration },
                new TestExprType[] { TestExprType.None, TestExprType.Integer, TestExprType.None },
                new TestExprType[] { TestExprType.None, TestExprType.Number, TestExprType.None },
                new TestExprType[] { TestExprType.None, TestExprType.String, TestExprType.None },
                new TestExprType[] { TestExprType.None, TestExprType.Boolean, TestExprType.None },
                new TestExprType[] { TestExprType.None, TestExprType.Time, TestExprType.None },
                new TestExprType[] { TestExprType.None, TestExprType.Date, TestExprType.None },
                new TestExprType[] { TestExprType.None, TestExprType.TimePeriod, TestExprType.None },
                new TestExprType[] { TestExprType.None, TestExprType.Duration, TestExprType.None },
                new TestExprType[] { TestExprType.None, TestExprType.Integer, TestExprType.Integer },
                new TestExprType[] { TestExprType.None, TestExprType.Integer, TestExprType.Number },
                new TestExprType[] { TestExprType.None, TestExprType.Number, TestExprType.Number },
                new TestExprType[] { TestExprType.None, TestExprType.Number, TestExprType.Integer },
                new TestExprType[] { TestExprType.None, TestExprType.Number, TestExprType.Number },
                new TestExprType[] { TestExprType.None, TestExprType.Number, TestExprType.Number },
                new TestExprType[] { TestExprType.None, TestExprType.String, TestExprType.String },
                new TestExprType[] { TestExprType.None, TestExprType.Boolean, TestExprType.Boolean },
                new TestExprType[] { TestExprType.None, TestExprType.Time, TestExprType.Time },
                new TestExprType[] { TestExprType.None, TestExprType.Date, TestExprType.Date },
                new TestExprType[] { TestExprType.None, TestExprType.TimePeriod, TestExprType.TimePeriod },
                new TestExprType[] { TestExprType.None, TestExprType.Duration, TestExprType.Duration },
                new TestExprType[] { TestExprType.IntsDataset, TestExprType.Integer, TestExprType.Integer },
                new TestExprType[] { TestExprType.IntsDataset, TestExprType.Integer, TestExprType.None },
                new TestExprType[] { TestExprType.IntsDataset, TestExprType.None, TestExprType.Integer },
                new TestExprType[] { TestExprType.IntsDataset, TestExprType.Number, TestExprType.Number },
                new TestExprType[] { TestExprType.IntsDataset, TestExprType.Number, TestExprType.None },
                new TestExprType[] { TestExprType.IntsDataset, TestExprType.None, TestExprType.Number },
                new TestExprType[] { TestExprType.IntsDataset, TestExprType.None, TestExprType.None },
                new TestExprType[] { TestExprType.IntsDataset, TestExprType.Integer, TestExprType.Number },
                new TestExprType[] { TestExprType.IntsDataset, TestExprType.Number, TestExprType.Integer },
                new TestExprType[] { TestExprType.NumbersDataset, TestExprType.Number, TestExprType.Number },
                new TestExprType[] { TestExprType.NumbersDataset, TestExprType.Number, TestExprType.None },
                new TestExprType[] { TestExprType.NumbersDataset, TestExprType.None, TestExprType.Number },
                new TestExprType[] { TestExprType.NumbersDataset, TestExprType.Integer, TestExprType.Integer },
                new TestExprType[] { TestExprType.NumbersDataset, TestExprType.Integer, TestExprType.None },
                new TestExprType[] { TestExprType.NumbersDataset, TestExprType.None, TestExprType.Integer },
                new TestExprType[] { TestExprType.NumbersDataset, TestExprType.None, TestExprType.None },
                new TestExprType[] { TestExprType.NumbersDataset, TestExprType.Number, TestExprType.Integer },
                new TestExprType[] { TestExprType.NumbersDataset, TestExprType.Integer, TestExprType.Number },
                new TestExprType[] { TestExprType.StringsDataset, TestExprType.String, TestExprType.String },
                new TestExprType[] { TestExprType.StringsDataset, TestExprType.String, TestExprType.None },
                new TestExprType[] { TestExprType.StringsDataset, TestExprType.None, TestExprType.String },
                new TestExprType[] { TestExprType.StringsDataset, TestExprType.None, TestExprType.None },
                new TestExprType[] { TestExprType.BoolsDataset, TestExprType.Boolean, TestExprType.Boolean },
                new TestExprType[] { TestExprType.BoolsDataset, TestExprType.Boolean, TestExprType.None },
                new TestExprType[] { TestExprType.BoolsDataset, TestExprType.None, TestExprType.Boolean },
                new TestExprType[] { TestExprType.BoolsDataset, TestExprType.None, TestExprType.None },
                new TestExprType[] { TestExprType.TimesDataset, TestExprType.Time, TestExprType.Time },
                new TestExprType[] { TestExprType.TimesDataset, TestExprType.Time, TestExprType.None },
                new TestExprType[] { TestExprType.TimesDataset, TestExprType.None, TestExprType.Time },
                new TestExprType[] { TestExprType.TimesDataset, TestExprType.None, TestExprType.None },
                new TestExprType[] { TestExprType.DatesDataset, TestExprType.Date, TestExprType.Date },
                new TestExprType[] { TestExprType.DatesDataset, TestExprType.Date, TestExprType.None },
                new TestExprType[] { TestExprType.DatesDataset, TestExprType.None, TestExprType.Date },
                new TestExprType[] { TestExprType.DatesDataset, TestExprType.None, TestExprType.None },
                new TestExprType[] { TestExprType.TimePeriodsDataset, TestExprType.TimePeriod, TestExprType.TimePeriod },
                new TestExprType[] { TestExprType.TimePeriodsDataset, TestExprType.TimePeriod, TestExprType.None },
                new TestExprType[] { TestExprType.TimePeriodsDataset, TestExprType.None, TestExprType.TimePeriod },
                new TestExprType[] { TestExprType.TimePeriodsDataset, TestExprType.None, TestExprType.None },
                new TestExprType[] { TestExprType.DurationsDataset, TestExprType.Duration, TestExprType.Duration },
                new TestExprType[] { TestExprType.DurationsDataset, TestExprType.Duration, TestExprType.None },
                new TestExprType[] { TestExprType.DurationsDataset, TestExprType.None, TestExprType.Duration },
                new TestExprType[] { TestExprType.DurationsDataset, TestExprType.None, TestExprType.None },
                new TestExprType[] { TestExprType.NonesDataset, TestExprType.None, TestExprType.None },
                new TestExprType[] { TestExprType.NonesDataset, TestExprType.None, TestExprType.Integer },
                new TestExprType[] { TestExprType.NonesDataset, TestExprType.None, TestExprType.Number },
                new TestExprType[] { TestExprType.NonesDataset, TestExprType.None, TestExprType.String },
                new TestExprType[] { TestExprType.NonesDataset, TestExprType.None, TestExprType.Boolean },
                new TestExprType[] { TestExprType.NonesDataset, TestExprType.None, TestExprType.Time },
                new TestExprType[] { TestExprType.NonesDataset, TestExprType.None, TestExprType.Date },
                new TestExprType[] { TestExprType.NonesDataset, TestExprType.None, TestExprType.TimePeriod },
                new TestExprType[] { TestExprType.NonesDataset, TestExprType.None, TestExprType.Duration },
                new TestExprType[] { TestExprType.NonesDataset, TestExprType.Integer, TestExprType.None },
                new TestExprType[] { TestExprType.NonesDataset, TestExprType.Number, TestExprType.None },
                new TestExprType[] { TestExprType.NonesDataset, TestExprType.String, TestExprType.None },
                new TestExprType[] { TestExprType.NonesDataset, TestExprType.Boolean, TestExprType.None },
                new TestExprType[] { TestExprType.NonesDataset, TestExprType.Time, TestExprType.None },
                new TestExprType[] { TestExprType.NonesDataset, TestExprType.Date, TestExprType.None },
                new TestExprType[] { TestExprType.NonesDataset, TestExprType.TimePeriod, TestExprType.None },
                new TestExprType[] { TestExprType.NonesDataset, TestExprType.Duration, TestExprType.None },
                new TestExprType[] { TestExprType.NonesDataset, TestExprType.Integer, TestExprType.Integer },
                new TestExprType[] { TestExprType.NonesDataset, TestExprType.Integer, TestExprType.Number },
                new TestExprType[] { TestExprType.NonesDataset, TestExprType.Number, TestExprType.Number },
                new TestExprType[] { TestExprType.NonesDataset, TestExprType.Number, TestExprType.Integer },
                new TestExprType[] { TestExprType.NonesDataset, TestExprType.Number, TestExprType.Number },
                new TestExprType[] { TestExprType.NonesDataset, TestExprType.String, TestExprType.String },
                new TestExprType[] { TestExprType.NonesDataset, TestExprType.Boolean, TestExprType.Boolean },
                new TestExprType[] { TestExprType.NonesDataset, TestExprType.Time, TestExprType.Time },
                new TestExprType[] { TestExprType.NonesDataset, TestExprType.Date, TestExprType.Date },
                new TestExprType[] { TestExprType.NonesDataset, TestExprType.TimePeriod, TestExprType.TimePeriod },
                new TestExprType[] { TestExprType.NonesDataset, TestExprType.Duration, TestExprType.Duration }
            };

            TestExprType[][] combinations = Enum.GetValues(typeof(TestExprType)).Cast<TestExprType>().GetCombinations(3);
            TestExprType[][] wrongCombs = combinations.Without(correctCombs);

            foreach (TestExprType[] wrongComb in wrongCombs.Where(wrongComb => (int)wrongComb[0] < 18)) // No mixed datasets
            {
                IExpression betweenExpr = TestExprFactory.GetExpression(wrongComb);
                betweenExpr.OperatorDefinition = this.opResolver("between");
                if (betweenExpr.Operands["ds_1"].Structure.Measures.Count > 1)
                    betweenExpr.Operands["ds_1"].Structure.Measures.RemoveAt(1);

                // Debug condition example: wrongComb[0] == TestExprType.Integer && wrongComb[1] == TestExprType.Integer && wrongComb[2] == TestExprType.Integer
                Assert.ThrowsAny<VtlOperatorError>(() => { betweenExpr.OperatorDefinition.GetOutputStructure(betweenExpr); });
            }
        }
    }
}
