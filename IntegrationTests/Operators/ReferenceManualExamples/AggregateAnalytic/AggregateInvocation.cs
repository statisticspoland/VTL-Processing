namespace VtlProcessing.IntegrationTests.TSQL.Operators.ReferenceManualExamples.AggregateAnalytic
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using Xunit;

    public class AggregateInvocation : TSQLTestBase
    {
        public AggregateInvocation() : base("Aggregate_invocation")
        {
            List<object[]> ds1a = new List<object[]>
            {
                new object[] { 2010, "E", "XX", 20, "" },
                new object[] { 2010, "B", "XX", 1, "H" },
                new object[] { 2010, "R", "XX", 1, "A" },
                new object[] { 2010, "F", "YY", 23, "" },
                new object[] { 2011, "E", "XX", 20, "P" },
                new object[] { 2011, "B", "ZZ", 1, "N" },
                new object[] { 2011, "R", "YY", -1, "P" },
                new object[] { 2011, "F", "XX", 20, "Z" },
                new object[] { 2012, "L", "ZZ", 40, "P" },
                new object[] { 2012, "E", "YY", 30, "P" },
            };

            List<object[]> ds1b = new List<object[]>
            {
                new object[] { 2010, "E", "XX", 20, "" },
                new object[] { 2010, "B", "XX", 1, "H" },
                new object[] { 2010, "R", "XX", 1, "A" },
                new object[] { 2010, "F", "YY", 23, "" },
                new object[] { 2011, "E", "XX", 20, "P" },
                new object[] { 2011, "B", "ZZ", 1, "N" },
                new object[] { 2011, "R", "YY", -1, "P" },
                new object[] { 2011, "F", "XX", 20, "Z" },
                new object[] { 2012, "L", "ZZ", 40, "P" },
                new object[] { 2012, "E", "YY", 30, "P" },
            };

            this.SqlFillData("[Aggregate_invocation].DS_1A", ds1a);
            this.SqlFillData("[Aggregate_invocation].DS_1B", ds1b);
        }

        [Fact]
        public void Example1()
        {
            DataTable expected = new DataTable();
            expected.Columns.Add("Id_1");
            expected.Columns.Add("Me_1");
            expected.LoadDataRow(new object[] { 2010, 11.25 }, false);
            expected.LoadDataRow(new object[] { 2011, 10 }, false);
            expected.LoadDataRow(new object[] { 2012, 35 }, false);

            string source = "DS_r := avg(DS_1A group by Id_1)";

            List<Exception> errors;
            string sql = this.TranslateVtl(source, out errors);

            Assert.Empty(errors);

            DataTable ds_r = this.RunSql(sql, "#DS_r");

            Assert.True(expected.EqualsTo(ds_r));
        }

        [Fact]
        public void Example1_Equivalent_A()
        {
            DataTable expected = new DataTable();
            expected.Columns.Add("Id_1");
            expected.Columns.Add("Me_1");
            expected.LoadDataRow(new object[] { 2010, 11.25 }, false);
            expected.LoadDataRow(new object[] { 2011, 10 }, false);
            expected.LoadDataRow(new object[] { 2012, 35 }, false);

            string source = "DS_r := avg(DS_1A group except Id_2, Id_3)";

            List<Exception> errors;
            string sql = this.TranslateVtl(source, out errors);

            Assert.Empty(errors);

            DataTable ds_r = this.RunSql(sql, "#DS_r");

            Assert.True(expected.EqualsTo(ds_r));
        }

        [Fact]
        public void Example1_Equivalent_B()
        {
            DataTable expected = new DataTable();
            expected.Columns.Add("Id_1");
            expected.Columns.Add("Me_1");
            expected.LoadDataRow(new object[] { 2010, 11.25 }, false);
            expected.LoadDataRow(new object[] { 2011, 10 }, false);
            expected.LoadDataRow(new object[] { 2012, 35 }, false);

            string source = "DS_r := avg(DS_1A#Me_1 group by Id_1)";

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
            expected.Columns.Add("Id_3");
            expected.Columns.Add("Me_1");
            // expected.LoadDataRow(new object[] { 2010, "XX", 22 }, false);
            // expected.LoadDataRow(new object[] { 2010, "YY", 23 }, false);
            // expected.LoadDataRow(new object[] { 2011, "XX", 40 }, false);
            // expected.LoadDataRow(new object[] { 2011, "ZZ", 1 }, false);
            // expected.LoadDataRow(new object[] { 2011, "YY", -1 }, false);
            // expected.LoadDataRow(new object[] { 2012, "ZZ", 40 }, false);
            // expected.LoadDataRow(new object[] { 2012, "YY", 30 }, false);
            // Changed order for test needs
            expected.LoadDataRow(new object[] { 2010, "XX", 22 }, false);
            expected.LoadDataRow(new object[] { 2011, "XX", 40 }, false);
            expected.LoadDataRow(new object[] { 2010, "YY", 23 }, false);
            expected.LoadDataRow(new object[] { 2011, "YY", -1 }, false);
            expected.LoadDataRow(new object[] { 2012, "YY", 30 }, false);
            expected.LoadDataRow(new object[] { 2011, "ZZ", 1 }, false);
            expected.LoadDataRow(new object[] { 2012, "ZZ", 40 }, false);

            string source = "DS_r := sum(DS_1A group by Id_1, Id_3)";

            List<Exception> errors;
            string sql = this.TranslateVtl(source, out errors);

            Assert.Empty(errors);

            DataTable ds_r = this.RunSql(sql, "#DS_r");

            Assert.True(expected.EqualsTo(ds_r));
        }

        // Example3 is wrong

        [Fact]
        public void Example4()
        {
            DataTable expected = new DataTable();
            expected.Columns.Add("Id_1");
            expected.Columns.Add("Me_2");
            expected.Columns.Add("Me_3");
            expected.Columns.Add("At_1");
            expected.LoadDataRow(new object[] { 2010, 23, 1, "" }, false);
            expected.LoadDataRow(new object[] { 2011, 20, -1, "N" }, false);
            expected.LoadDataRow(new object[] { 2012, 40, 30, "P" }, false);

            string source = "DS_r := DS_1B[aggr Me_2 := max(Me_1), Me_3 := min(Me_1) group by Id_1]";

            List<Exception> errors;
            string sql = this.TranslateVtl(source, out errors);

            Assert.Empty(errors);

            DataTable ds_r = this.RunSql(sql, "#DS_r");

            Assert.True(expected.EqualsTo(ds_r));
        }


    }
}
