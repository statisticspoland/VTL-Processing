namespace VtlProcessing.IntegrationTests.TSQL.Operators.ReferenceManualExamples.Comparison
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using Xunit;

    public class IsNull : TSQLTestBase
    {
        public IsNull() : base("IsNull")
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

            this.SqlFillData("[IsNull].DS_1", ds1);
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
            // expected.LoadDataRow(new object[] { 2012, "B", "Total", "Total", false }, false);
            // expected.LoadDataRow(new object[] { 2012, "G", "Total", "Total", false }, false);
            // expected.LoadDataRow(new object[] { 2012, "S", "Total", "Total", true }, false);
            // expected.LoadDataRow(new object[] { 2012, "M", "Total", "Total", false }, false);
            // expected.LoadDataRow(new object[] { 2012, "F", "Total", "Total", false }, false);
            // expected.LoadDataRow(new object[] { 2012, "N", "Total", "Total", true }, false);
            // Sorted and changed boolean values to bits for test needs
            expected.LoadDataRow(new object[] { 2012, "B", "Total", "Total", 0 }, false);
            expected.LoadDataRow(new object[] { 2012, "F", "Total", "Total", 0 }, false);
            expected.LoadDataRow(new object[] { 2012, "G", "Total", "Total", 0 }, false);
            expected.LoadDataRow(new object[] { 2012, "M", "Total", "Total", 0 }, false);
            expected.LoadDataRow(new object[] { 2012, "N", "Total", "Total", 1 }, false);
            expected.LoadDataRow(new object[] { 2012, "S", "Total", "Total", 1 }, false);

            string source = "DS_r := isnull(DS_1)";

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
            expected.Columns.Add("Me_2");
            // expected.LoadDataRow(new object[] { 2012, "B", "Total", "Total", 11094850, false }, false);
            // expected.LoadDataRow(new object[] { 2012, "G", "Total", "Total", 11123034, false }, false);
            // expected.LoadDataRow(new object[] { 2012, "S", "Total", "Total", null, true }, false);
            // expected.LoadDataRow(new object[] { 2012, "M", "Total", "Total", 417546, false }, false);
            // expected.LoadDataRow(new object[] { 2012, "F", "Total", "Total", 5401267, false }, false);
            // expected.LoadDataRow(new object[] { 2012, "N", "Total", "Total", null, true }, false);
            // Sorted and changed boolean values to bits for test needs
            expected.LoadDataRow(new object[] { 2012, "B", "Total", "Total", 11094850, 0 }, false);
            expected.LoadDataRow(new object[] { 2012, "F", "Total", "Total", 5401267, 0 }, false);
            expected.LoadDataRow(new object[] { 2012, "G", "Total", "Total", 11123034, 0 }, false);
            expected.LoadDataRow(new object[] { 2012, "M", "Total", "Total", 417546, 0 }, false);
            expected.LoadDataRow(new object[] { 2012, "N", "Total", "Total", null, 1 }, false);
            expected.LoadDataRow(new object[] { 2012, "S", "Total", "Total", null, 1 }, false);

            // string source = "DS_r := DS_1[Me_2 := is_null(Me_1)]"; - documentation example error
            string source = "DS_r := DS_1[calc Me_2 := isnull(Me_1)]";

            List<Exception> errors;
            string sql = this.TranslateVtl(source, out errors);

            Assert.Empty(errors);

            DataTable ds_r = this.RunSql(sql, "#DS_r");

            Assert.True(expected.EqualsTo(ds_r));
        }

        [Fact]
        public void ScalarExample1()
        {
            bool expected = false;
            string source = "var := isnull(\"Hello\")";

            List<Exception> errors;
            string sql = this.TranslateVtl(source, out errors);

            Assert.Empty(errors);

            DataTable var = this.RunSql(sql, "@var");

            Assert.Single(var.Columns);
            Assert.Single(var.Rows);
            Assert.Equal(expected, bool.Parse(var.Rows[0].Field<string>("value")));
        }

        [Fact]
        public void ScalarExample2()
        {
            bool expected = true;
            // string source = "var := isnull(NULL)"; - documentation example error
            string source = "var := isnull(null)";

            List<Exception> errors;
            string sql = this.TranslateVtl(source, out errors);

            Assert.Empty(errors);

            DataTable var = this.RunSql(sql, "@var");

            Assert.Single(var.Columns);
            Assert.Single(var.Rows);
            Assert.Equal(expected, bool.Parse(var.Rows[0].Field<string>("value")));
        }
    }
}
