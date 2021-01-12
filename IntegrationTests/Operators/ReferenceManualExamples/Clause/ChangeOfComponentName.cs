namespace VtlProcessing.IntegrationTests.TSQL.Operators.ReferenceManualExamples.Clause
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using Xunit;

    public class ChangeOfComponentName : TSQLTestBase
    {
        public ChangeOfComponentName() : base("Change_of_component_name")
        {
            List<object[]> ds1 = new List<object[]>
            {
                new object[] { 1, "B", "XX", 20, "F" },
                new object[] { 1, "B", "YY", 1, "F" },
                new object[] { 2, "A", "XX", 4, "E" },
                new object[] { 2, "A", "YY", 9, "F" }
            };

            this.SqlFillData("[Change_of_component_name].DS_1", ds1);
        }

        [Fact]
        public void Example1()
        {
            DataTable expected = new DataTable();
            expected.Columns.Add("Id_1");
            expected.Columns.Add("Id_2");
            expected.Columns.Add("Id_3");
            expected.Columns.Add("Me_2");
            expected.Columns.Add("At_2");
            expected.LoadDataRow(new object[] { 1, "B", "XX", 20, "F" }, false);
            expected.LoadDataRow(new object[] { 1, "B", "YY", 1, "F" }, false);
            expected.LoadDataRow(new object[] { 2, "A", "XX", 4, "E" }, false);
            expected.LoadDataRow(new object[] { 2, "A", "YY", 9, "F" }, false);

            string source = "DS_r := DS_1[rename Me_1 to Me_2, At_1 to At_2]";

            List<Exception> errors;
            string sql = this.TranslateVtl(source, out errors);

            Assert.Empty(errors);

            DataTable ds_r = this.RunSql(sql, "#DS_r");

            Assert.True(expected.EqualsTo(ds_r));
        }
    }
}
