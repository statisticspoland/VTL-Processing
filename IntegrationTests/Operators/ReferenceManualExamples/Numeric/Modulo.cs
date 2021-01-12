namespace VtlProcessing.IntegrationTests.TSQL.Operators.ReferenceManualExamples.Numeric
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using Xunit;

    public class Modulo : TSQLTestBase
    {
        public Modulo() : base("Modulo")
        {
            List<object[]> ds1 = new List<object[]>
            {
                new object[] { 10, "B", 10, 18.45 },
                new object[] { 10, "A", 100, 0.7545 },
                new object[] { 11, "A", 20, 1.87 },
                new object[] { 11, "B", 9, 12.3 }
            };

            List<object[]> ds2 = new List<object[]>
            {
                new object[] { 10, "A", 1, 0.25 },
                new object[] { 10, "C", 5, 3.0 },
                new object[] { 11, "B", 2, 2.0 }
            };

            this.SqlFillData("[Modulo].DS_1", ds1);
            this.SqlFillData("[Modulo].DS_2", ds2);
        }

        [Fact]
        public void Example1()
        {
            DataTable expected = new DataTable();
            expected.Columns.Add("Id_1");
            expected.Columns.Add("Id_2");
            expected.Columns.Add("Me_1");
            expected.Columns.Add("Me_2");
            expected.LoadDataRow(new object[] { 10, "A", 0, 0.0045 }, false);
            expected.LoadDataRow(new object[] { 11, "B", 1, 0.3 }, false);

            string source = "DS_r := mod(DS_1, DS_2 )";

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
            expected.LoadDataRow(new object[] { 10, "A", 10, 0.7545 }, false);
            expected.LoadDataRow(new object[] { 10, "B", 10, 3.45 }, false);
            expected.LoadDataRow(new object[] { 11, "A", 5, 1.87 }, false);
            expected.LoadDataRow(new object[] { 11, "B", 9, 12.3 }, false);

            string source = "DS_r := mod(DS_1, 15)";

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
            expected.LoadDataRow(new object[] { 10, "A", 100, 0.7545, 1.0 }, false);
            expected.LoadDataRow(new object[] { 10, "B", 10, 18.45, 1.0 }, false);
            expected.LoadDataRow(new object[] { 11, "A", 20, 1.87, 2.0 }, false);
            expected.LoadDataRow(new object[] { 11, "B", 9, 12.3, 0.0 }, false);

            // string source = "DS_r := DS_1[calc Me_3 := mod(DS_1#Me_1, 3.0)]"; - documentation example error
            string source = "DS_r := DS_1[calc Me_3 := mod(Me_1, 3.0)]";

            List<Exception> errors;
            string sql = this.TranslateVtl(source, out errors);

            Assert.Empty(errors);

            DataTable ds_r = this.RunSql(sql, "#DS_r");

            Assert.True(expected.EqualsTo(ds_r));
        }

        [Fact]
        public void ScalarExample1()
        {
            decimal expected = 1;
            string source = "var := mod(5, 2)";

            List<Exception> errors;
            string sql = this.TranslateVtl(source, out errors);

            Assert.Empty(errors);

            DataTable var = this.RunSql(sql, "@var");

            Assert.Single(var.Columns);
            Assert.Single(var.Rows);
            Assert.Equal(expected, decimal.Parse(var.Rows[0].Field<string>("value")));
        }

        //[Fact]
        //public void ScalarExample2()
        //{
        //    // Unsupported by T-SQL
        //    decimal expected = -1;
        //    string source = "var := mod(5, -2)";

        //    List<Exception> errors;
        //    string sql = this.TranslateVtl(source, out errors);

        //    Assert.Empty(errors);

        //    DataTable var = this.RunSql(sql, "@var");

        //    Assert.Single(var.Columns);
        //    Assert.Single(var.Rows);
        //    Assert.Equal(expected, decimal.Parse(var.Rows[0].Field<string>("value")));
        //}

        [Fact]
        public void ScalarExample3()
        {
            decimal expected = 0;
            string source = "var := mod(8, 2)";

            List<Exception> errors;
            string sql = this.TranslateVtl(source, out errors);

            Assert.Empty(errors);

            DataTable var = this.RunSql(sql, "@var");

            Assert.Single(var.Columns);
            Assert.Single(var.Rows);
            Assert.Equal(expected, decimal.Parse(var.Rows[0].Field<string>("value")));
        }

        [Fact]
        public void ScalarExample4()
        {
            decimal expected = 9;
            string source = "var := mod(9, 0)"; // Warning: if 0 is given by reference it will throw error

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
