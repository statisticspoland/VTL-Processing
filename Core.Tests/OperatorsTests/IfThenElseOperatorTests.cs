namespace StatisticsPoland.VtlProcessing.Core.Tests.OperatorsTests
{
    using StatisticsPoland.VtlProcessing.Core.ErrorHandling;
    using StatisticsPoland.VtlProcessing.Core.Infrastructure;
    using StatisticsPoland.VtlProcessing.Core.Models;
    using StatisticsPoland.VtlProcessing.Core.Models.Interfaces;
    using StatisticsPoland.VtlProcessing.Core.Models.Types;
    using StatisticsPoland.VtlProcessing.Core.Tests.Utilities;
    using System;
    using Xunit;

    public class IfThenElseOperatorTests
    {
        private readonly IExpression _ifThenElseExpr;

        public IfThenElseOperatorTests()
        {
            this._ifThenElseExpr = ModelResolvers.ExprResolver();
            this._ifThenElseExpr.OperatorDefinition = ModelResolvers.OperatorResolver("if");
            this._ifThenElseExpr.AddOperand("if", ModelResolvers.ExprResolver());
            this._ifThenElseExpr.AddOperand("then", ModelResolvers.ExprResolver());
            this._ifThenElseExpr.AddOperand("else", ModelResolvers.ExprResolver());
            this._ifThenElseExpr.Operands["if"].AddOperand("ds_1", ModelResolvers.ExprResolver());
            this._ifThenElseExpr.Operands["then"].AddOperand("ds_1", ModelResolvers.ExprResolver());
            this._ifThenElseExpr.Operands["else"].AddOperand("ds_1", ModelResolvers.ExprResolver());
        }

        [Theory]
        // 3 Scalars:
        [InlineData(TestExprType.Integer, TestExprType.Integer, /*result:*/ TestExprType.Integer)]
        [InlineData(TestExprType.Integer, TestExprType.Number, /*result:*/ TestExprType.Number)]
        [InlineData(TestExprType.Integer, TestExprType.None, /*result:*/ TestExprType.Integer)]
        [InlineData(TestExprType.Number, TestExprType.Integer, /*result:*/ TestExprType.Number)]
        [InlineData(TestExprType.Number, TestExprType.Number, /*result:*/ TestExprType.Number)]
        [InlineData(TestExprType.Number, TestExprType.None, /*result:*/ TestExprType.Number)]
        [InlineData(TestExprType.String, TestExprType.String, /*result:*/ TestExprType.String)]
        [InlineData(TestExprType.String, TestExprType.None, /*result:*/ TestExprType.String)]
        [InlineData(TestExprType.Boolean, TestExprType.Boolean, /*result:*/ TestExprType.Boolean)]
        [InlineData(TestExprType.Boolean, TestExprType.None, /*result:*/ TestExprType.Boolean)]
        [InlineData(TestExprType.Time, TestExprType.Time, /*result:*/ TestExprType.Time)]
        [InlineData(TestExprType.Time, TestExprType.None, /*result:*/ TestExprType.Time)]
        [InlineData(TestExprType.Date, TestExprType.Date, /*result:*/ TestExprType.Date)]
        [InlineData(TestExprType.Date, TestExprType.None, /*result:*/ TestExprType.Date)]
        [InlineData(TestExprType.TimePeriod, TestExprType.TimePeriod, /*result:*/ TestExprType.TimePeriod)]
        [InlineData(TestExprType.TimePeriod, TestExprType.None, /*result:*/ TestExprType.TimePeriod)]
        [InlineData(TestExprType.Duration, TestExprType.Duration, /*result:*/ TestExprType.Duration)]
        [InlineData(TestExprType.Duration, TestExprType.None, /*result:*/ TestExprType.Duration)]
        [InlineData(TestExprType.None, TestExprType.None, /*result:*/ TestExprType.None)]
        [InlineData(TestExprType.None, TestExprType.Integer, /*result:*/ TestExprType.Integer)]
        [InlineData(TestExprType.None, TestExprType.Number, /*result:*/ TestExprType.Number)]
        [InlineData(TestExprType.None, TestExprType.String, /*result:*/ TestExprType.String)]
        [InlineData(TestExprType.None, TestExprType.Boolean, /*result:*/ TestExprType.Boolean)]
        [InlineData(TestExprType.None, TestExprType.Time, /*result:*/ TestExprType.Time)]
        [InlineData(TestExprType.None, TestExprType.Date, /*result:*/ TestExprType.Date)]
        [InlineData(TestExprType.None, TestExprType.TimePeriod, /*result:*/ TestExprType.TimePeriod)]
        [InlineData(TestExprType.None, TestExprType.Duration, /*result:*/ TestExprType.Duration)]
        // 2 Scalars, Dataset:
        [InlineData(TestExprType.Integer, TestExprType.IntsDataset, /*result:*/ TestExprType.IntsDataset)]
        [InlineData(TestExprType.Integer, TestExprType.NumbersDataset, /*result:*/ TestExprType.NumbersDataset)]
        [InlineData(TestExprType.Integer, TestExprType.MixedIntNumDataset, /*result:*/ TestExprType.MixedIntNumDataset)]
        [InlineData(TestExprType.Integer, TestExprType.NonesDataset, /*result:*/ TestExprType.IntsDataset)]
        [InlineData(TestExprType.Integer, TestExprType.MixedNoneIntDataset, /*result:*/ TestExprType.IntsDataset)]
        [InlineData(TestExprType.Integer, TestExprType.MixedNoneNumDataset, /*result:*/ TestExprType.MixedIntNumDataset)]
        [InlineData(TestExprType.Number, TestExprType.IntsDataset, /*result:*/ TestExprType.NumbersDataset)]
        [InlineData(TestExprType.Number, TestExprType.NumbersDataset, /*result:*/ TestExprType.NumbersDataset)]
        [InlineData(TestExprType.Number, TestExprType.MixedIntNumDataset, /*result:*/ TestExprType.NumbersDataset)]
        [InlineData(TestExprType.Number, TestExprType.NonesDataset, /*result:*/ TestExprType.NumbersDataset)]
        [InlineData(TestExprType.Number, TestExprType.MixedNoneIntDataset, /*result:*/ TestExprType.NumbersDataset)]
        [InlineData(TestExprType.Number, TestExprType.MixedNoneNumDataset, /*result:*/ TestExprType.NumbersDataset)]
        [InlineData(TestExprType.String, TestExprType.StringsDataset, /*result:*/ TestExprType.StringsDataset)]
        [InlineData(TestExprType.String, TestExprType.NonesDataset, /*result:*/ TestExprType.StringsDataset)]
        [InlineData(TestExprType.String, TestExprType.MixedNoneStrDataset, /*result:*/ TestExprType.StringsDataset)]
        [InlineData(TestExprType.Boolean, TestExprType.BoolsDataset, /*result:*/ TestExprType.BoolsDataset)]
        [InlineData(TestExprType.Boolean, TestExprType.NonesDataset, /*result:*/ TestExprType.BoolsDataset)]
        [InlineData(TestExprType.Boolean, TestExprType.MixedNoneBoolDataset, /*result:*/ TestExprType.BoolsDataset)]
        [InlineData(TestExprType.Time, TestExprType.TimesDataset, /*result:*/ TestExprType.TimesDataset)]
        [InlineData(TestExprType.Time, TestExprType.NonesDataset, /*result:*/ TestExprType.TimesDataset)]
        [InlineData(TestExprType.Time, TestExprType.MixedNoneTimeDataset, /*result:*/ TestExprType.TimesDataset)]
        [InlineData(TestExprType.Date, TestExprType.DatesDataset, /*result:*/ TestExprType.DatesDataset)]
        [InlineData(TestExprType.Date, TestExprType.NonesDataset, /*result:*/ TestExprType.DatesDataset)]
        [InlineData(TestExprType.Date, TestExprType.MixedNoneDateDataset, /*result:*/ TestExprType.DatesDataset)]
        [InlineData(TestExprType.TimePeriod, TestExprType.TimePeriodsDataset, /*result:*/ TestExprType.TimePeriodsDataset)]
        [InlineData(TestExprType.TimePeriod, TestExprType.NonesDataset, /*result:*/ TestExprType.TimePeriodsDataset)]
        [InlineData(TestExprType.TimePeriod, TestExprType.MixedNoneTimePerDataset, /*result:*/ TestExprType.TimePeriodsDataset)]
        [InlineData(TestExprType.Duration, TestExprType.DurationsDataset, /*result:*/ TestExprType.DurationsDataset)]
        [InlineData(TestExprType.Duration, TestExprType.NonesDataset, /*result:*/ TestExprType.DurationsDataset)]
        [InlineData(TestExprType.Duration, TestExprType.MixedNoneDurDataset, /*result:*/ TestExprType.DurationsDataset)]
        [InlineData(TestExprType.None, TestExprType.NonesDataset, /*result:*/ TestExprType.NonesDataset)]
        [InlineData(TestExprType.None, TestExprType.IntsDataset, /*result:*/ TestExprType.IntsDataset)]
        [InlineData(TestExprType.None, TestExprType.NumbersDataset, /*result:*/ TestExprType.NumbersDataset)]
        [InlineData(TestExprType.None, TestExprType.StringsDataset, /*result:*/ TestExprType.StringsDataset)]
        [InlineData(TestExprType.None, TestExprType.BoolsDataset, /*result:*/ TestExprType.BoolsDataset)]
        [InlineData(TestExprType.None, TestExprType.TimesDataset, /*result:*/ TestExprType.TimesDataset)]
        [InlineData(TestExprType.None, TestExprType.DatesDataset, /*result:*/ TestExprType.DatesDataset)]
        [InlineData(TestExprType.None, TestExprType.TimePeriodsDataset, /*result:*/ TestExprType.TimePeriodsDataset)]
        [InlineData(TestExprType.None, TestExprType.DurationsDataset, /*result:*/ TestExprType.DurationsDataset)]
        [InlineData(TestExprType.None, TestExprType.MixedIntNumDataset, /*result:*/ TestExprType.MixedIntNumDataset)]
        [InlineData(TestExprType.None, TestExprType.MixedNoneIntDataset, /*result:*/ TestExprType.MixedNoneIntDataset)]
        [InlineData(TestExprType.None, TestExprType.MixedNoneNumDataset, /*result:*/ TestExprType.MixedNoneNumDataset)]
        [InlineData(TestExprType.None, TestExprType.MixedNoneStrDataset, /*result:*/ TestExprType.MixedNoneStrDataset)]
        [InlineData(TestExprType.None, TestExprType.MixedNoneBoolDataset, /*result:*/ TestExprType.MixedNoneBoolDataset)]
        [InlineData(TestExprType.None, TestExprType.MixedNoneTimeDataset, /*result:*/ TestExprType.MixedNoneTimeDataset)]
        [InlineData(TestExprType.None, TestExprType.MixedNoneDateDataset, /*result:*/ TestExprType.MixedNoneDateDataset)]
        [InlineData(TestExprType.None, TestExprType.MixedNoneTimePerDataset, /*result:*/ TestExprType.MixedNoneTimePerDataset)]
        [InlineData(TestExprType.None, TestExprType.MixedNoneDurDataset, /*result:*/ TestExprType.MixedNoneDurDataset)]
        // Scalar, Dataset, Scalar:
        [InlineData(TestExprType.IntsDataset, TestExprType.Integer, /*result:*/ TestExprType.IntsDataset)]
        [InlineData(TestExprType.IntsDataset, TestExprType.Number, /*result:*/ TestExprType.NumbersDataset)]
        [InlineData(TestExprType.IntsDataset, TestExprType.None, /*result:*/ TestExprType.IntsDataset)]
        [InlineData(TestExprType.NumbersDataset, TestExprType.Integer, /*result:*/ TestExprType.NumbersDataset)]
        [InlineData(TestExprType.NumbersDataset, TestExprType.Number, /*result:*/ TestExprType.NumbersDataset)]
        [InlineData(TestExprType.NumbersDataset, TestExprType.None, /*result:*/ TestExprType.NumbersDataset)]
        [InlineData(TestExprType.StringsDataset, TestExprType.String, /*result:*/ TestExprType.StringsDataset)]
        [InlineData(TestExprType.StringsDataset, TestExprType.None, /*result:*/ TestExprType.StringsDataset)]
        [InlineData(TestExprType.BoolsDataset, TestExprType.Boolean, /*result:*/ TestExprType.BoolsDataset)]
        [InlineData(TestExprType.BoolsDataset, TestExprType.None, /*result:*/ TestExprType.BoolsDataset)]
        [InlineData(TestExprType.TimesDataset, TestExprType.Time, /*result:*/ TestExprType.TimesDataset)]
        [InlineData(TestExprType.TimesDataset, TestExprType.None, /*result:*/ TestExprType.TimesDataset)]
        [InlineData(TestExprType.DatesDataset, TestExprType.Date, /*result:*/ TestExprType.DatesDataset)]
        [InlineData(TestExprType.DatesDataset, TestExprType.None, /*result:*/ TestExprType.DatesDataset)]
        [InlineData(TestExprType.TimePeriodsDataset, TestExprType.TimePeriod, /*result:*/ TestExprType.TimePeriodsDataset)]
        [InlineData(TestExprType.TimePeriodsDataset, TestExprType.None, /*result:*/ TestExprType.TimePeriodsDataset)]
        [InlineData(TestExprType.DurationsDataset, TestExprType.Duration, /*result:*/ TestExprType.DurationsDataset)]
        [InlineData(TestExprType.DurationsDataset, TestExprType.None, /*result:*/ TestExprType.DurationsDataset)]
        [InlineData(TestExprType.NonesDataset, TestExprType.None, /*result:*/ TestExprType.NonesDataset)]
        [InlineData(TestExprType.NonesDataset, TestExprType.Integer, /*result:*/ TestExprType.IntsDataset)]
        [InlineData(TestExprType.NonesDataset, TestExprType.Number, /*result:*/ TestExprType.NumbersDataset)]
        [InlineData(TestExprType.NonesDataset, TestExprType.String, /*result:*/ TestExprType.StringsDataset)]
        [InlineData(TestExprType.NonesDataset, TestExprType.Boolean, /*result:*/ TestExprType.BoolsDataset)]
        [InlineData(TestExprType.NonesDataset, TestExprType.Time, /*result:*/ TestExprType.TimesDataset)]
        [InlineData(TestExprType.NonesDataset, TestExprType.Date, /*result:*/ TestExprType.DatesDataset)]
        [InlineData(TestExprType.NonesDataset, TestExprType.TimePeriod, /*result:*/ TestExprType.TimePeriodsDataset)]
        [InlineData(TestExprType.NonesDataset, TestExprType.Duration, /*result:*/ TestExprType.DurationsDataset)]
        [InlineData(TestExprType.MixedIntNumDataset, TestExprType.Integer, /*result:*/ TestExprType.MixedIntNumDataset)]
        [InlineData(TestExprType.MixedIntNumDataset, TestExprType.Number, /*result:*/ TestExprType.NumbersDataset)]
        [InlineData(TestExprType.MixedIntNumDataset, TestExprType.None, /*result:*/ TestExprType.MixedIntNumDataset)]
        [InlineData(TestExprType.MixedNoneIntDataset, TestExprType.Integer, /*result:*/ TestExprType.IntsDataset)]
        [InlineData(TestExprType.MixedNoneIntDataset, TestExprType.Number, /*result:*/ TestExprType.NumbersDataset)]
        [InlineData(TestExprType.MixedNoneIntDataset, TestExprType.None, /*result:*/ TestExprType.MixedNoneIntDataset)]
        [InlineData(TestExprType.MixedNoneNumDataset, TestExprType.Integer, /*result:*/ TestExprType.MixedIntNumDataset)]
        [InlineData(TestExprType.MixedNoneNumDataset, TestExprType.Number, /*result:*/ TestExprType.NumbersDataset)]
        [InlineData(TestExprType.MixedNoneNumDataset, TestExprType.None, /*result:*/ TestExprType.MixedNoneNumDataset)]
        [InlineData(TestExprType.MixedNoneStrDataset, TestExprType.String, /*result:*/ TestExprType.StringsDataset)]
        [InlineData(TestExprType.MixedNoneStrDataset, TestExprType.None, /*result:*/ TestExprType.MixedNoneStrDataset)]
        [InlineData(TestExprType.MixedNoneBoolDataset, TestExprType.Boolean, /*result:*/ TestExprType.BoolsDataset)]
        [InlineData(TestExprType.MixedNoneBoolDataset, TestExprType.None, /*result:*/ TestExprType.MixedNoneBoolDataset)]
        [InlineData(TestExprType.MixedNoneTimeDataset, TestExprType.Time, /*result:*/ TestExprType.TimesDataset)]
        [InlineData(TestExprType.MixedNoneTimeDataset, TestExprType.None, /*result:*/ TestExprType.MixedNoneTimeDataset)]
        [InlineData(TestExprType.MixedNoneDateDataset, TestExprType.Date, /*result:*/ TestExprType.DatesDataset)]
        [InlineData(TestExprType.MixedNoneDateDataset, TestExprType.None, /*result:*/ TestExprType.MixedNoneDateDataset)]
        [InlineData(TestExprType.MixedNoneTimePerDataset, TestExprType.TimePeriod, /*result:*/ TestExprType.TimePeriodsDataset)]
        [InlineData(TestExprType.MixedNoneTimePerDataset, TestExprType.None, /*result:*/ TestExprType.MixedNoneTimePerDataset)]
        [InlineData(TestExprType.MixedNoneDurDataset, TestExprType.Duration, /*result:*/ TestExprType.DurationsDataset)]
        [InlineData(TestExprType.MixedNoneDurDataset, TestExprType.None, /*result:*/ TestExprType.MixedNoneDurDataset)]
        // Scalar, 2 Datasets:
        [InlineData(TestExprType.IntsDataset, TestExprType.IntsDataset, /*result:*/ TestExprType.IntsDataset)]
        [InlineData(TestExprType.IntsDataset, TestExprType.NumbersDataset, /*result:*/ TestExprType.NumbersDataset)]
        [InlineData(TestExprType.IntsDataset, TestExprType.MixedIntNumDataset, /*result:*/ TestExprType.MixedIntNumDataset)]
        [InlineData(TestExprType.IntsDataset, TestExprType.NonesDataset, /*result:*/ TestExprType.IntsDataset)]
        [InlineData(TestExprType.IntsDataset, TestExprType.MixedNoneIntDataset, /*result:*/ TestExprType.IntsDataset)]
        [InlineData(TestExprType.IntsDataset, TestExprType.MixedNoneNumDataset, /*result:*/ TestExprType.MixedIntNumDataset)]
        [InlineData(TestExprType.NumbersDataset, TestExprType.IntsDataset, /*result:*/ TestExprType.NumbersDataset)]
        [InlineData(TestExprType.NumbersDataset, TestExprType.NumbersDataset, /*result:*/ TestExprType.NumbersDataset)]
        [InlineData(TestExprType.NumbersDataset, TestExprType.MixedIntNumDataset, /*result:*/ TestExprType.NumbersDataset)]
        [InlineData(TestExprType.NumbersDataset, TestExprType.NonesDataset, /*result:*/ TestExprType.NumbersDataset)]
        [InlineData(TestExprType.NumbersDataset, TestExprType.MixedNoneIntDataset, /*result:*/ TestExprType.NumbersDataset)]
        [InlineData(TestExprType.NumbersDataset, TestExprType.MixedNoneNumDataset, /*result:*/ TestExprType.NumbersDataset)]
        [InlineData(TestExprType.StringsDataset, TestExprType.StringsDataset, /*result:*/ TestExprType.StringsDataset)]
        [InlineData(TestExprType.StringsDataset, TestExprType.NonesDataset, /*result:*/ TestExprType.StringsDataset)]
        [InlineData(TestExprType.StringsDataset, TestExprType.MixedNoneStrDataset, /*result:*/ TestExprType.StringsDataset)]
        [InlineData(TestExprType.BoolsDataset, TestExprType.BoolsDataset, /*result:*/ TestExprType.BoolsDataset)]
        [InlineData(TestExprType.BoolsDataset, TestExprType.NonesDataset, /*result:*/ TestExprType.BoolsDataset)]
        [InlineData(TestExprType.BoolsDataset, TestExprType.MixedNoneBoolDataset, /*result:*/ TestExprType.BoolsDataset)]
        [InlineData(TestExprType.TimesDataset, TestExprType.TimesDataset, /*result:*/ TestExprType.TimesDataset)]
        [InlineData(TestExprType.TimesDataset, TestExprType.NonesDataset, /*result:*/ TestExprType.TimesDataset)]
        [InlineData(TestExprType.TimesDataset, TestExprType.MixedNoneTimeDataset, /*result:*/ TestExprType.TimesDataset)]
        [InlineData(TestExprType.DatesDataset, TestExprType.DatesDataset, /*result:*/ TestExprType.DatesDataset)]
        [InlineData(TestExprType.DatesDataset, TestExprType.NonesDataset, /*result:*/ TestExprType.DatesDataset)]
        [InlineData(TestExprType.DatesDataset, TestExprType.MixedNoneDateDataset, /*result:*/ TestExprType.DatesDataset)]
        [InlineData(TestExprType.TimePeriodsDataset, TestExprType.TimePeriodsDataset, /*result:*/ TestExprType.TimePeriodsDataset)]
        [InlineData(TestExprType.TimePeriodsDataset, TestExprType.NonesDataset, /*result:*/ TestExprType.TimePeriodsDataset)]
        [InlineData(TestExprType.TimePeriodsDataset, TestExprType.MixedNoneTimePerDataset, /*result:*/ TestExprType.TimePeriodsDataset)]
        [InlineData(TestExprType.DurationsDataset, TestExprType.DurationsDataset, /*result:*/ TestExprType.DurationsDataset)]
        [InlineData(TestExprType.DurationsDataset, TestExprType.NonesDataset, /*result:*/ TestExprType.DurationsDataset)]
        [InlineData(TestExprType.DurationsDataset, TestExprType.MixedNoneDurDataset, /*result:*/ TestExprType.DurationsDataset)]
        [InlineData(TestExprType.NonesDataset, TestExprType.NonesDataset, /*result:*/ TestExprType.NonesDataset)]
        [InlineData(TestExprType.NonesDataset, TestExprType.IntsDataset, /*result:*/ TestExprType.IntsDataset)]
        [InlineData(TestExprType.NonesDataset, TestExprType.NumbersDataset, /*result:*/ TestExprType.NumbersDataset)]
        [InlineData(TestExprType.NonesDataset, TestExprType.StringsDataset, /*result:*/ TestExprType.StringsDataset)]
        [InlineData(TestExprType.NonesDataset, TestExprType.BoolsDataset, /*result:*/ TestExprType.BoolsDataset)]
        [InlineData(TestExprType.NonesDataset, TestExprType.TimesDataset, /*result:*/ TestExprType.TimesDataset)]
        [InlineData(TestExprType.NonesDataset, TestExprType.DatesDataset, /*result:*/ TestExprType.DatesDataset)]
        [InlineData(TestExprType.NonesDataset, TestExprType.TimePeriodsDataset, /*result:*/ TestExprType.TimePeriodsDataset)]
        [InlineData(TestExprType.NonesDataset, TestExprType.DurationsDataset, /*result:*/ TestExprType.DurationsDataset)]
        [InlineData(TestExprType.NonesDataset, TestExprType.MixedIntNumDataset, /*result:*/ TestExprType.MixedIntNumDataset)]
        [InlineData(TestExprType.NonesDataset, TestExprType.MixedNoneIntDataset, /*result:*/ TestExprType.MixedNoneIntDataset)]
        [InlineData(TestExprType.NonesDataset, TestExprType.MixedNoneNumDataset, /*result:*/ TestExprType.MixedNoneNumDataset)]
        [InlineData(TestExprType.NonesDataset, TestExprType.MixedNoneStrDataset, /*result:*/ TestExprType.MixedNoneStrDataset)]
        [InlineData(TestExprType.NonesDataset, TestExprType.MixedNoneBoolDataset, /*result:*/ TestExprType.MixedNoneBoolDataset)]
        [InlineData(TestExprType.NonesDataset, TestExprType.MixedNoneTimeDataset, /*result:*/ TestExprType.MixedNoneTimeDataset)]
        [InlineData(TestExprType.NonesDataset, TestExprType.MixedNoneDateDataset, /*result:*/ TestExprType.MixedNoneDateDataset)]
        [InlineData(TestExprType.NonesDataset, TestExprType.MixedNoneTimePerDataset, /*result:*/ TestExprType.MixedNoneTimePerDataset)]
        [InlineData(TestExprType.NonesDataset, TestExprType.MixedNoneDurDataset, /*result:*/ TestExprType.MixedNoneDurDataset)]
        [InlineData(TestExprType.MixedIntNumDataset, TestExprType.IntsDataset, /*result:*/ TestExprType.MixedIntNumDataset)]
        [InlineData(TestExprType.MixedIntNumDataset, TestExprType.NumbersDataset, /*result:*/ TestExprType.NumbersDataset)]
        [InlineData(TestExprType.MixedNoneIntDataset, TestExprType.IntsDataset, /*result:*/ TestExprType.IntsDataset)]
        [InlineData(TestExprType.MixedNoneNumDataset, TestExprType.NumbersDataset, /*result:*/ TestExprType.NumbersDataset)]
        [InlineData(TestExprType.MixedNoneStrDataset, TestExprType.StringsDataset, /*result:*/ TestExprType.StringsDataset)]
        [InlineData(TestExprType.MixedNoneBoolDataset, TestExprType.BoolsDataset, /*result:*/ TestExprType.BoolsDataset)]
        [InlineData(TestExprType.MixedNoneTimeDataset, TestExprType.TimesDataset, /*result:*/ TestExprType.TimesDataset)]
        [InlineData(TestExprType.MixedNoneDateDataset, TestExprType.DatesDataset, /*result:*/ TestExprType.DatesDataset)]
        [InlineData(TestExprType.MixedNoneTimePerDataset, TestExprType.TimePeriodsDataset, /*result:*/ TestExprType.TimePeriodsDataset)]
        [InlineData(TestExprType.MixedNoneDurDataset, TestExprType.DurationsDataset, /*result:*/ TestExprType.DurationsDataset)]
        public void GetOutputStructure_CorrectScalarConditionExpr_DataStructure(params TestExprType[] types)
        {
            IExpression ifExpr = this._ifThenElseExpr.Operands["if"];
            IExpression thenExpr = this._ifThenElseExpr.Operands["then"];
            IExpression elseExpr = this._ifThenElseExpr.Operands["else"];

            ifExpr.Operands["ds_1"] = TestExprFactory.GetExpression(TestExprType.Boolean);
            thenExpr.Operands["ds_1"] = TestExprFactory.GetExpression(types[0]);
            elseExpr.Operands["ds_1"] = TestExprFactory.GetExpression(types[1]);

            IExpression expected = TestExprFactory.GetExpression(types[2]);

            IDataStructure dataStructure = this._ifThenElseExpr.OperatorDefinition.GetOutputStructure(this._ifThenElseExpr);

            Assert.True(expected.Structure.EqualsObj(dataStructure));
        }

        [Fact]
        public void GetOutputStructure_CorrectDatasetCondition2ScalarsExpr_DataStructure()
        {
            IExpression ifExpr = this._ifThenElseExpr.Operands["if"];
            IExpression thenExpr = this._ifThenElseExpr.Operands["then"];
            IExpression elseExpr = this._ifThenElseExpr.Operands["else"];

            ifExpr.Operands["ds_1"] = TestExprFactory.GetExpression(TestExprType.Boolean);
            ifExpr.Operands["ds_1"].Structure.Identifiers.Add(new StructureComponent(BasicDataType.Integer, "Id1"));
            thenExpr.Operands["ds_1"] = TestExprFactory.GetExpression(TestExprType.Boolean);
            elseExpr.Operands["ds_1"] = TestExprFactory.GetExpression(TestExprType.Boolean);

            IExpression expected = TestExprFactory.GetExpression(TestExprType.Boolean);
            expected.Structure.Identifiers.Add(new StructureComponent(BasicDataType.Integer, "Id1"));

            IDataStructure dataStructure = this._ifThenElseExpr.OperatorDefinition.GetOutputStructure(this._ifThenElseExpr);

            Assert.True(expected.Structure.EqualsObj(dataStructure));
        }

        [Theory]
        // 3 Datasets:
        [InlineData(TestExprType.IntsDataset, TestExprType.IntsDataset, /*result:*/ TestExprType.IntsDataset)]
        [InlineData(TestExprType.IntsDataset, TestExprType.NumbersDataset, /*result:*/ TestExprType.NumbersDataset)]
        [InlineData(TestExprType.IntsDataset, TestExprType.MixedIntNumDataset, /*result:*/ TestExprType.MixedIntNumDataset)]
        [InlineData(TestExprType.IntsDataset, TestExprType.NonesDataset, /*result:*/ TestExprType.IntsDataset)]
        [InlineData(TestExprType.IntsDataset, TestExprType.MixedNoneIntDataset, /*result:*/ TestExprType.IntsDataset)]
        [InlineData(TestExprType.IntsDataset, TestExprType.MixedNoneNumDataset, /*result:*/ TestExprType.MixedIntNumDataset)]
        [InlineData(TestExprType.NumbersDataset, TestExprType.IntsDataset, /*result:*/ TestExprType.NumbersDataset)]
        [InlineData(TestExprType.NumbersDataset, TestExprType.NumbersDataset, /*result:*/ TestExprType.NumbersDataset)]
        [InlineData(TestExprType.NumbersDataset, TestExprType.MixedIntNumDataset, /*result:*/ TestExprType.NumbersDataset)]
        [InlineData(TestExprType.NumbersDataset, TestExprType.NonesDataset, /*result:*/ TestExprType.NumbersDataset)]
        [InlineData(TestExprType.NumbersDataset, TestExprType.MixedNoneIntDataset, /*result:*/ TestExprType.NumbersDataset)]
        [InlineData(TestExprType.NumbersDataset, TestExprType.MixedNoneNumDataset, /*result:*/ TestExprType.NumbersDataset)]
        [InlineData(TestExprType.StringsDataset, TestExprType.StringsDataset, /*result:*/ TestExprType.StringsDataset)]
        [InlineData(TestExprType.StringsDataset, TestExprType.NonesDataset, /*result:*/ TestExprType.StringsDataset)]
        [InlineData(TestExprType.StringsDataset, TestExprType.MixedNoneStrDataset, /*result:*/ TestExprType.StringsDataset)]
        [InlineData(TestExprType.BoolsDataset, TestExprType.BoolsDataset, /*result:*/ TestExprType.BoolsDataset)]
        [InlineData(TestExprType.BoolsDataset, TestExprType.NonesDataset, /*result:*/ TestExprType.BoolsDataset)]
        [InlineData(TestExprType.BoolsDataset, TestExprType.MixedNoneBoolDataset, /*result:*/ TestExprType.BoolsDataset)]
        [InlineData(TestExprType.TimesDataset, TestExprType.TimesDataset, /*result:*/ TestExprType.TimesDataset)]
        [InlineData(TestExprType.TimesDataset, TestExprType.NonesDataset, /*result:*/ TestExprType.TimesDataset)]
        [InlineData(TestExprType.TimesDataset, TestExprType.MixedNoneTimeDataset, /*result:*/ TestExprType.TimesDataset)]
        [InlineData(TestExprType.DatesDataset, TestExprType.DatesDataset, /*result:*/ TestExprType.DatesDataset)]
        [InlineData(TestExprType.DatesDataset, TestExprType.NonesDataset, /*result:*/ TestExprType.DatesDataset)]
        [InlineData(TestExprType.DatesDataset, TestExprType.MixedNoneDateDataset, /*result:*/ TestExprType.DatesDataset)]
        [InlineData(TestExprType.TimePeriodsDataset, TestExprType.TimePeriodsDataset, /*result:*/ TestExprType.TimePeriodsDataset)]
        [InlineData(TestExprType.TimePeriodsDataset, TestExprType.NonesDataset, /*result:*/ TestExprType.TimePeriodsDataset)]
        [InlineData(TestExprType.TimePeriodsDataset, TestExprType.MixedNoneTimePerDataset, /*result:*/ TestExprType.TimePeriodsDataset)]
        [InlineData(TestExprType.DurationsDataset, TestExprType.DurationsDataset, /*result:*/ TestExprType.DurationsDataset)]
        [InlineData(TestExprType.DurationsDataset, TestExprType.NonesDataset, /*result:*/ TestExprType.DurationsDataset)]
        [InlineData(TestExprType.DurationsDataset, TestExprType.MixedNoneDurDataset, /*result:*/ TestExprType.DurationsDataset)]
        [InlineData(TestExprType.NonesDataset, TestExprType.NonesDataset, /*result:*/ TestExprType.NonesDataset)]
        [InlineData(TestExprType.NonesDataset, TestExprType.IntsDataset, /*result:*/ TestExprType.IntsDataset)]
        [InlineData(TestExprType.NonesDataset, TestExprType.NumbersDataset, /*result:*/ TestExprType.NumbersDataset)]
        [InlineData(TestExprType.NonesDataset, TestExprType.StringsDataset, /*result:*/ TestExprType.StringsDataset)]
        [InlineData(TestExprType.NonesDataset, TestExprType.BoolsDataset, /*result:*/ TestExprType.BoolsDataset)]
        [InlineData(TestExprType.NonesDataset, TestExprType.TimesDataset, /*result:*/ TestExprType.TimesDataset)]
        [InlineData(TestExprType.NonesDataset, TestExprType.DatesDataset, /*result:*/ TestExprType.DatesDataset)]
        [InlineData(TestExprType.NonesDataset, TestExprType.TimePeriodsDataset, /*result:*/ TestExprType.TimePeriodsDataset)]
        [InlineData(TestExprType.NonesDataset, TestExprType.DurationsDataset, /*result:*/ TestExprType.DurationsDataset)]
        [InlineData(TestExprType.NonesDataset, TestExprType.MixedIntNumDataset, /*result:*/ TestExprType.MixedIntNumDataset)]
        [InlineData(TestExprType.NonesDataset, TestExprType.MixedNoneIntDataset, /*result:*/ TestExprType.MixedNoneIntDataset)]
        [InlineData(TestExprType.NonesDataset, TestExprType.MixedNoneNumDataset, /*result:*/ TestExprType.MixedNoneNumDataset)]
        [InlineData(TestExprType.NonesDataset, TestExprType.MixedNoneStrDataset, /*result:*/ TestExprType.MixedNoneStrDataset)]
        [InlineData(TestExprType.NonesDataset, TestExprType.MixedNoneBoolDataset, /*result:*/ TestExprType.MixedNoneBoolDataset)]
        [InlineData(TestExprType.NonesDataset, TestExprType.MixedNoneTimeDataset, /*result:*/ TestExprType.MixedNoneTimeDataset)]
        [InlineData(TestExprType.NonesDataset, TestExprType.MixedNoneDateDataset, /*result:*/ TestExprType.MixedNoneDateDataset)]
        [InlineData(TestExprType.NonesDataset, TestExprType.MixedNoneTimePerDataset, /*result:*/ TestExprType.MixedNoneTimePerDataset)]
        [InlineData(TestExprType.NonesDataset, TestExprType.MixedNoneDurDataset, /*result:*/ TestExprType.MixedNoneDurDataset)]
        [InlineData(TestExprType.MixedIntNumDataset, TestExprType.IntsDataset, /*result:*/ TestExprType.MixedIntNumDataset)]
        [InlineData(TestExprType.MixedIntNumDataset, TestExprType.NumbersDataset, /*result:*/ TestExprType.NumbersDataset)]
        [InlineData(TestExprType.MixedNoneIntDataset, TestExprType.IntsDataset, /*result:*/ TestExprType.IntsDataset)]
        [InlineData(TestExprType.MixedNoneNumDataset, TestExprType.NumbersDataset, /*result:*/ TestExprType.NumbersDataset)]
        [InlineData(TestExprType.MixedNoneStrDataset, TestExprType.StringsDataset, /*result:*/ TestExprType.StringsDataset)]
        [InlineData(TestExprType.MixedNoneBoolDataset, TestExprType.BoolsDataset, /*result:*/ TestExprType.BoolsDataset)]
        [InlineData(TestExprType.MixedNoneTimeDataset, TestExprType.TimesDataset, /*result:*/ TestExprType.TimesDataset)]
        [InlineData(TestExprType.MixedNoneDateDataset, TestExprType.DatesDataset, /*result:*/ TestExprType.DatesDataset)]
        [InlineData(TestExprType.MixedNoneTimePerDataset, TestExprType.TimePeriodsDataset, /*result:*/ TestExprType.TimePeriodsDataset)]
        [InlineData(TestExprType.MixedNoneDurDataset, TestExprType.DurationsDataset, /*result:*/ TestExprType.DurationsDataset)]
        // 2 Datasets, Scalar:
        [InlineData(TestExprType.IntsDataset, TestExprType.Integer, /*result:*/ TestExprType.IntsDataset)]
        [InlineData(TestExprType.IntsDataset, TestExprType.Number, /*result:*/ TestExprType.NumbersDataset)]
        [InlineData(TestExprType.IntsDataset, TestExprType.None, /*result:*/ TestExprType.IntsDataset)]
        [InlineData(TestExprType.NumbersDataset, TestExprType.Integer, /*result:*/ TestExprType.NumbersDataset)]
        [InlineData(TestExprType.NumbersDataset, TestExprType.Number, /*result:*/ TestExprType.NumbersDataset)]
        [InlineData(TestExprType.NumbersDataset, TestExprType.None, /*result:*/ TestExprType.NumbersDataset)]
        [InlineData(TestExprType.StringsDataset, TestExprType.String, /*result:*/ TestExprType.StringsDataset)]
        [InlineData(TestExprType.StringsDataset, TestExprType.None, /*result:*/ TestExprType.StringsDataset)]
        [InlineData(TestExprType.BoolsDataset, TestExprType.Boolean, /*result:*/ TestExprType.BoolsDataset)]
        [InlineData(TestExprType.BoolsDataset, TestExprType.None, /*result:*/ TestExprType.BoolsDataset)]
        [InlineData(TestExprType.TimesDataset, TestExprType.Time, /*result:*/ TestExprType.TimesDataset)]
        [InlineData(TestExprType.TimesDataset, TestExprType.None, /*result:*/ TestExprType.TimesDataset)]
        [InlineData(TestExprType.DatesDataset, TestExprType.Date, /*result:*/ TestExprType.DatesDataset)]
        [InlineData(TestExprType.DatesDataset, TestExprType.None, /*result:*/ TestExprType.DatesDataset)]
        [InlineData(TestExprType.TimePeriodsDataset, TestExprType.TimePeriod, /*result:*/ TestExprType.TimePeriodsDataset)]
        [InlineData(TestExprType.TimePeriodsDataset, TestExprType.None, /*result:*/ TestExprType.TimePeriodsDataset)]
        [InlineData(TestExprType.DurationsDataset, TestExprType.Duration, /*result:*/ TestExprType.DurationsDataset)]
        [InlineData(TestExprType.DurationsDataset, TestExprType.None, /*result:*/ TestExprType.DurationsDataset)]
        [InlineData(TestExprType.NonesDataset, TestExprType.None, /*result:*/ TestExprType.NonesDataset)]
        [InlineData(TestExprType.NonesDataset, TestExprType.Integer, /*result:*/ TestExprType.IntsDataset)]
        [InlineData(TestExprType.NonesDataset, TestExprType.Number, /*result:*/ TestExprType.NumbersDataset)]
        [InlineData(TestExprType.NonesDataset, TestExprType.String, /*result:*/ TestExprType.StringsDataset)]
        [InlineData(TestExprType.NonesDataset, TestExprType.Boolean, /*result:*/ TestExprType.BoolsDataset)]
        [InlineData(TestExprType.NonesDataset, TestExprType.Time, /*result:*/ TestExprType.TimesDataset)]
        [InlineData(TestExprType.NonesDataset, TestExprType.Date, /*result:*/ TestExprType.DatesDataset)]
        [InlineData(TestExprType.NonesDataset, TestExprType.TimePeriod, /*result:*/ TestExprType.TimePeriodsDataset)]
        [InlineData(TestExprType.NonesDataset, TestExprType.Duration, /*result:*/ TestExprType.DurationsDataset)]
        [InlineData(TestExprType.MixedIntNumDataset, TestExprType.Integer, /*result:*/ TestExprType.MixedIntNumDataset)]
        [InlineData(TestExprType.MixedIntNumDataset, TestExprType.Number, /*result:*/ TestExprType.NumbersDataset)]
        [InlineData(TestExprType.MixedIntNumDataset, TestExprType.None, /*result:*/ TestExprType.MixedIntNumDataset)]
        [InlineData(TestExprType.MixedNoneIntDataset, TestExprType.Integer, /*result:*/ TestExprType.IntsDataset)]
        [InlineData(TestExprType.MixedNoneIntDataset, TestExprType.Number, /*result:*/ TestExprType.NumbersDataset)]
        [InlineData(TestExprType.MixedNoneIntDataset, TestExprType.None, /*result:*/ TestExprType.MixedNoneIntDataset)]
        [InlineData(TestExprType.MixedNoneNumDataset, TestExprType.Integer, /*result:*/ TestExprType.MixedIntNumDataset)]
        [InlineData(TestExprType.MixedNoneNumDataset, TestExprType.Number, /*result:*/ TestExprType.NumbersDataset)]
        [InlineData(TestExprType.MixedNoneNumDataset, TestExprType.None, /*result:*/ TestExprType.MixedNoneNumDataset)]
        [InlineData(TestExprType.MixedNoneStrDataset, TestExprType.String, /*result:*/ TestExprType.StringsDataset)]
        [InlineData(TestExprType.MixedNoneStrDataset, TestExprType.None, /*result:*/ TestExprType.MixedNoneStrDataset)]
        [InlineData(TestExprType.MixedNoneBoolDataset, TestExprType.Boolean, /*result:*/ TestExprType.BoolsDataset)]
        [InlineData(TestExprType.MixedNoneBoolDataset, TestExprType.None, /*result:*/ TestExprType.MixedNoneBoolDataset)]
        [InlineData(TestExprType.MixedNoneTimeDataset, TestExprType.Time, /*result:*/ TestExprType.TimesDataset)]
        [InlineData(TestExprType.MixedNoneTimeDataset, TestExprType.None, /*result:*/ TestExprType.MixedNoneTimeDataset)]
        [InlineData(TestExprType.MixedNoneDateDataset, TestExprType.Date, /*result:*/ TestExprType.DatesDataset)]
        [InlineData(TestExprType.MixedNoneDateDataset, TestExprType.None, /*result:*/ TestExprType.MixedNoneDateDataset)]
        [InlineData(TestExprType.MixedNoneTimePerDataset, TestExprType.TimePeriod, /*result:*/ TestExprType.TimePeriodsDataset)]
        [InlineData(TestExprType.MixedNoneTimePerDataset, TestExprType.None, /*result:*/ TestExprType.MixedNoneTimePerDataset)]
        [InlineData(TestExprType.MixedNoneDurDataset, TestExprType.Duration, /*result:*/ TestExprType.DurationsDataset)]
        [InlineData(TestExprType.MixedNoneDurDataset, TestExprType.None, /*result:*/ TestExprType.MixedNoneDurDataset)]
        // Dataset, Scalar, Dataset:
        [InlineData(TestExprType.Integer, TestExprType.IntsDataset, /*result:*/ TestExprType.IntsDataset)]
        [InlineData(TestExprType.Integer, TestExprType.NumbersDataset, /*result:*/ TestExprType.NumbersDataset)]
        [InlineData(TestExprType.Integer, TestExprType.MixedIntNumDataset, /*result:*/ TestExprType.MixedIntNumDataset)]
        [InlineData(TestExprType.Integer, TestExprType.NonesDataset, /*result:*/ TestExprType.IntsDataset)]
        [InlineData(TestExprType.Integer, TestExprType.MixedNoneIntDataset, /*result:*/ TestExprType.IntsDataset)]
        [InlineData(TestExprType.Integer, TestExprType.MixedNoneNumDataset, /*result:*/ TestExprType.MixedIntNumDataset)]
        [InlineData(TestExprType.Number, TestExprType.IntsDataset, /*result:*/ TestExprType.NumbersDataset)]
        [InlineData(TestExprType.Number, TestExprType.NumbersDataset, /*result:*/ TestExprType.NumbersDataset)]
        [InlineData(TestExprType.Number, TestExprType.MixedIntNumDataset, /*result:*/ TestExprType.NumbersDataset)]
        [InlineData(TestExprType.Number, TestExprType.NonesDataset, /*result:*/ TestExprType.NumbersDataset)]
        [InlineData(TestExprType.Number, TestExprType.MixedNoneIntDataset, /*result:*/ TestExprType.NumbersDataset)]
        [InlineData(TestExprType.Number, TestExprType.MixedNoneNumDataset, /*result:*/ TestExprType.NumbersDataset)]
        [InlineData(TestExprType.String, TestExprType.StringsDataset, /*result:*/ TestExprType.StringsDataset)]
        [InlineData(TestExprType.String, TestExprType.NonesDataset, /*result:*/ TestExprType.StringsDataset)]
        [InlineData(TestExprType.String, TestExprType.MixedNoneStrDataset, /*result:*/ TestExprType.StringsDataset)]
        [InlineData(TestExprType.Boolean, TestExprType.BoolsDataset, /*result:*/ TestExprType.BoolsDataset)]
        [InlineData(TestExprType.Boolean, TestExprType.NonesDataset, /*result:*/ TestExprType.BoolsDataset)]
        [InlineData(TestExprType.Boolean, TestExprType.MixedNoneBoolDataset, /*result:*/ TestExprType.BoolsDataset)]
        [InlineData(TestExprType.Time, TestExprType.TimesDataset, /*result:*/ TestExprType.TimesDataset)]
        [InlineData(TestExprType.Time, TestExprType.NonesDataset, /*result:*/ TestExprType.TimesDataset)]
        [InlineData(TestExprType.Time, TestExprType.MixedNoneTimeDataset, /*result:*/ TestExprType.TimesDataset)]
        [InlineData(TestExprType.Date, TestExprType.DatesDataset, /*result:*/ TestExprType.DatesDataset)]
        [InlineData(TestExprType.Date, TestExprType.NonesDataset, /*result:*/ TestExprType.DatesDataset)]
        [InlineData(TestExprType.Date, TestExprType.MixedNoneDateDataset, /*result:*/ TestExprType.DatesDataset)]
        [InlineData(TestExprType.TimePeriod, TestExprType.TimePeriodsDataset, /*result:*/ TestExprType.TimePeriodsDataset)]
        [InlineData(TestExprType.TimePeriod, TestExprType.NonesDataset, /*result:*/ TestExprType.TimePeriodsDataset)]
        [InlineData(TestExprType.TimePeriod, TestExprType.MixedNoneTimePerDataset, /*result:*/ TestExprType.TimePeriodsDataset)]
        [InlineData(TestExprType.Duration, TestExprType.DurationsDataset, /*result:*/ TestExprType.DurationsDataset)]
        [InlineData(TestExprType.Duration, TestExprType.NonesDataset, /*result:*/ TestExprType.DurationsDataset)]
        [InlineData(TestExprType.Duration, TestExprType.MixedNoneDurDataset, /*result:*/ TestExprType.DurationsDataset)]
        [InlineData(TestExprType.None, TestExprType.NonesDataset, /*result:*/ TestExprType.NonesDataset)]
        [InlineData(TestExprType.None, TestExprType.IntsDataset, /*result:*/ TestExprType.IntsDataset)]
        [InlineData(TestExprType.None, TestExprType.NumbersDataset, /*result:*/ TestExprType.NumbersDataset)]
        [InlineData(TestExprType.None, TestExprType.StringsDataset, /*result:*/ TestExprType.StringsDataset)]
        [InlineData(TestExprType.None, TestExprType.BoolsDataset, /*result:*/ TestExprType.BoolsDataset)]
        [InlineData(TestExprType.None, TestExprType.TimesDataset, /*result:*/ TestExprType.TimesDataset)]
        [InlineData(TestExprType.None, TestExprType.DatesDataset, /*result:*/ TestExprType.DatesDataset)]
        [InlineData(TestExprType.None, TestExprType.TimePeriodsDataset, /*result:*/ TestExprType.TimePeriodsDataset)]
        [InlineData(TestExprType.None, TestExprType.DurationsDataset, /*result:*/ TestExprType.DurationsDataset)]
        [InlineData(TestExprType.None, TestExprType.MixedIntNumDataset, /*result:*/ TestExprType.MixedIntNumDataset)]
        [InlineData(TestExprType.None, TestExprType.MixedNoneIntDataset, /*result:*/ TestExprType.MixedNoneIntDataset)]
        [InlineData(TestExprType.None, TestExprType.MixedNoneNumDataset, /*result:*/ TestExprType.MixedNoneNumDataset)]
        [InlineData(TestExprType.None, TestExprType.MixedNoneStrDataset, /*result:*/ TestExprType.MixedNoneStrDataset)]
        [InlineData(TestExprType.None, TestExprType.MixedNoneBoolDataset, /*result:*/ TestExprType.MixedNoneBoolDataset)]
        [InlineData(TestExprType.None, TestExprType.MixedNoneTimeDataset, /*result:*/ TestExprType.MixedNoneTimeDataset)]
        [InlineData(TestExprType.None, TestExprType.MixedNoneDateDataset, /*result:*/ TestExprType.MixedNoneDateDataset)]
        [InlineData(TestExprType.None, TestExprType.MixedNoneTimePerDataset, /*result:*/ TestExprType.MixedNoneTimePerDataset)]
        [InlineData(TestExprType.None, TestExprType.MixedNoneDurDataset, /*result:*/ TestExprType.MixedNoneDurDataset)]
        public void GetOutputStructure_CorrectDatasetConditionNot2ScalarsExpr_DataStructure(params TestExprType[] types)
        {
            IExpression ifExpr = this._ifThenElseExpr.Operands["if"];
            IExpression thenExpr = this._ifThenElseExpr.Operands["then"];
            IExpression elseExpr = this._ifThenElseExpr.Operands["else"];

            ifExpr.Operands["ds_1"] = TestExprFactory.GetExpression(TestExprType.Boolean);
            ifExpr.Operands["ds_1"].Structure.Identifiers.Add(new StructureComponent(BasicDataType.Integer, "Id1"));
            thenExpr.Operands["ds_1"] = TestExprFactory.GetExpression(types[0]);
            elseExpr.Operands["ds_1"] = TestExprFactory.GetExpression(types[1]);

            IExpression expected = TestExprFactory.GetExpression(types[2]);

            IDataStructure dataStructure = this._ifThenElseExpr.OperatorDefinition.GetOutputStructure(this._ifThenElseExpr);

            Assert.True(expected.Structure.EqualsObj(dataStructure));
        }

        [Theory]
        [InlineData(TestExprType.IntsDataset, TestExprType.Integer, /*result:*/ TestExprType.IntsDataset)]
        [InlineData(TestExprType.IntsDataset, TestExprType.Number, /*result:*/ TestExprType.NumbersDataset)]
        [InlineData(TestExprType.IntsDataset, TestExprType.None, /*result:*/ TestExprType.IntsDataset)]
        [InlineData(TestExprType.NumbersDataset, TestExprType.Integer, /*result:*/ TestExprType.NumbersDataset)]
        [InlineData(TestExprType.NumbersDataset, TestExprType.Number, /*result:*/ TestExprType.NumbersDataset)]
        [InlineData(TestExprType.NumbersDataset, TestExprType.None, /*result:*/ TestExprType.NumbersDataset)]
        [InlineData(TestExprType.StringsDataset, TestExprType.String, /*result:*/ TestExprType.StringsDataset)]
        [InlineData(TestExprType.StringsDataset, TestExprType.None, /*result:*/ TestExprType.StringsDataset)]
        [InlineData(TestExprType.BoolsDataset, TestExprType.Boolean, /*result:*/ TestExprType.BoolsDataset)]
        [InlineData(TestExprType.BoolsDataset, TestExprType.None, /*result:*/ TestExprType.BoolsDataset)]
        [InlineData(TestExprType.TimesDataset, TestExprType.Time, /*result:*/ TestExprType.TimesDataset)]
        [InlineData(TestExprType.TimesDataset, TestExprType.None, /*result:*/ TestExprType.TimesDataset)]
        [InlineData(TestExprType.DatesDataset, TestExprType.Date, /*result:*/ TestExprType.DatesDataset)]
        [InlineData(TestExprType.DatesDataset, TestExprType.None, /*result:*/ TestExprType.DatesDataset)]
        [InlineData(TestExprType.TimePeriodsDataset, TestExprType.TimePeriod, /*result:*/ TestExprType.TimePeriodsDataset)]
        [InlineData(TestExprType.TimePeriodsDataset, TestExprType.None, /*result:*/ TestExprType.TimePeriodsDataset)]
        [InlineData(TestExprType.DurationsDataset, TestExprType.Duration, /*result:*/ TestExprType.DurationsDataset)]
        [InlineData(TestExprType.DurationsDataset, TestExprType.None, /*result:*/ TestExprType.DurationsDataset)]
        [InlineData(TestExprType.NonesDataset, TestExprType.None, /*result:*/ TestExprType.NonesDataset)]
        [InlineData(TestExprType.NonesDataset, TestExprType.Integer, /*result:*/ TestExprType.IntsDataset)]
        [InlineData(TestExprType.NonesDataset, TestExprType.Number, /*result:*/ TestExprType.NumbersDataset)]
        [InlineData(TestExprType.NonesDataset, TestExprType.String, /*result:*/ TestExprType.StringsDataset)]
        [InlineData(TestExprType.NonesDataset, TestExprType.Boolean, /*result:*/ TestExprType.BoolsDataset)]
        [InlineData(TestExprType.NonesDataset, TestExprType.Time, /*result:*/ TestExprType.TimesDataset)]
        [InlineData(TestExprType.NonesDataset, TestExprType.Date, /*result:*/ TestExprType.DatesDataset)]
        [InlineData(TestExprType.NonesDataset, TestExprType.TimePeriod, /*result:*/ TestExprType.TimePeriodsDataset)]
        [InlineData(TestExprType.NonesDataset, TestExprType.Duration, /*result:*/ TestExprType.DurationsDataset)]
        [InlineData(TestExprType.MixedIntNumDataset, TestExprType.Integer, /*result:*/ TestExprType.MixedIntNumDataset)]
        [InlineData(TestExprType.MixedIntNumDataset, TestExprType.Number, /*result:*/ TestExprType.NumbersDataset)]
        [InlineData(TestExprType.MixedIntNumDataset, TestExprType.None, /*result:*/ TestExprType.MixedIntNumDataset)]
        [InlineData(TestExprType.MixedNoneIntDataset, TestExprType.Integer, /*result:*/ TestExprType.IntsDataset)]
        [InlineData(TestExprType.MixedNoneIntDataset, TestExprType.Number, /*result:*/ TestExprType.NumbersDataset)]
        [InlineData(TestExprType.MixedNoneIntDataset, TestExprType.None, /*result:*/ TestExprType.MixedNoneIntDataset)]
        [InlineData(TestExprType.MixedNoneNumDataset, TestExprType.Integer, /*result:*/ TestExprType.MixedIntNumDataset)]
        [InlineData(TestExprType.MixedNoneNumDataset, TestExprType.Number, /*result:*/ TestExprType.NumbersDataset)]
        [InlineData(TestExprType.MixedNoneNumDataset, TestExprType.None, /*result:*/ TestExprType.MixedNoneNumDataset)]
        [InlineData(TestExprType.MixedNoneStrDataset, TestExprType.String, /*result:*/ TestExprType.StringsDataset)]
        [InlineData(TestExprType.MixedNoneStrDataset, TestExprType.None, /*result:*/ TestExprType.MixedNoneStrDataset)]
        [InlineData(TestExprType.MixedNoneBoolDataset, TestExprType.Boolean, /*result:*/ TestExprType.BoolsDataset)]
        [InlineData(TestExprType.MixedNoneBoolDataset, TestExprType.None, /*result:*/ TestExprType.MixedNoneBoolDataset)]
        [InlineData(TestExprType.MixedNoneTimeDataset, TestExprType.Time, /*result:*/ TestExprType.TimesDataset)]
        [InlineData(TestExprType.MixedNoneTimeDataset, TestExprType.None, /*result:*/ TestExprType.MixedNoneTimeDataset)]
        [InlineData(TestExprType.MixedNoneDateDataset, TestExprType.Date, /*result:*/ TestExprType.DatesDataset)]
        [InlineData(TestExprType.MixedNoneDateDataset, TestExprType.None, /*result:*/ TestExprType.MixedNoneDateDataset)]
        [InlineData(TestExprType.MixedNoneTimePerDataset, TestExprType.TimePeriod, /*result:*/ TestExprType.TimePeriodsDataset)]
        [InlineData(TestExprType.MixedNoneTimePerDataset, TestExprType.None, /*result:*/ TestExprType.MixedNoneTimePerDataset)]
        [InlineData(TestExprType.MixedNoneDurDataset, TestExprType.Duration, /*result:*/ TestExprType.DurationsDataset)]
        [InlineData(TestExprType.MixedNoneDurDataset, TestExprType.None, /*result:*/ TestExprType.MixedNoneDurDataset)]
        public void GetOutputStructure_CorrectNoCalc2DatasetsComponentExpr_DataStructure(params TestExprType[] types)
        {
            IExpression ifExpr = this._ifThenElseExpr.Operands["if"];
            IExpression thenExpr = this._ifThenElseExpr.Operands["then"];
            IExpression elseExpr = this._ifThenElseExpr.Operands["else"];

            ifExpr.Operands["ds_1"] = TestExprFactory.GetExpression(TestExprType.Boolean);
            ifExpr.Operands["ds_1"].Structure.Identifiers.Add(new StructureComponent(BasicDataType.Integer, "Id1"));
            thenExpr.Operands["ds_1"] = TestExprFactory.GetExpression(types[1]);
            elseExpr.Operands["ds_1"] = TestExprFactory.GetExpression(types[0]);

            elseExpr.Operands["ds_1"].ResultName = "Component";

            IExpression expected = TestExprFactory.GetExpression(types[2]);

            IDataStructure dataStructure = this._ifThenElseExpr.OperatorDefinition.GetOutputStructure(this._ifThenElseExpr);

            Assert.True(expected.Structure.EqualsObj(dataStructure));
        }

        [Theory]
        [InlineData(TestExprType.Integer, TestExprType.IntsDataset, /*result:*/ TestExprType.IntsDataset)]
        [InlineData(TestExprType.Integer, TestExprType.NumbersDataset, /*result:*/ TestExprType.NumbersDataset)]
        [InlineData(TestExprType.Integer, TestExprType.MixedIntNumDataset, /*result:*/ TestExprType.MixedIntNumDataset)]
        [InlineData(TestExprType.Integer, TestExprType.NonesDataset, /*result:*/ TestExprType.IntsDataset)]
        [InlineData(TestExprType.Integer, TestExprType.MixedNoneIntDataset, /*result:*/ TestExprType.IntsDataset)]
        [InlineData(TestExprType.Integer, TestExprType.MixedNoneNumDataset, /*result:*/ TestExprType.MixedIntNumDataset)]
        [InlineData(TestExprType.Number, TestExprType.IntsDataset, /*result:*/ TestExprType.NumbersDataset)]
        [InlineData(TestExprType.Number, TestExprType.NumbersDataset, /*result:*/ TestExprType.NumbersDataset)]
        [InlineData(TestExprType.Number, TestExprType.MixedIntNumDataset, /*result:*/ TestExprType.NumbersDataset)]
        [InlineData(TestExprType.Number, TestExprType.NonesDataset, /*result:*/ TestExprType.NumbersDataset)]
        [InlineData(TestExprType.Number, TestExprType.MixedNoneIntDataset, /*result:*/ TestExprType.NumbersDataset)]
        [InlineData(TestExprType.Number, TestExprType.MixedNoneNumDataset, /*result:*/ TestExprType.NumbersDataset)]
        [InlineData(TestExprType.String, TestExprType.StringsDataset, /*result:*/ TestExprType.StringsDataset)]
        [InlineData(TestExprType.String, TestExprType.NonesDataset, /*result:*/ TestExprType.StringsDataset)]
        [InlineData(TestExprType.String, TestExprType.MixedNoneStrDataset, /*result:*/ TestExprType.StringsDataset)]
        [InlineData(TestExprType.Boolean, TestExprType.BoolsDataset, /*result:*/ TestExprType.BoolsDataset)]
        [InlineData(TestExprType.Boolean, TestExprType.NonesDataset, /*result:*/ TestExprType.BoolsDataset)]
        [InlineData(TestExprType.Boolean, TestExprType.MixedNoneBoolDataset, /*result:*/ TestExprType.BoolsDataset)]
        [InlineData(TestExprType.Time, TestExprType.TimesDataset, /*result:*/ TestExprType.TimesDataset)]
        [InlineData(TestExprType.Time, TestExprType.NonesDataset, /*result:*/ TestExprType.TimesDataset)]
        [InlineData(TestExprType.Time, TestExprType.MixedNoneTimeDataset, /*result:*/ TestExprType.TimesDataset)]
        [InlineData(TestExprType.Date, TestExprType.DatesDataset, /*result:*/ TestExprType.DatesDataset)]
        [InlineData(TestExprType.Date, TestExprType.NonesDataset, /*result:*/ TestExprType.DatesDataset)]
        [InlineData(TestExprType.Date, TestExprType.MixedNoneDateDataset, /*result:*/ TestExprType.DatesDataset)]
        [InlineData(TestExprType.TimePeriod, TestExprType.TimePeriodsDataset, /*result:*/ TestExprType.TimePeriodsDataset)]
        [InlineData(TestExprType.TimePeriod, TestExprType.NonesDataset, /*result:*/ TestExprType.TimePeriodsDataset)]
        [InlineData(TestExprType.TimePeriod, TestExprType.MixedNoneTimePerDataset, /*result:*/ TestExprType.TimePeriodsDataset)]
        [InlineData(TestExprType.Duration, TestExprType.DurationsDataset, /*result:*/ TestExprType.DurationsDataset)]
        [InlineData(TestExprType.Duration, TestExprType.NonesDataset, /*result:*/ TestExprType.DurationsDataset)]
        [InlineData(TestExprType.Duration, TestExprType.MixedNoneDurDataset, /*result:*/ TestExprType.DurationsDataset)]
        [InlineData(TestExprType.None, TestExprType.NonesDataset, /*result:*/ TestExprType.NonesDataset)]
        [InlineData(TestExprType.None, TestExprType.IntsDataset, /*result:*/ TestExprType.IntsDataset)]
        [InlineData(TestExprType.None, TestExprType.NumbersDataset, /*result:*/ TestExprType.NumbersDataset)]
        [InlineData(TestExprType.None, TestExprType.StringsDataset, /*result:*/ TestExprType.StringsDataset)]
        [InlineData(TestExprType.None, TestExprType.BoolsDataset, /*result:*/ TestExprType.BoolsDataset)]
        [InlineData(TestExprType.None, TestExprType.TimesDataset, /*result:*/ TestExprType.TimesDataset)]
        [InlineData(TestExprType.None, TestExprType.DatesDataset, /*result:*/ TestExprType.DatesDataset)]
        [InlineData(TestExprType.None, TestExprType.TimePeriodsDataset, /*result:*/ TestExprType.TimePeriodsDataset)]
        [InlineData(TestExprType.None, TestExprType.DurationsDataset, /*result:*/ TestExprType.DurationsDataset)]
        [InlineData(TestExprType.None, TestExprType.MixedIntNumDataset, /*result:*/ TestExprType.MixedIntNumDataset)]
        [InlineData(TestExprType.None, TestExprType.MixedNoneIntDataset, /*result:*/ TestExprType.MixedNoneIntDataset)]
        [InlineData(TestExprType.None, TestExprType.MixedNoneNumDataset, /*result:*/ TestExprType.MixedNoneNumDataset)]
        [InlineData(TestExprType.None, TestExprType.MixedNoneStrDataset, /*result:*/ TestExprType.MixedNoneStrDataset)]
        [InlineData(TestExprType.None, TestExprType.MixedNoneBoolDataset, /*result:*/ TestExprType.MixedNoneBoolDataset)]
        [InlineData(TestExprType.None, TestExprType.MixedNoneTimeDataset, /*result:*/ TestExprType.MixedNoneTimeDataset)]
        [InlineData(TestExprType.None, TestExprType.MixedNoneDateDataset, /*result:*/ TestExprType.MixedNoneDateDataset)]
        [InlineData(TestExprType.None, TestExprType.MixedNoneTimePerDataset, /*result:*/ TestExprType.MixedNoneTimePerDataset)]
        [InlineData(TestExprType.None, TestExprType.MixedNoneDurDataset, /*result:*/ TestExprType.MixedNoneDurDataset)]
        public void GetOutputStructure_CorrectNoCalcDatasetComponentDatasetExpr_DataStructure(params TestExprType[] types)
        {
            IExpression ifExpr = this._ifThenElseExpr.Operands["if"];
            IExpression thenExpr = this._ifThenElseExpr.Operands["then"];
            IExpression elseExpr = this._ifThenElseExpr.Operands["else"];

            ifExpr.Operands["ds_1"] = TestExprFactory.GetExpression(TestExprType.Boolean);
            ifExpr.Operands["ds_1"].Structure.Identifiers.Add(new StructureComponent(BasicDataType.Integer, "Id1"));
            thenExpr.Operands["ds_1"] = TestExprFactory.GetExpression(types[1]);
            elseExpr.Operands["ds_1"] = TestExprFactory.GetExpression(types[0]);

            thenExpr.Operands["ds_1"].ResultName = "Component";

            IExpression expected = TestExprFactory.GetExpression(types[2]);

            IDataStructure dataStructure = this._ifThenElseExpr.OperatorDefinition.GetOutputStructure(this._ifThenElseExpr);

            Assert.True(expected.Structure.EqualsObj(dataStructure));
        }

        [Fact]
        public void GetOutputStructure_CorrectNoCalcDataset2ComponentsExpr_DataStructure()
        {
            IExpression ifExpr = this._ifThenElseExpr.Operands["if"];
            IExpression thenExpr = this._ifThenElseExpr.Operands["then"];
            IExpression elseExpr = this._ifThenElseExpr.Operands["else"];

            ifExpr.Operands["ds_1"] = TestExprFactory.GetExpression(TestExprType.Boolean);
            ifExpr.Operands["ds_1"].Structure.Identifiers.Add(new StructureComponent(BasicDataType.Integer, "Id1"));
            thenExpr.Operands["ds_1"] = TestExprFactory.GetExpression(TestExprType.Boolean);
            elseExpr.Operands["ds_1"] = TestExprFactory.GetExpression(TestExprType.Boolean);

            thenExpr.Operands["ds_1"].ResultName = "Component";
            elseExpr.Operands["ds_1"].ResultName = "Component";

            IExpression expected = TestExprFactory.GetExpression(TestExprType.Boolean);
            expected.Structure.Identifiers.Add(new StructureComponent(BasicDataType.Integer, "Id1"));

            IDataStructure dataStructure = this._ifThenElseExpr.OperatorDefinition.GetOutputStructure(this._ifThenElseExpr);

            Assert.True(expected.Structure.EqualsObj(dataStructure));
        }

        [Theory]
        [InlineData(TestExprType.Integer, TestExprType.IntsDataset, /*result:*/ TestExprType.IntsDataset)]
        [InlineData(TestExprType.Integer, TestExprType.NumbersDataset, /*result:*/ TestExprType.NumbersDataset)]
        [InlineData(TestExprType.Integer, TestExprType.MixedIntNumDataset, /*result:*/ TestExprType.MixedIntNumDataset)]
        [InlineData(TestExprType.Integer, TestExprType.NonesDataset, /*result:*/ TestExprType.IntsDataset)]
        [InlineData(TestExprType.Integer, TestExprType.MixedNoneIntDataset, /*result:*/ TestExprType.IntsDataset)]
        [InlineData(TestExprType.Integer, TestExprType.MixedNoneNumDataset, /*result:*/ TestExprType.MixedIntNumDataset)]
        [InlineData(TestExprType.Number, TestExprType.IntsDataset, /*result:*/ TestExprType.NumbersDataset)]
        [InlineData(TestExprType.Number, TestExprType.NumbersDataset, /*result:*/ TestExprType.NumbersDataset)]
        [InlineData(TestExprType.Number, TestExprType.MixedIntNumDataset, /*result:*/ TestExprType.NumbersDataset)]
        [InlineData(TestExprType.Number, TestExprType.NonesDataset, /*result:*/ TestExprType.NumbersDataset)]
        [InlineData(TestExprType.Number, TestExprType.MixedNoneIntDataset, /*result:*/ TestExprType.NumbersDataset)]
        [InlineData(TestExprType.Number, TestExprType.MixedNoneNumDataset, /*result:*/ TestExprType.NumbersDataset)]
        [InlineData(TestExprType.String, TestExprType.StringsDataset, /*result:*/ TestExprType.StringsDataset)]
        [InlineData(TestExprType.String, TestExprType.NonesDataset, /*result:*/ TestExprType.StringsDataset)]
        [InlineData(TestExprType.String, TestExprType.MixedNoneStrDataset, /*result:*/ TestExprType.StringsDataset)]
        [InlineData(TestExprType.Boolean, TestExprType.BoolsDataset, /*result:*/ TestExprType.BoolsDataset)]
        [InlineData(TestExprType.Boolean, TestExprType.NonesDataset, /*result:*/ TestExprType.BoolsDataset)]
        [InlineData(TestExprType.Boolean, TestExprType.MixedNoneBoolDataset, /*result:*/ TestExprType.BoolsDataset)]
        [InlineData(TestExprType.Time, TestExprType.TimesDataset, /*result:*/ TestExprType.TimesDataset)]
        [InlineData(TestExprType.Time, TestExprType.NonesDataset, /*result:*/ TestExprType.TimesDataset)]
        [InlineData(TestExprType.Time, TestExprType.MixedNoneTimeDataset, /*result:*/ TestExprType.TimesDataset)]
        [InlineData(TestExprType.Date, TestExprType.DatesDataset, /*result:*/ TestExprType.DatesDataset)]
        [InlineData(TestExprType.Date, TestExprType.NonesDataset, /*result:*/ TestExprType.DatesDataset)]
        [InlineData(TestExprType.Date, TestExprType.MixedNoneDateDataset, /*result:*/ TestExprType.DatesDataset)]
        [InlineData(TestExprType.TimePeriod, TestExprType.TimePeriodsDataset, /*result:*/ TestExprType.TimePeriodsDataset)]
        [InlineData(TestExprType.TimePeriod, TestExprType.NonesDataset, /*result:*/ TestExprType.TimePeriodsDataset)]
        [InlineData(TestExprType.TimePeriod, TestExprType.MixedNoneTimePerDataset, /*result:*/ TestExprType.TimePeriodsDataset)]
        [InlineData(TestExprType.Duration, TestExprType.DurationsDataset, /*result:*/ TestExprType.DurationsDataset)]
        [InlineData(TestExprType.Duration, TestExprType.NonesDataset, /*result:*/ TestExprType.DurationsDataset)]
        [InlineData(TestExprType.Duration, TestExprType.MixedNoneDurDataset, /*result:*/ TestExprType.DurationsDataset)]
        [InlineData(TestExprType.None, TestExprType.NonesDataset, /*result:*/ TestExprType.NonesDataset)]
        [InlineData(TestExprType.None, TestExprType.IntsDataset, /*result:*/ TestExprType.IntsDataset)]
        [InlineData(TestExprType.None, TestExprType.NumbersDataset, /*result:*/ TestExprType.NumbersDataset)]
        [InlineData(TestExprType.None, TestExprType.StringsDataset, /*result:*/ TestExprType.StringsDataset)]
        [InlineData(TestExprType.None, TestExprType.BoolsDataset, /*result:*/ TestExprType.BoolsDataset)]
        [InlineData(TestExprType.None, TestExprType.TimesDataset, /*result:*/ TestExprType.TimesDataset)]
        [InlineData(TestExprType.None, TestExprType.DatesDataset, /*result:*/ TestExprType.DatesDataset)]
        [InlineData(TestExprType.None, TestExprType.TimePeriodsDataset, /*result:*/ TestExprType.TimePeriodsDataset)]
        [InlineData(TestExprType.None, TestExprType.DurationsDataset, /*result:*/ TestExprType.DurationsDataset)]
        [InlineData(TestExprType.None, TestExprType.MixedIntNumDataset, /*result:*/ TestExprType.MixedIntNumDataset)]
        [InlineData(TestExprType.None, TestExprType.MixedNoneIntDataset, /*result:*/ TestExprType.MixedNoneIntDataset)]
        [InlineData(TestExprType.None, TestExprType.MixedNoneNumDataset, /*result:*/ TestExprType.MixedNoneNumDataset)]
        [InlineData(TestExprType.None, TestExprType.MixedNoneStrDataset, /*result:*/ TestExprType.MixedNoneStrDataset)]
        [InlineData(TestExprType.None, TestExprType.MixedNoneBoolDataset, /*result:*/ TestExprType.MixedNoneBoolDataset)]
        [InlineData(TestExprType.None, TestExprType.MixedNoneTimeDataset, /*result:*/ TestExprType.MixedNoneTimeDataset)]
        [InlineData(TestExprType.None, TestExprType.MixedNoneDateDataset, /*result:*/ TestExprType.MixedNoneDateDataset)]
        [InlineData(TestExprType.None, TestExprType.MixedNoneTimePerDataset, /*result:*/ TestExprType.MixedNoneTimePerDataset)]
        [InlineData(TestExprType.None, TestExprType.MixedNoneDurDataset, /*result:*/ TestExprType.MixedNoneDurDataset)]
        public void GetOutputStructure_CorrectNoCalc2ComponentsDatasetExpr_DataStructure(params TestExprType[] types)
        {
            IExpression ifExpr = this._ifThenElseExpr.Operands["if"];
            IExpression thenExpr = this._ifThenElseExpr.Operands["then"];
            IExpression elseExpr = this._ifThenElseExpr.Operands["else"];

            ifExpr.Operands["ds_1"] = TestExprFactory.GetExpression(TestExprType.Boolean);
            thenExpr.Operands["ds_1"] = TestExprFactory.GetExpression(types[0]);
            elseExpr.Operands["ds_1"] = TestExprFactory.GetExpression(types[1]);

            ifExpr.Operands["ds_1"].ResultName = "Component";
            thenExpr.Operands["ds_1"].ResultName = "Component";

            IExpression expected = TestExprFactory.GetExpression(types[2]);

            IDataStructure dataStructure = this._ifThenElseExpr.OperatorDefinition.GetOutputStructure(this._ifThenElseExpr);

            Assert.True(expected.Structure.EqualsObj(dataStructure));
        }

        [Theory]
        [InlineData(TestExprType.IntsDataset, TestExprType.IntsDataset, /*result:*/ TestExprType.IntsDataset)]
        [InlineData(TestExprType.IntsDataset, TestExprType.NumbersDataset, /*result:*/ TestExprType.NumbersDataset)]
        [InlineData(TestExprType.IntsDataset, TestExprType.MixedIntNumDataset, /*result:*/ TestExprType.MixedIntNumDataset)]
        [InlineData(TestExprType.IntsDataset, TestExprType.NonesDataset, /*result:*/ TestExprType.IntsDataset)]
        [InlineData(TestExprType.IntsDataset, TestExprType.MixedNoneIntDataset, /*result:*/ TestExprType.IntsDataset)]
        [InlineData(TestExprType.IntsDataset, TestExprType.MixedNoneNumDataset, /*result:*/ TestExprType.MixedIntNumDataset)]
        [InlineData(TestExprType.NumbersDataset, TestExprType.IntsDataset, /*result:*/ TestExprType.NumbersDataset)]
        [InlineData(TestExprType.NumbersDataset, TestExprType.NumbersDataset, /*result:*/ TestExprType.NumbersDataset)]
        [InlineData(TestExprType.NumbersDataset, TestExprType.MixedIntNumDataset, /*result:*/ TestExprType.NumbersDataset)]
        [InlineData(TestExprType.NumbersDataset, TestExprType.NonesDataset, /*result:*/ TestExprType.NumbersDataset)]
        [InlineData(TestExprType.NumbersDataset, TestExprType.MixedNoneIntDataset, /*result:*/ TestExprType.NumbersDataset)]
        [InlineData(TestExprType.NumbersDataset, TestExprType.MixedNoneNumDataset, /*result:*/ TestExprType.NumbersDataset)]
        [InlineData(TestExprType.StringsDataset, TestExprType.StringsDataset, /*result:*/ TestExprType.StringsDataset)]
        [InlineData(TestExprType.StringsDataset, TestExprType.NonesDataset, /*result:*/ TestExprType.StringsDataset)]
        [InlineData(TestExprType.StringsDataset, TestExprType.MixedNoneStrDataset, /*result:*/ TestExprType.StringsDataset)]
        [InlineData(TestExprType.BoolsDataset, TestExprType.BoolsDataset, /*result:*/ TestExprType.BoolsDataset)]
        [InlineData(TestExprType.BoolsDataset, TestExprType.NonesDataset, /*result:*/ TestExprType.BoolsDataset)]
        [InlineData(TestExprType.BoolsDataset, TestExprType.MixedNoneBoolDataset, /*result:*/ TestExprType.BoolsDataset)]
        [InlineData(TestExprType.TimesDataset, TestExprType.TimesDataset, /*result:*/ TestExprType.TimesDataset)]
        [InlineData(TestExprType.TimesDataset, TestExprType.NonesDataset, /*result:*/ TestExprType.TimesDataset)]
        [InlineData(TestExprType.TimesDataset, TestExprType.MixedNoneTimeDataset, /*result:*/ TestExprType.TimesDataset)]
        [InlineData(TestExprType.DatesDataset, TestExprType.DatesDataset, /*result:*/ TestExprType.DatesDataset)]
        [InlineData(TestExprType.DatesDataset, TestExprType.NonesDataset, /*result:*/ TestExprType.DatesDataset)]
        [InlineData(TestExprType.DatesDataset, TestExprType.MixedNoneDateDataset, /*result:*/ TestExprType.DatesDataset)]
        [InlineData(TestExprType.TimePeriodsDataset, TestExprType.TimePeriodsDataset, /*result:*/ TestExprType.TimePeriodsDataset)]
        [InlineData(TestExprType.TimePeriodsDataset, TestExprType.NonesDataset, /*result:*/ TestExprType.TimePeriodsDataset)]
        [InlineData(TestExprType.TimePeriodsDataset, TestExprType.MixedNoneTimePerDataset, /*result:*/ TestExprType.TimePeriodsDataset)]
        [InlineData(TestExprType.DurationsDataset, TestExprType.DurationsDataset, /*result:*/ TestExprType.DurationsDataset)]
        [InlineData(TestExprType.DurationsDataset, TestExprType.NonesDataset, /*result:*/ TestExprType.DurationsDataset)]
        [InlineData(TestExprType.DurationsDataset, TestExprType.MixedNoneDurDataset, /*result:*/ TestExprType.DurationsDataset)]
        [InlineData(TestExprType.NonesDataset, TestExprType.NonesDataset, /*result:*/ TestExprType.NonesDataset)]
        [InlineData(TestExprType.NonesDataset, TestExprType.IntsDataset, /*result:*/ TestExprType.IntsDataset)]
        [InlineData(TestExprType.NonesDataset, TestExprType.NumbersDataset, /*result:*/ TestExprType.NumbersDataset)]
        [InlineData(TestExprType.NonesDataset, TestExprType.StringsDataset, /*result:*/ TestExprType.StringsDataset)]
        [InlineData(TestExprType.NonesDataset, TestExprType.BoolsDataset, /*result:*/ TestExprType.BoolsDataset)]
        [InlineData(TestExprType.NonesDataset, TestExprType.TimesDataset, /*result:*/ TestExprType.TimesDataset)]
        [InlineData(TestExprType.NonesDataset, TestExprType.DatesDataset, /*result:*/ TestExprType.DatesDataset)]
        [InlineData(TestExprType.NonesDataset, TestExprType.TimePeriodsDataset, /*result:*/ TestExprType.TimePeriodsDataset)]
        [InlineData(TestExprType.NonesDataset, TestExprType.DurationsDataset, /*result:*/ TestExprType.DurationsDataset)]
        [InlineData(TestExprType.NonesDataset, TestExprType.MixedIntNumDataset, /*result:*/ TestExprType.MixedIntNumDataset)]
        [InlineData(TestExprType.NonesDataset, TestExprType.MixedNoneIntDataset, /*result:*/ TestExprType.MixedNoneIntDataset)]
        [InlineData(TestExprType.NonesDataset, TestExprType.MixedNoneNumDataset, /*result:*/ TestExprType.MixedNoneNumDataset)]
        [InlineData(TestExprType.NonesDataset, TestExprType.MixedNoneStrDataset, /*result:*/ TestExprType.MixedNoneStrDataset)]
        [InlineData(TestExprType.NonesDataset, TestExprType.MixedNoneBoolDataset, /*result:*/ TestExprType.MixedNoneBoolDataset)]
        [InlineData(TestExprType.NonesDataset, TestExprType.MixedNoneTimeDataset, /*result:*/ TestExprType.MixedNoneTimeDataset)]
        [InlineData(TestExprType.NonesDataset, TestExprType.MixedNoneDateDataset, /*result:*/ TestExprType.MixedNoneDateDataset)]
        [InlineData(TestExprType.NonesDataset, TestExprType.MixedNoneTimePerDataset, /*result:*/ TestExprType.MixedNoneTimePerDataset)]
        [InlineData(TestExprType.NonesDataset, TestExprType.MixedNoneDurDataset, /*result:*/ TestExprType.MixedNoneDurDataset)]
        [InlineData(TestExprType.MixedIntNumDataset, TestExprType.IntsDataset, /*result:*/ TestExprType.MixedIntNumDataset)]
        [InlineData(TestExprType.MixedIntNumDataset, TestExprType.NumbersDataset, /*result:*/ TestExprType.NumbersDataset)]
        [InlineData(TestExprType.MixedNoneIntDataset, TestExprType.IntsDataset, /*result:*/ TestExprType.IntsDataset)]
        [InlineData(TestExprType.MixedNoneNumDataset, TestExprType.NumbersDataset, /*result:*/ TestExprType.NumbersDataset)]
        [InlineData(TestExprType.MixedNoneStrDataset, TestExprType.StringsDataset, /*result:*/ TestExprType.StringsDataset)]
        [InlineData(TestExprType.MixedNoneBoolDataset, TestExprType.BoolsDataset, /*result:*/ TestExprType.BoolsDataset)]
        [InlineData(TestExprType.MixedNoneTimeDataset, TestExprType.TimesDataset, /*result:*/ TestExprType.TimesDataset)]
        [InlineData(TestExprType.MixedNoneDateDataset, TestExprType.DatesDataset, /*result:*/ TestExprType.DatesDataset)]
        [InlineData(TestExprType.MixedNoneTimePerDataset, TestExprType.TimePeriodsDataset, /*result:*/ TestExprType.TimePeriodsDataset)]
        [InlineData(TestExprType.MixedNoneDurDataset, TestExprType.DurationsDataset, /*result:*/ TestExprType.DurationsDataset)]
        public void GetOutputStructure_CorrectNoCalcComponent2DatasetsExpr_DataStructure(params TestExprType[] types)
        {
            IExpression ifExpr = this._ifThenElseExpr.Operands["if"];
            IExpression thenExpr = this._ifThenElseExpr.Operands["then"];
            IExpression elseExpr = this._ifThenElseExpr.Operands["else"];

            ifExpr.Operands["ds_1"] = TestExprFactory.GetExpression(TestExprType.Boolean);
            thenExpr.Operands["ds_1"] = TestExprFactory.GetExpression(types[0]);
            elseExpr.Operands["ds_1"] = TestExprFactory.GetExpression(types[1]);

            ifExpr.Operands["ds_1"].ResultName = "Component";

            IExpression expected = TestExprFactory.GetExpression(types[2]);

            IDataStructure dataStructure = this._ifThenElseExpr.OperatorDefinition.GetOutputStructure(this._ifThenElseExpr);

            Assert.True(expected.Structure.EqualsObj(dataStructure));
        }

        [Theory]
        [InlineData(TestExprType.Integer, TestExprType.IntsDataset, /*result:*/ TestExprType.IntsDataset)]
        [InlineData(TestExprType.Integer, TestExprType.NumbersDataset, /*result:*/ TestExprType.NumbersDataset)]
        [InlineData(TestExprType.Integer, TestExprType.MixedIntNumDataset, /*result:*/ TestExprType.MixedIntNumDataset)]
        [InlineData(TestExprType.Integer, TestExprType.NonesDataset, /*result:*/ TestExprType.IntsDataset)]
        [InlineData(TestExprType.Integer, TestExprType.MixedNoneIntDataset, /*result:*/ TestExprType.IntsDataset)]
        [InlineData(TestExprType.Integer, TestExprType.MixedNoneNumDataset, /*result:*/ TestExprType.MixedIntNumDataset)]
        [InlineData(TestExprType.Number, TestExprType.IntsDataset, /*result:*/ TestExprType.NumbersDataset)]
        [InlineData(TestExprType.Number, TestExprType.NumbersDataset, /*result:*/ TestExprType.NumbersDataset)]
        [InlineData(TestExprType.Number, TestExprType.MixedIntNumDataset, /*result:*/ TestExprType.NumbersDataset)]
        [InlineData(TestExprType.Number, TestExprType.NonesDataset, /*result:*/ TestExprType.NumbersDataset)]
        [InlineData(TestExprType.Number, TestExprType.MixedNoneIntDataset, /*result:*/ TestExprType.NumbersDataset)]
        [InlineData(TestExprType.Number, TestExprType.MixedNoneNumDataset, /*result:*/ TestExprType.NumbersDataset)]
        [InlineData(TestExprType.String, TestExprType.StringsDataset, /*result:*/ TestExprType.StringsDataset)]
        [InlineData(TestExprType.String, TestExprType.NonesDataset, /*result:*/ TestExprType.StringsDataset)]
        [InlineData(TestExprType.String, TestExprType.MixedNoneStrDataset, /*result:*/ TestExprType.StringsDataset)]
        [InlineData(TestExprType.Boolean, TestExprType.BoolsDataset, /*result:*/ TestExprType.BoolsDataset)]
        [InlineData(TestExprType.Boolean, TestExprType.NonesDataset, /*result:*/ TestExprType.BoolsDataset)]
        [InlineData(TestExprType.Boolean, TestExprType.MixedNoneBoolDataset, /*result:*/ TestExprType.BoolsDataset)]
        [InlineData(TestExprType.Time, TestExprType.TimesDataset, /*result:*/ TestExprType.TimesDataset)]
        [InlineData(TestExprType.Time, TestExprType.NonesDataset, /*result:*/ TestExprType.TimesDataset)]
        [InlineData(TestExprType.Time, TestExprType.MixedNoneTimeDataset, /*result:*/ TestExprType.TimesDataset)]
        [InlineData(TestExprType.Date, TestExprType.DatesDataset, /*result:*/ TestExprType.DatesDataset)]
        [InlineData(TestExprType.Date, TestExprType.NonesDataset, /*result:*/ TestExprType.DatesDataset)]
        [InlineData(TestExprType.Date, TestExprType.MixedNoneDateDataset, /*result:*/ TestExprType.DatesDataset)]
        [InlineData(TestExprType.TimePeriod, TestExprType.TimePeriodsDataset, /*result:*/ TestExprType.TimePeriodsDataset)]
        [InlineData(TestExprType.TimePeriod, TestExprType.NonesDataset, /*result:*/ TestExprType.TimePeriodsDataset)]
        [InlineData(TestExprType.TimePeriod, TestExprType.MixedNoneTimePerDataset, /*result:*/ TestExprType.TimePeriodsDataset)]
        [InlineData(TestExprType.Duration, TestExprType.DurationsDataset, /*result:*/ TestExprType.DurationsDataset)]
        [InlineData(TestExprType.Duration, TestExprType.NonesDataset, /*result:*/ TestExprType.DurationsDataset)]
        [InlineData(TestExprType.Duration, TestExprType.MixedNoneDurDataset, /*result:*/ TestExprType.DurationsDataset)]
        [InlineData(TestExprType.None, TestExprType.NonesDataset, /*result:*/ TestExprType.NonesDataset)]
        [InlineData(TestExprType.None, TestExprType.IntsDataset, /*result:*/ TestExprType.IntsDataset)]
        [InlineData(TestExprType.None, TestExprType.NumbersDataset, /*result:*/ TestExprType.NumbersDataset)]
        [InlineData(TestExprType.None, TestExprType.StringsDataset, /*result:*/ TestExprType.StringsDataset)]
        [InlineData(TestExprType.None, TestExprType.BoolsDataset, /*result:*/ TestExprType.BoolsDataset)]
        [InlineData(TestExprType.None, TestExprType.TimesDataset, /*result:*/ TestExprType.TimesDataset)]
        [InlineData(TestExprType.None, TestExprType.DatesDataset, /*result:*/ TestExprType.DatesDataset)]
        [InlineData(TestExprType.None, TestExprType.TimePeriodsDataset, /*result:*/ TestExprType.TimePeriodsDataset)]
        [InlineData(TestExprType.None, TestExprType.DurationsDataset, /*result:*/ TestExprType.DurationsDataset)]
        [InlineData(TestExprType.None, TestExprType.MixedIntNumDataset, /*result:*/ TestExprType.MixedIntNumDataset)]
        [InlineData(TestExprType.None, TestExprType.MixedNoneIntDataset, /*result:*/ TestExprType.MixedNoneIntDataset)]
        [InlineData(TestExprType.None, TestExprType.MixedNoneNumDataset, /*result:*/ TestExprType.MixedNoneNumDataset)]
        [InlineData(TestExprType.None, TestExprType.MixedNoneStrDataset, /*result:*/ TestExprType.MixedNoneStrDataset)]
        [InlineData(TestExprType.None, TestExprType.MixedNoneBoolDataset, /*result:*/ TestExprType.MixedNoneBoolDataset)]
        [InlineData(TestExprType.None, TestExprType.MixedNoneTimeDataset, /*result:*/ TestExprType.MixedNoneTimeDataset)]
        [InlineData(TestExprType.None, TestExprType.MixedNoneDateDataset, /*result:*/ TestExprType.MixedNoneDateDataset)]
        [InlineData(TestExprType.None, TestExprType.MixedNoneTimePerDataset, /*result:*/ TestExprType.MixedNoneTimePerDataset)]
        [InlineData(TestExprType.None, TestExprType.MixedNoneDurDataset, /*result:*/ TestExprType.MixedNoneDurDataset)]
        public void GetOutputStructure_CorrectNoCalcComponentDatasetScalarExpr_DataStructure(params TestExprType[] types)
        {
            IExpression ifExpr = this._ifThenElseExpr.Operands["if"];
            IExpression thenExpr = this._ifThenElseExpr.Operands["then"];
            IExpression elseExpr = this._ifThenElseExpr.Operands["else"];

            ifExpr.Operands["ds_1"] = TestExprFactory.GetExpression(TestExprType.Boolean);
            thenExpr.Operands["ds_1"] = TestExprFactory.GetExpression(types[1]);
            elseExpr.Operands["ds_1"] = TestExprFactory.GetExpression(types[0]);

            ifExpr.Operands["ds_1"].ResultName = "Component";

            IExpression expected = TestExprFactory.GetExpression(types[2]);

            IDataStructure dataStructure = this._ifThenElseExpr.OperatorDefinition.GetOutputStructure(this._ifThenElseExpr);

            Assert.True(expected.Structure.EqualsObj(dataStructure));
        }

        [Theory]
        [InlineData(TestExprType.Integer)]
        [InlineData(TestExprType.Number)]
        [InlineData(TestExprType.String)]
        [InlineData(TestExprType.Time)]
        [InlineData(TestExprType.Date)]
        [InlineData(TestExprType.TimePeriod)]
        [InlineData(TestExprType.Duration)]
        public void GetOutputStructure_NoBooleanScalarIfExpr_ThrowsException(TestExprType type)
        {
            IExpression ifExpr = this._ifThenElseExpr.Operands["if"];
            IExpression thenExpr = this._ifThenElseExpr.Operands["then"];
            IExpression elseExpr = this._ifThenElseExpr.Operands["else"];

            ifExpr.Operands["ds_1"] = TestExprFactory.GetExpression(type);
            thenExpr.Operands["ds_1"] = TestExprFactory.GetExpression(TestExprType.Integer);
            elseExpr.Operands["ds_1"] = TestExprFactory.GetExpression(TestExprType.Integer);

            Assert.ThrowsAny<VtlOperatorError>(() => { this._ifThenElseExpr.OperatorDefinition.GetOutputStructure(this._ifThenElseExpr); });
        }

        [Theory]
        [InlineData(TestExprType.Integer)]
        [InlineData(TestExprType.Number)]
        [InlineData(TestExprType.String)]
        [InlineData(TestExprType.Time)]
        [InlineData(TestExprType.Date)]
        [InlineData(TestExprType.TimePeriod)]
        [InlineData(TestExprType.Duration)]
        public void GetOutputStructure_NoBooleanMeasureDatasetIfExpr_ThrowsException(TestExprType type)
        {
            IExpression ifExpr = this._ifThenElseExpr.Operands["if"];
            IExpression thenExpr = this._ifThenElseExpr.Operands["then"];
            IExpression elseExpr = this._ifThenElseExpr.Operands["else"];

            ifExpr.Operands["ds_1"] = TestExprFactory.GetExpression(type);
            ifExpr.Operands["ds_1"].Structure.Identifiers.Add(new StructureComponent(BasicDataType.Integer, "Id1"));

            thenExpr.Operands["ds_1"] = TestExprFactory.GetExpression(TestExprType.Integer);
            elseExpr.Operands["ds_1"] = TestExprFactory.GetExpression(TestExprType.Integer);

            Assert.ThrowsAny<VtlOperatorError>(() => { this._ifThenElseExpr.OperatorDefinition.GetOutputStructure(this._ifThenElseExpr); });
        }

        [Theory]
        [InlineData(TestExprType.Integer)]
        [InlineData(TestExprType.Number)]
        [InlineData(TestExprType.String)]
        [InlineData(TestExprType.Time)]
        [InlineData(TestExprType.Date)]
        [InlineData(TestExprType.TimePeriod)]
        [InlineData(TestExprType.Duration)]
        public void GetOutputStructure_Dataset2NoBooleanScalarsExpr_ThrowsException(TestExprType type)
        {
            IExpression ifExpr = this._ifThenElseExpr.Operands["if"];
            IExpression thenExpr = this._ifThenElseExpr.Operands["then"];
            IExpression elseExpr = this._ifThenElseExpr.Operands["else"];

            ifExpr.Operands["ds_1"] = TestExprFactory.GetExpression(TestExprType.Boolean);
            ifExpr.Operands["ds_1"].Structure.Identifiers.Add(new StructureComponent(BasicDataType.String, "Id1"));
            thenExpr.Operands["ds_1"] = TestExprFactory.GetExpression(type);
            elseExpr.Operands["ds_1"] = TestExprFactory.GetExpression(type);

            Assert.ThrowsAny<VtlOperatorError>(() => { this._ifThenElseExpr.OperatorDefinition.GetOutputStructure(this._ifThenElseExpr); });
        }

        [Fact]
        public void GetOutputStructure_NoSingleMeasureDatasetIfExpr_ThrowsException()
        {
            IExpression ifExpr = this._ifThenElseExpr.Operands["if"];
            IExpression thenExpr = this._ifThenElseExpr.Operands["then"];
            IExpression elseExpr = this._ifThenElseExpr.Operands["else"];

            thenExpr.Operands["ds_1"] = TestExprFactory.GetExpression(TestExprType.Integer);
            elseExpr.Operands["ds_1"] = TestExprFactory.GetExpression(TestExprType.Integer);

            ifExpr.Operands["ds_1"].Structure = ModelResolvers.DsResolver("Id_1", ComponentType.Identifier, BasicDataType.Integer);
            ifExpr.Operands["ds_1"].Structure.Measures.Add(new StructureComponent(BasicDataType.Boolean, "Me1"));
            ifExpr.Operands["ds_1"].Structure.Measures.Add(new StructureComponent(BasicDataType.Boolean, "Me2"));

            Assert.ThrowsAny<VtlOperatorError>(() => { this._ifThenElseExpr.OperatorDefinition.GetOutputStructure(this._ifThenElseExpr); });
        }

        [Fact]
        public void GetOutputStructure_CalcComponentWithoutDatasetExpr_DataStructure()
        {
            IExpression ifExpr = this._ifThenElseExpr.Operands["if"];
            IExpression thenExpr = this._ifThenElseExpr.Operands["then"];
            IExpression elseExpr = this._ifThenElseExpr.Operands["else"];
            IExpression calcBranchExpr = TestExprFactory.GetExpression("Calc", ExpressionFactoryNameTarget.ResultName);
            IExpression calcExpr = TestExprFactory.GetExpression("Calc expression", ExpressionFactoryNameTarget.ResultName);
            IExpression compExpr = TestExprFactory.GetExpression(TestExprType.Integer);

            calcBranchExpr.AddOperand("ds_1", calcExpr);
            calcExpr.AddOperand("ds_1", compExpr);
            calcExpr.AddOperand("ds_2", this._ifThenElseExpr);
            compExpr.ResultName = "Component";

            ifExpr.Operands["ds_1"] = TestExprFactory.GetExpression(TestExprType.Boolean);
            thenExpr.Operands["ds_1"] = TestExprFactory.GetExpression(TestExprType.Integer);
            elseExpr.Operands["ds_1"] = TestExprFactory.GetExpression(TestExprType.Integer);

            for (int i = 0; i < 7; i++)
            {
                ifExpr.ResultName = thenExpr.ResultName = elseExpr.ResultName = string.Empty;
                switch (i)
                {
                    case 0: ifExpr.Operands["ds_1"].ResultName = "Component"; break;
                    case 1: thenExpr.Operands["ds_1"].ResultName = "Component"; break;
                    case 2: elseExpr.Operands["ds_1"].ResultName = "Component"; break;
                    case 3: ifExpr.Operands["ds_1"].ResultName = thenExpr.Operands["ds_1"].ResultName = "Component"; break;
                    case 4: ifExpr.Operands["ds_1"].ResultName = elseExpr.Operands["ds_1"].ResultName = "Component"; break;
                    case 5: thenExpr.Operands["ds_1"].ResultName = elseExpr.Operands["ds_1"].ResultName = "Component"; break;
                    case 6: ifExpr.Operands["ds_1"].ResultName = thenExpr.Operands["ds_1"].ResultName = elseExpr.Operands["ds_1"].ResultName = "Component"; break;
                    default: throw new Exception();
                }

                IDataStructure dataStructure = this._ifThenElseExpr.OperatorDefinition.GetOutputStructure(this._ifThenElseExpr);

                Assert.True(dataStructure.IsSingleComponent);
                Assert.True(compExpr.Structure.Components[0].ValueDomain.DataType == dataStructure.Components[0].ValueDomain.DataType);
            }
        }

        // Do rozpatrzenia
        //[Fact]
        //public void GetOutputStructure_NoCalcComponentWithoutDatasetExpr_ThrowsException()
        //{
        //    IExpression ifExpr = this.ifThenElseExpr.Operands["if"];
        //    IExpression thenExpr = this.ifThenElseExpr.Operands["then"];
        //    IExpression elseExpr = this.ifThenElseExpr.Operands["else"];

        //    ifExpr.Operands["ds_1"] = TestExprFactory.GetExpression(TestExprType.Boolean);
        //    thenExpr.Operands["ds_1"] = TestExprFactory.GetExpression(TestExprType.Integer);
        //    elseExpr.Operands["ds_1"] = TestExprFactory.GetExpression(TestExprType.Integer);

        //    for (int i = 0; i < 7; i++)
        //    {
        //        ifExpr.ResultName = thenExpr.ResultName = elseExpr.ResultName = string.Empty;
        //        switch (i)
        //        {
        //            case 0: ifExpr.Operands["ds_1"].ResultName = "Component"; break;
        //            case 1: thenExpr.Operands["ds_1"].ResultName = "Component"; break;
        //            case 2: elseExpr.Operands["ds_1"].ResultName = "Component"; break;
        //            case 3: ifExpr.Operands["ds_1"].ResultName = thenExpr.Operands["ds_1"].ResultName = "Component"; break;
        //            case 4: ifExpr.Operands["ds_1"].ResultName = elseExpr.Operands["ds_1"].ResultName = "Component"; break;
        //            case 5: thenExpr.Operands["ds_1"].ResultName = elseExpr.Operands["ds_1"].ResultName = "Component"; break;
        //            case 6: ifExpr.Operands["ds_1"].ResultName = thenExpr.Operands["ds_1"].ResultName = elseExpr.Operands["ds_1"].ResultName = "Component"; break;
        //            default: throw new Exception();
        //        }

        //        Assert.ThrowsAny<VtlOperatorError>(() => { this.ifThenElseExpr.OperatorDefinition.GetOutputStructure(this.ifThenElseExpr); });
        //    }
        //}

        [Fact]
        public void GetOutputStructure_3NotMatchingDatasetsExpr_ThrowsException()
        {
            IExpression ifExpr = this._ifThenElseExpr.Operands["if"];
            IExpression thenExpr = this._ifThenElseExpr.Operands["then"];
            IExpression elseExpr = this._ifThenElseExpr.Operands["else"];

            ifExpr.Operands["ds_1"] = TestExprFactory.GetExpression(TestExprType.Boolean);
            ifExpr.Operands["ds_1"].Structure.Identifiers.Add(new StructureComponent(BasicDataType.String, "Id1"));
            thenExpr.Operands["ds_1"] = TestExprFactory.GetExpression(TestExprType.IntsDataset);
            elseExpr.Operands["ds_1"] = TestExprFactory.GetExpression(TestExprType.StringsDataset);

            Assert.ThrowsAny<VtlOperatorError>(() => { this._ifThenElseExpr.OperatorDefinition.GetOutputStructure(this._ifThenElseExpr); });
        }

        [Fact]
        public void GetOutputStructure_2NotMatchingDatasetsExpr_ThrowsException()
        {
            IExpression ifExpr = this._ifThenElseExpr.Operands["if"];
            IExpression thenExpr = this._ifThenElseExpr.Operands["then"];
            IExpression elseExpr = this._ifThenElseExpr.Operands["else"];

            ifExpr.Operands["ds_1"] = TestExprFactory.GetExpression(TestExprType.Boolean);
            thenExpr.Operands["ds_1"] = TestExprFactory.GetExpression(TestExprType.Integer);
            elseExpr.Operands["ds_1"] = TestExprFactory.GetExpression(TestExprType.Integer);

            for (int i = 0; i < 3; i++)
            {
                switch (i)
                {
                    case 0:
                        thenExpr.Operands["ds_1"] = TestExprFactory.GetExpression(TestExprType.IntsDataset);
                        elseExpr.Operands["ds_1"] = TestExprFactory.GetExpression(TestExprType.StringsDataset);
                        break;
                    case 1:
                        ifExpr.Operands["ds_1"] = TestExprFactory.GetExpression(TestExprType.Boolean);
                        ifExpr.Operands["ds_1"].Structure.Identifiers.Add(new StructureComponent(BasicDataType.String, "Id1"));
                        thenExpr.Operands["ds_1"] = TestExprFactory.GetExpression(TestExprType.IntsDataset);
                        break;
                    case 2:
                        ifExpr.Operands["ds_1"] = TestExprFactory.GetExpression(TestExprType.Boolean);
                        ifExpr.Operands["ds_1"].Structure.Identifiers.Add(new StructureComponent(BasicDataType.String, "Id1"));
                        elseExpr.Operands["ds_1"] = TestExprFactory.GetExpression(TestExprType.IntsDataset);
                        break;
                }

                Assert.ThrowsAny<VtlOperatorError>(() => { this._ifThenElseExpr.OperatorDefinition.GetOutputStructure(this._ifThenElseExpr); });
            }
        }

        [Fact]
        public void GetOutputStructure_NotMatchingTypesScalarThenScalarElseExpr_ThrowsException()
        {
            IExpression ifExpr = this._ifThenElseExpr.Operands["if"];
            IExpression thenExpr = this._ifThenElseExpr.Operands["then"];
            IExpression elseExpr = this._ifThenElseExpr.Operands["else"];

            ifExpr.Operands["ds_1"] = TestExprFactory.GetExpression(TestExprType.Boolean);
            thenExpr.Operands["ds_1"] = TestExprFactory.GetExpression(TestExprType.Integer);
            elseExpr.Operands["ds_1"] = TestExprFactory.GetExpression(TestExprType.String);

            Assert.ThrowsAny<VtlOperatorError>(() => { this._ifThenElseExpr.OperatorDefinition.GetOutputStructure(this._ifThenElseExpr); });
        }

        [Fact]
        public void GetOutputStructure_NotMatchingTypesScalarThenDatasetElseExpr_ThrowsException()
        {
            IExpression ifExpr = this._ifThenElseExpr.Operands["if"];
            IExpression thenExpr = this._ifThenElseExpr.Operands["then"];
            IExpression elseExpr = this._ifThenElseExpr.Operands["else"];

            ifExpr.Operands["ds_1"] = TestExprFactory.GetExpression(TestExprType.Boolean);
            thenExpr.Operands["ds_1"] = TestExprFactory.GetExpression(TestExprType.Integer);
            elseExpr.Operands["ds_1"] = TestExprFactory.GetExpression(TestExprType.StringsDataset);

            Assert.ThrowsAny<VtlOperatorError>(() => { this._ifThenElseExpr.OperatorDefinition.GetOutputStructure(this._ifThenElseExpr); });
        }

        [Fact]
        public void GetOutputStructure_NotMatchingTypesDatasetThenScalarElseExpr_ThrowsException()
        {
            IExpression ifExpr = this._ifThenElseExpr.Operands["if"];
            IExpression thenExpr = this._ifThenElseExpr.Operands["then"];
            IExpression elseExpr = this._ifThenElseExpr.Operands["else"];

            ifExpr.Operands["ds_1"] = TestExprFactory.GetExpression(TestExprType.Boolean);
            thenExpr.Operands["ds_1"] = TestExprFactory.GetExpression(TestExprType.IntsDataset);
            elseExpr.Operands["ds_1"] = TestExprFactory.GetExpression(TestExprType.String);

            Assert.ThrowsAny<VtlOperatorError>(() => { this._ifThenElseExpr.OperatorDefinition.GetOutputStructure(this._ifThenElseExpr); });
        }
    }
}
