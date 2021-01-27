namespace VtlProcessing.IntegrationTests.TSQL.Operators.ReferenceManualExamples.Clause
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using Xunit;

    public class FilteringDataPoints : TSQLTestBase
    {
        public FilteringDataPoints() : base("Filtering_data_points")
        {
            List<object[]> ds1 = new List<object[]>
            {
                new object[] { 1, "A", "XX", 2, "E" },
                new object[] { 1, "A", "YY", 2, "F" },
                new object[] { 1, "B", "XX", 20, "F" },
                new object[] { 1, "B", "YY", 1, "F" },
                new object[] { 2, "A", "XX", 4, "E" },
                new object[] { 2, "A", "YY", 9, "F" }
            };

            this.SqlFillData("[Filtering_data_points].DS_1", ds1);
        }

        [Fact]
        public void Example1()
        {
            DataTable expected = new DataTable();
            expected.Columns.Add("Id_1");
            expected.Columns.Add("Id_2");
            expected.Columns.Add("Id_3");
            expected.Columns.Add("Me_1");
            expected.Columns.Add("At_1");
            expected.LoadDataRow(new object[] { 1, "A", "XX", 2, "E" }, false);
            expected.LoadDataRow(new object[] { 1, "A", "YY", 2, "F" }, false);
            expected.LoadDataRow(new object[] { 1, "B", "YY", 1, "F" }, false);

            string source = "DS_r := DS_1[filter Id_1 = 1 and Me_1 < 10]";

            List<Exception> errors;
            string sql = this.TranslateVtl(source, out errors);

            Assert.Empty(errors);

            DataTable ds_r = this.RunSql(sql, "#DS_r");

            Assert.True(expected.EqualsTo(ds_r));
        }
    }
}
