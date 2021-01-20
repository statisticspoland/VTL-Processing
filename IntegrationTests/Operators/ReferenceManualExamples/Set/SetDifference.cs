namespace VtlProcessing.IntegrationTests.TSQL.Operators.ReferenceManualExamples.Set
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using Xunit;

    public class SetDifference : TSQLTestBase
    {
        public SetDifference() : base("Set_difference")
        {
            List<object[]> ds1a = new List<object[]>
            {
                new object[] { 2012, "B", "Total", "Total", 10 },
                new object[] { 2012, "G", "Total", "Total", 20 },
                new object[] { 2012, "F", "Total", "Total", 30 },
                new object[] { 2012, "M", "Total", "Total", 40 },
                new object[] { 2012, "I", "Total", "Total", 50 },
                new object[] { 2012, "S", "Total", "Total", 60 }
            };

            List<object[]> ds2a = new List<object[]>
            {
                new object[] { 2011, "B", "Total", "Total", 10 },
                new object[] { 2012, "G", "Total", "Total", 20 },
                new object[] { 2012, "F", "Total", "Total", 30 },
                new object[] { 2012, "M", "Total", "Total", 40 },
                new object[] { 2012, "I", "Total", "Total", 50 },
                new object[] { 2012, "S", "Total", "Total", 60 }
            };

            List<object[]> ds1b = new List<object[]>
            {
                new object[] { "R", "M", 2011, 7 },
                new object[] { "R", "F", 2011, 10 },
                new object[] { "R", "T", 2011, 12 }
            };

            List<object[]> ds2b = new List<object[]>
            {
                new object[] { "R", "M", 2011, 7 },
                new object[] { "R", "F", 2011, 10 }
            };

            this.SqlFillData("[Set_difference].DS_1A", ds1a);
            this.SqlFillData("[Set_difference].DS_1B", ds1b);
            this.SqlFillData("[Set_difference].DS_2A", ds2a);
            this.SqlFillData("[Set_difference].DS_2B", ds2b);
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
            expected.LoadDataRow(new object[] { 2012, "B", "Total", "Total", 10 }, false);

            string source = "DS_r := setdiff(DS_1A, DS_2A)";

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
            expected.Columns.Add("Me_1");
            expected.LoadDataRow(new object[] { "R", "T", 2011, 12 }, false);

            string source = "DS_r := setdiff(DS_1B, DS_2B)";

            List<Exception> errors;
            string sql = this.TranslateVtl(source, out errors);

            Assert.Empty(errors);

            DataTable ds_r = this.RunSql(sql, "#DS_r");

            Assert.True(expected.EqualsTo(ds_r));
        }
    }
}
