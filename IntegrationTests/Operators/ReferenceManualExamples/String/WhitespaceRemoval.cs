namespace VtlProcessing.IntegrationTests.TSQL.Operators.ReferenceManualExamples.String
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using Xunit;

    public class WhitespaceRemoval : TSQLTestBase
    {
        public WhitespaceRemoval() : base("Whitespace_removal")
        {
            List<object[]> ds1 = new List<object[]>
            {
                new object[] { 1, "A", "hello   " },
                new object[] { 2, "B", "hi    " }
            };

            this.SqlFillData("[Whitespace_removal].DS_1", ds1);
        }

        [Fact]
        public void Example1()
        {
            DataTable expected = new DataTable();
            expected.Columns.Add("Id_1");
            expected.Columns.Add("Id_2");
            expected.Columns.Add("Me_1");
            expected.LoadDataRow(new object[] { 1, "A", "hello" }, false);
            expected.LoadDataRow(new object[] { 2, "B", "hi" }, false);

            string source = "DS_r := rtrim(DS_1)";

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
            expected.LoadDataRow(new object[] { 1, "A", "hello   ", "hello" }, false);
            expected.LoadDataRow(new object[] { 2, "B", "hi    ", "hi" }, false);

            string source = "DS_r := DS_1[ calc Me_2:= rtrim(Me_1)]";

            List<Exception> errors;
            string sql = this.TranslateVtl(source, out errors);

            Assert.Empty(errors);

            DataTable ds_r = this.RunSql(sql, "#DS_r");

            Assert.True(expected.EqualsTo(ds_r));
        }

        [Fact]
        public void ScalarExample1()
        {
            // TODO: Update database to support TRIM
            string expected = "Hello";
            string source = "var := trim(\"Hello \")";

            List<Exception> errors;
            string sql = this.TranslateVtl(source, out errors);

            Assert.Empty(errors);

            DataTable var = this.RunSql(sql, "@var");

            Assert.Single(var.Columns);
            Assert.Single(var.Rows);
            Assert.Equal(expected, var.Rows[0].Field<string>("value"));
        }
    }
}
