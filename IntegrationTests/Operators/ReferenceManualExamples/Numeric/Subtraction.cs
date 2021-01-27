namespace VtlProcessing.IntegrationTests.TSQL.Operators.ReferenceManualExamples.Numeric
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using Xunit;

    public class Subtraction : TSQLTestBase
    {
        public Subtraction() : base("Subtraction")
        {
            List<object[]> ds1 = new List<object[]>
            {
                new object[] { 10, "A", 5, 5.0 },
                new object[] { 10, "B", 2, 10.5 },
                new object[] { 11, "A", 3, 12.2 },
                new object[] { 11, "B", 4, 20.3 }
            };

            List<object[]> ds2 = new List<object[]>
            {
                new object[] { 10, "A", 10, 3.0 },
                new object[] { 10, "C", 11, 6.2 },
                new object[] { 11, "B", 6, 7.0 }
            };

            this.SqlFillData("[Subtraction].DS_1", ds1);
            this.SqlFillData("[Subtraction].DS_2", ds2);
        }

        [Fact]
        public void Example1()
        {
            DataTable expected = new DataTable();
            expected.Columns.Add("Id_1");
            expected.Columns.Add("Id_2");
            expected.Columns.Add("Me_1");
            expected.Columns.Add("Me_2");
            expected.LoadDataRow(new object[] { 10, "A", -5, 2.0 }, false);
            expected.LoadDataRow(new object[] { 11, "B", -2, 13.3 }, false);

            string source = "DS_r := DS_1 - DS_2";

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
            expected.LoadDataRow(new object[] { 10, "A", 2, 2.0 }, false);
            expected.LoadDataRow(new object[] { 10, "B", -1, 7.5 }, false);
            expected.LoadDataRow(new object[] { 11, "A", 0, 9.2 }, false);
            expected.LoadDataRow(new object[] { 11, "B", 1, 17.3 }, false);

            string source = "DS_r := DS_1 - 3";

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
            expected.LoadDataRow(new object[] { 10, "A", 5, 5.0, 2 }, false);
            expected.LoadDataRow(new object[] { 10, "B", 2, 10.5, -1 }, false);
            expected.LoadDataRow(new object[] { 11, "A", 3, 12.2, 0 }, false);
            expected.LoadDataRow(new object[] { 11, "B", 4, 20.3, 1 }, false);

            string source = "DS_r := DS_1 [calc Me_3 := Me_1 - 3]";

            List<Exception> errors;
            string sql = this.TranslateVtl(source, out errors);

            Assert.Empty(errors);

            DataTable ds_r = this.RunSql(sql, "#DS_r");

            Assert.True(expected.EqualsTo(ds_r));
        }

        [Fact]
        public void ScalarExample1()
        {
            int expected = -2;
            string source = "var := 3 - 5";

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
