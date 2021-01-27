namespace VtlProcessing.IntegrationTests.TSQL.Operators.ReferenceManualExamples.Numeric
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using Xunit;

    public class Exponential : TSQLTestBase
    {
        public Exponential() : base("Exponential")
        {
            List<object[]> ds1 = new List<object[]>
            {
                new object[] { 10, "A", 5, 0.7545 },
                new object[] { 10, "B", 8, 13.45 },
                new object[] { 11, "A", 2, 1.87 }
            };

            this.SqlFillData("[Exponential].DS_1", ds1);
        }

        [Fact]
        public void Example1()
        {
            DataTable expected = new DataTable();
            expected.Columns.Add("Id_1");
            expected.Columns.Add("Id_2");
            expected.Columns.Add("Me_1");
            expected.Columns.Add("Me_2");
            // expected.LoadDataRow(new object[] { 10, "A", 148.413, 2.126547 }, false);
            // expected.LoadDataRow(new object[] { 10, "B", 2980.95, 693842.3 }, false);
            // expected.LoadDataRow(new object[] { 11, "A", 7.38905, 6.488296 }, false);
            // Increased precision for test needs
            expected.LoadDataRow(new object[] { 10, "A", 148.4131591025766, 2.1265479835007413 }, false);
            expected.LoadDataRow(new object[] { 10, "B", 2980.9579870417283, 693842.3137116284 }, false);
            expected.LoadDataRow(new object[] { 11, "A", 7.38905609893065, 6.488296399286712 }, false);

            string source = "DS_r := exp(DS_1)";

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
            // expected.LoadDataRow(new object[] { 10, "A", 148.413, 0.7545 }, false);
            // expected.LoadDataRow(new object[] { 10, "B", 2980.95, 13.45 }, false);
            // expected.LoadDataRow(new object[] { 11, "A", 7.389, 1.87 }, false)
            // Increased precision for test needs
            expected.LoadDataRow(new object[] { 10, "A", 148.4131591025766, 0.7545 }, false);
            expected.LoadDataRow(new object[] { 10, "B", 2980.9579870417283, 13.45 }, false);
            expected.LoadDataRow(new object[] { 11, "A", 7.38905609893065, 1.87 }, false);

            // string source = "DS_r := DS_1[Me_1 := exp(Me_1)]"; - documentation example error
            string source = "DS_r := DS_1[calc Me_1 := exp(Me_1)]";

            List<Exception> errors;
            string sql = this.TranslateVtl(source, out errors);

            Assert.Empty(errors);

            DataTable ds_r = this.RunSql(sql, "#DS_r");

            Assert.True(expected.EqualsTo(ds_r));
        }

        [Fact]
        public void ScalarExample1()
        {
            decimal expected = 148.413159103m;
            string source = "var := exp(5)";

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
            decimal expected = 2.718281828m;
            string source = "var := exp(1)";

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
            decimal expected = 1.0m;
            string source = "var := exp(0)";

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
            decimal expected = 0.367879441m;
            string source = "var := exp(-1)";

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
