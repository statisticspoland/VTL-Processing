namespace VtlProcessing.IntegrationTests.TSQL.Operators.ReferenceManualExamples.Boolean
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using Xunit;

    public class LogicalDisjunction : TSQLTestBase
    {
        public LogicalDisjunction() : base("Logical_disjunction")
        {
            List<object[]> ds1 = new List<object[]>
            {
                new object[] { "M", 15, "B", 2013, true },
                new object[] { "M", 64, "B", 2013, false },
                new object[] { "M", 65, "B", 2013, true },
                new object[] { "F", 15, "U", 2013, false },
                new object[] { "F", 64, "U", 2013, false },
                new object[] { "F", 65, "U", 2013, true }
            };

            List<object[]> ds2 = new List<object[]>
            {
                new object[] { "M", 15, "B", 2013, false },
                new object[] { "M", 64, "B", 2013, true },
                new object[] { "M", 65, "B", 2013, true },
                new object[] { "F", 15, "U", 2013, true },
                new object[] { "F", 64, "U", 2013, false },
                new object[] { "F", 65, "U", 2013, false }
            };

            this.SqlFillData("[Logical_disjunction].DS_1", ds1);
            this.SqlFillData("[Logical_disjunction].DS_2", ds2);
        }

        [Fact]
        public void Example1()
        {
            DataTable expected = new DataTable();
            expected.Columns.Add("Id_1");
            expected.Columns.Add("Id_2");
            expected.Columns.Add("Id_3");
            expected.Columns.Add("Id_4");
            expected.Columns.Add("Me_1");
            // expected.LoadDataRow(new object[] { "M", 15, "B", 2013, true }, false);
            // expected.LoadDataRow(new object[] { "M", 64, "B", 2013, true }, false);
            // expected.LoadDataRow(new object[] { "M", 65, "B", 2013, true }, false);
            // expected.LoadDataRow(new object[] { "F", 15, "U", 2013, true }, false);
            // expected.LoadDataRow(new object[] { "F", 64, "U", 2013, false }, false);
            // expected.LoadDataRow(new object[] { "F", 65, "U", 2013, true }, false);
            // Sorted or changed boolean values to bits for test needs
            expected.LoadDataRow(new object[] { "F", 15, "U", 2013, 1 }, false);
            expected.LoadDataRow(new object[] { "F", 64, "U", 2013, 0 }, false);
            expected.LoadDataRow(new object[] { "F", 65, "U", 2013, 1 }, false);
            expected.LoadDataRow(new object[] { "M", 15, "B", 2013, 1 }, false);
            expected.LoadDataRow(new object[] { "M", 64, "B", 2013, 1 }, false);
            expected.LoadDataRow(new object[] { "M", 65, "B", 2013, 1 }, false);

            string source = "DS_r := DS_1 or DS_2";

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
            expected.Columns.Add("Id_3");
            expected.Columns.Add("Id_4");
            expected.Columns.Add("Me_1");
            expected.Columns.Add("Me_2");
            // expected.LoadDataRow(new object[] { "M", 15, "B", 2013, true, true }, false);
            // expected.LoadDataRow(new object[] { "M", 64, "B", 2013, false , true}, false);
            // expected.LoadDataRow(new object[] { "M", 65, "B", 2013, true, true }, false);
            // expected.LoadDataRow(new object[] { "F", 15, "U", 2013, false, true }, false);
            // expected.LoadDataRow(new object[] { "F", 64, "U", 2013, false, true }, false);
            // expected.LoadDataRow(new object[] { "F", 65, "U", 2013, true, true }, false);
            // Sorted or changed boolean values to bits for test needs
            expected.LoadDataRow(new object[] { "F", 15, "U", 2013, false, 1 }, false);
            expected.LoadDataRow(new object[] { "F", 64, "U", 2013, false, 1 }, false);
            expected.LoadDataRow(new object[] { "F", 65, "U", 2013, true, 1 }, false);
            expected.LoadDataRow(new object[] { "M", 15, "B", 2013, true, 1 }, false);
            expected.LoadDataRow(new object[] { "M", 64, "B", 2013, false, 1 }, false);
            expected.LoadDataRow(new object[] { "M", 65, "B", 2013, true, 1 }, false);

            // string source = "DS_r := DS_1[Me_2 := Me_1 or true]"; - documentation example error
            string source = "DS_r := DS_1[calc Me_2 := Me_1 or true]";

            List<Exception> errors;
            string sql = this.TranslateVtl(source, out errors);

            Assert.Empty(errors);

            DataTable ds_r = this.RunSql(sql, "#DS_r");

            Assert.True(expected.EqualsTo(ds_r));
        }

        [Fact]
        public void ScalarExample1()
        {
            bool expected = false;
            // string source = "var := FALSE or FALSE"; - documentation example error
            string source = "var := false or false";

            List<Exception> errors;
            string sql = this.TranslateVtl(source, out errors);

            Assert.Empty(errors);

            DataTable var = this.RunSql(sql, "@var");

            Assert.Single(var.Columns);
            Assert.Single(var.Rows);
            Assert.Equal(expected, bool.Parse(var.Rows[0].Field<string>("value")));
        }

        [Fact]
        public void ScalarExample2()
        {
            bool expected = true;
            // string source = "var := FALSE or TRUE"; - documentation example error
            string source = "var := false or true";

            List<Exception> errors;
            string sql = this.TranslateVtl(source, out errors);

            Assert.Empty(errors);

            DataTable var = this.RunSql(sql, "@var");

            Assert.Single(var.Columns);
            Assert.Single(var.Rows);
            Assert.Equal(expected, bool.Parse(var.Rows[0].Field<string>("value")));
        }

        [Fact]
        public void ScalarExample3()
        {
            bool expected = true;
            // string source = "var := TRUE or FALSE"; - documentation example error
            string source = "var := true or false";

            List<Exception> errors;
            string sql = this.TranslateVtl(source, out errors);

            Assert.Empty(errors);

            DataTable var = this.RunSql(sql, "@var");

            Assert.Single(var.Columns);
            Assert.Single(var.Rows);
            Assert.Equal(expected, bool.Parse(var.Rows[0].Field<string>("value")));
        }

        [Fact]
        public void ScalarExample4()
        {
            bool expected = true;
            // string source = "var := TRUE or TRUE"; - documentation example error
            string source = "var := true or true";

            List<Exception> errors;
            string sql = this.TranslateVtl(source, out errors);

            Assert.Empty(errors);

            DataTable var = this.RunSql(sql, "@var");

            Assert.Single(var.Columns);
            Assert.Single(var.Rows);
            Assert.Equal(expected, bool.Parse(var.Rows[0].Field<string>("value")));
        }
    }
}
