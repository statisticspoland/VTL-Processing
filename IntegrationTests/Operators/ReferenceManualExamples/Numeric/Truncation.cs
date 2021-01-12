//namespace VtlProcessing.IntegrationTests.TSQL.Operators.ReferenceManualExamples.Numeric
//{
//    using System;
//    using System.Collections.Generic;
//    using System.Data;
//    using Xunit;

//    public class Truncation : TSQLTestBase
//    {
//        public Truncation() : base("Truncation")
//        {
//            List<object[]> ds1 = new List<object[]>
//            {
//                new object[] { 10, "A", 7.5, 5.9 },
//                new object[] { 10, "B", 7.1, 5.5 },
//                new object[] { 11, "A", 36.2, 17.7 },
//                new object[] { 11, "B", 44.5, 24.3 }
//            };

//            this.SqlFillData("[Truncation].DS_1", ds1);
//        }

//        [Fact]
//        public void Example1()
//        {
//            // [TRUNC] is not supported in T-SQL
//            DataTable expected = new DataTable();
//            expected.Columns.Add("Id_1");
//            expected.Columns.Add("Id_2");
//            expected.Columns.Add("Me_1");
//            expected.Columns.Add("Me_2");
//            expected.LoadDataRow(new object[] { 10, "A", 7.0, 5.0 }, false);
//            expected.LoadDataRow(new object[] { 10, "B", 7.0, 5.0 }, false);
//            expected.LoadDataRow(new object[] { 11, "A", 36.0, 17.0 }, false);
//            expected.LoadDataRow(new object[] { 11, "B", 44.0, 24.0 }, false);

//            string source = "DS_r := trunc(DS_1, 0)";

//            List<Exception> errors;
//            string sql = this.TranslateVtl(source, out errors);

//            Assert.Empty(errors);

//            DataTable ds_r = this.RunSql(sql, "#DS_r");

//            Assert.True(expected.EqualsTo(ds_r));
//        }

//        [Fact]
//        public void Example2()
//        {
//            // [TRUNC] is not supported in T-SQL
//            DataTable expected = new DataTable();
//            expected.Columns.Add("Id_1");
//            expected.Columns.Add("Id_2");
//            expected.Columns.Add("Me_1");
//            expected.Columns.Add("Me_2");
//            expected.Columns.Add("Me_10");
//            expected.LoadDataRow(new object[] { 10, "A", 7.5, 5.9, 7 }, false);
//            expected.LoadDataRow(new object[] { 10, "B", 7.1, 5.5, 7 }, false);
//            expected.LoadDataRow(new object[] { 11, "A", 36.2, 17.7, 36 }, false);
//            expected.LoadDataRow(new object[] { 11, "B", 44.5, 24.3, 44 }, false);

//            string source = "DS_r := DS_1[calc Me_10 := trunc(Me_1)]";

//            List<Exception> errors;
//            string sql = this.TranslateVtl(source, out errors);

//            Assert.Empty(errors);

//            DataTable ds_r = this.RunSql(sql, "#DS_r");

//            Assert.True(expected.EqualsTo(ds_r));
//        }

//        [Fact]
//        public void Example3()
//        {
//            // [TRUNC] is not supported in T-SQL
//            DataTable expected = new DataTable();
//            expected.Columns.Add("Id_1");
//            expected.Columns.Add("Id_2");
//            expected.Columns.Add("Me_1");
//            expected.Columns.Add("Me_2");
//            expected.Columns.Add("Me_20");
//            expected.LoadDataRow(new object[] { 10, "A", 7.5, 5.9, 0 }, false);
//            expected.LoadDataRow(new object[] { 10, "B", 7.1, 5.5, 0 }, false);
//            expected.LoadDataRow(new object[] { 11, "A", 36.2, 17.7, 30 }, false);
//            expected.LoadDataRow(new object[] { 11, "B", 44.5, 24.3, 40 }, false);

