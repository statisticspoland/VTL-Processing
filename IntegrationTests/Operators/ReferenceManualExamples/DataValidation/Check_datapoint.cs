namespace VtlProcessing.IntegrationTests.TSQL.Operators.ReferenceManualExamples.DataValidation
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Text;
    using Xunit;

    public class Check_datapoint : TSQLTestBase
    {
        private readonly string _rulesetSource;

        public Check_datapoint() : base("Check_datapoint")
        {
            List<object[]> ds1 = new List<object[]>
            {
                new object[] { 2011, 'l', "CREDIT", 10 },
                new object[] { 2011, 'l', "DEBIT", -2 },
                new object[] { 2012, 'l', "CREDIT", 10 },
                new object[] { 2012, 'l', "DEBIT", 2 }
            };

            this.SqlFillData("[Check_datapoint].DS_1", ds1);

            this._rulesetSource = new StringBuilder()
                .AppendLine("define datapoint ruleset dpr1 ( variable Id_3, Me_1 ) is")
                .AppendLine("     when Id_3 = \"CREDIT\" then Me_1 >= 0 errorcode \"Bad credit\"")
                .AppendLine("   ; when Id_3 = \"DEBIT\" then Me_1 >= 0 errorcode \"Bad debit\"")
                .AppendLine("end datapoint ruleset;")
                .ToString();
        }

        [Fact]
        public void Example1()
        {
            DataTable expected = new DataTable();
            expected.Columns.Add("Id_1");
            expected.Columns.Add("Id_2");
            expected.Columns.Add("Id_3");
            expected.Columns.Add("ruleid");
            // expected.Columns.Add("obs_value"); - documentation example's error
            expected.Columns.Add("Me_1");
            expected.Columns.Add("errorcode");
            expected.Columns.Add("errorlevel");
            expected.LoadDataRow(new object[] { 2011, 'l', "DEBIT", "dpr1_2", -2, "Bad debit", null }, false);

            string source = "DS_r := check_datapoint(DS_1, dpr1)";

            List<Exception> errors;
            string sql = this.TranslateVtl($"{this._rulesetSource}{source}", out errors);

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
            expected.Columns.Add("ruleid");
            expected.Columns.Add("bool_var");
            expected.Columns.Add("errorcode");
            expected.Columns.Add("errorlevel");
            // expected.LoadDataRow(new object[] { 2011, 'l', "CREDIT", "dpr1_1", true, null, null }, false);
            // expected.LoadDataRow(new object[] { 2011, 'l', "CREDIT", "dpr1_2", true, null, null }, false);
            // expected.LoadDataRow(new object[] { 2011, 'l', "DEBIT", "dpr1_1", true, null, null }, false);
            // expected.LoadDataRow(new object[] { 2011, 'l', "DEBIT", "dpr1_2", false, "Bad debit", null }, false);
            // expected.LoadDataRow(new object[] { 2012, 'l', "CREDIT", "dpr1_1", true, null, null }, false);
            // expected.LoadDataRow(new object[] { 2012, 'l', "CREDIT", "dpr1_2", true, null, null }, false);
            // expected.LoadDataRow(new object[] { 2012, 'l', "DEBIT", "dpr1_1", true, null, null }, false);
            // expected.LoadDataRow(new object[] { 2012, 'l', "DEBIT", "dpr1_2", true, null, null }, false);
            // Changed order and boolean values to bits for test needs
            expected.LoadDataRow(new object[] { 2011, 'l', "CREDIT", "dpr1_1", 1, null, null }, false);
            expected.LoadDataRow(new object[] { 2011, 'l', "DEBIT", "dpr1_1", 1, null, null }, false);
            expected.LoadDataRow(new object[] { 2012, 'l', "CREDIT", "dpr1_1", 1, null, null }, false);
            expected.LoadDataRow(new object[] { 2012, 'l', "DEBIT", "dpr1_1", 1, null, null }, false);
            expected.LoadDataRow(new object[] { 2011, 'l', "CREDIT", "dpr1_2", 1, null, null }, false);
            expected.LoadDataRow(new object[] { 2011, 'l', "DEBIT", "dpr1_2", 0, "Bad debit", null }, false);
            expected.LoadDataRow(new object[] { 2012, 'l', "CREDIT", "dpr1_2", 1, null, null }, false);
            expected.LoadDataRow(new object[] { 2012, 'l', "DEBIT", "dpr1_2", 1, null, null }, false);

            string source = "DS_r := check_datapoint(DS_1, dpr1 all)";

            List<Exception> errors;
            string sql = this.TranslateVtl($"{this._rulesetSource}{source}", out errors);

            Assert.Empty(errors);

            DataTable ds_r = this.RunSql(sql, "#DS_r");

            Assert.True(expected.EqualsTo(ds_r));
        }
    }
}
