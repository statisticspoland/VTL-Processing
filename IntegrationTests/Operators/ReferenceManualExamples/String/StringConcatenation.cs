namespace VtlProcessing.IntegrationTests.TSQL.Operators.ReferenceManualExamples.String
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using VtlProcessing.IntegrationTests.TSQL;
    using Xunit;

    public class StringConcatenation : TSQLTestBase
    {
        public StringConcatenation() : base("String_concatenation")
        {
            List<object[]> ds1 = new List<object[]>
            {
                new object[] { 1, "A", "hello" },
                new object[] { 2, "B", "hi" }
            };

            List<object[]> ds2 = new List<object[]>
            {
                new object[] { 1, "A", "world" },
                new object[] { 2, "B", "there" }
            };

            this.SqlFillData("[String_concatenation].DS_1", ds1);
            this.SqlFillData("[String_concatenation].DS_2", ds2);
        }

        [Fact]
        public void Example1()
        {
            DataTable expected = new DataTable();
            expected.Columns.Add("Id_1");
            expected.Columns.Add("Id_2");
            expected.Columns.Add("Me_1");
            expected.LoadDataRow(new object[] { 1, "A", "helloworld" }, false);
            expected.LoadDataRow(new object[] { 2, "B", "hithere" }, false);

            string source = "DS_r := DS_1 || DS_2";

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
            expected.LoadDataRow(new object[] { 1, "A", "hello", "hello world" }, false);
            expected.LoadDataRow(new object[] { 2, "B", "hi", "hi world" }, false);

            string source = "DS_r := DS_1[calc Me_2:= Me_1 || \" world\"]";

            List<Exception> errors;
            string sql = this.TranslateVtl(source, out errors);

            Assert.Empty(errors);

            DataTable ds_r = this.RunSql(sql, "#DS_r");

            Assert.True(expected.EqualsTo(ds_r));
        }

        [Fact]
        public void ScalarExample1()
        {
            string expected = "Hello, world!";
            string source = "var := \"Hello\" || \", world!\"";

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