//            string source = "DS_r := DS_1[calc Me_20 := trunc(Me_1, -1)]";

//            List<Exception> errors;
//            string sql = this.TranslateVtl(source, out errors);

//            Assert.Empty(errors);

//            DataTable ds_r = this.RunSql(sql, "#DS_r");

//            Assert.True(expected.EqualsTo(ds_r));
//        }

//        [Fact]
//        public void ScalarExample1()
//        {
//            // [TRUNC] is not supported in T-SQL
//            decimal expected = 3.14m;
//            string source = "var := trunc(3.14159, 2)";

//            List<Exception> errors;
//            string sql = this.TranslateVtl(source, out errors);

//            Assert.Empty(errors);

//            DataTable var = this.RunSql(sql, "@var");

//            Assert.Single(var.Columns);
//            Assert.Single(var.Rows);
//            Assert.Equal(expected, decimal.Parse(var.Rows[0].Field<string>("value")));
//        }

//        [Fact]
//        public void ScalarExample2()
//        {
//            // [TRUNC] is not supported in T-SQL
//            decimal expected = 3.1415m;
//            string source = "var := trunc(3.14159, 4)";

//            List<Exception> errors;
//            string sql = this.TranslateVtl(source, out errors);

//            Assert.Empty(errors);

//            DataTable var = this.RunSql(sql, "@var");

//            Assert.Single(var.Columns);
//            Assert.Single(var.Rows);
//            Assert.Equal(expected, decimal.Parse(var.Rows[0].Field<string>("value")));
//        }

//        [Fact]
//        public void ScalarExample3()
//        {
//            // [TRUNC] is not supported in T-SQL
//            decimal expected = 12345.0m;
//            string source = "var := trunc(12345.6, 0)";

//            List<Exception> errors;
//            string sql = this.TranslateVtl(source, out errors);

//            Assert.Empty(errors);

//            DataTable var = this.RunSql(sql, "@var");

//            Assert.Single(var.Columns);
//            Assert.Single(var.Rows);
//            Assert.Equal(expected, decimal.Parse(var.Rows[0].Field<string>("value")));
//        }

//        [Fact]
//        public void ScalarExample4()
//        {
//            // [TRUNC] is not supported in T-SQL
//            decimal expected = 12345;
//            string source = "var := trunc(12345.6)";

//            List<Exception> errors;
//            string sql = this.TranslateVtl(source, out errors);

//            Assert.Empty(errors);

//            DataTable var = this.RunSql(sql, "@var");

//            Assert.Single(var.Columns);
//            Assert.Single(var.Rows);
//            Assert.Equal(expected, decimal.Parse(var.Rows[0].Field<string>("value")));
//        }

//        [Fact]
//        public void ScalarExample5()
//        {
//            // [TRUNC] is not supported in T-SQL
//            decimal expected = 12345;
//            string source = "var := trunc(12345.6, _)";

//            List<Exception> errors;
//            string sql = this.TranslateVtl(source, out errors);

//            Assert.Empty(errors);

//            DataTable var = this.RunSql(sql, "@var");

//            Assert.Single(var.Columns);
//            Assert.Single(var.Rows);
//            Assert.Equal(expected, decimal.Parse(var.Rows[0].Field<string>("value")));
//        }

//        [Fact]
//        public void ScalarExample6()
//        {
//            // [TRUNC] is not supported in T-SQL
//            decimal expected = 12340.0m;
//            string source = "var := trunc(12345.6, -1)";

//            List<Exception> errors;
//            string sql = this.TranslateVtl(source, out errors);

//            Assert.Empty(errors);

//            DataTable var = this.RunSql(sql, "@var");

//            Assert.Single(var.Columns);
//            Assert.Single(var.Rows);
//            Assert.Equal(expected, decimal.Parse(var.Rows[0].Field<string>("value")));
//        }
//    }
//}
