namespace StatisticsPoland.VtlProcessing.Core.Tests.OperatorsTests
{
    using StatisticsPoland.VtlProcessing.Core.ErrorHandling;
    using StatisticsPoland.VtlProcessing.Core.Infrastructure;
    using StatisticsPoland.VtlProcessing.Core.Models.Interfaces;
    using StatisticsPoland.VtlProcessing.Core.Tests.Utilities;
    using System;
    using System.Linq;
    using Xunit;

    public class NvlOperatorTests
    {
        [Theory]
        // 2 Scalars:
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
        // Scalar, Dataset:
        [InlineData(TestExprType.Integer, TestExprType.IntsDataset, /*result:*/ TestExprType.IntsDataset)]
        [InlineData(TestExprType.Integer, TestExprType.NumbersDataset, /*result:*/ TestExprType.NumbersDataset)]
        [InlineData(TestExprType.Integer, TestExprType.NonesDataset, /*result:*/ TestExprType.IntsDataset)]
        [InlineData(TestExprType.Number, TestExprType.IntsDataset, /*result:*/ TestExprType.NumbersDataset)]
        [InlineData(TestExprType.Number, TestExprType.NumbersDataset, /*result:*/ TestExprType.NumbersDataset)]
        [InlineData(TestExprType.Number, TestExprType.NonesDataset, /*result:*/ TestExprType.NumbersDataset)]
        [InlineData(TestExprType.String, TestExprType.StringsDataset, /*result:*/ TestExprType.StringsDataset)]
        [InlineData(TestExprType.String, TestExprType.NonesDataset, /*result:*/ TestExprType.StringsDataset)]
        [InlineData(TestExprType.Boolean, TestExprType.BoolsDataset, /*result:*/ TestExprType.BoolsDataset)]
        [InlineData(TestExprType.Boolean, TestExprType.NonesDataset, /*result:*/ TestExprType.BoolsDataset)]
        [InlineData(TestExprType.Time, TestExprType.TimesDataset, /*result:*/ TestExprType.TimesDataset)]
        [InlineData(TestExprType.Time, TestExprType.NonesDataset, /*result:*/ TestExprType.TimesDataset)]
        [InlineData(TestExprType.Date, TestExprType.DatesDataset, /*result:*/ TestExprType.DatesDataset)]
        [InlineData(TestExprType.Date, TestExprType.NonesDataset, /*result:*/ TestExprType.DatesDataset)]
        [InlineData(TestExprType.TimePeriod, TestExprType.TimePeriodsDataset, /*result:*/ TestExprType.TimePeriodsDataset)]
        [InlineData(TestExprType.TimePeriod, TestExprType.NonesDataset, /*result:*/ TestExprType.TimePeriodsDataset)]
        [InlineData(TestExprType.Duration, TestExprType.DurationsDataset, /*result:*/ TestExprType.DurationsDataset)]
        [InlineData(TestExprType.Duration, TestExprType.NonesDataset, /*result:*/ TestExprType.DurationsDataset)]
        [InlineData(TestExprType.None, TestExprType.NonesDataset, /*result:*/ TestExprType.NonesDataset)]
        [InlineData(TestExprType.None, TestExprType.IntsDataset, /*result:*/ TestExprType.IntsDataset)]
        [InlineData(TestExprType.None, TestExprType.NumbersDataset, /*result:*/ TestExprType.NumbersDataset)]
        [InlineData(TestExprType.None, TestExprType.StringsDataset, /*result:*/ TestExprType.StringsDataset)]
        [InlineData(TestExprType.None, TestExprType.BoolsDataset, /*result:*/ TestExprType.BoolsDataset)]
        [InlineData(TestExprType.None, TestExprType.TimesDataset, /*result:*/ TestExprType.TimesDataset)]
        [InlineData(TestExprType.None, TestExprType.DatesDataset, /*result:*/ TestExprType.DatesDataset)]
        [InlineData(TestExprType.None, TestExprType.TimePeriodsDataset, /*result:*/ TestExprType.TimePeriodsDataset)]
        [InlineData(TestExprType.None, TestExprType.DurationsDataset, /*result:*/ TestExprType.DurationsDataset)]
        // Dataset, Scalar:
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
        // 2 Datasets:
        [InlineData(TestExprType.IntsDataset, TestExprType.IntsDataset, /*result:*/ TestExprType.IntsDataset)]
        [InlineData(TestExprType.IntsDataset, TestExprType.NumbersDataset, /*result:*/ TestExprType.NumbersDataset)]
        [InlineData(TestExprType.IntsDataset, TestExprType.NonesDataset, /*result:*/ TestExprType.IntsDataset)]
        [InlineData(TestExprType.NumbersDataset, TestExprType.IntsDataset, /*result:*/ TestExprType.NumbersDataset)]
        [InlineData(TestExprType.NumbersDataset, TestExprType.NumbersDataset, /*result:*/ TestExprType.NumbersDataset)]
        [InlineData(TestExprType.NumbersDataset, TestExprType.NonesDataset, /*result:*/ TestExprType.NumbersDataset)]
        [InlineData(TestExprType.StringsDataset, TestExprType.StringsDataset, /*result:*/ TestExprType.StringsDataset)]
        [InlineData(TestExprType.StringsDataset, TestExprType.NonesDataset, /*result:*/ TestExprType.StringsDataset)]
        [InlineData(TestExprType.BoolsDataset, TestExprType.BoolsDataset, /*result:*/ TestExprType.BoolsDataset)]
        [InlineData(TestExprType.BoolsDataset, TestExprType.NonesDataset, /*result:*/ TestExprType.BoolsDataset)]
        [InlineData(TestExprType.TimesDataset, TestExprType.TimesDataset, /*result:*/ TestExprType.TimesDataset)]
        [InlineData(TestExprType.TimesDataset, TestExprType.NonesDataset, /*result:*/ TestExprType.TimesDataset)]
        [InlineData(TestExprType.DatesDataset, TestExprType.DatesDataset, /*result:*/ TestExprType.DatesDataset)]
        [InlineData(TestExprType.DatesDataset, TestExprType.NonesDataset, /*result:*/ TestExprType.DatesDataset)]
        [InlineData(TestExprType.TimePeriodsDataset, TestExprType.TimePeriodsDataset, /*result:*/ TestExprType.TimePeriodsDataset)]
        [InlineData(TestExprType.TimePeriodsDataset, TestExprType.NonesDataset, /*result:*/ TestExprType.TimePeriodsDataset)]
        [InlineData(TestExprType.DurationsDataset, TestExprType.DurationsDataset, /*result:*/ TestExprType.DurationsDataset)]
        [InlineData(TestExprType.DurationsDataset, TestExprType.NonesDataset, /*result:*/ TestExprType.DurationsDataset)]
        [InlineData(TestExprType.NonesDataset, TestExprType.NonesDataset, /*result:*/ TestExprType.NonesDataset)]
        [InlineData(TestExprType.NonesDataset, TestExprType.IntsDataset, /*result:*/ TestExprType.IntsDataset)]
        [InlineData(TestExprType.NonesDataset, TestExprType.NumbersDataset, /*result:*/ TestExprType.NumbersDataset)]
        [InlineData(TestExprType.NonesDataset, TestExprType.StringsDataset, /*result:*/ TestExprType.StringsDataset)]
        [InlineData(TestExprType.NonesDataset, TestExprType.BoolsDataset, /*result:*/ TestExprType.BoolsDataset)]
        [InlineData(TestExprType.NonesDataset, TestExprType.TimesDataset, /*result:*/ TestExprType.TimesDataset)]
        [InlineData(TestExprType.NonesDataset, TestExprType.DatesDataset, /*result:*/ TestExprType.DatesDataset)]
        [InlineData(TestExprType.NonesDataset, TestExprType.TimePeriodsDataset, /*result:*/ TestExprType.TimePeriodsDataset)]
        [InlineData(TestExprType.NonesDataset, TestExprType.DurationsDataset, /*result:*/ TestExprType.DurationsDataset)]
        public void GetOutputStructure_CorrectSingleMeasureExpr_DataStructure(params TestExprType[] types)
        {
            IExpression nvlExpr = TestExprFactory.GetExpression(types[0], types[1]);
            nvlExpr.OperatorDefinition = ModelResolvers.OperatorResolver("nvl");

            if (nvlExpr.Operands["ds_1"].Structure.Measures.Count > 1) nvlExpr.Operands["ds_1"].Structure.Measures.RemoveAt(1);
            if (nvlExpr.Operands["ds_2"].Structure.Measures.Count > 1) nvlExpr.Operands["ds_2"].Structure.Measures.RemoveAt(1);

            IExpression expected = TestExprFactory.GetExpression(types[2]);
            if (expected.Structure.Measures.Count > 1) expected.Structure.Measures.RemoveAt(1);

            IDataStructure dataStructure = nvlExpr.OperatorDefinition.GetOutputStructure(nvlExpr);

            Assert.True(expected.Structure.EqualsObj(dataStructure));
        }

