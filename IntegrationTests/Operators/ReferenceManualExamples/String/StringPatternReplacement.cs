namespace VtlProcessing.IntegrationTests.TSQL.Operators.ReferenceManualExamples.String
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using Xunit;

    public class StringPatternReplacement : TSQLTestBase
    {
        public StringPatternReplacement() : base("String_pattern_replacement")
        {
            List<object[]> ds1 = new List<object[]>
            {
                new object[] { 1, "A", "hello world" },
                new object[] { 2, "A", "say hello" },
                new object[] { 3, "A", "he" },
                new object[] { 4, "A", "hello!" }
            };

            this.SqlFillData("[String_pattern_replacement].DS_1", ds1);
        }

        [Fact]
        public void Example1()
        {
            DataTable expected = new DataTable();
            expected.Columns.Add("Id_1");
            expected.Columns.Add("Id_2");
            expected.Columns.Add("Me_1");
            expected.LoadDataRow(new object[] { 1, "A", "hi world" }, false);
            expected.LoadDataRow(new object[] { 2, "A", "say hi" }, false);
            expected.LoadDataRow(new object[] { 3, "A", "he" }, false);
            expected.LoadDataRow(new object[] { 4, "A", "hi!" }, false);

            string source = "DS_r := replace(ds_1, \"ello\", \"i\")";

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
            expected.LoadDataRow(new object[] { 1, "A", "hello world", "hi world" }, false);
            expected.LoadDataRow(new object[] { 2, "A", "say hello", "say hi" }, false);
            expected.LoadDataRow(new object[] { 3, "A", "he", "he" }, false);
            expected.LoadDataRow(new object[] { 4, "A", "hello!", "hi!" }, false);

            string source = "DS_r := DS_1[calc Me_2:= replace (Me_1, \"ello\", \"i\")]";

            List<Exception> errors;
            string sql = this.TranslateVtl(source, out errors);

            Assert.Empty(errors);

            DataTable ds_r = this.RunSql(sql, "#DS_r");

            Assert.True(expected.EqualsTo(ds_r));
        }

        [Fact]
        public void ScalarExample1()
        {
            string expected = "Hi world";
            string source = "var := replace(\"Hello world\", \"Hello\", \"Hi\")";

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
            string expected = " world";
            string source = "var := replace(\"Hello world\", \"Hello\")";

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
            string expected = "Hi";
            string source = "var := replace(\"Hello\", \"ello\", \"i\")";

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
