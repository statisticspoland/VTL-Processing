namespace VtlProcessing.IntegrationTests.TSQL.Operators.ReferenceManualExamples.Comparison
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using Xunit;

    public class Between : TSQLTestBase
    {
        public Between() : base("Between")
        {
            List<object[]> ds1 = new List<object[]>
            {
                new object[] { "G", "Total", "Percentage", "Total", 6 },
                new object[] { "R", "Total", "Percentage", "Total", -2 }
            };

            this.SqlFillData("[Between].DS_1", ds1);
        }

        [Fact]
        public void Example1()
        {
            DataTable expected = new DataTable();
            expected.Columns.Add("Id_1");
            expected.Columns.Add("Id_2");
            expected.Columns.Add("Id_3");
            expected.Columns.Add("Id_4");
            expected.Columns.Add("bool_var");
            // expected.LoadDataRow(new object[] { "G", "Total", "Percentage", "Total", true }, false);
            // expected.LoadDataRow(new object[] { "R", "Total", "Percentage", "Total", false }, false);
            // Changed boolean values to bits for test needs
            expected.LoadDataRow(new object[] { "G", "Total", "Percentage", "Total", 1 }, false);
            expected.LoadDataRow(new object[] { "R", "Total", "Percentage", "Total", 0 }, false);

            string source = "DS_r := between(DS_1, 5, 10)";

            List<Exception> errors;
            string sql = this.TranslateVtl(source, out errors);

            Assert.Empty(errors);

            DataTable ds_r = this.RunSql(sql, "#DS_r");

            Assert.True(expected.EqualsTo(ds_r));
        }
    }
}