        [Fact]
        public void GetOutputStructure_NotSingleMeasureExpr_ThrowsException()
        {
            TestExprType[][] combinations = Enum.GetValues(typeof(TestExprType)).Cast<TestExprType>().GetCombinations(2);
            TestExprType[][] wrongCombs = combinations.Where(comb => (int)comb[0] >= 9 && (int)comb[1] >= 9).ToArray(); // only datasets

            foreach (TestExprType[] wrongComb in wrongCombs)
            {
                IExpression nvlExpr = TestExprFactory.GetExpression(wrongComb);
                nvlExpr.OperatorDefinition = ModelResolvers.OperatorResolver("nvl");

                // Debug condition example: wrongComb[0] == TestExprType.Integer && wrongComb[1] == TestExprType.Integer
                Assert.ThrowsAny<VtlOperatorError>(() => { nvlExpr.OperatorDefinition.GetOutputStructure(nvlExpr); });
            }
        }

        [Fact]
        public void GetOutputStructure_WrongSingleMeasureExpr_ThrowsException()
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
                new TestExprType[] { TestExprType.NonesDataset, TestExprType.Duration },
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
                new TestExprType[] { TestExprType.NonesDataset, TestExprType.DurationsDataset }
            };

            TestExprType[][] combinations = Enum.GetValues(typeof(TestExprType)).Cast<TestExprType>().GetCombinations(2);
            TestExprType[][] wrongCombs = combinations.Without(correctCombs);

            foreach (TestExprType[] wrongComb in wrongCombs.Where(wrongComb => (int)wrongComb[0] < 18 && (int)wrongComb[1] < 18)) // No mixed datasets
            {
                IExpression nvlExpr = TestExprFactory.GetExpression(wrongComb);
                nvlExpr.OperatorDefinition = ModelResolvers.OperatorResolver("nvl");

                if (nvlExpr.Operands["ds_1"].Structure.Measures.Count > 1) nvlExpr.Operands["ds_1"].Structure.Measures.RemoveAt(1);
                if (nvlExpr.Operands["ds_2"].Structure.Measures.Count > 1) nvlExpr.Operands["ds_2"].Structure.Measures.RemoveAt(1);

                // Debug condition example: wrongComb[0] == TestExprType.Integer && wrongComb[1] == TestExprType.Integer
                Assert.ThrowsAny<VtlOperatorError>(() => { nvlExpr.OperatorDefinition.GetOutputStructure(nvlExpr); });
            }
        }
    }
}
