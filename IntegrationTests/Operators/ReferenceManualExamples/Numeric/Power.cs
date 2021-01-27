namespace VtlProcessing.IntegrationTests.TSQL.Operators.ReferenceManualExamples.Numeric
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using Xunit;

    public class Power : TSQLTestBase
    {
        public Power() : base("Power")
        {
            List<object[]> ds1 = new List<object[]>
            {
                new object[] { 10, "A", 3, 0.7545 },
                new object[] { 10, "B", 4, 13.45 },
                new object[] { 11, "A", 5, 1.87 }
            };

            this.SqlFillData("[Power].DS_1", ds1);
        }

        [Fact]
        public void Example1()
        {
            DataTable expected = new DataTable();
            expected.Columns.Add("Id_1");
            expected.Columns.Add("Id_2");
            expected.Columns.Add("Me_1");
            expected.Columns.Add("Me_2");
            // expected.LoadDataRow(new object[] { 10, "A", 9, 0.56927 }, false);
            // expected.LoadDataRow(new object[] { 10, "B", 16, 180.9025 }, false);
            // expected.LoadDataRow(new object[] { 11, "A", 25, 3.4969 }, false);
            // Increased precision for test needs
            expected.LoadDataRow(new object[] { 10, "A", 9, 0.569270250 }, false);
            expected.LoadDataRow(new object[] { 10, "B", 16, 180.902500000 }, false);
            expected.LoadDataRow(new object[] { 11, "A", 25, 3.496900000 }, false);

            string source = "DS_r := power(DS_1, 2)";

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
            expected.LoadDataRow(new object[] { 10, "A", 9, 0.7545 }, false);
            expected.LoadDataRow(new object[] { 10, "B", 16, 13.45 }, false);
            expected.LoadDataRow(new object[] { 11, "A", 25, 1.87 }, false);

            string source = "DS_r := DS_1[calc Me_1 := power(Me_1, 2)]";

            List<Exception> errors;
            string sql = this.TranslateVtl(source, out errors);

            Assert.Empty(errors);

            DataTable ds_r = this.RunSql(sql, "#DS_r");

            Assert.True(expected.EqualsTo(ds_r));
        }

        [Fact]
        public void ScalarExample1()
        {
            decimal expected = 25;
            string source = "var := power(5, 2)";

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
            decimal expected = 5;
            string source = "var := power(5, 1)";

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
            decimal expected = 1;
            string source = "var := power(5, 0)";

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
            decimal expected = 0.2m;
            // string source = "var := power(5, -1)"; - documentation example error
            string source = "var := power(5.0, -1)";

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
            decimal expected = -125;
            string source = "var := power(-5, 3)";

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
