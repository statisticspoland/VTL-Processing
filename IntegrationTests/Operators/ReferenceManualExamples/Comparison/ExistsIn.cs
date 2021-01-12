namespace VtlProcessing.IntegrationTests.TSQL.Operators.ReferenceManualExamples.Comparison
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using Xunit;

    public class ExistsIn : TSQLTestBase
    {
        public ExistsIn() : base("Exists_in")
        {
            List<object[]> ds1 = new List<object[]>
            {
                new object[] { 2012, "B", "Total", "Total", 11094850 },
                new object[] { 2012, "G", "Total", "Total", 11123034 },
                new object[] { 2012, "S", "Total", "Total", 46818219 },
                new object[] { 2012, "M", "Total", "Total", 417546 },
                new object[] { 2012, "F", "Total", "Total", 5401267 },
                new object[] { 2012, "W", "Total", "Total", 7954662 }
            };

            List<object[]> ds2 = new List<object[]>
            {
                new object[] { 2012, "B", "Total", "Total", 0.023 },
                new object[] { 2012, "G", "Total", "M", 0.286 },
                new object[] { 2012, "S", "Total", "Total", 0.064 },
                new object[] { 2012, "M", "Total", "M", 0.043 },
                new object[] { 2012, "F", "Total", "Total", null },
                new object[] { 2012, "W", "Total", "Total", 0.08 }
            };

            this.SqlFillData("[Exists_In].DS_1", ds1);
            this.SqlFillData("[Exists_In].DS_2", ds2);
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
            // expected.LoadDataRow(new object[] { 2012, "B", "Total", "Total", true }, false);
            // expected.LoadDataRow(new object[] { 2012, "G", "Total", "Total", false }, false);
            // expected.LoadDataRow(new object[] { 2012, "S", "Total", "Total", true }, false);
            // expected.LoadDataRow(new object[] { 2012, "M", "Total", "Total", false }, false);
            // expected.LoadDataRow(new object[] { 2012, "F", "Total", "Total", true }, false);
            // expected.LoadDataRow(new object[] { 2012, "W", "Total", "Total", true }, false);
            // Sorted and changed boolean values to bits for test needs
            expected.LoadDataRow(new object[] { 2012, "B", "Total", "Total", 1 }, false);
            expected.LoadDataRow(new object[] { 2012, "F", "Total", "Total", 1 }, false);
            expected.LoadDataRow(new object[] { 2012, "G", "Total", "Total", 0 }, false);
            expected.LoadDataRow(new object[] { 2012, "M", "Total", "Total", 0 }, false);
            expected.LoadDataRow(new object[] { 2012, "S", "Total", "Total", 1 }, false);
            expected.LoadDataRow(new object[] { 2012, "W", "Total", "Total", 1 }, false);

            string source = "DS_r := exists_in(DS_1, DS_2, all)";

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
            expected.Columns.Add("bool_var");
            // expected.LoadDataRow(new object[] { 2012, "B", "Total", "Total", true }, false);
            // expected.LoadDataRow(new object[] { 2012, "S", "Total", "Total", true }, false);
            // expected.LoadDataRow(new object[] { 2012, "F", "Total", "Total", true }, false);
            // expected.LoadDataRow(new object[] { 2012, "W", "Total", "Total", true }, false);
            // Sorted and changed boolean values to bits for test needs
            expected.LoadDataRow(new object[] { 2012, "B", "Total", "Total", 1 }, false);
            expected.LoadDataRow(new object[] { 2012, "F", "Total", "Total", 1 }, false);
            expected.LoadDataRow(new object[] { 2012, "S", "Total", "Total", 1 }, false);
            expected.LoadDataRow(new object[] { 2012, "W", "Total", "Total", 1 }, false);

            string source = "DS_r := exists_in(DS_1, DS_2, true)";

            List<Exception> errors;
            string sql = this.TranslateVtl(source, out errors);

            Assert.Empty(errors);

            DataTable ds_r = this.RunSql(sql, "#DS_r");

            Assert.True(expected.EqualsTo(ds_r));
        }

        [Fact]
        public void Example3()
        {
            DataTable expected = new DataTable();
            expected.Columns.Add("Id_1");
            expected.Columns.Add("Id_2");
            expected.Columns.Add("Id_3");
            expected.Columns.Add("Id_4");
            expected.Columns.Add("bool_var");
            // expected.LoadDataRow(new object[] { 2012, "G", "Total", "Total", false }, false);
            // expected.LoadDataRow(new object[] { 2012, "M", "Total", "Total", false }, false);
            // Sorted and changed boolean values to bits for test needs
            expected.LoadDataRow(new object[] { 2012, "G", "Total", "Total", 0 }, false);
            expected.LoadDataRow(new object[] { 2012, "M", "Total", "Total", 0 }, false);

            string source = "DS_r := exists_in(DS_1, DS_2, false)";

            List<Exception> errors;
            string sql = this.TranslateVtl(source, out errors);

            Assert.Empty(errors);

            DataTable ds_r = this.RunSql(sql, "#DS_r");

            Assert.True(expected.EqualsTo(ds_r));
        }
    }
}
