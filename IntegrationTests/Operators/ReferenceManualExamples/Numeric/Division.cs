namespace VtlProcessing.IntegrationTests.TSQL.Operators.ReferenceManualExamples.Numeric
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using Xunit;

    public class Division : TSQLTestBase
    {
        public Division() : base("Division")
        {
            List<object[]> ds1 = new List<object[]>
            {
                new object[] { 10, "A", 100, 7.6 },
                new object[] { 10, "B", 10, 12.3 },
                new object[] { 11, "A", 20, 25.0 },
                new object[] { 11, "B", 10, 12.3 }
            };

            List<object[]> ds2 = new List<object[]>
            {
                new object[] { 10, "A", 1, 2.0 },
                new object[] { 10, "C", 5, 3.0 },
                new object[] { 11, "B", 2, 1.0 }
            };

            this.SqlFillData("[Division].DS_1", ds1);
            this.SqlFillData("[Division].DS_2", ds2);
        }

        [Fact]
        public void Example1()
        {
            DataTable expected = new DataTable();
            expected.Columns.Add("Id_1");
            expected.Columns.Add("Id_2");
            expected.Columns.Add("Me_1");
            expected.Columns.Add("Me_2");
            expected.LoadDataRow(new object[] { 10, "A", 100, 3.8 }, false);
            // expected.LoadDataRow(new object[] { 11, "B", 10, 25.0 }, false); - documentation example result error
            expected.LoadDataRow(new object[] { 11, "B", 5, 12.3 }, false);

            string source = "DS_r := DS_1 / DS_2";

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
            expected.LoadDataRow(new object[] { 10, "A", 10, 0.76 }, false);
            expected.LoadDataRow(new object[] { 10, "B", 1, 1.23 }, false);
            expected.LoadDataRow(new object[] { 11, "A", 2, 2.5 }, false);
            // expected.LoadDataRow(new object[] { 11, "B", 0.2, 2.0 }, false); - documentation example result error
            expected.LoadDataRow(new object[] { 11, "B", 1, 1.23 }, false);

            string source = "DS_r := DS_1 / 10";

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
            expected.LoadDataRow(new object[] { 10, "A", 100, 7.6, 0.076 }, false);
            expected.LoadDataRow(new object[] { 10, "B", 10, 12.3, 1.23 }, false);
            expected.LoadDataRow(new object[] { 11, "A", 20, 25.0, 1.25 }, false);
            // expected.LoadDataRow(new object[] { 11, "B", 2, 20.0, 10.0 }, false); - documentation example result error
            expected.LoadDataRow(new object[] { 11, "B", 10, 12.3, 1.23 }, false);

            string source = "DS_r := DS_1 [calc Me_3 := Me_2 / Me_1]";

            List<Exception> errors;
            string sql = this.TranslateVtl(source, out errors);

            Assert.Empty(errors);

            DataTable ds_r = this.RunSql(sql, "#DS_r");

            Assert.True(expected.EqualsTo(ds_r));
        }

        [Fact]
        public void ScalarExample1()
        {
            decimal expected = 0.6m;
            // string source = "var := 3 / 5"; - documentation example error
            string source = "var := 3.0 / 5";

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
