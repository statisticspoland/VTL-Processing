namespace VtlProcessing.IntegrationTests.TSQL.Operators.ReferenceManualExamples.Numeric
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using Xunit;

    public class AbsoluteValue : TSQLTestBase
    {
        public AbsoluteValue() : base("Absolute_value")
        {
            List<object[]> ds1 = new List<object[]>
            {
                new object[] { 10, "A", 0.484183, 0.7545 },
                new object[] { 10, "B", -0.515817, -13.45 },
                new object[] { 11, "A", -1.000000, 187.0 }
            };

            this.SqlFillData("[Absolute_value].DS_1", ds1);
        }

        [Fact]
        public void Example1()
        {
            DataTable expected = new DataTable();
            expected.Columns.Add("Id_1");
            expected.Columns.Add("Id_2");
            expected.Columns.Add("Me_1");
            expected.Columns.Add("Me_2");
            expected.LoadDataRow(new object[] { 10, "A", 0.484183, 0.7545 }, false);
            expected.LoadDataRow(new object[] { 10, "B", 0.515817, 13.45 }, false);
            expected.LoadDataRow(new object[] { 11, "A", 1.000000, 187 }, false);

            string source = "DS_r := abs(DS_1)";

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
            expected.LoadDataRow(new object[] { 10, "A", 0.484183, 0.7545, 0.484183 }, false);
            expected.LoadDataRow(new object[] { 10, "B", -0.515817, -13.45, 0.515817 }, false);
            expected.LoadDataRow(new object[] { 11, "A", -1.000000, 187, 1.000000 }, false);

            // string source = "DS_r := DS_1[Me_10 := abs(Me_1)]"; - documentation example error
            string source = "DS_r := DS_1[calc Me_10 := abs(Me_1)]";

            List<Exception> errors;
            string sql = this.TranslateVtl(source, out errors);

            Assert.Empty(errors);

            DataTable ds_r = this.RunSql(sql, "#DS_r");

            Assert.True(expected.EqualsTo(ds_r));
        }

        [Fact]
        public void ScalarExample1()
        {
            decimal expected = 5.49m;
            string source = "var := abs(-5.49)";

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
            decimal expected = 5.49m;
            string source = "var := abs(5.49)";

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
