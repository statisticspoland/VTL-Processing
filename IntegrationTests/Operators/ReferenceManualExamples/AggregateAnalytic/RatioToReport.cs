namespace VtlProcessing.IntegrationTests.TSQL.Operators.ReferenceManualExamples.AggregateAnalytic
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using Xunit;

    public class RatioToReport : TSQLTestBase
    {
        public RatioToReport() : base("Ratio_to_report")
        {
            List<object[]> ds1 = new List<object[]>
            {
                new object[] {"A", "XX", 2000, 3, 1 },
                new object[] {"A", "XX", 2001, 4, 3 },
                new object[] {"A", "XX", 2002, 7, 5 },
                new object[] {"A", "XX", 2003, 6, 1 },
                new object[] {"A", "YY", 2000, 12, 0 },
                new object[] {"A", "YY", 2001, 8, 8 },
                new object[] {"A", "YY", 2002, 6, 5 },
                new object[] {"A", "YY", 2003, 14, -3 },
            };

            this.SqlFillData("[Ratio_to_report].DS_1", ds1);
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
            expected.LoadDataRow(new object[] { "A", "XX", 2000, 0.15, 0.1 }, false);
            expected.LoadDataRow(new object[] { "A", "XX", 2001, 0.2, 0.3 }, false);
            expected.LoadDataRow(new object[] { "A", "XX", 2002, 0.35, 0.5 }, false);
            expected.LoadDataRow(new object[] { "A", "XX", 2003, 0.3, 0.1 }, false);
            expected.LoadDataRow(new object[] { "A", "YY", 2000, 0.3, 0 }, false);
            expected.LoadDataRow(new object[] { "A", "YY", 2001, 0.2, 0.8 }, false);
            expected.LoadDataRow(new object[] { "A", "YY", 2002, 0.15, 0.5 }, false);
            expected.LoadDataRow(new object[] { "A", "YY", 2003, 0.35, -0.3 }, false);

            string source = "DS_r := ratio_to_report(DS_1 over (partition by Id_1, Id_2))";

            List<Exception> errors;
            string sql = this.TranslateVtl(source, out errors);

            Assert.Empty(errors);

            DataTable ds_r = this.RunSql(sql, "#DS_r");

            Assert.True(expected.EqualsTo(ds_r));
        }
    }
}
