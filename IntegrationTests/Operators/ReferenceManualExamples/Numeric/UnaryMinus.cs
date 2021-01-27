namespace VtlProcessing.IntegrationTests.TSQL.Operators.ReferenceManualExamples.Numeric
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using Xunit;

    public class UnaryMinus : TSQLTestBase
    {
        public UnaryMinus() : base("Unary_minus")
        {
            List<object[]> ds1 = new List<object[]>
            {
                new object[] { 10, "A", 1, 5.0 },
                new object[] { 10, "B", 2, 10.0 },
                new object[] { 11, "A", 3, 12.0 }
            };

            this.SqlFillData("[Unary_minus].DS_1", ds1);
        }

        [Fact]
        public void Example1()
        {
            DataTable expected = new DataTable();
            expected.Columns.Add("Id_1");
            expected.Columns.Add("Id_2");
            expected.Columns.Add("Me_1");
            expected.Columns.Add("Me_2");
            expected.LoadDataRow(new object[] { 10, "A", -1, -5.0 }, false);
            expected.LoadDataRow(new object[] { 10, "B", -2, -10.0 }, false);
            expected.LoadDataRow(new object[] { 11, "A", -3, -12.0 }, false);

            string source = "DS_r := - DS_1";

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
            expected.Columns.Add("Me_3");
            expected.LoadDataRow(new object[] { 10, "A", 1, 5.0, -1 }, false);
            expected.LoadDataRow(new object[] { 10, "B", 2, 10.0, -2 }, false);
            expected.LoadDataRow(new object[] { 11, "A", 3, 12.0, -3 }, false);

            string source = "DS_r := DS_1 [calc Me_3 := - Me_1]";

            List<Exception> errors;
            string sql = this.TranslateVtl(source, out errors);

            Assert.Empty(errors);

            DataTable ds_r = this.RunSql(sql, "#DS_r");

            Assert.True(expected.EqualsTo(ds_r));
        }

        [Fact]
        public void ScalarExample1()
        {
            int expected = -3;
            string source = "var := -3";

            List<Exception> errors;
            string sql = this.TranslateVtl(source, out errors);

            Assert.Empty(errors);

            DataTable var = this.RunSql(sql, "@var");

            Assert.Single(var.Columns);
            Assert.Single(var.Rows);
            Assert.Equal(expected, int.Parse(var.Rows[0].Field<string>("value")));
        }

        [Fact]
        public void ScalarExample2()
        {
            int expected = 5;
            string source = "var := -(-5)";

            List<Exception> errors;
            string sql = this.TranslateVtl(source, out errors);

            Assert.Empty(errors);

            DataTable var = this.RunSql(sql, "@var");

            Assert.Single(var.Columns);
            Assert.Single(var.Rows);
            Assert.Equal(expected, int.Parse(var.Rows[0].Field<string>("value")));
        }
    }
}
