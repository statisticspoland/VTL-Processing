namespace VtlProcessing.IntegrationTests.TSQL.Operators.ReferenceManualExamples.AggregateAnalytic
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using Xunit;

    public class SampleStandardDeviation : TSQLTestBase
    {
        public SampleStandardDeviation() : base("Sample_Standard_Deviation")
        {
            List<object[]> ds1 = new List<object[]>
            {
                new object[] { 2011, "A", "XX", 3 },
                new object[] { 2011, "A", "YY", 5 },
                new object[] { 2011, "B", "YY", 7 },
                new object[] { 2012, "A", "XX", 2 },
                new object[] { 2012, "B", "YY", 4 },
            };

            this.SqlFillData("[Sample_Standard_Deviation].DS_1", ds1);
        }

        [Fact]
        public void Example1()
        {
            DataTable expected = new DataTable();
            expected.Columns.Add("Id_1");
            expected.Columns.Add("Me_1");
            expected.LoadDataRow(new object[] { 2011, 2 }, false);
            // expected.LoadDataRow(new object[] { 2012, 1.4142 }, false);
            expected.LoadDataRow(new object[] { 2012, 1.4142135623730951 }, false); // Increased precision for test needs

            string source = "DS_r := stddev_samp(DS_1 group by Id_1)";

            List<Exception> errors;
            string sql = this.TranslateVtl(source, out errors);

            Assert.Empty(errors);

            DataTable ds_r = this.RunSql(sql, "#DS_r");

            Assert.True(expected.EqualsTo(ds_r));
        }
    }
}
