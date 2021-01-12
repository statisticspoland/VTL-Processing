namespace VtlProcessing.IntegrationTests.TSQL.Operators.ReferenceManualExamples.GeneralPurpose
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using Xunit;

    public class Membership : TSQLTestBase
    {
        public Membership() : base("Membership")
        {
            List<object[]> ds1 = new List<object[]>
            {
                new object[] { 1, "A", 1, 5, null },
                new object[] { 1, "B", 2, 10, "P" },
                new object[] { 2, "A", 3, 12, null}
            };

            this.SqlFillData("[Membership].DS_1", ds1);
        }

        [Fact]
        public void Example1()
        {
            DataTable expected = new DataTable();
            expected.Columns.Add("Id_1");
            expected.Columns.Add("Id_2");
            expected.Columns.Add("Me_1");
            expected.Columns.Add("At_1");
            expected.LoadDataRow(new object[] { 1, "A", 1, null }, false);
            expected.LoadDataRow(new object[] { 1, "B", 2, "P" }, false);
            expected.LoadDataRow(new object[] { 2, "A", 3, null }, false);

            string source = "DS_r := DS_1#Me_1";

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
            expected.Columns.Add("int_var");
            expected.Columns.Add("At_1");
            expected.LoadDataRow(new object[] { 1, "A", 1, null }, false);
            expected.LoadDataRow(new object[] { 1, "B", 1, "P" }, false);
            expected.LoadDataRow(new object[] { 2, "A", 2, null }, false);

            string source = "DS_r := DS_1#Id_1";

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
            expected.Columns.Add("string_var");
            expected.Columns.Add("At_1");
            expected.LoadDataRow(new object[] { 1, "A", null, null }, false);
            expected.LoadDataRow(new object[] { 1, "B", "P", "P" }, false);
            expected.LoadDataRow(new object[] { 2, "A", null, null }, false);

            string source = "DS_r := DS_1#At_1";

            List<Exception> errors;
            string sql = this.TranslateVtl(source, out errors);

            Assert.Empty(errors);

            DataTable ds_r = this.RunSql(sql, "#DS_r");

            Assert.True(expected.EqualsTo(ds_r));
        }
    }
}
