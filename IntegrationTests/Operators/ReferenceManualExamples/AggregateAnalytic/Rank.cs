namespace VtlProcessing.IntegrationTests.TSQL.Operators.ReferenceManualExamples.AggregateAnalytic
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using Xunit;

    public class Rank : TSQLTestBase
    {
        public Rank() : base("Rank")
        {
            List<object[]> ds1 = new List<object[]>
            {
                new object[] {"A", "XX", 2000, 3, 1 },
                new object[] {"A", "XX", 2001, 4, 9 },
                new object[] {"A", "XX", 2002, 7, 5 },
                new object[] {"A", "XX", 2003, 6, 8 },
                new object[] {"A", "YY", 2000, 9, 3 },
                new object[] {"A", "YY", 2001, 5, 4 },
                new object[] {"A", "YY", 2002, 10, 2 },
                new object[] {"A", "YY", 2003, 5, 7 },
            };

            this.SqlFillData("[Rank].DS_1", ds1);
        }

        [Fact]
        public void Example1()
        {
            DataTable expected = new DataTable();
            expected.Columns.Add("Id_1");
            expected.Columns.Add("Id_2");
            expected.Columns.Add("Id_3");
            expected.Columns.Add("Me_1");
            expected.Columns.Add("Me_2");
            // expected.LoadDataRow(new object[] { "A", "XX", 2000, 3, 1 }, false);
            // expected.LoadDataRow(new object[] { "A", "XX", 2001, 4, 2 }, false);
            // expected.LoadDataRow(new object[] { "A", "XX", 2002, 7, 4 }, false);
            // expected.LoadDataRow(new object[] { "A", "XX", 2003, 6, 3 }, false);
            // expected.LoadDataRow(new object[] { "A", "YY", 2000, 9, 3 }, false);
            // expected.LoadDataRow(new object[] { "A", "YY", 2001, 5, 1 }, false);
            // expected.LoadDataRow(new object[] { "A", "YY", 2002, 10, 4 }, false);
            // expected.LoadDataRow(new object[] { "A", "YY", 2003, 5, 1 }, false);
            // Changed order for test needs
            expected.LoadDataRow(new object[] { "A", "XX", 2000, 3, 1 }, false);
            expected.LoadDataRow(new object[] { "A", "XX", 2001, 4, 2 }, false);
            expected.LoadDataRow(new object[] { "A", "XX", 2003, 6, 3 }, false);
            expected.LoadDataRow(new object[] { "A", "XX", 2002, 7, 4 }, false);
            expected.LoadDataRow(new object[] { "A", "YY", 2001, 5, 1 }, false);
            expected.LoadDataRow(new object[] { "A", "YY", 2003, 5, 1 }, false);
            expected.LoadDataRow(new object[] { "A", "YY", 2000, 9, 3 }, false);
            expected.LoadDataRow(new object[] { "A", "YY", 2002, 10, 4 }, false);

            // string source = "DS_r := DS_1[calc Me2 := rank(over (partition by Id_1, Id_2 order by Me_1))"; documentation example's error
            string source = "DS_r := DS_1[calc Me_2 := rank(over (partition by Id_1, Id_2 order by Me_1))]";

            List<Exception> errors;
            string sql = this.TranslateVtl(source, out errors);

            Assert.Empty(errors);

            DataTable ds_r = this.RunSql(sql, "#DS_r");

            Assert.True(expected.EqualsTo(ds_r));
        }
    }
}
