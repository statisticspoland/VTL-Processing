namespace VtlProcessing.IntegrationTests.TSQL.Operators.ReferenceManualExamples.Set
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using Xunit;

    public class Intersection : TSQLTestBase
    {
        public Intersection() : base("Intersection")
        {
            List<object[]> ds1 = new List<object[]>
            {
                new object[] { 2012, "B", "Total", "Total", 1 },
                new object[] { 2012, "G", "Total", "Total", 2 },
                new object[] { 2012, "F", "Total", "Total", 3 }
            };

            List<object[]> ds2 = new List<object[]>
            {
                new object[] { 2011, "B", "Total", "Total", 10 },
                new object[] { 2012, "G", "Total", "Total", 2 },
                new object[] { 2011, "M", "Total", "Total", 40 }
            };


            this.SqlFillData("[Intersection].DS_1", ds1);
            this.SqlFillData("[Intersection].DS_2", ds2);
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
            expected.LoadDataRow(new object[] { 2012, "G", "Total", "Total", 2 }, false);

            string source = "DS_r := intersect(DS_1, DS_2)";

            List<Exception> errors;
            string sql = this.TranslateVtl(source, out errors);

            Assert.Empty(errors);

            DataTable ds_r = this.RunSql(sql, "#DS_r");

            Assert.True(expected.EqualsTo(ds_r));
        }
    }
}
