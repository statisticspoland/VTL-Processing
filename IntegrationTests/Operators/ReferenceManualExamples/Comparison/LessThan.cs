namespace VtlProcessing.IntegrationTests.TSQL.Operators.ReferenceManualExamples.Comparison
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using Xunit;

    public class LessThan : TSQLTestBase
    {

        public LessThan() : base("Less_than")
        {
            List<object[]> ds1 = new List<object[]>
            {
                new object[] { 2012, "B", "Total", "Total", 11094850 },
                new object[] { 2012, "G", "Total", "Total", 11123034 },
                new object[] { 2012, "S", "Total", "Total", 46818219 },
                new object[] { 2012, "M", "Total", "Total", null },
                new object[] { 2012, "F", "Total", "Total", 5401267 },
                new object[] { 2012, "W", "Total", "Total", 7954662 }
            };

            this.SqlFillData("[Less_than].DS_1", ds1);
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
            // expected.LoadDataRow(new object[] { 2012, "G", "Total", "Total", true }, false);
            // expected.LoadDataRow(new object[] { 2012, "S", "Total", "Total", false }, false);
            // expected.LoadDataRow(new object[] { 2012, "M", "Total", "Total", null }, false);
            // expected.LoadDataRow(new object[] { 2012, "F", "Total", "Total", true }, false);
            // expected.LoadDataRow(new object[] { 2012, "W", "Total", "Total", true }, false);
            // Sorted and changed boolean values to bits for test needs
            expected.LoadDataRow(new object[] { 2012, "B", "Total", "Total", 1 }, false);
            expected.LoadDataRow(new object[] { 2012, "F", "Total", "Total", 1 }, false);
            expected.LoadDataRow(new object[] { 2012, "G", "Total", "Total", 1 }, false);
            expected.LoadDataRow(new object[] { 2012, "M", "Total", "Total", null }, false);
            expected.LoadDataRow(new object[] { 2012, "S", "Total", "Total", 0 }, false);
            expected.LoadDataRow(new object[] { 2012, "W", "Total", "Total", 1 }, false);

            string source = "DS_r := DS_1 < 15000000";

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
            string source = "var := 5 < 4";

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
            string source = "var := 5 <= 5";

            List<Exception> errors;
            string sql = this.TranslateVtl(source, out errors);

            Assert.Empty(errors);

            DataTable var = this.RunSql(sql, "@var");

            Assert.Single(var.Columns);
            Assert.Single(var.Rows);
            Assert.Equal(expected, bool.Parse(var.Rows[0].Field<string>("value")));
        }

        [Fact]
        public void ScalarExample3()
        {
            bool expected = true;
            string source = "var := \"hello\" < \"hi\"";

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
