namespace VtlProcessing.IntegrationTests.TSQL.Operators.ReferenceManualExamples.Conditional
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using Xunit;

    public class IfThenElse : TSQLTestBase
    {
        public IfThenElse() : base("If_then_else")
        {
            List<object[]> dsCond = new List<object[]>
            {
                new object[] { 2012, "B", "Total", "M", 5451780 },
                new object[] { 2012, "B", "Total", "F", 5643070 },
                new object[] { 2012, "G", "Total", "M", 5449803 },
                new object[] { 2012, "G", "Total", "F", 5673231 },
                new object[] { 2012, "S", "Total", "M", 23099012 },
                new object[] { 2012, "S", "Total", "F", 23719207 },
                new object[] { 2012, "F", "Total", "M", 31616281 },
                new object[] { 2012, "F", "Total", "F", 33671580 },
                new object[] { 2012, "I", "Total", "M", 28726599 },
                new object[] { 2012, "I", "Total", "F", 30667608 },
                new object[] { 2012, "A", "Total", "M", null },
                new object[] { 2012, "A", "Total", "F", null }
            };

            List<object[]> ds1 = new List<object[]>
            {
                new object[] { 2012, "S", "Total", "F", 25.8 },
                new object[] { 2012, "F", "Total", "F", null },
                new object[] { 2012, "I", "Total", "F", 20.9 },
                new object[] { 2012, "A", "Total", "M", 6.3 }
            };

            List<object[]> ds2 = new List<object[]>
            {                                      
                new object[] { 2012, "B", "Total", "M", 0.12 },
                new object[] { 2012, "G", "Total", "M", 22.5 },
                new object[] { 2012, "S", "Total", "M", 23.7 },
                new object[] { 2012, "A", "Total", "F", null }
            };

            this.SqlFillData("[If_then_else].DS_cond", dsCond);
            this.SqlFillData("[If_then_else].DS_1", ds1);
            this.SqlFillData("[If_then_else].DS_2", ds2);
        }

        [Fact]
        public void Example1()
        {
            DataTable expected = new DataTable();
            expected.Columns.Add("Id_1");
            expected.Columns.Add("Id_2");
            expected.Columns.Add("Id_3");
            expected.Columns.Add("Id_4");
            expected.Columns.Add("Me_1");
            // expected.LoadDataRow(new object[] { 2012, "S", "Total", "F", 25.8 }, false);
            // expected.LoadDataRow(new object[] { 2012, "F", "Total", "F", null }, false);
            // expected.LoadDataRow(new object[] { 2012, "I", "Total", "F", 20.9 }, false);
            // Sorted for test needs
            expected.LoadDataRow(new object[] { 2012, "F", "Total", "F", null }, false);
            expected.LoadDataRow(new object[] { 2012, "I", "Total", "F", 20.9 }, false);
            expected.LoadDataRow(new object[] { 2012, "S", "Total", "F", 25.8 }, false);

            string source = "DS_r := if (DS_cond#Id_4 = \"F\") then DS_1 else DS_2";

            List<Exception> errors;
            string sql = this.TranslateVtl(source, out errors);

            Assert.Empty(errors);

            DataTable ds_r = this.RunSql(sql, "#DS_r");

            Assert.True(expected.EqualsTo(ds_r));
        }
    }
}
