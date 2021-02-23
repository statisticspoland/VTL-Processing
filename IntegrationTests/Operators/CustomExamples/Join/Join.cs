namespace VtlProcessing.IntegrationTests.TSQL.Operators.CustomExamples.Join
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using Xunit;

    public class Join : TSQLTestBase
    {
        public Join() : base("Custom_Join")
        {
            List<object[]> ds1 = new List<object[]>
            {
                new object[] { 1, "A", "A", "B", "X", 1 },
                new object[] { 1, "B", "C", "D", "Y", 4 },
                new object[] { 2, "A", "E", "F", "X", 7 }
            };

            List<object[]> ds2 = new List<object[]>
            {
                new object[] { 1, "A", "B", "Q", "Y", 4 },
                new object[] { 1, "B", "S", "T", "Y", 2 },
                new object[] { 3, "A", "Z", "M", "X", 5 }
            };

            this.SqlFillData("[Custom_Join].DS_1", ds1);
            this.SqlFillData("[Custom_Join].DS_2", ds2);
        }

        [Fact]
        public void KeepAttribute_AttributePropagation()
        {
            DataTable expected = new DataTable();
            expected.Columns.Add("Id_1");
            expected.Columns.Add("Id_2");
            expected.Columns.Add("At_2");
            expected.LoadDataRow(new object[] { 1, "A", 4 }, false);
            expected.LoadDataRow(new object[] { 1, "B", 4 }, false);

            string source = "DS_r := inner_join(DS_1 as ds1, DS_2 as ds2 keep At_2)";

            List<Exception> errors;
            string sql = this.TranslateVtl(source, out errors);

            Assert.Empty(errors);

            DataTable ds_r = this.RunSql(sql, "#DS_r");

            Assert.True(expected.EqualsTo(ds_r));
        }

        [Fact]
        public void KeepChosen()
        {
            DataTable expected = new DataTable();
            expected.Columns.Add("Id_1");
            expected.Columns.Add("Id_2");
            expected.Columns.Add("Me_1");
            expected.Columns.Add("At_2");
            expected.LoadDataRow(new object[] { 1, "A", "B", 1 }, false);
            expected.LoadDataRow(new object[] { 1, "B", "S", 4 }, false);

            string source = "DS_r := inner_join(DS_1 as ds1, DS_2 as ds2 keep ds1#At_2, ds2#Me_1)";

            List<Exception> errors;
            string sql = this.TranslateVtl(source, out errors);

            Assert.Empty(errors);

            DataTable ds_r = this.RunSql(sql, "#DS_r");

            Assert.True(expected.EqualsTo(ds_r));
        }

        [Fact]
        public void Drop()
        {
            DataTable expected = new DataTable();
            expected.Columns.Add("Id_1");
            expected.Columns.Add("Id_2");
            expected.Columns.Add("Me_1");
            expected.Columns.Add("Me_2");
            expected.Columns.Add("At_1");
            expected.LoadDataRow(new object[] { 1, "A", "B", "B", "X" }, false);
            expected.LoadDataRow(new object[] { 1, "B", "S", "D", "Y" }, false);

            string source = "DS_r := inner_join(DS_1 as ds1, DS_2 as ds2 drop ds1#Me_1, ds2#Me_2, At_2)";

            List<Exception> errors;
            string sql = this.TranslateVtl(source, out errors);

            Assert.Empty(errors);

            DataTable ds_r = this.RunSql(sql, "#DS_r");

            Assert.True(expected.EqualsTo(ds_r));
        }
    }
}
