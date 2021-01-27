namespace VtlProcessing.IntegrationTests.TSQL.Operators.ReferenceManualExamples.String
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using Xunit;

    public class StringPatternLocation : TSQLTestBase
    {
        public StringPatternLocation() : base("String_pattern_location")
        {
            List<object[]> ds1 = new List<object[]>
            {
                new object[] { 1, "A", "hello world" },
                new object[] { 2, "A", "say hello" },
                new object[] { 3, "A", "he" },
                new object[] { 4, "A", "hi, hello!" }
            };

            List<object[]> ds2 = new List<object[]>
            {
                new object[] { 1, "A", "hello", "world" },
                new object[] { 2, "B", null, "hi" }
            };

            this.SqlFillData("[String_pattern_location].DS_1", ds1);
            this.SqlFillData("[String_pattern_location].DS_2", ds2);
        }

        [Fact]
        public void Example1()
        {
            DataTable expected = new DataTable();
            expected.Columns.Add("Id_1");
            expected.Columns.Add("Id_2");
            expected.Columns.Add("int_var");
            expected.LoadDataRow(new object[] { 1, "A", 1 }, false);
            expected.LoadDataRow(new object[] { 2, "A", 5 }, false);
            expected.LoadDataRow(new object[] { 3, "A", 0 }, false);
            expected.LoadDataRow(new object[] { 4, "A", 5 }, false);

            string source = "DS_r := instr(DS_1, \"hello\")";

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
            expected.LoadDataRow(new object[] { 1, "A", "hello world", 1 }, false);
            expected.LoadDataRow(new object[] { 2, "A", "say hello", 5 }, false);
            expected.LoadDataRow(new object[] { 3, "A", "he", 0 }, false);
            expected.LoadDataRow(new object[] { 4, "A", "hi, hello!", 5 }, false);

            string source = "DS_r := DS_1[calc Me_2 := instr(Me_1, \"hello\")]";

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
            expected.Columns.Add("Me_10");
            expected.Columns.Add("Me_20");
            expected.LoadDataRow(new object[] { 1, "A", "hello", "world", 5, 2 }, false);
            expected.LoadDataRow(new object[] { 2, "B", null, "hi", null, 0 }, false);

            string source = "DS_r := DS_2[calc Me_10:= instr(Me_1, \"o\"), Me_20 := instr(Me_2, \"o\")]";

            List<Exception> errors;
            string sql = this.TranslateVtl(source, out errors);

            Assert.Empty(errors);

            DataTable ds_r = this.RunSql(sql, "#DS_r");

            Assert.True(expected.EqualsTo(ds_r));
        }

        [Fact]
        public void Example4()
        {
            string source = "DS_r := instr(DS_2, \"o\")";

            List<Exception> errors;
            string sql = this.TranslateVtl(source, out errors);

            Assert.NotEmpty(errors);
        }

        [Fact]
        public void ScalarExample1()
        {
            int expected = 3;
            string source = "var := instr(\"abcde\", \"c\")";

            List<Exception> errors;
            string sql = this.TranslateVtl(source, out errors);

            Assert.Empty(errors);

            DataTable var = this.RunSql(sql, "@var");

            Assert.Single(var.Columns);
            Assert.Single(var.Rows);
            Assert.Equal(expected, int.Parse(var.Rows[0].Field<string>("value")));
        }

        //[Fact]
        //public void ScalarExample2()
        //{
        //    // Parameter 'occurrence' (arg4) is unsupported in T-SQL : INSTR
        //    int expected = 10;
        //    string source = "var := instr(\"abcdecfrxcwsd\", \"c\", _, 3)";

        //    List<Exception> errors;
        //    string sql = this.TranslateVtl(source, out errors);

        //    Assert.Empty(errors);

        //    DataTable var = this.RunSql(sql, "@var");

        //    Assert.Single(var.Columns);
        //    Assert.Single(var.Rows);
        //    Assert.Equal(expected, int.Parse(var.Rows[0].Field<string>("value")));
        //}

        //[Fact]
        //public void ScalarExample3()
        //{
        //    // Parameter 'occurrence' (arg4) is unsupported in T-SQL : INSTR
        //    int expected = 0;
        //    string source = "var := instr(\"abcdecfrxcwsd\", \"c\", 5, 3)";

        //    List<Exception> errors;
        //    string sql = this.TranslateVtl(source, out errors);

        //    Assert.Empty(errors);

        //    DataTable var = this.RunSql(sql, "@var");

        //    Assert.Single(var.Columns);
        //    Assert.Single(var.Rows);
        //    Assert.Equal(expected, int.Parse(var.Rows[0].Field<string>("value")));
        //}
    }
}
