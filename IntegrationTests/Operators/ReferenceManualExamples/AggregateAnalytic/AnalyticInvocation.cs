namespace VtlProcessing.IntegrationTests.TSQL.Operators.ReferenceManualExamples.AggregateAnalytic
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using Xunit;

    public class AnalyticInvocation : TSQLTestBase
    {
        public AnalyticInvocation() : base("Analytic_invocation")
        {
            List<object[]> ds1 = new List<object[]>
            {
                new object[] { 2010, "E", "XX", 5 },
                new object[] { 2010, "B", "XX", -3 },
                new object[] { 2010, "R", "XX", 9 },
                new object[] { 2010, "E", "YY", 13 },
                new object[] { 2011, "E", "XX", 11 },
                new object[] { 2011, "B", "ZZ", 7 },
                new object[] { 2011, "E", "YY", -1 },
                new object[] { 2011, "F", "XX", 0 },
                new object[] { 2012, "L", "ZZ", -2 },
                new object[] { 2012, "E", "YY", 3 },
            };

            this.SqlFillData("[Analytic_invocation].DS_1", ds1);
        }

        [Fact]
        public void Example1()
        {
            DataTable expected = new DataTable();
            expected.Columns.Add("Id_1");
            expected.Columns.Add("Id_2");
            expected.Columns.Add("Id_3");
            expected.Columns.Add("Me_1");
            expected.LoadDataRow(new object[] { 2010, "B", "XX",  2 }, false);
            expected.LoadDataRow(new object[] { 2010, "E", "XX", 15 }, false);
            expected.LoadDataRow(new object[] { 2010, "E", "YY", 27 }, false);
            expected.LoadDataRow(new object[] { 2010, "R", "XX", 29 }, false);
            expected.LoadDataRow(new object[] { 2011, "B", "ZZ", 27 }, false);
            expected.LoadDataRow(new object[] { 2011, "E", "XX", 17 }, false);
            expected.LoadDataRow(new object[] { 2011, "E", "YY", 10 }, false);
            expected.LoadDataRow(new object[] { 2011, "F", "XX",  2 }, false);
            expected.LoadDataRow(new object[] { 2012, "E", "YY",  1 }, false);
            expected.LoadDataRow(new object[] { 2012, "L", "ZZ",  1 }, false);

            string source = "DS_r := sum(DS_1 over (order by Id_1, Id_2, Id_3 data points between 1 preceding and 1 following))";

            List<Exception> errors;
            string sql = this.TranslateVtl(source, out errors);

            Assert.Empty(errors);

            DataTable ds_r = this.RunSql(sql, "#DS_r");

            Assert.True(expected.EqualsTo(ds_r));
        }
    }
}
