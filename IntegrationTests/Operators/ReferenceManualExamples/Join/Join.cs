namespace VtlProcessing.IntegrationTests.TSQL.Operators.ReferenceManualExamples.Join
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using Xunit;

    public class Join : TSQLTestBase
    {
        public Join() : base("Join")
        {
            List<object[]> ds1 = new List<object[]>
            {
                new object[] { 1, "A", "A", "B" },
                new object[] { 1, "B", "C", "D" },
                new object[] { 2, "A", "E", "F" }
            };

            List<object[]> ds2 = new List<object[]>
            {
                new object[] { 1, "A", "B", "Q" },
                new object[] { 1, "B", "S", "T" },
                new object[] { 3, "A", "Z", "M" }
            };

            this.SqlFillData("[Join].DS_1", ds1);
            this.SqlFillData("[Join].DS_2A", ds2); // Me_1A
            this.SqlFillData("[Join].DS_2B", ds2); // Me_1
        }

        [Fact]
        public void Example1()
        {
            DataTable expected = new DataTable();
            expected.Columns.Add("Id_1");
            expected.Columns.Add("Id_2");
            expected.Columns.Add("Me_1");
            expected.Columns.Add("Me_2");
            expected.Columns.Add("Me_1A");
            expected.LoadDataRow(new object[] { 1, "A", "A", "Q", "B" }, false);
            expected.LoadDataRow(new object[] { 1, "B", "C", "T", "S" }, false);

            // string source = "DS_r := inner_join (DS_1 as d1, DS_2A as d2, keep Me_1, d2#Me_2, Me_1A)"; - documentatiion example error
            string source = "DS_r := inner_join (DS_1 as d1, DS_2A as d2 keep Me_1, d2#Me_2, Me_1A)";

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
            expected.Columns.Add("Me_1A");
            expected.LoadDataRow(new object[] { 1, "A", "A", "Q", "B" }, false);
            expected.LoadDataRow(new object[] { 1, "B", "C", "T", "S" }, false);
            expected.LoadDataRow(new object[] { 2, "A", "E", null, null }, false);

            // string source = "DS_r := left_join (DS_1 as d1, DS_2A as d2, keep Me_1, d2#Me_2, Me_1A)"; - documentation example error
            string source = "DS_r := left_join (DS_1 as d1, DS_2A as d2 keep Me_1, d2#Me_2, Me_1A)";

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
            expected.Columns.Add("Me_1A");
            expected.LoadDataRow(new object[] { 1, "A", "A", "Q", "B" }, false);
            expected.LoadDataRow(new object[] { 1, "B", "C", "T", "S" }, false);
            expected.LoadDataRow(new object[] { 2, "A", "E", null, null }, false);
            expected.LoadDataRow(new object[] { 3, "A", null, "M", "Z" }, false);

            // string source = "DS_r := full_join (DS_1 as d1, DS_2A as d2, keep Me_1, d2#Me_2, Me_1A)"; - documentation example error
            string source = "DS_r := full_join (DS_1 as d1, DS_2A as d2 keep Me_1, d2#Me_2, Me_1A)";

            List<Exception> errors;
            string sql = this.TranslateVtl(source, out errors);

            Assert.Empty(errors);

            DataTable ds_r = this.RunSql(sql, "#DS_r");

            Assert.True(expected.EqualsTo(ds_r));
        }

        [Fact]
        public void Example4()
        {
            DataTable expected = new DataTable();
            expected.Columns.Add("Id_11");
            expected.Columns.Add("Id_12");
            expected.Columns.Add("Id_21");
            expected.Columns.Add("Id_22");
            expected.Columns.Add("Me_1");
            expected.Columns.Add("Me_12");
            expected.Columns.Add("Me_1A");
            expected.Columns.Add("Me_2");
            // expected.LoadDataRow(new object[] { 1, "A", 1, "A", "A", "B", "B", "Q" }, false);
            // expected.LoadDataRow(new object[] { 1, "A", 1, "B", "A", "B", "S", "T" }, false);
            // expected.LoadDataRow(new object[] { 1, "A", 3, "A", "A", "B", "Z", "M" }, false);
            // expected.LoadDataRow(new object[] { 1, "B", 1, "A", "C", "D", "B", "Q" }, false);
            // expected.LoadDataRow(new object[] { 1, "B", 1, "B", "C", "D", "S", "T" }, false);
            // expected.LoadDataRow(new object[] { 1, "B", 3, "A", "C", "D", "Z", "M" }, false);
            // expected.LoadDataRow(new object[] { 2, "A", 1, "A", "E", "F", "B", "Q" }, false);
            // expected.LoadDataRow(new object[] { 2, "A", 1, "B", "E", "F", "S", "T" }, false);
            // expected.LoadDataRow(new object[] { 2, "A", 3, "A", "E", "F", "Z", "M" }, false);
            // Changed order for test needs
            expected.LoadDataRow(new object[] { 1, "A", 1, "A", "A", "B", "B", "Q" }, false);
            expected.LoadDataRow(new object[] { 1, "B", 1, "A", "C", "D", "B", "Q" }, false);
            expected.LoadDataRow(new object[] { 2, "A", 1, "A", "E", "F", "B", "Q" }, false);
            expected.LoadDataRow(new object[] { 1, "A", 1, "B", "A", "B", "S", "T" }, false);
            expected.LoadDataRow(new object[] { 1, "B", 1, "B", "C", "D", "S", "T" }, false);
            expected.LoadDataRow(new object[] { 2, "A", 1, "B", "E", "F", "S", "T" }, false);
            expected.LoadDataRow(new object[] { 1, "A", 3, "A", "A", "B", "Z", "M" }, false);
            expected.LoadDataRow(new object[] { 1, "B", 3, "A", "C", "D", "Z", "M" }, false);
            expected.LoadDataRow(new object[] { 2, "A", 3, "A", "E", "F", "Z", "M" }, false);

            //string source = 
            //    "DS_r := DS_r := cross_join (DS_1 as d1, DS_2 as d2, " +
            // rename d1#Id_1 to Id11, d1#Id_2 to Id12, d2#Id1 to Id21, d2#Id2 to Id22, d1#Me_2 to Me12)"; - documentation example 
            string source =
                "DS_r := cross_join (DS_1 as d1, DS_2A as d2 " +
                "rename d1#Id_1 to Id_11, d1#Id_2 to Id_12, d2#Id_1 to Id_21, d2#Id_2 to Id_22, d1#Me_2 to Me_12)";

            List<Exception> errors;
            string sql = this.TranslateVtl(source, out errors);

            Assert.Empty(errors);

            DataTable ds_r = this.RunSql(sql, "#DS_r");

            Assert.True(expected.EqualsTo(ds_r));
        }

        [Fact]
        public void Example5()
        {
            DataTable expected = new DataTable();
            expected.Columns.Add("Id_1");
            expected.Columns.Add("Id_2");
            expected.Columns.Add("Me_1");
            expected.Columns.Add("Me_2");
            expected.Columns.Add("Me_1A");
            expected.Columns.Add("Me_4");
            expected.LoadDataRow(new object[] { 1, "A", "A", "Q", "B", "AB" }, false);

            //string source =
            //    "DS_r := inner_join (DS_1 as d1, DS_2A as d2, " +
            //    "filter Me_1 = \"A\", calc Me_4 := Me_1 || Me_1A, drop d1#Me_2)"; - documentation example error
            string source = 
                "DS_r := inner_join (DS_1 as d1, DS_2A as d2 " +
                "filter Me_1 = \"A\" calc Me_4 := Me_1 || Me_1A drop d1#Me_2)";

            List<Exception> errors;
            string sql = this.TranslateVtl(source, out errors);

            Assert.Empty(errors);

            DataTable ds_r = this.RunSql(sql, "#DS_r");

            Assert.True(expected.EqualsTo(ds_r));
        }

        [Fact]
        public void Example6()
        {
            DataTable expected = new DataTable();
            expected.Columns.Add("Id_1");
            expected.Columns.Add("Id_2");
            expected.Columns.Add("Me_1");
            expected.Columns.Add("Me_2");
            expected.LoadDataRow(new object[] { 1, "B", "C", "D_NEW" }, false);

            //string source =
            //    "DS_r := inner_join(DS_1 " +
            //    "calc Me_2 := Me_2 || \"_NEW\" filter Id_2 = \"B\" keep Me_1, Me_2)"; - documentation example error
            string source =
                "DS_r := inner_join(DS_1 " +
                "filter Id_2 = \"B\" calc Me_2 := Me_2 || \"_NEW\" keep Me_1, Me_2)";

            List<Exception> errors;
            string sql = this.TranslateVtl(source, out errors);

            Assert.Empty(errors);

            DataTable ds_r = this.RunSql(sql, "#DS_r");

            Assert.True(expected.EqualsTo(ds_r));
        }

        [Fact]
        public void Example7()
        {
            DataTable expected = new DataTable();
            expected.Columns.Add("Id_1");
            expected.Columns.Add("Id_2");
            expected.Columns.Add("Me_1");
            expected.Columns.Add("Me_2");
            expected.LoadDataRow(new object[] { 1, "A", "AB", "BQ" }, false);
            expected.LoadDataRow(new object[] { 1, "B", "CS", "DT" }, false);


            // string source = "DS_r := inner_join (DS_1 as d1, DS_2B as d2, apply d1 || d2)"; - documentation example error
            string source = "DS_r := inner_join (DS_1 as d1, DS_2B as d2 apply d1 || d2)";

            List<Exception> errors;
            string sql = this.TranslateVtl(source, out errors);

            Assert.Empty(errors);

            DataTable ds_r = this.RunSql(sql, "#DS_r");

            Assert.True(expected.EqualsTo(ds_r));
        }
    }
}
