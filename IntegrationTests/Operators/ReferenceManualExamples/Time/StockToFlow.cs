namespace VtlProcessing.IntegrationTests.TSQL.Operators.ReferenceManualExamples.Time
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using Xunit;

    public class StockToFlow : TSQLTestBase
    {
        public StockToFlow() : base("Stock_to_flow")
        {
            List<object[]> ds1 = new List<object[]>
            {
                new object[] { "A", "2010-01/2010-12", 2 },
                new object[] { "A", "2011-01/2011-12", 7 },
                new object[] { "A", "2012-01/2012-12", 4 },
                new object[] { "A", "2013-01/2013-12", 13 },
                new object[] { "B", "2010-01/2010-12", 4 },
                new object[] { "B", "2011-01/2011-12", -4 },
                new object[] { "B", "2012-01/2012-12", -4 },
                new object[] { "B", "2013-01/2013-12", 2 }
            };

            List<object[]> ds2 = new List<object[]>
            {
                new object[] { "A", "2010-12-31", 2 },
                new object[] { "A", "2011-12-31", 7 },
                new object[] { "A", "2012-12-31", 4 },
                new object[] { "A", "2013-12-31", 13 },
                new object[] { "B", "2010-12-31", 4 },
                new object[] { "B", "2011-12-31", -4 },
                new object[] { "B", "2012-12-31", -4 },
                new object[] { "B", "2013-12-31", 2 }
            };

            // List<object[]> ds3 = new List<object[]>
            // {
            //     new object[] { "A", "2010Y", 2 },
            //     new object[] { "A", "2011Y", 7 },
            //     new object[] { "A", "2012Y", 4 },
            //     new object[] { "A", "2013Y", 13 },
            //     new object[] { "B", "2010Y", 4 },
            //     new object[] { "B", "2011Y", -4 },
            //     new object[] { "B", "2012Y", -4 },
            //     new object[] { "B", "2013Y", 2 }
            // }; - documentation example input error
            List<object[]> ds3 = new List<object[]>
            {
                new object[] { "A", "2010A", 2 },
                new object[] { "A", "2011A", 7 },
                new object[] { "A", "2012A", 4 },
                new object[] { "A", "2013A", 13 },
                new object[] { "B", "2010A", 4 },
                new object[] { "B", "2011A", -4 },
                new object[] { "B", "2012A", -4 },
                new object[] { "B", "2013A", 2 }
            };

            // List<object[]> ds4 = new List<object[]>
            // {
            //     new object[] { "A", "2010Y", 2 },
            //     new object[] { "A", "2011Y", 9 },
            //     new object[] { "A", "2012Y", 13 },
            //     new object[] { "A", "2013Y", 26 },
            //     new object[] { "A", "2010Q1", 2 },
            //     new object[] { "A", "2010Q2", -1 },
            //     new object[] { "A", "2010Q3", 6 },
            //     new object[] { "A", "2010Q4", 2 }
            // }; - documentation example input error
            List<object[]> ds4 = new List<object[]>
            {
                new object[] { "A", "2010A", 2 },
                new object[] { "A", "2011A", 9 },
                new object[] { "A", "2012A", 13 },
                new object[] { "A", "2013A", 26 },
                new object[] { "B", "2010Q1", 2 },
                new object[] { "B", "2010Q2", -1 },
                new object[] { "B", "2010Q3", 6 },
                new object[] { "B", "2010Q4", 2 }
            };

            this.SqlFillData("[Stock_to_flow].DS_1", ds1);
            this.SqlFillData("[Stock_to_flow].DS_2", ds2);
            this.SqlFillData("[Stock_to_flow].DS_3", ds3);
            this.SqlFillData("[Stock_to_flow].DS_4", ds4);
        }

        [Fact]
        public void Example1()
        {
            DataTable expected = new DataTable();
            expected.Columns.Add("Id_1");
            expected.Columns.Add("Id_2");
            expected.Columns.Add("Me_1");
            expected.LoadDataRow(new object[] { "A", "2010-01/2010-12", 2 }, false);
            expected.LoadDataRow(new object[] { "A", "2011-01/2011-12", 5 }, false);
            expected.LoadDataRow(new object[] { "A", "2012-01/2012-12", -3 }, false);
            expected.LoadDataRow(new object[] { "A", "2013-01/2013-12", 9 }, false);
            expected.LoadDataRow(new object[] { "B", "2010-01/2010-12", 4 }, false);
            expected.LoadDataRow(new object[] { "B", "2011-01/2011-12", -8 }, false);
            expected.LoadDataRow(new object[] { "B", "2012-01/2012-12", 0 }, false);
            expected.LoadDataRow(new object[] { "B", "2013-01/2013-12", 6 }, false);

            string source = "DS_r := stock_to_flow(DS_1)";

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
            expected.LoadDataRow(new object[] { "A", "2010-12-31", 2 }, false);
            expected.LoadDataRow(new object[] { "A", "2011-12-31", 5 }, false);
            expected.LoadDataRow(new object[] { "A", "2012-12-31", -3 }, false);
            expected.LoadDataRow(new object[] { "A", "2013-12-31", 9 }, false);
            expected.LoadDataRow(new object[] { "B", "2010-12-31", 4 }, false);
            expected.LoadDataRow(new object[] { "B", "2011-12-31", -8 }, false);
            expected.LoadDataRow(new object[] { "B", "2012-12-31", -0 }, false);
            expected.LoadDataRow(new object[] { "B", "2013-12-31", 6 }, false);

            string source = "DS_r := stock_to_flow(DS_2)";

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
            // expected.LoadDataRow(new object[] { "A", "2010Y", 2 }, false);
            // expected.LoadDataRow(new object[] { "A", "2011Y", 5 }, false);
            // expected.LoadDataRow(new object[] { "A", "2012Y", -3 }, false);
            // expected.LoadDataRow(new object[] { "A", "2013Y", 9 }, false);
            // expected.LoadDataRow(new object[] { "B", "2010Y", 4 }, false);
            // expected.LoadDataRow(new object[] { "B", "2011Y", -8 }, false);
            // expected.LoadDataRow(new object[] { "B", "2012Y", -0 }, false);
            // expected.LoadDataRow(new object[] { "B", "2013Y", 6 }, false);
            // documentation example result error
            expected.LoadDataRow(new object[] { "A", "2010A", 2 }, false);
            expected.LoadDataRow(new object[] { "A", "2011A", 5 }, false);
            expected.LoadDataRow(new object[] { "A", "2012A", -3 }, false);
            expected.LoadDataRow(new object[] { "A", "2013A", 9 }, false);
            expected.LoadDataRow(new object[] { "B", "2010A", 4 }, false);
            expected.LoadDataRow(new object[] { "B", "2011A", -8 }, false);
            expected.LoadDataRow(new object[] { "B", "2012A", -0 }, false);
            expected.LoadDataRow(new object[] { "B", "2013A", 6 }, false);

            string source = "DS_r := stock_to_flow(DS_3)";

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
            // expected.LoadDataRow(new object[] { "A", "2010Y", 2 }, false);
            // expected.LoadDataRow(new object[] { "A", "2011Y", 7 }, false);
            // expected.LoadDataRow(new object[] { "A", "2012Y", 4 }, false);
            // expected.LoadDataRow(new object[] { "A", "2013Y", 13 }, false);
            // expected.LoadDataRow(new object[] { "A", "2010Q1", 2 }, false);
            // expected.LoadDataRow(new object[] { "A", "2010Q2", -3 }, false);
            // expected.LoadDataRow(new object[] { "A", "2010Q3", 7 }, false);
            // expected.LoadDataRow(new object[] { "A", "2010Q4", -4 }, false);
            // documentation example result error
            expected.LoadDataRow(new object[] { "A", "2010A", 2 }, false);
            expected.LoadDataRow(new object[] { "A", "2011A", 7 }, false);
            expected.LoadDataRow(new object[] { "A", "2012A", 4 }, false);
            expected.LoadDataRow(new object[] { "A", "2013A", 13 }, false);
            expected.LoadDataRow(new object[] { "B", "2010Q1", 2 }, false);
            expected.LoadDataRow(new object[] { "B", "2010Q2", -3 }, false);
            expected.LoadDataRow(new object[] { "B", "2010Q3", 7 }, false);
            expected.LoadDataRow(new object[] { "B", "2010Q4", -4 }, false);

            string source = "DS_r := stock_to_flow(DS_4)";

            List<Exception> errors;
            string sql = this.TranslateVtl(source, out errors);

            Assert.Empty(errors);

            DataTable ds_r = this.RunSql(sql, "#DS_r");

            Assert.True(expected.EqualsTo(ds_r));
        }
    }
}
