namespace VtlProcessing.IntegrationTests.TSQL.Operators.ReferenceManualExamples.String
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using Xunit;

    public class Length : TSQLTestBase
    {
        public Length() : base("Length")
        {
            List<object[]> ds1 = new List<object[]>
            {
                new object[] { 1, "A", "hello" },
                new object[] { 2, "B", null }
            };

            List<object[]> ds2 = new List<object[]>
            {
                new object[] { 1, "A", "hello", "world" },
                new object[] { 2, "B", null, "hi" }
            };

            this.SqlFillData("[Length].DS_1", ds1);
            this.SqlFillData("[Length].DS_2", ds2);
        }

        [Fact]
        public void Example1()
        {
            DataTable expected = new DataTable();
            expected.Columns.Add("Id_1");
            expected.Columns.Add("Id_2");
            expected.Columns.Add("int_var");
            expected.LoadDataRow(new object[] { 1, "A", 5 }, false);
            expected.LoadDataRow(new object[] { 2, "B", null }, false);

            string source = "DS_r := length(DS_1)";

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
            expected.LoadDataRow(new object[] { 1, "A", "hello", 5 }, false);
            expected.LoadDataRow(new object[] { 2, "B", null, null }, false);

            string source = "DS_r := DS_1[calc Me_2 := length(Me_1)]";

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
            expected.LoadDataRow(new object[] { 1, "A", "hello", "world", 5, 5 }, false);
            expected.LoadDataRow(new object[] { 2, "B", null, "hi", null, 2 }, false);

            string source = "DS_r := DS_2 [calc Me_10 := length(Me_1), Me_20 := length(Me_2)]";

            List<Exception> errors;
            string sql = this.TranslateVtl(source, out errors);

            Assert.Empty(errors);

            DataTable ds_r = this.RunSql(sql, "#DS_r");

            Assert.True(expected.EqualsTo(ds_r));
        }

        [Fact]
        public void Example4()
        {
            string source = "DS_r := length(DS_2)";

            List<Exception> errors;
            string sql = this.TranslateVtl(source, out errors);

            Assert.NotEmpty(errors);
        }

        [Fact]
        public void ScalarExample1()
        {
            int expected = 13;
            string source = "var := length(\"Hello, World!\")";

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
