namespace VtlProcessing.IntegrationTests.TSQL.Operators.ReferenceManualExamples.Time
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using Xunit;

    public class FillTimeSeries : TSQLTestBase
    {
        public FillTimeSeries() : base("Fill_time_series")
        {
            List<object[]> ds1 = new List<object[]>
            {
                new object[] { "A", "2010-01/2010-12", "hello world" },
                new object[] { "A", "2012-01/2012-12", "say hello" },
                new object[] { "A", "2013-01/2013-12", "he" },
                new object[] { "B", "2011-01/2011-12", "hi, hello!" },
                new object[] { "B", "2012-01/2012-12", "hi" },
                new object[] { "B", "2014-01/2014-12", "hello!" }
            };

            List<object[]> ds2 = new List<object[]>
            {
                new object[] { "A", "2010-12-31", "hello world" },
                new object[] { "A", "2012-12-31", "say hello" },
                new object[] { "A", "2013-12-31", "he" },
                new object[] { "B", "2011-12-31", "hi, hello!" },
                new object[] { "B", "2012-12-31", "hi" },
                new object[] { "B", "2014-12-31", "hello!" }
            };

            // List<object[]> ds3 = new List<object[]>
            // {
            //     new object[] { "A", "2010Y", "hello world" },
            //     new object[] { "A", "2012Y", "say hello" },
            //     new object[] { "A", "2013Y", "he" },
            //     new object[] { "B", "2011Y", "hi, hello!" },
            //     new object[] { "B", "2012Y", "hi" },
            //     new object[] { "B", "2014Y", "hello!" }
            // }; - documentation example input error

            List<object[]> ds3 = new List<object[]>
             {
                 new object[] { "A", "2010A", "hello world" },
                 new object[] { "A", "2012A", "say hello" },
                 new object[] { "A", "2013A", "he" },
                 new object[] { "B", "2011A", "hi, hello!" },
                 new object[] { "B", "2012A", "hi" },
                 new object[] { "B", "2014A", "hello!" }
             };

            List<object[]> ds4 = new List<object[]>
            {
                // new object[] { "A", "2010Y", "hello world" }, - documentation example input error
                new object[] { "A", "2010A", "hello world" }, 
                // new object[] { "A", "2012Y", "say hello" }, - documentation example input error
                new object[] { "A", "2012A", "say hello" },
                new object[] { "A", "2010Q1", "he" },
                new object[] { "A", "2010Q2", "hi, hello!" },
                new object[] { "A", "2010Q4", "hi" },
                new object[] { "A", "2011Q2", "hello!" }
            };

            this.SqlFillData("[Fill_time_series].DS_1", ds1);
            this.SqlFillData("[Fill_time_series].DS_2", ds2);
            this.SqlFillData("[Fill_time_series].DS_3", ds3);
            this.SqlFillData("[Fill_time_series].DS_4", ds4);
        }

        [Fact]
        public void Example1()
        {
            DataTable expected = new DataTable();
            expected.Columns.Add("Id_1");
            expected.Columns.Add("Id_2");
            expected.Columns.Add("Me_1");
            expected.LoadDataRow(new object[] { "A", "2010-01/2010-12", "hello world" }, false);
            expected.LoadDataRow(new object[] { "A", "2011-01/2011-12", null }, false);
            expected.LoadDataRow(new object[] { "A", "2012-01/2012-12", "say hello" }, false);
            expected.LoadDataRow(new object[] { "A", "2013-01/2013-12", "he" }, false);
            expected.LoadDataRow(new object[] { "B", "2011-01/2011-12", "hi, hello!" }, false);
            expected.LoadDataRow(new object[] { "B", "2012-01/2012-12", "hi" }, false);
            expected.LoadDataRow(new object[] { "B", "2013-01/2013-12", null }, false);
            expected.LoadDataRow(new object[] { "B", "2014-01/2014-12", "hello!" }, false);

            string source = "DS_r := fill_time_series(DS_1, single)";

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
            expected.Columns.Add("Id_1");
            expected.Columns.Add("Id_2");
            expected.Columns.Add("Me_1");
            expected.LoadDataRow(new object[] { "A", "2010-01/2010-12", "hello world" }, false);
            expected.LoadDataRow(new object[] { "A", "2011-01/2011-12", null }, false);
            expected.LoadDataRow(new object[] { "A", "2012-01/2012-12", "say hello" }, false);
            expected.LoadDataRow(new object[] { "A", "2013-01/2013-12", "he" }, false);
            expected.LoadDataRow(new object[] { "A", "2014-01/2014-12", null }, false);
            expected.LoadDataRow(new object[] { "B", "2010-01/2010-12", null }, false);
            expected.LoadDataRow(new object[] { "B", "2011-01/2011-12", "hi, hello!" }, false);
            expected.LoadDataRow(new object[] { "B", "2012-01/2012-12", "hi" }, false);
            expected.LoadDataRow(new object[] { "B", "2013-01/2013-12", null }, false);
            expected.LoadDataRow(new object[] { "B", "2014-01/2014-12", "hello!" }, false);

            string source = "DS_r := fill_time_series(DS_1, all)";

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
            expected.Columns.Add("Id_2");
            expected.Columns.Add("Me_1");
            expected.LoadDataRow(new object[] { "A", "2010-12-31", "hello world" }, false);
            expected.LoadDataRow(new object[] { "A", "2011-12-31", null }, false);
            expected.LoadDataRow(new object[] { "A", "2012-12-31", "say hello" }, false);
            expected.LoadDataRow(new object[] { "A", "2013-12-31", "he" }, false);
            expected.LoadDataRow(new object[] { "B", "2011-12-31", "hi, hello!" }, false);
            expected.LoadDataRow(new object[] { "B", "2012-12-31", "hi" }, false);
            expected.LoadDataRow(new object[] { "B", "2013-12-31", null }, false);
            expected.LoadDataRow(new object[] { "B", "2014-12-31", "hello!" }, false);

            string source = "DS_r := fill_time_series(DS_2, single)";

            List<Exception> errors;
            string sql = this.TranslateVtl(source, out errors);

            Assert.Empty(errors);

            DataTable ds_r = this.RunSql(sql, "#DS_r");

            Assert.True(expected.EqualsTo(ds_r));
        }

        [Fact]
        public void Example4()
        {
            DataTable expected = new DataTable();
            expected.Columns.Add("Id_1");
            expected.Columns.Add("Id_2");
            expected.Columns.Add("Me_1");
            expected.LoadDataRow(new object[] { "A", "2010-12-31", "hello world" }, false);
            expected.LoadDataRow(new object[] { "A", "2011-12-31", null }, false);
            expected.LoadDataRow(new object[] { "A", "2012-12-31", "say hello" }, false);
            expected.LoadDataRow(new object[] { "A", "2013-12-31", "he" }, false);
            expected.LoadDataRow(new object[] { "A", "2014-12-31", null }, false);
            expected.LoadDataRow(new object[] { "B", "2010-12-31", null }, false);
            expected.LoadDataRow(new object[] { "B", "2011-12-31", "hi, hello!" }, false);
            expected.LoadDataRow(new object[] { "B", "2012-12-31", "hi" }, false);
            expected.LoadDataRow(new object[] { "B", "2013-12-31", null }, false);
            expected.LoadDataRow(new object[] { "B", "2014-12-31", "hello!" }, false);

            string source = "DS_r := fill_time_series(DS_2, all)";

            List<Exception> errors;
            string sql = this.TranslateVtl(source, out errors);

            Assert.Empty(errors);

            DataTable ds_r = this.RunSql(sql, "#DS_r");

            Assert.True(expected.EqualsTo(ds_r));
        }

        [Fact]
        public void Example5()
        {
            DataTable expected = new DataTable();
            expected.Columns.Add("Id_1");
            expected.Columns.Add("Id_2");
            expected.Columns.Add("Me_1");
            // expected.LoadDataRow(new object[] { "A", "2010Y", "hello world" }, false);
            // expected.LoadDataRow(new object[] { "A", "2011Y", null }, false);
            // expected.LoadDataRow(new object[] { "A", "2012Y", "say hello" }, false);
            // expected.LoadDataRow(new object[] { "A", "2013Y", "he" }, false);
            // expected.LoadDataRow(new object[] { "B", "2011Y", "hi, hello!" }, false);
            // expected.LoadDataRow(new object[] { "B", "2012Y", "hi" }, false);
            // expected.LoadDataRow(new object[] { "B", "2013Y", null }, false);
            // expected.LoadDataRow(new object[] { "B", "2014Y", "hello!" }, false);
            // documentation example result error
            expected.LoadDataRow(new object[] { "A", "2010A", "hello world" }, false);
            expected.LoadDataRow(new object[] { "A", "2011A", null }, false);
            expected.LoadDataRow(new object[] { "A", "2012A", "say hello" }, false);
            expected.LoadDataRow(new object[] { "A", "2013A", "he" }, false);
            expected.LoadDataRow(new object[] { "B", "2011A", "hi, hello!" }, false);
            expected.LoadDataRow(new object[] { "B", "2012A", "hi" }, false);
            expected.LoadDataRow(new object[] { "B", "2013A", null }, false);
            expected.LoadDataRow(new object[] { "B", "2014A", "hello!" }, false);

            string source = "DS_r := fill_time_series(DS_3, single)";

            List<Exception> errors;
            string sql = this.TranslateVtl(source, out errors);

            Assert.Empty(errors);

            DataTable ds_r = this.RunSql(sql, "#DS_r");

            Assert.True(expected.EqualsTo(ds_r));
        }

        [Fact]
        public void Example6()
        {
            DataTable expected = new DataTable();
            expected.Columns.Add("Id_1");
            expected.Columns.Add("Id_2");
            expected.Columns.Add("Me_1");
            // expected.LoadDataRow(new object[] { "A", "2010Y", "hello world" }, false);
            // expected.LoadDataRow(new object[] { "A", "2011Y", null }, false);
            // expected.LoadDataRow(new object[] { "A", "2012Y", "say hello" }, false);
            // expected.LoadDataRow(new object[] { "A", "2013Y", "he" }, false);
            // expected.LoadDataRow(new object[] { "A", "2014Y", null }, false);
            // expected.LoadDataRow(new object[] { "B", "2010Y", null }, false);
            // expected.LoadDataRow(new object[] { "B", "2011Y", "hi, hello!" }, false);
            // expected.LoadDataRow(new object[] { "B", "2012Y", "hi" }, false);
            // expected.LoadDataRow(new object[] { "B", "2013Y", null }, false);
            // expected.LoadDataRow(new object[] { "B", "2014Y", "hello!" }, false);
            // documentation example result error
            expected.LoadDataRow(new object[] { "A", "2010A", "hello world" }, false);
            expected.LoadDataRow(new object[] { "A", "2011A", null }, false);
            expected.LoadDataRow(new object[] { "A", "2012A", "say hello" }, false);
            expected.LoadDataRow(new object[] { "A", "2013A", "he" }, false);
            expected.LoadDataRow(new object[] { "A", "2014A", null }, false);
            expected.LoadDataRow(new object[] { "B", "2010A", null }, false);
            expected.LoadDataRow(new object[] { "B", "2011A", "hi, hello!" }, false);
            expected.LoadDataRow(new object[] { "B", "2012A", "hi" }, false);
            expected.LoadDataRow(new object[] { "B", "2013A", null }, false);
            expected.LoadDataRow(new object[] { "B", "2014A", "hello!" }, false);

            string source = "DS_r := fill_time_series(DS_3, all)";

            List<Exception> errors;
            string sql = this.TranslateVtl(source, out errors);

            Assert.Empty(errors);

            DataTable ds_r = this.RunSql(sql, "#DS_r");

            Assert.True(expected.EqualsTo(ds_r));
        }

        //[Fact]
        //public void Example7()
        //{
        //    DataTable expected = new DataTable();
        //    expected.Columns.Add("Id_1");
        //    expected.Columns.Add("Id_2");
        //    expected.Columns.Add("Me_1");
        //    // expected.LoadDataRow(new object[] { "A", "2010Y", "hello world" }, false); - documentation example result error
        //    expected.LoadDataRow(new object[] { "A", "2010A", "hello world" }, false);
        //    // expected.LoadDataRow(new object[] { "A", "2011Y", null }, false); - documentation example result error
        //    expected.LoadDataRow(new object[] { "A", "2011A", null }, false);
        //    // expected.LoadDataRow(new object[] { "A", "2012Y", "say hello" }, false); - documentation example result error
        //    expected.LoadDataRow(new object[] { "A", "2012A", "say hello" }, false);
        //    expected.LoadDataRow(new object[] { "A", "2010Q1", "he" }, false);
        //    expected.LoadDataRow(new object[] { "A", "2010Q2", "hi, hello!" }, false);
        //    expected.LoadDataRow(new object[] { "A", "2010Q3", null }, false);
        //    expected.LoadDataRow(new object[] { "A", "2010Q4", "hi" }, false);
        //    expected.LoadDataRow(new object[] { "A", "2011Q2", "hello!" }, false);

        //    string source = "DS_r := fill_time_series(DS_4, single)";

        //    List<Exception> errors;
        //    string sql = this.TranslateVtl(source, out errors);

        //    Assert.Empty(errors);

        //    DataTable ds_r = this.RunSql(sql, "#DS_r");

        //    Assert.True(expected.EqualsTo(ds_r));
        //}

        //[Fact]
        //public void Example8()
        //{
        //    DataTable expected = new DataTable();
        //    expected.Columns.Add("Id_1");
        //    expected.Columns.Add("Id_2");
        //    expected.Columns.Add("Me_1");
        //    // expected.LoadDataRow(new object[] { "A", "2010Y", "hello world" }, false); - documentation example result error
        //    expected.LoadDataRow(new object[] { "A", "2010A", "hello world" }, false);
        //    // expected.LoadDataRow(new object[] { "A", "2011Y", null }, false); - documentation example result error
        //    expected.LoadDataRow(new object[] { "A", "2011A", null }, false);
        //    // expected.LoadDataRow(new object[] { "A", "2012Y", "say hello" }, false); - documentation example result error
        //    expected.LoadDataRow(new object[] { "A", "2012A", "say hello" }, false);
        //    expected.LoadDataRow(new object[] { "A", "2010Q1", "he" }, false);
        //    expected.LoadDataRow(new object[] { "A", "2010Q2", "hi, hello!" }, false);
        //    expected.LoadDataRow(new object[] { "A", "2010Q3", null }, false);
        //    expected.LoadDataRow(new object[] { "A", "2010Q4", "hi" }, false);
        //    expected.LoadDataRow(new object[] { "A", "2011Q1", null }, false);
        //    expected.LoadDataRow(new object[] { "A", "2011Q2", "hello!" }, false);
        //    expected.LoadDataRow(new object[] { "A", "2011Q3", null }, false);
        //    expected.LoadDataRow(new object[] { "A", "2011Q4", null }, false);
        //    expected.LoadDataRow(new object[] { "A", "2012Q1", null }, false);
        //    expected.LoadDataRow(new object[] { "A", "2012Q2", null }, false);
        //    expected.LoadDataRow(new object[] { "A", "2012Q3", null }, false);
        //    expected.LoadDataRow(new object[] { "A", "2012Q4", null }, false);

        //    string source = "DS_r := fill_time_series(DS_4, all)";

        //    List<Exception> errors;
        //    string sql = this.TranslateVtl(source, out errors);

        //    Assert.Empty(errors);

        //    DataTable ds_r = this.RunSql(sql, "#DS_r");

        //    Assert.True(expected.EqualsTo(ds_r));
        //}
    }
}
