namespace VtlProcessing.IntegrationTests.TSQL.Operators.ReferenceManualExamples.Comparison
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using Xunit;

    public class EqualTo : TSQLTestBase
    {
        public EqualTo() : base("Equal_to")
        {
            List<object[]> ds1 = new List<object[]>
            {
                new object[] { 2012, "B", "Total", "Total", null },
                new object[] { 2012, "G", "Total", "Total", 0.286 },
                new object[] { 2012, "S", "Total", "Total", 0.064 },
                new object[] { 2012, "M", "Total", "Total", 0.043 },
                new object[] { 2012, "F", "Total", "Total", 0.08 },
                new object[] { 2012, "W", "Total", "Total", 0.08 }
            };

            this.SqlFillData("[Equal_to].DS_1", ds1);
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
            // expected.LoadDataRow(new object[] { 2012, "B", "Total", "Total", null }, false);
            // expected.LoadDataRow(new object[] { 2012, "G", "Total", "Total", false }, false);
            // expected.LoadDataRow(new object[] { 2012, "S", "Total", "Total", false }, false);
            // expected.LoadDataRow(new object[] { 2012, "M", "Total", "Total", false }, false);
            // expected.LoadDataRow(new object[] { 2012, "F", "Total", "Total", true }, false);
            // expected.LoadDataRow(new object[] { 2012, "W", "Total", "Total", true }, false);
            // Sorted and changed boolean values to bits for test needs
            expected.LoadDataRow(new object[] { 2012, "B", "Total", "Total", null }, false);
            expected.LoadDataRow(new object[] { 2012, "F", "Total", "Total", 1 }, false);
            expected.LoadDataRow(new object[] { 2012, "G", "Total", "Total", 0 }, false);
            expected.LoadDataRow(new object[] { 2012, "M", "Total", "Total", 0 }, false);
            expected.LoadDataRow(new object[] { 2012, "S", "Total", "Total", 0 }, false);
            expected.LoadDataRow(new object[] { 2012, "W", "Total", "Total", 1 }, false);

            string source = "DS_r := DS_1 = 0.08";

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
            // expected.LoadDataRow(new object[] { 2012, "B", "Total", "Total", null, null }, false);
            // expected.LoadDataRow(new object[] { 2012, "G", "Total", "Total", 0.286, false }, false);
            // expected.LoadDataRow(new object[] { 2012, "S", "Total", "Total", 0.064, false }, false);
            // expected.LoadDataRow(new object[] { 2012, "M", "Total", "Total", 0.043, false }, false);
            // expected.LoadDataRow(new object[] { 2012, "F", "Total", "Total", 0.08, true }, false);
            // expected.LoadDataRow(new object[] { 2012, "W", "Total", "Total", 0.08, true }, false);
            // Sorted and changed boolean values to bits for test needs
            expected.LoadDataRow(new object[] { 2012, "B", "Total", "Total", null, null }, false);
            expected.LoadDataRow(new object[] { 2012, "F", "Total", "Total", 0.08, 1 }, false);
            expected.LoadDataRow(new object[] { 2012, "G", "Total", "Total", 0.286, 0 }, false);
            expected.LoadDataRow(new object[] { 2012, "M", "Total", "Total", 0.043, 0 }, false);
            expected.LoadDataRow(new object[] { 2012, "S", "Total", "Total", 0.064, 0 }, false);
            expected.LoadDataRow(new object[] { 2012, "W", "Total", "Total", 0.08, 1 }, false);

            string source = "DS_r := DS_1[calc Me_2 := Me_1 = 0.08]";

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
            string source = "var := 5 = 9";

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
            string source = "var := 5 = 5";

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
            bool expected = false;
            string source = "var := \"hello\" = \"hi\"";

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
