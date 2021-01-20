namespace VtlProcessing.IntegrationTests.TSQL.Operators.ReferenceManualExamples.AggregateAnalytic
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using Xunit;

    public class CountingTheNumberOfDataPoints : TSQLTestBase
    {
        public CountingTheNumberOfDataPoints() : base("Counting_the_number_of_data_points")
        {
            List<object[]> ds1 = new List<object[]>
            {
                new object[] { 2011, "A", "XX", "iii"},
                new object[] { 2011, "A", "YY", "jjj"},
                new object[] { 2011, "B", "YY", "iii"},
                new object[] { 2012, "A", "XX", "kkk"},
                new object[] { 2012, "B", "YY", "iii"},
            };

            this.SqlFillData("[Counting_the_number_of_data_points].DS_1", ds1);
        }

        [Fact]
        public void Example1()
        {
            DataTable expected = new DataTable();
            expected.Columns.Add("Id_1");
            expected.Columns.Add("int_var");
            expected.LoadDataRow(new object[] { 2011, 3 }, false);
            expected.LoadDataRow(new object[] { 2012, 2 }, false);

            string source = "DS_r := count(DS_1 group by Id_1)";

            List<Exception> errors;
            string sql = this.TranslateVtl(source, out errors);

            Assert.Empty(errors);

            DataTable ds_r = this.RunSql(sql, "#DS_r");

            Assert.True(expected.EqualsTo(ds_r));
        }

        [Fact]
        public void Example1_Having()
        {
            DataTable expected = new DataTable();
            expected.Columns.Add("Id_1");
            expected.Columns.Add("int_var");
            expected.LoadDataRow(new object[] { 2011, 3 }, false);

            // string source = "DS_r := sum(DS_1 group by Id_1 having count() > 2)"; // documentation example's error
            string source = "DS_r := count(DS_1 group by Id_1 having count() > 2)";

            List<Exception> errors;
            string sql = this.TranslateVtl(source, out errors);

            Assert.Empty(errors);

            DataTable ds_r = this.RunSql(sql, "#DS_r");

            Assert.True(expected.EqualsTo(ds_r));
        }
    }
}
