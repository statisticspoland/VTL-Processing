namespace VtlProcessing.IntegrationTests.TSQL.Operators.ReferenceManualExamples.Time
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using Xunit;

    public class TimeAggregation : TSQLTestBase
    {
        public TimeAggregation() : base("Time_aggregation")
        {
            List<object[]> ds1 = new List<object[]>
            {
                new object[] { "2010Q1", "A", 20 },
                new object[] { "2010Q2", "A", 20 },
                new object[] { "2010Q3", "A", 20 },
                new object[] { "2010Q1", "B", 50 },
                new object[] { "2010Q2", "B", 50 },
                new object[] { "2010Q1", "C", 10 },
                new object[] { "2010Q2", "C", 10 }
            };

            this.SqlFillData("[Time_aggregation].DS_1", ds1);
        }

        //[Fact]
        //public void Example1()
        //{
        //    DataTable expected = new DataTable();
        //    expected.Columns.Add("Id_1");
        //    expected.Columns.Add("Id_2");
        //    expected.Columns.Add("Me_1");
        //    expected.LoadDataRow(new object[] { "2010", "A", 60 }, false);
        //    expected.LoadDataRow(new object[] { "2011", "B", 100 }, false);
        //    expected.LoadDataRow(new object[] { "2010", "C", 20 }, false);


        //    string source = "DS_r := sum(DS_1) group all time_agg(\"A\", _, Me_1)";

        //    List<Exception> errors;
        //    string sql = this.TranslateVtl(source, out errors);

        //    Assert.Empty(errors);

        //    DataTable ds_r = this.RunSql(sql, "#DS_r");

        //    Assert.True(expected.EqualsTo(ds_r));
        //}

        //[Fact]
        //public void Example2()
        //{
        //    string expected = "2012Q1";
        //    string source = "var :=  time_agg(\"Q\", cast(\"2012M01\", time_period, \"YYYY\\MMM\"))";

        //    List<Exception> errors;
        //    string sql = this.TranslateVtl(source, out errors);

        //    Assert.Empty(errors);

        //    DataTable var = this.RunSql(sql, "@var");

        //    Assert.Single(var.Columns);
        //    Assert.Single(var.Rows);
        //    Assert.Equal(expected, var.Rows[0].Field<string>("value"));
        //}

        //[Fact]
        //public void Example3()
        //{
        //    string expected = "20120331";
        //    string source = "var := time_agg(\"Q\", cast(\"20120213\", date, \"YYYYMMDD\"), _, false)";

        //    List<Exception> errors;
        //    string sql = this.TranslateVtl(source, out errors);

        //    Assert.Empty(errors);

        //    DataTable var = this.RunSql(sql, "@var");

        //    Assert.Single(var.Columns);
        //    Assert.Single(var.Rows);
        //    Assert.Equal(expected, var.Rows[0].Field<string>("value"));
        //}

        //[Fact]
        //public void Example4()
        //{
        //    string expected = "20120101";
        //    string source = "var := time_agg(cast(\"A\", \"2012M1\", date, \"YYYYMMDD\"), _, true )";

        //    List<Exception> errors;
        //    string sql = this.TranslateVtl(source, out errors);

        //    Assert.Empty(errors);

        //    DataTable var = this.RunSql(sql, "@var");

        //    Assert.Single(var.Columns);
        //    Assert.Single(var.Rows);
        //    Assert.Equal(expected, var.Rows[0].Field<string>("value"));
        //}
    }
}
