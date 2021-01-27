namespace VtlProcessing.IntegrationTests.TSQL.Operators.ReferenceManualExamples.Boolean
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using Xunit;

    public class LogicalNegation : TSQLTestBase
    {
        public LogicalNegation() : base("Logical_negation")
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

            this.SqlFillData("[Logical_negation].DS_1", ds1);
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
            // expected.LoadDataRow(new object[] { "M", 15, "B", 2013, false }, false);
            // expected.LoadDataRow(new object[] { "M", 64, "B", 2013, true }, false);
            // expected.LoadDataRow(new object[] { "M", 65, "B", 2013, false }, false);
            // expected.LoadDataRow(new object[] { "F", 15, "U", 2013, true }, false);
            // expected.LoadDataRow(new object[] { "F", 64, "U", 2013, true }, false);
            // expected.LoadDataRow(new object[] { "F", 65, "U", 2013, false }, false);
            // Sorted and changed boolean values to bits for test needs
            expected.LoadDataRow(new object[] { "F", 15, "U", 2013, 1 }, false);
            expected.LoadDataRow(new object[] { "F", 64, "U", 2013, 1 }, false);
            expected.LoadDataRow(new object[] { "F", 65, "U", 2013, 0 }, false);
            expected.LoadDataRow(new object[] { "M", 15, "B", 2013, 0 }, false);
            expected.LoadDataRow(new object[] { "M", 64, "B", 2013, 1 }, false);
            expected.LoadDataRow(new object[] { "M", 65, "B", 2013, 0 }, false);

            string source = "DS_r := not DS_1";

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
            // expected.LoadDataRow(new object[] { "M", 15, "B", 2013, true, false }, false);
            // expected.LoadDataRow(new object[] { "M", 64, "B", 2013, false , true}, false);
            // expected.LoadDataRow(new object[] { "M", 65, "B", 2013, true, false }, false);
            // expected.LoadDataRow(new object[] { "F", 15, "U", 2013, false, true }, false);
            // expected.LoadDataRow(new object[] { "F", 64, "U", 2013, false, true }, false);
            // expected.LoadDataRow(new object[] { "F", 65, "U", 2013, true, false }, false);
            // Sorted and changed boolean values to bits for test needs
            expected.LoadDataRow(new object[] { "F", 15, "U", 2013, false, 1 }, false);
            expected.LoadDataRow(new object[] { "F", 64, "U", 2013, false, 1 }, false);
            expected.LoadDataRow(new object[] { "F", 65, "U", 2013, true, 0 }, false);
            expected.LoadDataRow(new object[] { "M", 15, "B", 2013, true, 0 }, false);
            expected.LoadDataRow(new object[] { "M", 64, "B", 2013, false, 1 }, false);
            expected.LoadDataRow(new object[] { "M", 65, "B", 2013, true, 0 }, false);

            string source = "DS_r := DS_1[calc Me_2 := not Me_1]";

            List<Exception> errors;
            string sql = this.TranslateVtl(source, out errors);

            Assert.Empty(errors);

            DataTable ds_r = this.RunSql(sql, "#DS_r");

            Assert.True(expected.EqualsTo(ds_r));
        }

        [Fact]
        public void ScalarExample1()
        {
            bool expected = true;
            // string source = "var := not FALSE"; - documentation example error
            string source = "var := not false";

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
            bool expected = false;
            // string source = "var := not TRUE"; - documentation example error
            string source = "var := not true";

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
