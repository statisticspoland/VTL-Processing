namespace VtlProcessing.IntegrationTests.TSQL.Operators.ReferenceManualExamples.String
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using Xunit;

    public class SubStringExtraction : TSQLTestBase
    {
        public SubStringExtraction() : base("Sub_string_extraction")
        {
            List<object[]> ds1 = new List<object[]>
            {
                new object[] { 1, "A", "hello world", "medium size text" },
                new object[] { 1, "B", "abcdefghilmno", "short text" },
                new object[] { 2, "A", "pqrstuvwxyz", "this is a long description" }
            };

            this.SqlFillData("[Sub_string_extraction].DS_1", ds1);
        }

        [Fact]
        public void Example1()
        {
            DataTable expected = new DataTable();
            expected.Columns.Add("Id_1");
            expected.Columns.Add("Id_2");
            expected.Columns.Add("Me_1");
            expected.Columns.Add("Me_2");
            expected.LoadDataRow(new object[] { 1, "A", "world", " size text" }, false);
            expected.LoadDataRow(new object[] { 1, "B", "ghilmno", "text" }, false);
            expected.LoadDataRow(new object[] { 2, "A", "vwxyz", "s a long description" }, false);

            string source = "DS_r := substr(DS_1, 7)";

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
            expected.LoadDataRow(new object[] { 1, "A", "hello", "mediu" }, false);
            expected.LoadDataRow(new object[] { 1, "B", "abcde", "short" }, false);
            expected.LoadDataRow(new object[] { 2, "A", "pqrst", "this " }, false);

            string source = "DS_r := substr(DS_1, 1, 5)";

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
            expected.LoadDataRow(new object[] { 1, "A", "hello world", "mediu" }, false);
            expected.LoadDataRow(new object[] { 1, "B", "abcdefghilmno", "short" }, false);
            expected.LoadDataRow(new object[] { 2, "A", "pqrstuvwxyz", "this " }, false);

            string source = "DS_r := DS_1[calc Me_2 := substr (Me_2, 1, 5)]";

            List<Exception> errors;
            string sql = this.TranslateVtl(source, out errors);

            Assert.Empty(errors);

            DataTable ds_r = this.RunSql(sql, "#DS_r");

            Assert.True(expected.EqualsTo(ds_r));
        }

        [Fact]
        public void ScalarExample1()
        {
            string expected = "efghijklmn";
            // string source = "var := substr(\"abcdefghijklmnopqrstuvwxyz\", start := 5, length := 10)"; - documentation example error
            string source = "var := substr(\"abcdefghijklmnopqrstuvwxyz\", 5, 10)";

            List<Exception> errors;
            string sql = this.TranslateVtl(source, out errors);

            Assert.Empty(errors);

            DataTable var = this.RunSql(sql, "@var");

            Assert.Single(var.Columns);
            Assert.Single(var.Rows);
            Assert.Equal(expected, var.Rows[0].Field<string>("value"));
        }

        [Fact]
        public void ScalarExample2()
        {
            string expected = "yz";
            // string source = "var := substr(\"abcdefghijklmnopqrstuvwxyz\", start := 25, length := 10)"; - documentation example error
            string source = "var := substr(\"abcdefghijklmnopqrstuvwxyz\", 25, 10)";

            List<Exception> errors;
            string sql = this.TranslateVtl(source, out errors);

            Assert.Empty(errors);

            DataTable var = this.RunSql(sql, "@var");

            Assert.Single(var.Columns);
            Assert.Single(var.Rows);
            Assert.Equal(expected, var.Rows[0].Field<string>("value"));
        }

        [Fact]
        public void ScalarExample3()
        {
            string expected = string.Empty;
            // string source = "var := substr(\"abcdefghijklmnopqrstuvwxyz\", start := 30, length := 10)"; - documentation example error
            string source = "var := substr(\"abcdefghijklmnopqrstuvwxyz\", 30, 10)";

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
