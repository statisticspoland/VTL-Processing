namespace VtlProcessing.IntegrationTests.TSQL.Operators.CustomExamples.Time
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using Xunit;

    public class CurrentDate : TSQLTestBase
    {
        public CurrentDate() : base("dbo")
        {
        }

        [Fact]
        public void GetCurrentDate()
        {
            string expected = DateTime.Now.ToString("yyyy-MM-dd");
            string source = "var := current_date";

            List<Exception> errors;
            string sql = this.TranslateVtl(source, out errors);

            Assert.Empty(errors);

            DataTable var = this.RunSql(sql, "@var");

            Assert.Single(var.Columns);
            Assert.Single(var.Rows);
            Assert.Equal(expected, var.Rows[0].Field<string>("value"));
        }
    }
}
