namespace VtlProcessing.IntegrationTests.TSQL.Operators.ReferenceManualExamples.Numeric
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using Xunit;

    public class UnaryPlus : TSQLTestBase
    {
        public UnaryPlus() : base("Unary_plus")
        {
            List<object[]> ds1 = new List<object[]>
            {
                new object[] { 10, "A", 1.0, 5 },
                new object[] { 10, "B", 2.3, 10 },
                new object[] { 11, "A", 3.2, 12 }
            };

            this.SqlFillData("[Unary_plus].DS_1", ds1);
        }

        [Fact]
        public void Example1()
        {
            DataTable expected = new DataTable();
            expected.Columns.Add("Id_1");
            expected.Columns.Add("Id_2");
            expected.Columns.Add("Me_1");
            expected.Columns.Add("Me_2");
            expected.LoadDataRow(new object[] { 10, "A", 1.0, 5 }, false);
            expected.LoadDataRow(new object[] { 10, "B", 2.3, 10 }, false);
            expected.LoadDataRow(new object[] { 11, "A", 3.2, 12 }, false);

            string source = "DS_r := + DS_1";

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
            expected.LoadDataRow(new object[] { 10, "A", 1.0, 5, 1.0 }, false);
            expected.LoadDataRow(new object[] { 10, "B", 2.3, 10, 2.3 }, false);
            expected.LoadDataRow(new object[] { 11, "A", 3.2, 12, 3.2 }, false);

            string source = "DS_r := DS_1[calc Me_3 := + Me_1]";

            List<Exception> errors;
            string sql = this.TranslateVtl(source, out errors);

            Assert.Empty(errors);

            DataTable ds_r = this.RunSql(sql, "#DS_r");

            Assert.True(expected.EqualsTo(ds_r));
        }

        [Fact]
        public void ScalarExample1()
        {
            int expected = 3;
            string source = "var := +3";

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
            int expected = -5;
            string source = "var := +(-5)";

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
