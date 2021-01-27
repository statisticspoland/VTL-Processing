namespace VtlProcessing.IntegrationTests.TSQL.Operators.ReferenceManualExamples.GeneralPurpose
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using Xunit;

    public class NonPersistentAssignment : TSQLTestBase
    {
        public NonPersistentAssignment() : base("Non_Persistent_Assignment")
        {
            List<object[]> ds1 = new List<object[]>
            {
                new object[] { 2013, "Belgium", 5, 5 },
                new object[] { 2013, "Denmark", 2, 10 },
                new object[] { 2013, "France", 3, 12 },
                new object[] { 2013, "Spain", 4, 20 }
            };

            this.SqlFillData("[Non_Persistent_assignment].DS_1", ds1);
        }

        [Fact]
        public void Example1()
        {
            DataTable expected = new DataTable();
            expected.Columns.Add("Id_1");
            expected.Columns.Add("Id_2");
            expected.Columns.Add("Me_1");
            expected.Columns.Add("Me_2");
            expected.LoadDataRow(new object[] { 2013, "Belgium", 5, 5 }, false);
            expected.LoadDataRow(new object[] { 2013, "Denmark", 2, 10 }, false);
            expected.LoadDataRow(new object[] { 2013, "France", 3, 12 }, false);
            expected.LoadDataRow(new object[] { 2013, "Spain", 4, 20 }, false);

            string source = "DS_r := DS_1";

            List<Exception> errors;
            string sql = this.TranslateVtl(source, out errors);

            Assert.Empty(errors);

            DataTable ds_r = this.RunSql(sql, "#DS_r");

            Assert.True(expected.EqualsTo(ds_r));
        }
    }
}
