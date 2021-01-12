namespace VtlProcessing.IntegrationTests.TSQL.Operators.ReferenceManualExamples.Time
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using Xunit;

    public class PeriodIndicator : TSQLTestBase
    {
        public PeriodIndicator() : base("Period_indicator")
        {
            List<object[]> ds1 = new List<object[]>
            {
                new object[] { "A", 1, "2010", 10 },
                new object[] { "A", 1, "2013Q1", 50 }
            };

            this.SqlFillData("[Period_indicator].DS_1", ds1);
        }

        [Fact]
        public void Example1()
        {
            DataTable expected = new DataTable();
            expected.Columns.Add("Id_1");
            expected.Columns.Add("Id_2");
            expected.Columns.Add("Id_3");
            expected.Columns.Add("duration_var");
            expected.LoadDataRow(new object[] { "A", 1, "2010", "A" }, false);
            expected.LoadDataRow(new object[] { "A", 1, "2013Q1", "Q" }, false);

            string source = "DS_r := period_indicator(DS_1)";

            List<Exception> errors;
            string sql = this.TranslateVtl(source, out errors);

            Assert.Empty(errors);

            DataTable ds_r = this.RunSql(sql, "#DS_r");

            Assert.True(expected.EqualsTo(ds_r));
        }

        [Fact]
        public void Example2()
        {
            DataTable expected = new DataTable();
            expected.Columns.Add("Id_1");
            expected.Columns.Add("Id_2");
            expected.Columns.Add("Id_3");
            expected.Columns.Add("Me_1");
            expected.LoadDataRow(new object[] { "A", 1, "2010", 10 }, false);

            string source = "DS_r := DS_1[filter period_indicator(Id_3) = t\"A\"]";

            List<Exception> errors;
            string sql = this.TranslateVtl(source, out errors);

            Assert.Empty(errors);

            DataTable ds_r = this.RunSql(sql, "#DS_r");

            Assert.True(expected.EqualsTo(ds_r));
        }
    }
}
