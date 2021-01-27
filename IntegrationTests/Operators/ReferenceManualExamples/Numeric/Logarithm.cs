namespace VtlProcessing.IntegrationTests.TSQL.Operators.ReferenceManualExamples.Numeric
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using Xunit;

    public class Logarithm : TSQLTestBase
    {
        public Logarithm() : base("Logarithm")
        {
            List<object[]> ds1 = new List<object[]>
            {
                new object[] { 10, "A", 1024, 0.7545 },
                new object[] { 10, "B", 64, 13.45 },
                new object[] { 11, "A", 32, 1.87 }
            };

            this.SqlFillData("[Logarithm].DS_1", ds1);
        }

        [Fact]
        public void Example1()
        {
            DataTable expected = new DataTable();
            expected.Columns.Add("Id_1");
            expected.Columns.Add("Id_2");
            expected.Columns.Add("Me_1");
            expected.Columns.Add("Me_2");
            // expected.LoadDataRow(new object[] { 10, "A", 10.0, -0.40641 }, false);
            // expected.LoadDataRow(new object[] { 10, "B", 6.0, 3.749534 }, false);
            // expected.LoadDataRow(new object[] { 11, "A", 5.0, 0.903038 }, false);
            // Increased precision for test needs
            expected.LoadDataRow(new object[] { 10, "A", 10.0, -0.4064071941354039 }, false);
            expected.LoadDataRow(new object[] { 10, "B", 6.0, 3.749534267669262 }, false);
            expected.LoadDataRow(new object[] { 11, "A", 5.0, 0.9030382701129122 }, false);

            string source = "DS_r := log(DS_1, 2)";

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
            expected.LoadDataRow(new object[] { 10, "A", 10.0, 0.7545 }, false);
            expected.LoadDataRow(new object[] { 10, "B", 6.0, 13.45 }, false);
            expected.LoadDataRow(new object[] { 11, "A", 5.0, 1.87 }, false);

            string source = "DS_r := DS_1[calc Me_1 := log(Me_1, 2)]";

            List<Exception> errors;
            string sql = this.TranslateVtl(source, out errors);

            Assert.Empty(errors);

            DataTable ds_r = this.RunSql(sql, "#DS_r");

            Assert.True(expected.EqualsTo(ds_r));
        }

        [Fact]
        public void ScalarExample1()
        {
            decimal expected = 10;
            string source = "var := log(1024, 2)";

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
            // decimal expected = 3.01m;
            // Increased precision for test needs
            decimal expected = 3.010299957m;
            string source = "var := log(1024, 10)";

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
