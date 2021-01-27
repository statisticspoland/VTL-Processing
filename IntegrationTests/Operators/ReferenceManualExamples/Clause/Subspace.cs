namespace VtlProcessing.IntegrationTests.TSQL.Operators.ReferenceManualExamples.Clause
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using Xunit;

    public class Subspace : TSQLTestBase
    {
        public Subspace() : base("Subspace")
        {
            List<object[]> ds1 = new List<object[]>
            {
                new object[] { 1, "A", "XX", 20, "F" },
                new object[] { 1, "A", "YY", 1, "F" },
                new object[] { 1, "B", "XX", 4, "E" },
                new object[] { 1, "B", "YY", 9, "F" },
                new object[] { 2, "A", "XX", 7, "F" },
                new object[] { 2, "A", "YY", 5, "E" },
                new object[] { 2, "B", "XX", 12, "F" },
                new object[] { 2, "B", "YY", 15, "F" },
            };

            this.SqlFillData("[Subspace].DS_1", ds1);
        }

        [Fact]
        public void Example1()
        {
            DataTable expected = new DataTable();
            expected.Columns.Add("Id_3");
            expected.Columns.Add("Me_1");
            expected.Columns.Add("At_1");
            expected.LoadDataRow(new object[] {"XX", 20, "F" }, false);
            expected.LoadDataRow(new object[] {"YY", 1, "F" }, false);

            string source = "DS_r := DS_1[sub Id_1 = 1, Id_2 = \"A\"]";

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
            expected.Columns.Add("Me_1");
            expected.Columns.Add("At_1");
            expected.LoadDataRow(new object[] {9, "F" }, false);

            string source = "DS_r := DS_1[sub Id_1 = 1, Id_2 = \"B\", Id_3 = \"YY\"]";

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
            expected.Columns.Add("Id_3");
            expected.Columns.Add("Me_1");
            expected.Columns.Add("At_1");
            // expected.LoadDataRow(new object[] { 1, "XX", 24, "F" }, false); - documentation example error
            expected.LoadDataRow(new object[] { 1, "XX", 24, "E" }, false);
            expected.LoadDataRow(new object[] { 1, "YY", 10, "F" }, false);
            expected.LoadDataRow(new object[] { 2, "XX", 19, "F" }, false);
            // expected.LoadDataRow(new object[] { 2, "XX", 19, "F" }, false); - documentation example error
            expected.LoadDataRow(new object[] { 2, "YY", 20, "E" }, false);

            string source = "DS_r := DS_1[sub Id_2 = \"A\"] + DS_1[sub Id_2 = \"B\"]";

            List<Exception> errors;
            string sql = this.TranslateVtl(source, out errors);

            Assert.Empty(errors);

            DataTable ds_r = this.RunSql(sql, "#DS_r");

            Assert.True(expected.EqualsTo(ds_r));
        }
    }
}
