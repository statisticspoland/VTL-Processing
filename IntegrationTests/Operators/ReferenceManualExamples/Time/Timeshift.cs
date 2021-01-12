namespace VtlProcessing.IntegrationTests.TSQL.Operators.ReferenceManualExamples.Time
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using Xunit;

    public class TimeShift : TSQLTestBase
    {
        public TimeShift() : base("Time_shift")
        {
            List<object[]> ds1 = new List<object[]>
            {
                new object[] { "A", "2010-01/2010-12", "hello world" },
                new object[] { "A", "2011-01/2011-12", null },
                new object[] { "A", "2012-01/2012-12", "say hello" },
                new object[] { "A", "2013-01/2013-12", "he" },
                new object[] { "B", "2010-01/2010-12", "hi, hello!" },
                new object[] { "B", "2011-01/2011-12", "hi" },
                new object[] { "B", "2012-01/2012-12", null },
                new object[] { "B", "2013-01/2013-12", "hello!" }
            };

            List<object[]> ds2 = new List<object[]>
            {
                new object[] { "A", "2010-12-31", "hello world" },
                new object[] { "A", "2011-12-31", null },
                new object[] { "A", "2012-12-31", "say hello" },
                new object[] { "A", "2013-12-31", "he" },
                new object[] { "B", "2010-12-31", "hi, hello!" },
                new object[] { "B", "2011-12-31", "hi" },
                new object[] { "B", "2012-12-31", null },
                new object[] { "B", "2013-12-31", "hello!" }
            };

            //List<object[]> ds3 = new List<object[]>
            //{
            //    new object[] { "A", "2010Y", "hello world" },
            //    new object[] { "A", "2011Y", null },
            //    new object[] { "A", "2012Y", "say hello" },
            //    new object[] { "A", "2013Y", "he" },
            //    new object[] { "B", "2010Y", "hi, hello!" },
            //    new object[] { "B", "2011Y", "hi" },
            //    new object[] { "B", "2012Y", null },
            //    new object[] { "B", "2013Y", "hello!" }
            //}; - documentation example input error
            List<object[]> ds3 = new List<object[]>
            {
                new object[] { "A", "2010A", "hello world" },
                new object[] { "A", "2011A", null },
                new object[] { "A", "2012A", "say hello" },
                new object[] { "A", "2013A", "he" },
                new object[] { "B", "2010A", "hi, hello!" },
                new object[] { "B", "2011A", "hi" },
                new object[] { "B", "2012A", null },
                new object[] { "B", "2013A", "hello!" }
            };

            List<object[]> ds4 = new List<object[]>
            {
                // new object[] { "A", "2010Y", "hello world" }, - documentation example input error
                new object[] { "A", "2010A", "hello world" },
                // new object[] { "A", "2011Y", null }, - documentation example input error
                new object[] { "A", "2011A", null },
                // new object[] { "A", "2012Y", "say hello" }, - documentation example input error
                new object[] { "A", "2012A", "say hello" },
                // new object[] { "A", "2013Y", "he" }, - documentation example input error
                new object[] { "A", "2013A", "he" },
                new object[] { "A", "2010Q1", "hi, hello!" },
                new object[] { "A", "2010Q2", "hi" },
                new object[] { "A", "2010Q3", null },
                new object[] { "A", "2010Q4", "hello!" }
            };

            this.SqlFillData("[Time_shift].DS_1", ds1);
            this.SqlFillData("[Time_shift].DS_2", ds2);
            this.SqlFillData("[Time_shift].DS_3", ds3);
            this.SqlFillData("[Time_shift].DS_4", ds4);
        }

        [Fact]
        public void Example1()
        {
            DataTable expected = new DataTable();
            expected.Columns.Add("Id_1");
            expected.Columns.Add("Id_2");
            expected.Columns.Add("Me_1");
            expected.LoadDataRow(new object[] { "A", "2009-01/2009-12", "hello world" }, false);
            expected.LoadDataRow(new object[] { "A", "2010-01/2010-12", null }, false);
            expected.LoadDataRow(new object[] { "A", "2011-01/2011-12", "say hello" }, false);
            expected.LoadDataRow(new object[] { "A", "2012-01/2012-12", "he" }, false);
            expected.LoadDataRow(new object[] { "B", "2009-01/2009-12", "hi, hello!" }, false);
            expected.LoadDataRow(new object[] { "B", "2010-01/2010-12", "hi" }, false);
            expected.LoadDataRow(new object[] { "B", "2011-01/2011-12", null }, false);
            expected.LoadDataRow(new object[] { "B", "2012-01/2012-12", "hello!" }, false);

            // string source = "DS_r := time_shift(DS_1, -1)"; - documentation example error
            string source = "DS_r := timeshift(DS_1, -1)";

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
            expected.LoadDataRow(new object[] { "A", "2012-12-31", "hello world" }, false);
            expected.LoadDataRow(new object[] { "A", "2013-12-31", null }, false);
            expected.LoadDataRow(new object[] { "A", "2014-12-31", "say hello" }, false);
            expected.LoadDataRow(new object[] { "A", "2015-12-31", "he" }, false);
            expected.LoadDataRow(new object[] { "B", "2012-12-31", "hi, hello!" }, false);
            expected.LoadDataRow(new object[] { "B", "2013-12-31", "hi" }, false);
            expected.LoadDataRow(new object[] { "B", "2014-12-31", null }, false);
            expected.LoadDataRow(new object[] { "B", "2015-12-31", "hello!" }, false);

            // string source = "DS_r := time_shift(DS_2, 2)"; - documentation example error
            string source = "DS_r := timeshift(DS_2, 2)";

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
            // expected.LoadDataRow(new object[] { "A", "2011Y", "hello world" }, false);
            // expected.LoadDataRow(new object[] { "A", "2012Y", null }, false);
            // expected.LoadDataRow(new object[] { "A", "2013Y", "say hello" }, false);
            // expected.LoadDataRow(new object[] { "A", "2014Y", "he" }, false);
            // expected.LoadDataRow(new object[] { "B", "2011Y", "hi, hello!" }, false);
            // expected.LoadDataRow(new object[] { "B", "2012Y", "hi" }, false);
            // expected.LoadDataRow(new object[] { "B", "2013Y", null }, false);
            // expected.LoadDataRow(new object[] { "B", "2014Y", "hello!" }, false);
            // documentation example result error
            expected.LoadDataRow(new object[] { "A", "2011A", "hello world" }, false);
            expected.LoadDataRow(new object[] { "A", "2012A", null }, false);
            expected.LoadDataRow(new object[] { "A", "2013A", "say hello" }, false);
            expected.LoadDataRow(new object[] { "A", "2014A", "he" }, false);
            expected.LoadDataRow(new object[] { "B", "2011A", "hi, hello!" }, false);
            expected.LoadDataRow(new object[] { "B", "2012A", "hi" }, false);
            expected.LoadDataRow(new object[] { "B", "2013A", null }, false);
            expected.LoadDataRow(new object[] { "B", "2014A", "hello!" }, false);

            // string source = "DS_r := time_shift(DS_3, 1)"; - documentation example error
            string source = "DS_r := timeshift(DS_3, 1)";

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
            // // expected.LoadDataRow(new object[] { "A", "2009Y", "hello world" }, false); - documentation example result error
            // expected.LoadDataRow(new object[] { "A", "2009A", "hello world" }, false);
            // // expected.LoadDataRow(new object[] { "A", "2010Y", null }, false); - documentation example result error
            // expected.LoadDataRow(new object[] { "A", "2010A", null }, false);
            // // expected.LoadDataRow(new object[] { "A", "2011Y", "say hello" }, false); - documentation example result error
            // expected.LoadDataRow(new object[] { "A", "2011A", "say hello" }, false);
            // // expected.LoadDataRow(new object[] { "A", "2012Y", "he" }, false); - documentation example result error
            // expected.LoadDataRow(new object[] { "A", "2012A", "he" }, false);
            // expected.LoadDataRow(new object[] { "A", "2009Q4", "hi, hello!" }, false);
            // expected.LoadDataRow(new object[] { "A", "2010Q1", "hi" }, false);
            // expected.LoadDataRow(new object[] { "A", "2010Q2", null }, false);
            // expected.LoadDataRow(new object[] { "A", "2010Q3", "hello!" }, false);
            // Sorted for test needs
            expected.LoadDataRow(new object[] { "A", "2009A", "hello world" }, false);
            expected.LoadDataRow(new object[] { "A", "2009Q4", "hi, hello!" }, false);
            expected.LoadDataRow(new object[] { "A", "2010Q1", "hi" }, false);
            expected.LoadDataRow(new object[] { "A", "2010Q2", null }, false);
            expected.LoadDataRow(new object[] { "A", "2010Q3", "hello!" }, false);
            expected.LoadDataRow(new object[] { "A", "2010A", null }, false);
            expected.LoadDataRow(new object[] { "A", "2011A", "say hello" }, false);
            expected.LoadDataRow(new object[] { "A", "2012A", "he" }, false);
            
            // string source = "DS_r := time_shift(DS_4, -1)"; - documentation example error
            string source = "DS_r := timeshift(DS_4, -1)";

            List<Exception> errors;
            string sql = this.TranslateVtl(source, out errors);

            Assert.Empty(errors);

            DataTable ds_r = this.RunSql(sql, "#DS_r");

            Assert.True(expected.EqualsTo(ds_r));
        }
    }
}
