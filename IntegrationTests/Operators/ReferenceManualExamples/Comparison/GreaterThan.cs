namespace VtlProcessing.IntegrationTests.TSQL.Operators.ReferenceManualExamples.Comparison
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using Xunit;

    public class GreaterThan : TSQLTestBase
    {
        public GreaterThan() : base("Greater_than")
        {
            List<object[]> ds1a = new List<object[]>
            {
                new object[] { 2, "G", 2011, "Total", "Percentage", null },
                new object[] { 2, "R", 2011, "Total", "Percentage", 12.2 },
                new object[] { 2, "F", 2011, "Total", "Percentage", 29.5 }
            };

            List<object[]> ds1b = new List<object[]>
            {
                new object[] { "G", "Total", "Percentage", "Total", 7.1 },
                new object[] { "R", "Total", "Percentage", "Total", 42.5 }
            };

            List<object[]> ds2 = new List<object[]>
            {
                new object[] { "G", "Total", "Percentage", "Total", 7.5 },
                new object[] { "R", "Total", "Percentage", "Total", 33.7 }
            };

            this.SqlFillData("[Greater_than].DS_1A", ds1a);
            this.SqlFillData("[Greater_than].DS_1B", ds1b);
            this.SqlFillData("[Greater_than].DS_2", ds2);
        }

        [Fact]
        public void Example1()
        {
            DataTable expected = new DataTable();
            expected.Columns.Add("Id_1");
            expected.Columns.Add("Id_2");
            expected.Columns.Add("Id_3");
            expected.Columns.Add("Id_4");
            expected.Columns.Add("Id_5");
            expected.Columns.Add("bool_var");
            // expected.LoadDataRow(new object[] { 2, "G", 2011, "Total", "Percentage", null }, false);
            // expected.LoadDataRow(new object[] { 2, "R", 2011, "Total", "Percentage", false }, false);
            // expected.LoadDataRow(new object[] { 2, "F", 2011, "Total", "Percentage", true }, false);
            // Sorted and changed boolean values to bits for test needs
            expected.LoadDataRow(new object[] { 2, "F", 2011, "Total", "Percentage", 1 }, false);
            expected.LoadDataRow(new object[] { 2, "G", 2011, "Total", "Percentage", null }, false);
            expected.LoadDataRow(new object[] { 2, "R", 2011, "Total", "Percentage", 0 }, false);

            string source = "DS_r := DS_1A > 20";

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
            expected.Columns.Add("Id_5");
            expected.Columns.Add("Me_1");
            expected.Columns.Add("Me_2");
            // expected.LoadDataRow(new object[] { 2, "G", 2011, "Total", "Percentage", null, null }, false);
            // expected.LoadDataRow(new object[] { 2, "R", 2011, "Total", "Percentage", 12.2, false }, false);
            // expected.LoadDataRow(new object[] { 2, "F", 2011, "Total", "Percentage", 29.5, true }, false);
            // Sorted and changed boolean values to bits for test needs
            expected.LoadDataRow(new object[] { 2, "F", 2011, "Total", "Percentage", 29.5, 1 }, false);
            expected.LoadDataRow(new object[] { 2, "G", 2011, "Total", "Percentage", null, null }, false);
            expected.LoadDataRow(new object[] { 2, "R", 2011, "Total", "Percentage", 12.2, 0 }, false);

            string source = "DS_r := DS_1A[calc Me_2 := Me_1 > 20]";

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
            // expected.LoadDataRow(new object[] { "G", "Total", "Percentage", "Total", false }, false);
            // expected.LoadDataRow(new object[] { "R", "Total", "Percentage", "Total", true }, false);
            // Sorted and changed boolean values to bits for test needs
            expected.LoadDataRow(new object[] { "G", "Total", "Percentage", "Total", 0 }, false);
            expected.LoadDataRow(new object[] { "R", "Total", "Percentage", "Total", 1 }, false);

            string source = "DS_r := DS_1B > DS_2";

            List<Exception> errors;
            string sql = this.TranslateVtl(source, out errors);

            Assert.Empty(errors);

            DataTable ds_r = this.RunSql(sql, "#DS_r");

            Assert.True(expected.EqualsTo(ds_r));
        }

        [Fact]
        public void Example4()
        {
            DataTable expected = new DataTable();
            expected.Columns.Add("Id_1");
            expected.Columns.Add("Id_2");
            expected.Columns.Add("Id_3");
            expected.Columns.Add("Id_4");
            expected.Columns.Add("bool_var");
            // expected.LoadDataRow(new object[] { "G", "Total", "Percentage", "Total", null }, false);
            // expected.LoadDataRow(new object[] { "R", "Total", "Percentage", "Total", true }, false);
            // Sorted and changed boolean values to bits for test needs
            expected.LoadDataRow(new object[] { "G", "Total", "Percentage", "Total", null }, false);
            expected.LoadDataRow(new object[] { "R", "Total", "Percentage", "Total", 1 }, false);

            string source = "DS_r := DS_1B > DS_2";

            List<Exception> errors;
            string sql = this.TranslateVtl(source, out errors);

            Assert.Empty(errors);

            List<object[]> ds2 = new List<object[]>
            {
                new object[] {"G", "Total", "Percentage", "Total", null},
                new object[] {"R", "Total", "Percentage", "Total", 33.7}
            };

            this.SqlFillData("[Greater_than].DS_2", ds2);

            DataTable ds_r = this.RunSql(sql, "#DS_r");

            Assert.True(expected.EqualsTo(ds_r));
        }


        [Fact]
        public void ScalarExample1()
        {
            bool expected = false;
            string source = "var := 5 > 9";

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
            string source = "var := 5 >= 5";

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
            string source = "var := \"hello\" > \"hi\"";

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
