namespace VtlProcessing.IntegrationTests.TSQL.Operators.ReferenceManualExamples.Set
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using Xunit;

    public class Union : TSQLTestBase
    {
        public Union() : base("Union")
        {
            List<object[]> ds1 = new List<object[]>
            {
                new object[] { 2012, "B", "Total", "Total", 5 },
                new object[] { 2012, "G", "Total", "Total", 2 },
                new object[] { 2012, "F", "Total", "Total", 3 }
            };

            List<object[]> ds2a = new List<object[]>
            {
                new object[] { 2012, "N", "Total", "Total", 23 },
                new object[] { 2012, "S", "Total", "Total", 5 }
            };

            List<object[]> ds2b = new List<object[]>
            {
                new object[] { 2012, "B", "Total", "Total", 23 },
                new object[] { 2012, "S", "Total", "Total", 5 }
            };

            this.SqlFillData("[Union].DS_1", ds1);
            this.SqlFillData("[Union].DS_2A", ds2a);
            this.SqlFillData("[Union].DS_2B", ds2b);
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
            // expected.LoadDataRow(new object[] { 2012, "B", "Total", "Total", 5 }, false);
            // expected.LoadDataRow(new object[] { 2012, "G", "Total", "Total", 2 }, false);
            // expected.LoadDataRow(new object[] { 2012, "F", "Total", "Total", 3 }, false);
            // expected.LoadDataRow(new object[] { 2012, "N", "Total", "Total", 23 }, false);
            // expected.LoadDataRow(new object[] { 2012, "S", "Total", "Total", 5 }, false);
            // Sorted for test needs
            expected.LoadDataRow(new object[] { 2012, "B", "Total", "Total", 5 }, false);
            expected.LoadDataRow(new object[] { 2012, "F", "Total", "Total", 3 }, false);
            expected.LoadDataRow(new object[] { 2012, "G", "Total", "Total", 2 }, false);
            expected.LoadDataRow(new object[] { 2012, "N", "Total", "Total", 23 }, false);
            expected.LoadDataRow(new object[] { 2012, "S", "Total", "Total", 5 }, false);

            string source = "DS_r := union(DS_1, DS_2A)";

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
            expected.Columns.Add("Id_4");
            expected.Columns.Add("Me_1");
            // expected.LoadDataRow(new object[] { 2012, "B", "Total", "Total", 5 }, false);
            // expected.LoadDataRow(new object[] { 2012, "G", "Total", "Total", 2 }, false);
            // expected.LoadDataRow(new object[] { 2012, "F", "Total", "Total", 3 }, false);
            // expected.LoadDataRow(new object[] { 2012, "S", "Total", "Total", 5 }, false);
            // Sorted for test neeeds
            expected.LoadDataRow(new object[] { 2012, "B", "Total", "Total", 5 }, false);
            expected.LoadDataRow(new object[] { 2012, "F", "Total", "Total", 3 }, false);
            expected.LoadDataRow(new object[] { 2012, "G", "Total", "Total", 2 }, false);
            expected.LoadDataRow(new object[] { 2012, "S", "Total", "Total", 5 }, false);

            string source = "DS_r := union(DS_1, DS_2B)";

            List<Exception> errors;
            string sql = this.TranslateVtl(source, out errors);

            Assert.Empty(errors);

            DataTable ds_r = this.RunSql(sql, "#DS_r");

            Assert.True(expected.EqualsTo(ds_r));
        }
    }
}
