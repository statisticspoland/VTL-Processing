﻿namespace VtlProcessing.IntegrationTests.TSQL.Operators.ReferenceManualExamples.Numeric
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using Xunit;

    public class Ceiling : TSQLTestBase
    {
        public Ceiling() : base("Ceiling")
        {
            List<object[]> ds1 = new List<object[]>
            {
                new object[] { 10, "A", 7.0, 5.9 },
                new object[] { 10, "B", 0.1, -5.0 },
                new object[] { 11, "A", -32.2, 17.7 },
                new object[] { 11, "B", 44.5, -0.3 }
            };

            this.SqlFillData("[Ceiling].DS_1", ds1);
        }

        [Fact]
        public void Example1()
        {
            DataTable expected = new DataTable();
            expected.Columns.Add("Id_1");
            expected.Columns.Add("Id_2");
            expected.Columns.Add("Me_1");
            expected.Columns.Add("Me_2");
            expected.LoadDataRow(new object[] { 10, "A", 7, 6 }, false);
            expected.LoadDataRow(new object[] { 10, "B", 1, -5 }, false);
            expected.LoadDataRow(new object[] { 11, "A", -32, 18 }, false);
            expected.LoadDataRow(new object[] { 11, "B", 45, 0 }, false);

            string source = "DS_r := ceil (DS_1)";

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
            expected.LoadDataRow(new object[] { 10, "A", 7.0, 5.9, 7 }, false);
            expected.LoadDataRow(new object[] { 10, "B", 0.1, -5.0, 1 }, false);
            expected.LoadDataRow(new object[] { 11, "A", -32.2, 17.7, -32 }, false);
            expected.LoadDataRow(new object[] { 11, "B", 44.5, -0.3, 45 }, false);

            // string source = "DS_r := DS_1 [Me_10 := ceil (Me_1)]"; - documentation example error
            string source = "DS_r := DS_1 [calc Me_10 := ceil (Me_1)]";

            List<Exception> errors;
            string sql = this.TranslateVtl(source, out errors);

            Assert.Empty(errors);

            DataTable ds_r = this.RunSql(sql, "#DS_r");

            Assert.True(expected.EqualsTo(ds_r));
        }

        [Fact]
        public void ScalarExample1()
        {
            int expected = 4;
            string source = "var := ceil(3.14159)";

            List<Exception> errors;
            string sql = this.TranslateVtl(source, out errors);

            Assert.Empty(errors);

            DataTable var = this.RunSql(sql, "@var");

            Assert.Single(var.Columns);
            Assert.Single(var.Rows);
            Assert.Equal(expected, int.Parse(var.Rows[0].Field<string>("value")));
        }

        [Fact]
        public void ScalarExample2()
        {
            int expected = 15;
            string source = "var := ceil(15)";

            List<Exception> errors;
            string sql = this.TranslateVtl(source, out errors);

            Assert.Empty(errors);

            DataTable var = this.RunSql(sql, "@var");

            Assert.Single(var.Columns);
            Assert.Single(var.Rows);
            Assert.Equal(expected, int.Parse(var.Rows[0].Field<string>("value")));
        }

        [Fact]
        public void ScalarExample3()
        {
            int expected = -3;
            string source = "var := ceil(-3.1415)";

            List<Exception> errors;
            string sql = this.TranslateVtl(source, out errors);

            Assert.Empty(errors);

            DataTable var = this.RunSql(sql, "@var");

            Assert.Single(var.Columns);
            Assert.Single(var.Rows);
            Assert.Equal(expected, int.Parse(var.Rows[0].Field<string>("value")));
        }

        [Fact]
        public void ScalarExample4()
        {
            int expected = 0;
            string source = "var := ceil(-0.1415)";

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
