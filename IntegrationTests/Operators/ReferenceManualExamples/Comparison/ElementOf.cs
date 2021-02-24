namespace VtlProcessing.IntegrationTests.TSQL.Operators.ReferenceManualExamples.Comparison
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using Xunit;

    public class ElementOf : TSQLTestBase
    {
        public ElementOf() : base("Element_of")
        {
            List<object[]> ds1 = new List<object[]>
            {
                new object[] { 2012, "BS", 0 },
                new object[] { 2012, "GZ", 4 },
                new object[] { 2012, "SQ", 9 },
                new object[] { 2012, "MO", 6 },
                new object[] { 2012, "FJ", 7 },
                new object[] { 2012, "CQ", 2 }
            };

            this.SqlFillData("[Element_of].DS_1", ds1);
        }

        [Fact]
        public void Example1()
        {
            DataTable expected = new DataTable();
            expected.Columns.Add("Id_1");
            expected.Columns.Add("Id_2");
            expected.Columns.Add("bool_var");
            // expected.LoadDataRow(new object[] { 2012, "BS", true }, false);
            // expected.LoadDataRow(new object[] { 2012, "GZ", false }, false);
            // expected.LoadDataRow(new object[] { 2012, "SQ", false }, false);
            // expected.LoadDataRow(new object[] { 2012, "MO", true }, false);
            // expected.LoadDataRow(new object[] { 2012, "FJ", false }, false);
            // expected.LoadDataRow(new object[] { 2012, "CQ", false }, false);
            // Sorted and changed boolean values to bits for test needs
            expected.LoadDataRow(new object[] { 2012, "BS", 1 }, false);
            expected.LoadDataRow(new object[] { 2012, "CQ", 0 }, false);
            expected.LoadDataRow(new object[] { 2012, "FJ", 0 }, false);
            expected.LoadDataRow(new object[] { 2012, "GZ", 0 }, false);
            expected.LoadDataRow(new object[] { 2012, "MO", 1 }, false);
            expected.LoadDataRow(new object[] { 2012, "SQ", 0 }, false);
            
            // string source = "DS_r := DS_1 in { \"BS\", \"MO\", \"HH\", \"PP\" }"; - documentation example error
            string source = "DS_r := DS_1 in { 0, 1, 3, 6 }"; // new invented expression

            List<Exception> errors;
            string sql = this.TranslateVtl(source, out errors);

            Assert.Empty(errors);

            DataTable ds_r = this.RunSql(sql, "#DS_r");

            Assert.True(expected.EqualsTo(ds_r));
        }

        [Fact]
        public void Example2()
        {
            // TODO: Calc component
            DataTable expected = new DataTable();
            expected.Columns.Add("Id_1");
            expected.Columns.Add("Id_2");
            expected.Columns.Add("Me_1");
            expected.Columns.Add("Me_2");
            // expected.LoadDataRow(new object[] { 2012, "BS", 0, true }, false);
            // expected.LoadDataRow(new object[] { 2012, "GZ", 4, false }, false);
            // expected.LoadDataRow(new object[] { 2012, "SQ", 9, false }, false);
            // expected.LoadDataRow(new object[] { 2012, "MO", 6, true }, false);
            // expected.LoadDataRow(new object[] { 2012, "FJ", 7, false }, false);
            // expected.LoadDataRow(new object[] { 2012, "CQ", 2, false }, false);
            // Sorted and changed boolean values to bits for test needs
            expected.LoadDataRow(new object[] { 2012, "BS", 0, 1 }, false);
            expected.LoadDataRow(new object[] { 2012, "CQ", 2, 0 }, false);
            expected.LoadDataRow(new object[] { 2012, "FJ", 7, 0 }, false);
            expected.LoadDataRow(new object[] { 2012, "GZ", 4, 0 }, false);
            expected.LoadDataRow(new object[] { 2012, "MO", 6, 1 }, false);
            expected.LoadDataRow(new object[] { 2012, "SQ", 9, 0 }, false);

            // string source = "DS_r := DS_1[calc Me_2 := Me_1 in { \"BS\", \"MO\", \"HH\", \"PP\" }]"; - documentation example error
            string source = "DS_r := DS_1[calc Me_2 := Me_1 in { 0, 1, 3, 6 }]"; // new invented expression

            List<Exception> errors;
            string sql = this.TranslateVtl(source, out errors);

            Assert.Empty(errors);

            DataTable ds_r = this.RunSql(sql, "#DS_r");

            Assert.True(expected.EqualsTo(ds_r));
        }

        //[Fact]
        //public void Example3()
        //{
        //    // TODO: Value domain
        //    DataTable expected = new DataTable();
        //    expected.Columns.Add("Id_1");
        //    expected.Columns.Add("Id_2");
        //    expected.Columns.Add("bool_var");
        //    // expected.LoadDataRow(new object[] { 2012, "BS", true }, false);
        //    // expected.LoadDataRow(new object[] { 2012, "GZ", false }, false);
        //    // expected.LoadDataRow(new object[] { 2012, "SQ", false }, false);
        //    // expected.LoadDataRow(new object[] { 2012, "MO", true }, false);
        //    // expected.LoadDataRow(new object[] { 2012, "FJ", true }, false);
        //    // expected.LoadDataRow(new object[] { 2012, "CQ", false }, false);
        //    // Sorted and changed boolean values to bits for test needs
        //    expected.LoadDataRow(new object[] { 2012, "BS", 1 }, false);
        //    expected.LoadDataRow(new object[] { 2012, "CQ", 0 }, false);
        //    expected.LoadDataRow(new object[] { 2012, "FJ", 0 }, false);
        //    expected.LoadDataRow(new object[] { 2012, "GZ", 1 }, false);
        //    expected.LoadDataRow(new object[] { 2012, "MO", 1 }, false);
        //    expected.LoadDataRow(new object[] { 2012, "SQ", 0 }, false);

        //    string source = "DS_r := DS_1#Id_2 in myGeoValueDomain";

        //    throw new NotImplementedException(source);

        //    // List<Exception> errors;
        //    // string sql = this.TranslateVtl(source, out errors);
               
        //    // Assert.Empty(errors);
               
        //    // DataTable ds_r = this.RunSql(sql, "#DS_r", this.beforeScript);
               
        //    // Assert.True(expected.EqualsTo(ds_r));
        //}

        [Fact]
        public void ScalarExample1()
        {
            bool expected = true;
            string source = "var := 1 in { 1, 2, 3 }";

            List<Exception> errors;
            string sql = this.TranslateVtl(source, out errors);

            Assert.Empty(errors);

            DataTable var = this.RunSql(sql, "@var");

            Assert.Single(var.Columns);
            Assert.Single(var.Rows);
            Assert.Equal(expected, bool.Parse(var.Rows[0].Field<string>("value")));
        }

        [Fact]
        public void ScalarExample2()
        {
            bool expected = false;
            string source = "var := \"a\" in { \"c\", \"ab\", \"bb\", \"bc\" }";

            List<Exception> errors;
            string sql = this.TranslateVtl(source, out errors);

            Assert.Empty(errors);

            DataTable var = this.RunSql(sql, "@var");

            Assert.Single(var.Columns);
            Assert.Single(var.Rows);
            Assert.Equal(expected, bool.Parse(var.Rows[0].Field<string>("value")));
        }

        [Fact]
        public void ScalarExample3()
        {
            bool expected = false;
            string source = "var := \"b\" not_in { \"b\", \"hello\", \"c\" }";

            List<Exception> errors;
            string sql = this.TranslateVtl(source, out errors);

            Assert.Empty(errors);

            DataTable var = this.RunSql(sql, "@var");

            Assert.Single(var.Columns);
            Assert.Single(var.Rows);
            Assert.Equal(expected, bool.Parse(var.Rows[0].Field<string>("value")));
        }

        [Fact]
        public void ScalarExample4()
        {
            bool expected = true;
            string source = "var := \"b\" not_in { \"a\", \"hello\", \"c\" }";

            List<Exception> errors;
            string sql = this.TranslateVtl(source, out errors);

            Assert.Empty(errors);

            DataTable var = this.RunSql(sql, "@var");

            Assert.Single(var.Columns);
            Assert.Single(var.Rows);
            Assert.Equal(expected, bool.Parse(var.Rows[0].Field<string>("value")));
        }
    }
}
