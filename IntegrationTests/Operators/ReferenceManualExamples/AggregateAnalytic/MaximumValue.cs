﻿namespace VtlProcessing.IntegrationTests.TSQL.Operators.ReferenceManualExamples.AggregateAnalytic
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using Xunit;

    public class MaximumValue : TSQLTestBase
    {
        public MaximumValue() : base("Maximum_value")
        {
            List<object[]> ds1 = new List<object[]>
            {
                new object[] { 2011, "A", "XX", 3 },
                new object[] { 2011, "A", "YY", 5 },
                new object[] { 2011, "B", "YY", 7 },
                new object[] { 2012, "A", "XX", 2 },
                new object[] { 2012, "B", "YY", 4 },
            };

            this.SqlFillData("[Maximum_value].DS_1", ds1);
        }

        [Fact]
        public void Example1()
        {
            DataTable expected = new DataTable();
            expected.Columns.Add("Id_1");
            expected.Columns.Add("Me_1");
            expected.LoadDataRow(new object[] { 2011, 7 }, false);
            expected.LoadDataRow(new object[] { 2012, 4 }, false);

            string source = "DS_r := max(DS_1 group by Id_1)";

            List<Exception> errors;
            string sql = this.TranslateVtl(source, out errors);

            Assert.Empty(errors);

            DataTable ds_r = this.RunSql(sql, "#DS_r");

            Assert.True(expected.EqualsTo(ds_r));
        }
    }
}
