namespace VtlProcessing.IntegrationTests.TSQL.Operators.ReferenceManualExamples.Comparison
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using Xunit;

    public class NotEqualTo : TSQLTestBase
    {
        public NotEqualTo() : base("Not_equal_to")
        {
            List<object[]> ds1 = new List<object[]>
            {
                new object[] { "G", "Total", "Percentage", "Total", 7.1 },
                new object[] { "R", "Total", "Percentage", "Total", null }
            };

            List<object[]> ds2 = new List<object[]>
            {
                new object[] { "G", "Total", "Percentage", "Total", 7.5 },
                new object[] { "R", "Total", "Percentage", "Total", 3 }
            };

            this.SqlFillData("[Not_equal_to].DS_1", ds1);
            this.SqlFillData("[Not_equal_to].DS_2", ds2);
        }

        [Fact]
        public void Example1()
        {
            DataTable expected = new DataTable();
            expected.Columns.Add("Id_1");
            expected.Columns.Add("Id_2");
            expected.Columns.Add("Id_3");
            expected.Columns.Add("Id_4");
            expected.Columns.Add("bool_var");
            // expected.LoadDataRow(new object[] { "G", "Total", "Percentage", "Total", true }, false);
            // expected.LoadDataRow(new object[] { "R", "Total", "Percentage", "Total", null }, false);
            // Changed boolean values to bits for test needs
            expected.LoadDataRow(new object[] { "G", "Total", "Percentage", "Total", 1 }, false);
            expected.LoadDataRow(new object[] { "R", "Total", "Percentage", "Total", null }, false);

            string source = "DS_r := DS_1 <> DS_2";

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
            // // expected.LoadDataRow(new object[] { "G", "Total", "Percentage", "Total", 7.5, true }, false); - documentation exaple's result error
            // expected.LoadDataRow(new object[] { "G", "Total", "Percentage", "Total", 7.1, true }, false);
            // // expected.LoadDataRow(new object[] { "R", "Total", "Percentage", "Total", 3, null }, false); - documentation exaple's result error
            // expected.LoadDataRow(new object[] { "R", "Total", "Percentage", "Total", null, null }, false);
            // Changed boolean values to bits for test needs
            expected.LoadDataRow(new object[] { "G", "Total", "Percentage", "Total", 7.1, 1 }, false);
            expected.LoadDataRow(new object[] { "R", "Total", "Percentage", "Total", null, null }, false);

            string source = "DS_r := DS_1[calc Me_2 := Me_1 <> 7.5]";

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
            string source = "var := 5 <> 9";

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
            string source = "var := 5 <> 5";

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
            string source = "var := \"hello\" <> \"hi\"";

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
