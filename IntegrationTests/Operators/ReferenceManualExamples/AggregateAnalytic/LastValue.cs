namespace VtlProcessing.IntegrationTests.TSQL.Operators.ReferenceManualExamples.AggregateAnalytic
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using Xunit;

    public class LastValue : TSQLTestBase
    {
        public LastValue() : base("Last_value")
        {
            List<object[]> ds1 = new List<object[]>
            {
                new object[] {"A", "XX", 1993, 3, 1 },
                new object[] {"A", "XX", 1994, 4, 9 },
                new object[] {"A", "XX", 1995, 7, 5 },
                new object[] {"A", "XX", 1996, 6, 8 },
                new object[] {"A", "YY", 1993, 9, 3 },
                new object[] {"A", "YY", 1994, 5, 4 },
                new object[] {"A", "YY", 1995, 10, 2 },
                new object[] {"A", "YY", 1996, 2, 7 },
            };

            this.SqlFillData("[Last_value].DS_1", ds1);
        }

        [Fact]
        public void Example1()
        {
            DataTable expected = new DataTable();
            expected.Columns.Add("Id_1");
            expected.Columns.Add("Id_2");
            expected.Columns.Add("Id_3");
            expected.Columns.Add("Me_1");
            expected.Columns.Add("Me_2");
            expected.LoadDataRow(new object[] { "A", "XX", 1993, 4, 9 }, false);
            // expected.LoadDataRow(new object[] { "A", "XX", 1994, 7, 9 }, false); - documentation example result error
            expected.LoadDataRow(new object[] { "A", "XX", 1994, 7, 5 }, false);
            // expected.LoadDataRow(new object[] { "A", "XX", 1995, 7, 9 }, false); - documentation example result error
            expected.LoadDataRow(new object[] { "A", "XX", 1995, 6, 8 }, false);
            // expected.LoadDataRow(new object[] { "A", "XX", 1996, 7, 8 }, false); - documentation example result error
            expected.LoadDataRow(new object[] { "A", "XX", 1996, 6, 8 }, false);
            // expected.LoadDataRow(new object[] { "A", "YY", 1993, 9, 4 }, false); - documentation example result error
            expected.LoadDataRow(new object[] { "A", "YY", 1993, 5, 4 }, false);
            // expected.LoadDataRow(new object[] { "A", "YY", 1994, 10, 4 }, false); - documentation example result error
            expected.LoadDataRow(new object[] { "A", "YY", 1994, 10, 2 }, false);
            // expected.LoadDataRow(new object[] { "A", "YY", 1995, 10, 7 }, false); - documentation example result error
            expected.LoadDataRow(new object[] { "A", "YY", 1995, 2, 7 }, false);
            // expected.LoadDataRow(new object[] { "A", "YY", 1996, 10, 7 }, false); - documentation example result error
            expected.LoadDataRow(new object[] { "A", "YY", 1996, 2, 7 }, false);

            string source = "DS_r := last_value(DS_1 over ( partition by Id_1, Id_2 order by Id_3 data points between 1 preceding and 1 following))";

            List<Exception> errors;
            string sql = this.TranslateVtl(source, out errors);

            Assert.Empty(errors);

            DataTable ds_r = this.RunSql(sql, "#DS_r");

            Assert.True(expected.EqualsTo(ds_r));
        }
    }
}
