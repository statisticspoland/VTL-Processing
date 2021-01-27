namespace VtlProcessing.IntegrationTests.TSQL.Operators.ReferenceManualExamples.Numeric
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using Xunit;

    public class Rounding : TSQLTestBase
    {
        public Rounding() : base("Rounding")
        {
            List<object[]> ds1 = new List<object[]>
            {
                new object[] { 10, "A", 7.5, 5.9 },
                new object[] { 10, "B", 7.1, 5.5 },
                new object[] { 11, "A", 36.2, 17.7 },
                new object[] { 11, "B", 44.5, 24.3 }
            };

            this.SqlFillData("[Rounding].DS_1", ds1);
        }

        [Fact]
        public void Example1()
        {
            DataTable expected = new DataTable();
            expected.Columns.Add("Id_1");
            expected.Columns.Add("Id_2");
            expected.Columns.Add("Me_1");
            expected.Columns.Add("Me_2");
            expected.LoadDataRow(new object[] { 10, "A", 8.0, 6.0 }, false);
            expected.LoadDataRow(new object[] { 10, "B", 7.0, 6.0 }, false);
            expected.LoadDataRow(new object[] { 11, "A", 36.0, 18.0 }, false);
            expected.LoadDataRow(new object[] { 11, "B", 45.0, 24.0 }, false);

            string source = "DS_r := round(DS_1, 0)";

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
            expected.Columns.Add("Me_1");
            expected.Columns.Add("Me_2");
            expected.Columns.Add("Me_10");
            expected.LoadDataRow(new object[] { 10, "A", 7.5, 5.9, 8 }, false);
            expected.LoadDataRow(new object[] { 10, "B", 7.1, 5.5, 7 }, false);
            expected.LoadDataRow(new object[] { 11, "A", 36.2, 17.7, 36 }, false);
            expected.LoadDataRow(new object[] { 11, "B", 44.5, 24.3, 45 }, false);

            string source = "DS_r := DS_1[calc Me_10:= round(Me_1)]";

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
            expected.Columns.Add("Me_1");
            expected.Columns.Add("Me_2");
            expected.Columns.Add("Me_20");
            expected.LoadDataRow(new object[] { 10, "A", 7.5, 5.9, 10 }, false);
            expected.LoadDataRow(new object[] { 10, "B", 7.1, 5.5, 10 }, false);
            expected.LoadDataRow(new object[] { 11, "A", 36.2, 17.7, 40 }, false);
            expected.LoadDataRow(new object[] { 11, "B", 44.5, 24.3, 40 }, false);

            string source = "DS_r := DS_1 [calc Me_20 := round(Me_1, -1)]";

            List<Exception> errors;
            string sql = this.TranslateVtl(source, out errors);

            Assert.Empty(errors);

            DataTable ds_r = this.RunSql(sql, "#DS_r");

            Assert.True(expected.EqualsTo(ds_r));
        }

        [Fact]
        public void ScalarExample1()
        {
            decimal expected = 3.14m;
            string source = "var := round(3.14159, 2)";

            List<Exception> errors;
            string sql = this.TranslateVtl(source, out errors);

            Assert.Empty(errors);

            DataTable var = this.RunSql(sql, "@var");

            Assert.Single(var.Columns);
            Assert.Single(var.Rows);
            Assert.Equal(expected, decimal.Parse(var.Rows[0].Field<string>("value")));
        }

        [Fact]
        public void ScalarExample2()
        {
            decimal expected = 3.1416m;
            string source = "var := round(3.14159, 4)";

            List<Exception> errors;
            string sql = this.TranslateVtl(source, out errors);

            Assert.Empty(errors);

            DataTable var = this.RunSql(sql, "@var");

            Assert.Single(var.Columns);
            Assert.Single(var.Rows);
            Assert.Equal(expected, decimal.Parse(var.Rows[0].Field<string>("value")));
        }

        [Fact]
        public void ScalarExample3()
        {
            decimal expected = 12346.0m;
            string source = "var := round(12345.6, 0)";

            List<Exception> errors;
            string sql = this.TranslateVtl(source, out errors);

            Assert.Empty(errors);

            DataTable var = this.RunSql(sql, "@var");

            Assert.Single(var.Columns);
            Assert.Single(var.Rows);
            Assert.Equal(expected, decimal.Parse(var.Rows[0].Field<string>("value")));
        }

        [Fact]
        public void ScalarExample4()
        {
            decimal expected = 12346;
            string source = "var := round(12345.6)";

            List<Exception> errors;
            string sql = this.TranslateVtl(source, out errors);

            Assert.Empty(errors);

            DataTable var = this.RunSql(sql, "@var");

            Assert.Single(var.Columns);
            Assert.Single(var.Rows);
            Assert.Equal(expected, decimal.Parse(var.Rows[0].Field<string>("value")));
        }

        [Fact]
        public void ScalarExample5()
        {
            decimal expected = 12346;
            string source = "var := round(12345.6, _)";

            List<Exception> errors;
            string sql = this.TranslateVtl(source, out errors);

            Assert.Empty(errors);

            DataTable var = this.RunSql(sql, "@var");

            Assert.Single(var.Columns);
            Assert.Single(var.Rows);
            Assert.Equal(expected, decimal.Parse(var.Rows[0].Field<string>("value")));
        }

        [Fact]
        public void ScalarExample6()
        {
            decimal expected = 12350.0m;
            string source = "var := round(12345.6, -1)";

            List<Exception> errors;
            string sql = this.TranslateVtl(source, out errors);

            Assert.Empty(errors);

            DataTable var = this.RunSql(sql, "@var");

            Assert.Single(var.Columns);
            Assert.Single(var.Rows);
            Assert.Equal(expected, decimal.Parse(var.Rows[0].Field<string>("value")));
        }
    }
}
