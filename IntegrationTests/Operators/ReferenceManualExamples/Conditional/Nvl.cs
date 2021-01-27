namespace VtlProcessing.IntegrationTests.TSQL.Operators.ReferenceManualExamples.Conditional
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using Xunit;

    public class Nvl : TSQLTestBase
    {
        public Nvl() : base("Nvl")
        {
            List<object[]> ds1 = new List<object[]>
            {
                new object[] { 2012, "B", "Total", "Total", 11094850 },
                new object[] { 2012, "G", "Total", "Total", 11123034 },
                new object[] { 2012, "S", "Total", "Total", null },
                new object[] { 2012, "M", "Total", "Total", 417546 },
                new object[] { 2012, "F", "Total", "Total", 5401267 },
                new object[] { 2012, "N", "Total", "Total", null }
            };

            this.SqlFillData("[Nvl].DS_1", ds1);
        }

        [Fact]
        public void Example1()
        {
            DataTable expected = new DataTable();
            expected.Columns.Add("Id_1");
            expected.Columns.Add("Id_2");
            expected.Columns.Add("Id_3");
            expected.Columns.Add("Id_4");
            expected.Columns.Add("Me_1");
            // expected.LoadDataRow(new object[] { 2012, "B", "Total", "Total", 11094850 }, false);
            // expected.LoadDataRow(new object[] { 2012, "G", "Total", "Total", 11123034 }, false);
            // expected.LoadDataRow(new object[] { 2012, "S", "Total", "Total", 0 }, false);
            // expected.LoadDataRow(new object[] { 2012, "M", "Total", "Total", 417546 }, false);
            // expected.LoadDataRow(new object[] { 2012, "F", "Total", "Total", 5401267 }, false);
            // expected.LoadDataRow(new object[] { 2012, "N", "Total", "Total", 0 }, false);
            // Sorted for test needs
            expected.LoadDataRow(new object[] { 2012, "B", "Total", "Total", 11094850 }, false);
            expected.LoadDataRow(new object[] { 2012, "F", "Total", "Total", 5401267 }, false);
            expected.LoadDataRow(new object[] { 2012, "G", "Total", "Total", 11123034 }, false);
            expected.LoadDataRow(new object[] { 2012, "M", "Total", "Total", 417546 }, false);
            expected.LoadDataRow(new object[] { 2012, "N", "Total", "Total", 0 }, false);
            expected.LoadDataRow(new object[] { 2012, "S", "Total", "Total", 0 }, false);

            string source = "DS_r := nvl(DS_1, 0)";

            List<Exception> errors;
            string sql = this.TranslateVtl(source, out errors);

            Assert.Empty(errors);

            DataTable ds_r = this.RunSql(sql, "#DS_r");

            Assert.True(expected.EqualsTo(ds_r));
        }
    }
}
