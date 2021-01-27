namespace VtlProcessing.IntegrationTests.TSQL.Operators.ReferenceManualExamples.Numeric
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using Xunit;

    public class Multiplication : TSQLTestBase
    {
        public Multiplication() : base("Multiplication")
        {
            List<object[]> ds1 = new List<object[]>
            {
                new object[] { 10, "A", 100, 7.6 },
                new object[] { 10, "B", 10, 12.3 },
                new object[] { 11, "A", 20, 25.0 },
                new object[] { 11, "B", 2, 20.0 }
            };

            List<object[]> ds2 = new List<object[]>
            {
                new object[] { 10, "A", 1, 2.0 },
                new object[] { 10, "C", 5, 3.0 },
                new object[] { 11, "B", 2, 1.0 }
            };

            this.SqlFillData("[Multiplication].DS_1", ds1);
            this.SqlFillData("[Multiplication].DS_2", ds2);
        }

        [Fact]
        public void Example1()
        {
            DataTable expected = new DataTable();
            expected.Columns.Add("Id_1");
            expected.Columns.Add("Id_2");
            expected.Columns.Add("Me_1");
            expected.Columns.Add("Me_2");
            expected.LoadDataRow(new object[] { 10, "A", 100, 15.2 }, false);
            expected.LoadDataRow(new object[] { 11, "B", 4, 20.0 }, false);

            string source = "DS_r := DS_1 * DS_2";

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
            expected.LoadDataRow(new object[] { 10, "A", -300, -22.8 }, false);
            expected.LoadDataRow(new object[] { 10, "B", -30, -36.9 }, false);
            expected.LoadDataRow(new object[] { 11, "A", -60, -75.0 }, false);
            expected.LoadDataRow(new object[] { 11, "B", -6, -60.0 }, false);

            string source = "DS_r := DS_1 * -3";

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
            expected.Columns.Add("Me_3");
            expected.LoadDataRow(new object[] { 10, "A", 100, 7.6, 760.0 }, false);
            expected.LoadDataRow(new object[] { 10, "B", 10, 12.3, 123.0 }, false);
            expected.LoadDataRow(new object[] { 11, "A", 20, 25.0, 500.0 }, false);
            expected.LoadDataRow(new object[] { 11, "B", 2, 20.0, 40.0 }, false);

            string source = "DS_r := DS_1 [calc Me_3 := Me_1 * Me_2]";

            List<Exception> errors;
            string sql = this.TranslateVtl(source, out errors);

            Assert.Empty(errors);

            DataTable ds_r = this.RunSql(sql, "#DS_r");

            Assert.True(expected.EqualsTo(ds_r));
        }

        [Fact]
        public void ScalarExample1()
        {
            int expected = 15;
            string source = "var := 3 * 5";

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
