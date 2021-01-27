namespace VtlProcessing.IntegrationTests.TSQL.Operators.ReferenceManualExamples.Clause
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using Xunit;

    public class CalculationOfComponent : TSQLTestBase
    {
        public CalculationOfComponent() : base("Calculation_of_a_Component")
        {
            List<object[]> ds1 = new List<object[]>
            {
                new object[] { 1, "A", "CA", 20 },
                new object[] { 1, "B", "CA", 2 },
                new object[] { 2, "A", "CA", 2 }
            };

            this.SqlFillData("[Calculation_of_a_Component].DS_1", ds1);
        }

        [Fact]
        public void Example1()
        {
            DataTable expected = new DataTable();
            expected.Columns.Add("Id_1");
            expected.Columns.Add("Id_2");
            expected.Columns.Add("Id_3");
            expected.Columns.Add("Me_1");
            expected.LoadDataRow(new object[] { 1, "A", "CA", 40 }, false);
            expected.LoadDataRow(new object[] { 1, "B", "CA", 4 }, false);
            expected.LoadDataRow(new object[] { 2, "A", "CA", 4 }, false);

            string source = "DS_r := DS_1[calc Me_1 := Me_1 * 2]";

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
            expected.Columns.Add("At_1");
            // expected.LoadDataRow(new object[] { 1, "A", "CA", 40, "EP" }, false); - documentation example result error
            expected.LoadDataRow(new object[] { 1, "A", "CA", 20, "EP" }, false);
            // expected.LoadDataRow(new object[] { 1, "B", "CA", 4, "EP" }, false); - documentation example result error
            expected.LoadDataRow(new object[] { 1, "B", "CA", 2, "EP" }, false);
            // expected.LoadDataRow(new object[] { 2, "A", "CA", 4, "EP" }, false); - documentation example result error
            expected.LoadDataRow(new object[] { 2, "A", "CA", 2, "EP" }, false);

            // string source = "DS_r := DS_1[calc attribute At_1 := \"EP\"]"; - documentation example error
            string source = "DS_r := DS_1[calc viral attribute At_1 := \"EP\"]";

            List<Exception> errors;
            string sql = this.TranslateVtl(source, out errors);

            Assert.Empty(errors);

            DataTable ds_r = this.RunSql(sql, "#DS_r");

            Assert.True(expected.EqualsTo(ds_r));
        }
    }
}
