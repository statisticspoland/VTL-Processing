namespace VtlProcessing.IntegrationTests.TSQL.Operators.CustomExamples.Time
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
        public void JoinClause()
        {
            DataTable expected = new DataTable();
            expected.Columns.Add("Id_1");
            expected.Columns.Add("Id_2");
            expected.Columns.Add("Id_3");
            expected.Columns.Add("Me_1");
            expected.Columns.Add("Me_2");
            expected.LoadDataRow(new object[] { "A", 1, "2010", 10, "A" }, false);

            string source = "DS_r := inner_join(DS_1 as ds1 filter period_indicator(Id_3) = t\"A\" calc Me_2 := period_indicator())";

            List<Exception> errors;
            string sql = this.TranslateVtl(source, out errors);

            Assert.Empty(errors);

            DataTable ds_r = this.RunSql(sql, "#DS_r");

            Assert.True(expected.EqualsTo(ds_r));
        }

        [Fact]
        public void Scalar_SingleYear()
        {
            string expected = "A";
            string source = "var := period_indicator(t\"2020\")";

            List<Exception> errors;
            string sql = this.TranslateVtl(source, out errors);

            Assert.Empty(errors);

            DataTable var = this.RunSql(sql, "@var");

            Assert.Single(var.Columns);
            Assert.Single(var.Rows);
            Assert.Equal(expected, var.Rows[0].Field<string>("value"));
        }

        [Fact]
        public void Scalar_YearWithDuration()
        {
            string expected = "W";
            string source = "var := period_indicator(t\"2020W14\")";

            List<Exception> errors;
            string sql = this.TranslateVtl(source, out errors);

            Assert.Empty(errors);

            DataTable var = this.RunSql(sql, "@var");

            Assert.Single(var.Columns);
            Assert.Single(var.Rows);
            Assert.Equal(expected, var.Rows[0].Field<string>("value"));
        }
    }
}
