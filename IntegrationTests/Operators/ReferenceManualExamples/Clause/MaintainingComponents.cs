namespace VtlProcessing.IntegrationTests.TSQL.Operators.ReferenceManualExamples.Clause
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using Xunit;

    public class MaintainingComponents : TSQLTestBase
    {
        public MaintainingComponents() : base("Maintaining_components")
        {
            List<object[]> ds1 = new List<object[]>
            {
                new object[] { 2010, "A", "XX", 20, 36, "E" },
                new object[] { 2010, "A", "YY", 4, 9, "F" },
                new object[] { 2010, "B", "XX", 9, 10, "F" }
            };

            this.SqlFillData("[Maintaining_components].DS_1", ds1);
        }

        [Fact]
        public void Example1()
        {
            DataTable expected = new DataTable();
            expected.Columns.Add("Id_1");
            expected.Columns.Add("Id_2");
            expected.Columns.Add("Id_3");
            expected.Columns.Add("Me_1");
            expected.LoadDataRow(new object[] { 2010, "A", "XX", 20 }, false);
            expected.LoadDataRow(new object[] { 2010, "A", "YY", 4 }, false);
            expected.LoadDataRow(new object[] { 2010, "B", "XX", 9 }, false);

            string source = "DS_r := DS_1[keep Me_1]";

            List<Exception> errors;
            string sql = this.TranslateVtl(source, out errors);

            Assert.Empty(errors);

            DataTable ds_r = this.RunSql(sql, "#DS_r");

            Assert.True(expected.EqualsTo(ds_r));
        }
    }
}
