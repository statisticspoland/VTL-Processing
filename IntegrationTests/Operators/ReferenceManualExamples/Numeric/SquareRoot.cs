namespace VtlProcessing.IntegrationTests.TSQL.Operators.ReferenceManualExamples.Numeric
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using Xunit;

    public class SquareRoot : TSQLTestBase
    {
        public SquareRoot() : base("Square_root")
        {
            List<object[]> ds1 = new List<object[]>
            {
                new object[] { 10, "A", 16, 0.7545 },
                new object[] { 10, "B", 81, 13.45 },
                new object[] { 11, "A", 64, 1.87 }
            };

            this.SqlFillData("[Square_root].DS_1", ds1);
        }

        [Fact]
        public void Example1()
        {
            DataTable expected = new DataTable();
            expected.Columns.Add("Id_1");
            expected.Columns.Add("Id_2");
            expected.Columns.Add("Me_1");
            expected.Columns.Add("Me_2");
            // expected.LoadDataRow(new object[] { 10, "A", 4, 0.86862 }, false);
            // expected.LoadDataRow(new object[] { 10, "B", 9, 3.667424 }, false);
            // expected.LoadDataRow(new object[] { 11, "A", 8, 1.367479 }, false);
            // Increased precision for test needs
            expected.LoadDataRow(new object[] { 10, "A", 4, 0.8686195945291586 }, false);
            expected.LoadDataRow(new object[] { 10, "B", 9, 3.6674241641784495 }, false);
            expected.LoadDataRow(new object[] { 11, "A", 8, 1.3674794331177345 }, false);

            string source = "DS_r := sqrt(DS_1)";

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
            expected.LoadDataRow(new object[] { 10, "A", 4, 0.7545 }, false);
            expected.LoadDataRow(new object[] { 10, "B", 9, 13.45 }, false);
            expected.LoadDataRow(new object[] { 11, "A", 8, 1.87 }, false);

            string source = "DS_r := DS_1[calc Me_1 := sqrt(Me_1)]";

            List<Exception> errors;
            string sql = this.TranslateVtl(source, out errors);

            Assert.Empty(errors);

            DataTable ds_r = this.RunSql(sql, "#DS_r");

            Assert.True(expected.EqualsTo(ds_r));
        }

        [Fact]
        public void ScalarExample1()
        {
            decimal expected = 5;
            string source = "var := sqrt(25)";

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
