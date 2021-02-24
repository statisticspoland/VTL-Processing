namespace VtlProcessing.IntegrationTests.TSQL.Operators.CustomExamples.GeneralPurpose
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
        public void NonViral_Membership()
        {
            DataTable expected = new DataTable();
            expected.Columns.Add("Id_1");
            expected.Columns.Add("Id_2");
            expected.Columns.Add("Me_1");
            expected.LoadDataRow(new object[] { 1, "A", 1 }, false);
            expected.LoadDataRow(new object[] { 1, "B", 2 }, false);
            expected.LoadDataRow(new object[] { 2, "A", 3 }, false);

            string source =
                "DS_1NV := DS_1[calc attribute At_1 := At_1];" +
                "DS_r := DS_1NV#Me_1";

            List<Exception> errors;
            string sql = this.TranslateVtl(source, out errors);

            Assert.Empty(errors);

            DataTable ds_r = this.RunSql(sql, "#DS_r");

            Assert.True(expected.EqualsTo(ds_r));
        }

        [Fact]
        public void Mebership_Membership()
        {
            DataTable expected = new DataTable();
            expected.Columns.Add("Id_1");
            expected.Columns.Add("Id_2");
            expected.Columns.Add("int_var");
            expected.Columns.Add("At_1");
            expected.LoadDataRow(new object[] { 1, "A", 1, null }, false);
            expected.LoadDataRow(new object[] { 1, "B", 1, "P" }, false);
            expected.LoadDataRow(new object[] { 2, "A", 2, null }, false);

            string source = "DS_r := (DS_1#Me_1)#Id_1";

            List<Exception> errors;
            string sql = this.TranslateVtl(source, out errors);

            Assert.Empty(errors);

            DataTable ds_r = this.RunSql(sql, "#DS_r");

            Assert.True(expected.EqualsTo(ds_r));
        }

        [Fact]
        public void Clause_Membership()
        {
            DataTable expected = new DataTable();
            expected.Columns.Add("Id_1");
            expected.Columns.Add("Id_2");
            expected.Columns.Add("Me_3");
            expected.Columns.Add("At_1");
            expected.LoadDataRow(new object[] { 1, "A", 2, null }, false);
            expected.LoadDataRow(new object[] { 1, "B", 4, "P" }, false);
            expected.LoadDataRow(new object[] { 2, "A", 6, null }, false);

            string source = "DS_r := DS_1[calc Me_3 := Me_1 * 2]#Me_3";

            List<Exception> errors;
            string sql = this.TranslateVtl(source, out errors);

            Assert.Empty(errors);

            DataTable ds_r = this.RunSql(sql, "#DS_r");

            Assert.True(expected.EqualsTo(ds_r));
        }

        [Fact]
        public void Join_Membership()
        {
            DataTable expected = new DataTable();
            expected.Columns.Add("Id_1");
            expected.Columns.Add("Id_2");
            expected.Columns.Add("Id_3");
            expected.Columns.Add("string_var");
            expected.Columns.Add("At_1");
            expected.LoadDataRow(new object[] { 1, "A", 1, null, null }, false);
            expected.LoadDataRow(new object[] { 1, "B", 1, "P", "P" }, false);
            expected.LoadDataRow(new object[] { 2, "A", 2, null, null }, false);

            string source = "DS_r := inner_join(DS_1 as ds1, DS_1 as ds2 calc identifier Id_3 := ds1#Id_1 drop ds1#At_1 rename ds2#Me_1 to Me_12, ds2#Me_2 to Me_22)#At_1";

            List<Exception> errors;
            string sql = this.TranslateVtl(source, out errors);

            Assert.Empty(errors);

            DataTable ds_r = this.RunSql(sql, "#DS_r");

            Assert.True(expected.EqualsTo(ds_r));
        }
        
        [Fact]
        public void Arithmetic_Membership()
        {
            DataTable expected = new DataTable();
            expected.Columns.Add("Id_1");
            expected.Columns.Add("Id_2");
            expected.Columns.Add("Me_1");
            expected.Columns.Add("At_1");
            expected.LoadDataRow(new object[] { 1, "A", 2, null }, false);
            expected.LoadDataRow(new object[] { 1, "B", 4, "P" }, false);
            expected.LoadDataRow(new object[] { 2, "A", 6, null }, false);

            string source = "DS_r := (DS_1 + DS_1)#Me_1";

            List<Exception> errors;
            string sql = this.TranslateVtl(source, out errors);

            Assert.Empty(errors);

            DataTable ds_r = this.RunSql(sql, "#DS_r");

            Assert.True(expected.EqualsTo(ds_r));
        }
    }
}
