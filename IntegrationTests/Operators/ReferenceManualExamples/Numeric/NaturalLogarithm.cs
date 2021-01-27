namespace VtlProcessing.IntegrationTests.TSQL.Operators.ReferenceManualExamples.Numeric
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using Xunit;

    public class NaturalLogarithm : TSQLTestBase
    {
        public NaturalLogarithm() : base("Natural_logarithm")
        {
            List<object[]> ds1 = new List<object[]>
            {
                new object[] { 10, "A", 148.413, 0.7545 },
                new object[] { 10, "B", 2980.95, 13.45 },
                new object[] { 11, "A", 7.38905, 1.87 }
            };

            this.SqlFillData("[Natural_logarithm].DS_1", ds1);
        }

        [Fact]
        public void Example1()
        {
            DataTable expected = new DataTable();
            expected.Columns.Add("Id_1");
            expected.Columns.Add("Id_2");
            expected.Columns.Add("Me_1");
            expected.Columns.Add("Me_2");
            // expected.LoadDataRow(new object[] { 10, "A", 5.0, -0.281700 }, false);
            // expected.LoadDataRow(new object[] { 10, "B", 8.0, 2.598979 }, false);
            // expected.LoadDataRow(new object[] { 11, "A", 2.0, 0.625938 }, false);
            // Increased precision for test needs
            expected.LoadDataRow(new object[] { 10, "A", 4.999998927974697, -0.2817000007742335 }, false);
            expected.LoadDataRow(new object[] { 10, "B", 7.999997320642403, 2.598979106047848 }, false);
            expected.LoadDataRow(new object[] { 11, "A", 1.9999991745991523, 0.6259384308664954 }, false);

            string source = "DS_r := ln(DS_1)";

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
            // expected.LoadDataRow(new object[] { 10, "A", 148.413, 5.0 }, false);
            // expected.LoadDataRow(new object[] { 10, "B", 2980.95, 8.0 }, false);
            // expected.LoadDataRow(new object[] { 11, "A", 7.38905, 2.0 }, false);
            // Increased precision for test needs
            expected.LoadDataRow(new object[] { 10, "A", 148.413, 4.999998927974697 }, false);
            expected.LoadDataRow(new object[] { 10, "B", 2980.95, 7.999997320642403 }, false);
            expected.LoadDataRow(new object[] { 11, "A", 7.38905, 1.9999991745991523 }, false);

            // string source = "DS_r := DS_1[Me_2 := ln(DS_1#Me_1)]"; - documentation example error
            string source = "DS_r := DS_1[calc Me_2 := ln(Me_1)]";

            List<Exception> errors;
            string sql = this.TranslateVtl(source, out errors);

            Assert.Empty(errors);

            DataTable ds_r = this.RunSql(sql, "#DS_r");

            Assert.True(expected.EqualsTo(ds_r));
        }

        [Fact]
        public void ScalarExample1()
        {
            decimal expected = 4.997212274m;
            string source = "var := ln(148)";

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
            decimal expected = 1.0m;
            // string source = "var := ln(e)";
            // Number e assignment for test needs
            string source =
                "e := 2.718281828459;" +
                "var := ln(e)";

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
            decimal expected = 0.0m;
            string source = "var := ln(1)";

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
            decimal expected = -0.693147181m;
            string source = "var := ln(0.5)";

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
