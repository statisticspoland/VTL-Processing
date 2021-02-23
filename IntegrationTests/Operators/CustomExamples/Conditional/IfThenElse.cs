namespace VtlProcessing.IntegrationTests.TSQL.Operators.CustomExamples.Conditional
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using Xunit;

    public class IfThenElse : TSQLTestBase
    {
        public IfThenElse() : base("Custom_If_then_else")
        {
            List<object[]> ds1 = new List<object[]>
            {
                new object[] { 1, "A", "Custom", 0 },
                new object[] { 1, "B", "Custom", 0 },
                new object[] { 2, "A", "Custom", 0 },
                new object[] { 2, "B", "Custom", 1 },
                new object[] { 3, "A", "Custom", 1 },
                new object[] { 3, "B", "Custom", 0 },
                new object[] { 4, "A", "Custom", 1 },
                new object[] { 4, "B", "Custom", 1 }
            }; // Last values are boolean

            List<object[]> ds2 = new List<object[]>
            {
                new object[] { 1, "A", "Custom", 2, 3, "First" },
                new object[] { 1, "B", "Custom", 4, 5, "Second" },
                new object[] { 2, "A", "Custom", 6, 7, "Third" },
                new object[] { 2, "B", "Custom", 8, 9, "Fourth" },
                new object[] { 3, "A", "Custom", 10, 11, "Fifth" },
                new object[] { 3, "B", "Custom", 12, 13, "Sixth" },
                new object[] { 4, "A", "Custom", 14, 15, "Seventh" },
                new object[] { 4, "B", "Custom", 16, 17, "Eighth" }
            };

            List<object[]> ds3 = new List<object[]>
            {
                new object[] { 1, "A", "Custom", 2, "DS_3", "First" },
                new object[] { 1, "B", "Custom", 4, "DS_3", "Second" },
                new object[] { 2, "A", "Custom", 6, "DS_3", "Third" },
                new object[] { 2, "B", "Custom", 8, "DS_3", "Fourth" },
                new object[] { 3, "A", "Custom", 10, "DS_3", "Fifth" },
                new object[] { 3, "B", "Custom", 12, "DS_3", "Sixth" },
                new object[] { 4, "A", "Custom", 14, "DS_3", "Seventh" },
                new object[] { 4, "B", "Custom", 16, "DS_3", "Eighth" }
            };

            List<object[]> ds4 = new List<object[]>
            {
                new object[] { 1, "A", "Custom", 3, "DS_4", "1st" },
                new object[] { 1, "B", "Custom", 5, "DS_4", "2nd" },
                new object[] { 2, "A", "Custom", 7, "DS_4", "3rd" },
                new object[] { 2, "B", "Custom", 9, "DS_4", "4th" },
                new object[] { 3, "A", "Custom", 11, "DS_4", "5th" },
                new object[] { 3, "B", "Custom", 13, "DS_4", "6th" },
                new object[] { 4, "A", "Custom", 15, "DS_4", "7th" },
                new object[] { 4, "B", "Custom", 17, "DS_4", "8th" }
            };

            this.SqlFillData("[Custom_If_then_else].DS_1", ds1);
            this.SqlFillData("[Custom_If_then_else].DS_2", ds2);
            this.SqlFillData("[Custom_If_then_else].DS_3", ds3);
            this.SqlFillData("[Custom_If_then_else].DS_4", ds4);
        }

        [Theory]
        [InlineData("Standard")]
        [InlineData("JoinApply")]
        public void ThreeDatasets(string type)
        {
            DataTable expected = new DataTable();
            expected.Columns.Add("Id_1");
            expected.Columns.Add("Id_2");
            expected.Columns.Add("Id_3");
            expected.Columns.Add("Me_1");
            expected.Columns.Add("Me_2");
            expected.Columns.Add("At_1");
            expected.LoadDataRow(new object[] { 1, "A", "Custom", 3, "DS_4", "1st" }, false);
            expected.LoadDataRow(new object[] { 1, "B", "Custom", 5, "DS_4", "2nd" }, false);
            expected.LoadDataRow(new object[] { 2, "A", "Custom", 7, "DS_4", "3rd" }, false);
            expected.LoadDataRow(new object[] { 2, "B", "Custom", 8, "DS_3", "4th" }, false);
            expected.LoadDataRow(new object[] { 3, "A", "Custom", 10, "DS_3", "5th" }, false);
            expected.LoadDataRow(new object[] { 3, "B", "Custom", 13, "DS_4", "6th" }, false);
            expected.LoadDataRow(new object[] { 4, "A", "Custom", 14, "DS_3", "7th" }, false);
            expected.LoadDataRow(new object[] { 4, "B", "Custom", 16, "DS_3", "8th" }, false);

            string source = "DS_r := if DS_1 then DS_3 else DS_4";
            if (type == "JoinApply") source = "DS_r := inner_join(DS_1 as ds1, DS_3 as ds3, DS_4 as ds4 apply if ds1 then ds3 else ds4)";

            List<Exception> errors;
            string sql = this.TranslateVtl(source, out errors);

            Assert.Empty(errors);

            DataTable ds_r = this.RunSql(sql, "#DS_r");

            Assert.True(expected.EqualsTo(ds_r));
        }

        [Theory]
        [InlineData("Standard")]
        [InlineData("JoinApply")]
        public void TwoDatasetsScalar(string type)
        {
            DataTable expected = new DataTable();
            expected.Columns.Add("Id_1");
            expected.Columns.Add("Id_2");
            expected.Columns.Add("Id_3");
            expected.Columns.Add("Me_1");
            expected.Columns.Add("Me_2");
            expected.Columns.Add("At_1");
            expected.LoadDataRow(new object[] { 1, "A", "Custom", 24, 24, "First" }, false);
            expected.LoadDataRow(new object[] { 1, "B", "Custom", 24, 24, "Second" }, false);
            expected.LoadDataRow(new object[] { 2, "A", "Custom", 24, 24, "Third" }, false);
            expected.LoadDataRow(new object[] { 2, "B", "Custom", 8, 9, "Fourth" }, false);
            expected.LoadDataRow(new object[] { 3, "A", "Custom", 10, 11, "Fifth" }, false);
            expected.LoadDataRow(new object[] { 3, "B", "Custom", 24, 24, "Sixth" }, false);
            expected.LoadDataRow(new object[] { 4, "A", "Custom", 14, 15, "Seventh" }, false);
            expected.LoadDataRow(new object[] { 4, "B", "Custom", 16, 17, "Eighth" }, false);

            string source = "DS_r := if DS_1 then DS_2 else 24";
            if (type == "JoinApply") source = "DS_r := inner_join(DS_1 as ds1, DS_2 as ds2 apply if ds1 then ds2 else 24)";

            List<Exception> errors;
            string sql = this.TranslateVtl(source, out errors);

            Assert.Empty(errors);

            DataTable ds_r = this.RunSql(sql, "#DS_r");

            Assert.True(expected.EqualsTo(ds_r));
        }

        [Theory]
        [InlineData("Standard")]
        [InlineData("JoinApply")]
        public void DatasetScalarDataset(string type)
        {
            DataTable expected = new DataTable();
            expected.Columns.Add("Id_1");
            expected.Columns.Add("Id_2");
            expected.Columns.Add("Id_3");
            expected.Columns.Add("Me_1");
            expected.LoadDataRow(new object[] { 1, "A", "Custom", 0 }, false);
            expected.LoadDataRow(new object[] { 1, "B", "Custom", 0 }, false);
            expected.LoadDataRow(new object[] { 2, "A", "Custom", 0 }, false);
            expected.LoadDataRow(new object[] { 2, "B", "Custom", 0 }, false);
            expected.LoadDataRow(new object[] { 3, "A", "Custom", 0 }, false);
            expected.LoadDataRow(new object[] { 3, "B", "Custom", 0 }, false);
            expected.LoadDataRow(new object[] { 4, "A", "Custom", 0 }, false);
            expected.LoadDataRow(new object[] { 4, "B", "Custom", 0 }, false);
            // Last values are boolean

            string source = "DS_r := if DS_1 then false else DS_1";
            if (type == "JoinApply") source = "DS_r := inner_join(DS_1 as ds1 apply if ds1 then false else ds1)";

            List<Exception> errors;
            string sql = this.TranslateVtl(source, out errors);

            Assert.Empty(errors);

            DataTable ds_r = this.RunSql(sql, "#DS_r");

            Assert.True(expected.EqualsTo(ds_r));
        }

        [Theory]
        [InlineData("Standard")]
        [InlineData("JoinApply")]
        public void DatasetTwoScalars(string type)
        {
            DataTable expected = new DataTable();
            expected.Columns.Add("Id_1");
            expected.Columns.Add("Id_2");
            expected.Columns.Add("Id_3");
            expected.Columns.Add("Me_1");
            expected.LoadDataRow(new object[] { 1, "A", "Custom", 1 }, false);
            expected.LoadDataRow(new object[] { 1, "B", "Custom", 1 }, false);
            expected.LoadDataRow(new object[] { 2, "A", "Custom", 1 }, false);
            expected.LoadDataRow(new object[] { 2, "B", "Custom", 0 }, false);
            expected.LoadDataRow(new object[] { 3, "A", "Custom", 0 }, false);
            expected.LoadDataRow(new object[] { 3, "B", "Custom", 1 }, false);
            expected.LoadDataRow(new object[] { 4, "A", "Custom", 0 }, false);
            expected.LoadDataRow(new object[] { 4, "B", "Custom", 0 }, false);
            // Last values are boolean

            string source = "DS_r := if DS_1 then false else true";
            if (type == "JoinApply") source = "DS_r := inner_join(DS_1 as ds1 apply if ds1 then false else true)";

            List<Exception> errors;
            string sql = this.TranslateVtl(source, out errors);

            Assert.Empty(errors);

            DataTable ds_r = this.RunSql(sql, "#DS_r");

            Assert.True(expected.EqualsTo(ds_r));
        }

        [Fact]
        public void JoinApplyThreeComponents()
        {
            DataTable expected = new DataTable();
            expected.Columns.Add("Id_1");
            expected.Columns.Add("Id_2");
            expected.Columns.Add("Id_3");
            expected.Columns.Add("Me_1");
            expected.Columns.Add("Me_2");
            expected.Columns.Add("At_1");
            expected.LoadDataRow(new object[] { 1, "A", "Custom", 2, "DS_3", "1st" }, false);
            expected.LoadDataRow(new object[] { 1, "B", "Custom", 4, "DS_3", "2nd" }, false);
            expected.LoadDataRow(new object[] { 2, "A", "Custom", 6, "DS_3", "3rd" }, false);
            expected.LoadDataRow(new object[] { 2, "B", "Custom", 8, "DS_3", "4th" }, false);
            expected.LoadDataRow(new object[] { 3, "A", "Custom", 10, "DS_3", "5th" }, false);
            expected.LoadDataRow(new object[] { 3, "B", "Custom", 12, "DS_3", "6th" }, false);
            expected.LoadDataRow(new object[] { 4, "A", "Custom", 14, "DS_3", "7th" }, false);
            expected.LoadDataRow(new object[] { 4, "B", "Custom", 16, "DS_3", "8th" }, false);

            string source = "DS_r := inner_join(DS_4 as ds4, DS_2 as ds2, DS_3 as ds3 apply if isnull(ds2) then ds4 else ds3)";

            List<Exception> errors;
            string sql = this.TranslateVtl(source, out errors);

            Assert.Empty(errors);

            DataTable ds_r = this.RunSql(sql, "#DS_r");

            Assert.True(expected.EqualsTo(ds_r));
        }

        [Theory]
        [InlineData("Standard")]
        [InlineData("JoinApply")]
        public void TwoScalarsDataset(string type)
        {
            DataTable expected = new DataTable();
            expected.Columns.Add("Id_1");
            expected.Columns.Add("Id_2");
            expected.Columns.Add("Id_3");
            expected.Columns.Add("Me_1");
            expected.Columns.Add("Me_2");
            expected.Columns.Add("At_1");
            expected.LoadDataRow(new object[] { 1, "A", "Custom", 2, 3, "First" }, false);
            expected.LoadDataRow(new object[] { 1, "B", "Custom", 4, 5, "Second" }, false);
            expected.LoadDataRow(new object[] { 2, "A", "Custom", 6, 7, "Third" }, false);
            expected.LoadDataRow(new object[] { 2, "B", "Custom", 8, 9, "Fourth" }, false);
            expected.LoadDataRow(new object[] { 3, "A", "Custom", 10, 11, "Fifth" }, false);
            expected.LoadDataRow(new object[] { 3, "B", "Custom", 12, 13, "Sixth" }, false);
            expected.LoadDataRow(new object[] { 4, "A", "Custom", 14, 15, "Seventh" }, false);
            expected.LoadDataRow(new object[] { 4, "B", "Custom", 16, 17, "Eighth" }, false);

            string source = "DS_r := if 10 = 4 then 4 else DS_2";
            if (type == "JoinApply") source = "DS_r := inner_join(DS_2 as ds2 apply if 10 = 4 then 4 else ds2)";

            List<Exception> errors;
            string sql = this.TranslateVtl(source, out errors);

            Assert.Empty(errors);

            DataTable ds_r = this.RunSql(sql, "#DS_r");

            Assert.True(expected.EqualsTo(ds_r));
        }

        [Theory]
        [InlineData("Standard")]
        [InlineData("JoinApply")]
        public void ScalarDatasetScalar(string type)
        {
            DataTable expected = new DataTable();
            expected.Columns.Add("Id_1");
            expected.Columns.Add("Id_2");
            expected.Columns.Add("Id_3");
            expected.Columns.Add("Me_1");
            expected.Columns.Add("Me_2");
            expected.Columns.Add("At_1");
            expected.LoadDataRow(new object[] { 1, "A", "Custom", 15, 15, "First" }, false);
            expected.LoadDataRow(new object[] { 1, "B", "Custom", 15, 15, "Second" }, false);
            expected.LoadDataRow(new object[] { 2, "A", "Custom", 15, 15, "Third" }, false);
            expected.LoadDataRow(new object[] { 2, "B", "Custom", 15, 15, "Fourth" }, false);
            expected.LoadDataRow(new object[] { 3, "A", "Custom", 15, 15, "Fifth" }, false);
            expected.LoadDataRow(new object[] { 3, "B", "Custom", 15, 15, "Sixth" }, false);
            expected.LoadDataRow(new object[] { 4, "A", "Custom", 15, 15, "Seventh" }, false);
            expected.LoadDataRow(new object[] { 4, "B", "Custom", 15, 15, "Eighth" }, false);

            string source = "DS_r := if 5 < 4 then DS_2 else 15";
            if (type == "JoinApply") source = "DS_r := inner_join(DS_2 as ds2 apply if 5 < 4 then ds2 else 15)";

            List<Exception> errors;
            string sql = this.TranslateVtl(source, out errors);

            Assert.Empty(errors);

            DataTable ds_r = this.RunSql(sql, "#DS_r");

            Assert.True(expected.EqualsTo(ds_r));
        }

        [Theory]
        [InlineData("Standard")]
        [InlineData("JoinApply")]
        public void ScalarTwoDatasets(string type)
        {
            DataTable expected = new DataTable();
            expected.Columns.Add("Id_1");
            expected.Columns.Add("Id_2");
            expected.Columns.Add("Id_3");
            expected.Columns.Add("Me_1");
            expected.Columns.Add("Me_2");
            expected.Columns.Add("At_1");
            expected.LoadDataRow(new object[] { 1, "A", "Custom", 2, "DS_3", "1st" }, false);
            expected.LoadDataRow(new object[] { 1, "B", "Custom", 4, "DS_3", "2nd" }, false);
            expected.LoadDataRow(new object[] { 2, "A", "Custom", 6, "DS_3", "3rd" }, false);
            expected.LoadDataRow(new object[] { 2, "B", "Custom", 8, "DS_3", "4th" }, false);
            expected.LoadDataRow(new object[] { 3, "A", "Custom", 10, "DS_3", "5th" }, false);
            expected.LoadDataRow(new object[] { 3, "B", "Custom", 12, "DS_3", "6th" }, false);
            expected.LoadDataRow(new object[] { 4, "A", "Custom", 14, "DS_3", "7th" }, false);
            expected.LoadDataRow(new object[] { 4, "B", "Custom", 16, "DS_3", "8th" }, false);

            string source = "DS_r := if true then DS_3 else DS_4";
            if (type == "JoinApply") source = "DS_r := inner_join(DS_3 as ds3, DS_4 as ds4 apply if true then ds3 else ds4)";

            List<Exception> errors;
            string sql = this.TranslateVtl(source, out errors);

            Assert.Empty(errors);

            DataTable ds_r = this.RunSql(sql, "#DS_r");

            Assert.True(expected.EqualsTo(ds_r));
        }

        [Fact]
        public void JoinCalcThreeComponents()
        {
            DataTable expected = new DataTable();
            expected.Columns.Add("Id_1");
            expected.Columns.Add("Id_2");
            expected.Columns.Add("Id_3");
            expected.Columns.Add("Me_1");
            expected.Columns.Add("Me_2");
            expected.Columns.Add("Me_3");
            expected.Columns.Add("At_1");
            expected.LoadDataRow(new object[] { 1, "A", "Custom", 2, "DS_4", 3, "1st" }, false);
            expected.LoadDataRow(new object[] { 1, "B", "Custom", 4, "DS_4", 5, "2nd" }, false);
            expected.LoadDataRow(new object[] { 2, "A", "Custom", 6, "DS_4", 7, "3rd" }, false);
            expected.LoadDataRow(new object[] { 2, "B", "Custom", 8, "DS_4", 9, "4th" }, false);
            expected.LoadDataRow(new object[] { 3, "A", "Custom", 10, "DS_4", 11, "5th" }, false);
            expected.LoadDataRow(new object[] { 3, "B", "Custom", 12, "DS_4", 12, "6th" }, false);
            expected.LoadDataRow(new object[] { 4, "A", "Custom", 14, "DS_4", 14, "7th" }, false);
            expected.LoadDataRow(new object[] { 4, "B", "Custom", 16, "DS_4", 16, "8th" }, false);

            string source = "DS_r := inner_join(DS_3 as ds3, DS_4 as ds4 calc Me_3 := if ds3#Me_1 > 10 then ds3#Me_1 else ds4#Me_1 keep ds3#Me_1, ds4#Me_2, At_1)";

            List<Exception> errors;
            string sql = this.TranslateVtl(source, out errors);

            Assert.Empty(errors);

            DataTable ds_r = this.RunSql(sql, "#DS_r");

            Assert.True(expected.EqualsTo(ds_r));
        }

        [Fact]
        public void JoinCalcTwoComponentsScalar()
        {
            DataTable expected = new DataTable();
            expected.Columns.Add("Id_1");
            expected.Columns.Add("Id_2");
            expected.Columns.Add("Id_3");
            expected.Columns.Add("Me_1");
            expected.Columns.Add("Me_2");
            expected.Columns.Add("Me_3");
            expected.Columns.Add("At_1");
            expected.LoadDataRow(new object[] { 1, "A", "Custom", 2, "DS_4", 200, "1st" }, false);
            expected.LoadDataRow(new object[] { 1, "B", "Custom", 4, "DS_4", 200, "2nd" }, false);
            expected.LoadDataRow(new object[] { 2, "A", "Custom", 6, "DS_4", 200, "3rd" }, false);
            expected.LoadDataRow(new object[] { 2, "B", "Custom", 8, "DS_4", 200, "4th" }, false);
            expected.LoadDataRow(new object[] { 3, "A", "Custom", 10, "DS_4", 200, "5th" }, false);
            expected.LoadDataRow(new object[] { 3, "B", "Custom", 12, "DS_4", 12, "6th" }, false);
            expected.LoadDataRow(new object[] { 4, "A", "Custom", 14, "DS_4", 14, "7th" }, false);
            expected.LoadDataRow(new object[] { 4, "B", "Custom", 16, "DS_4", 16, "8th" }, false);

            string source = "DS_r := inner_join(DS_3 as ds3, DS_4 as ds4 calc Me_3 := if ds3#Me_1 > 10 then ds3#Me_1 else 200 keep ds3#Me_1, ds4#Me_2, At_1)";

            List<Exception> errors;
            string sql = this.TranslateVtl(source, out errors);

            Assert.Empty(errors);

            DataTable ds_r = this.RunSql(sql, "#DS_r");

            Assert.True(expected.EqualsTo(ds_r));
        }

        [Fact]
        public void JoinCalcComponentScalarComponent()
        {
            DataTable expected = new DataTable();
            expected.Columns.Add("Id_1");
            expected.Columns.Add("Id_2");
            expected.Columns.Add("Id_3");
            expected.Columns.Add("Me_1");
            expected.Columns.Add("Me_2");
            expected.Columns.Add("Me_3");
            expected.Columns.Add("At_1");
            expected.LoadDataRow(new object[] { 1, "A", "Custom", 2, "DS_4", 3, "1st" }, false);
            expected.LoadDataRow(new object[] { 1, "B", "Custom", 4, "DS_4", 5, "2nd" }, false);
            expected.LoadDataRow(new object[] { 2, "A", "Custom", 6, "DS_4", 7, "3rd" }, false);
            expected.LoadDataRow(new object[] { 2, "B", "Custom", 8, "DS_4", 9, "4th" }, false);
            expected.LoadDataRow(new object[] { 3, "A", "Custom", 10, "DS_4", 11, "5th" }, false);
            expected.LoadDataRow(new object[] { 3, "B", "Custom", 12, "DS_4", 100, "6th" }, false);
            expected.LoadDataRow(new object[] { 4, "A", "Custom", 14, "DS_4", 100, "7th" }, false);
            expected.LoadDataRow(new object[] { 4, "B", "Custom", 16, "DS_4", 100, "8th" }, false);

            string source = "DS_r := inner_join(DS_3 as ds3, DS_4 as ds4 calc Me_3 := if ds3#Me_1 > 10 then 100 else ds4#Me_1 keep ds3#Me_1, ds4#Me_2, At_1)";

            List<Exception> errors;
            string sql = this.TranslateVtl(source, out errors);

            Assert.Empty(errors);

            DataTable ds_r = this.RunSql(sql, "#DS_r");

            Assert.True(expected.EqualsTo(ds_r));
        }

        [Fact]
        public void JoinCalcComponentTwoScalars()
        {
            DataTable expected = new DataTable();
            expected.Columns.Add("Id_1");
            expected.Columns.Add("Id_2");
            expected.Columns.Add("Id_3");
            expected.Columns.Add("Me_1");
            expected.Columns.Add("Me_2");
            expected.LoadDataRow(new object[] { 1, "A", "Custom", false, 2 }, false);
            expected.LoadDataRow(new object[] { 1, "B", "Custom", false, 2 }, false);
            expected.LoadDataRow(new object[] { 2, "A", "Custom", false, 2 }, false);
            expected.LoadDataRow(new object[] { 2, "B", "Custom", true, 1 }, false);
            expected.LoadDataRow(new object[] { 3, "A", "Custom", true, 1 }, false);
            expected.LoadDataRow(new object[] { 3, "B", "Custom", false, 2 }, false);
            expected.LoadDataRow(new object[] { 4, "A", "Custom", true, 1 }, false);
            expected.LoadDataRow(new object[] { 4, "B", "Custom", true, 1 }, false);

            string source = "DS_r := inner_join(DS_1 as ds1 calc Me_2 := if ds1#Me_1 then 1 else 2)";

            List<Exception> errors;
            string sql = this.TranslateVtl(source, out errors);

            Assert.Empty(errors);

            DataTable ds_r = this.RunSql(sql, "#DS_r");

            Assert.True(expected.EqualsTo(ds_r));
        }

        [Fact]
        public void JoinCalcTwoScalarsComponent()
        {
            DataTable expected = new DataTable();
            expected.Columns.Add("Id_1");
            expected.Columns.Add("Id_2");
            expected.Columns.Add("Id_3");
            expected.Columns.Add("Me_1");
            expected.Columns.Add("Me_2");
            expected.Columns.Add("Me_3");
            expected.Columns.Add("At_1");
            expected.LoadDataRow(new object[] { 1, "A", "Custom", 2, "DS_4", 3, "1st" }, false);
            expected.LoadDataRow(new object[] { 1, "B", "Custom", 4, "DS_4", 5, "2nd" }, false);
            expected.LoadDataRow(new object[] { 2, "A", "Custom", 6, "DS_4", 7, "3rd" }, false);
            expected.LoadDataRow(new object[] { 2, "B", "Custom", 8, "DS_4", 9, "4th" }, false);
            expected.LoadDataRow(new object[] { 3, "A", "Custom", 10, "DS_4", 11, "5th" }, false);
            expected.LoadDataRow(new object[] { 3, "B", "Custom", 12, "DS_4", 13, "6th" }, false);
            expected.LoadDataRow(new object[] { 4, "A", "Custom", 14, "DS_4", 15, "7th" }, false);
            expected.LoadDataRow(new object[] { 4, "B", "Custom", 16, "DS_4", 17, "8th" }, false);

            string source = "DS_r := inner_join(DS_3 as ds3, DS_4 as ds4 calc Me_3 := if 1 = 2 then ds3#Me_1 else ds4#Me_1 keep ds3#Me_1, ds4#Me_2, At_1)";

            List<Exception> errors;
            string sql = this.TranslateVtl(source, out errors);

            Assert.Empty(errors);

            DataTable ds_r = this.RunSql(sql, "#DS_r");

            Assert.True(expected.EqualsTo(ds_r));
        }

        [Theory]
        [InlineData("Standard")]
        [InlineData("JoinCalc")]
        public void ThreeScalars(string type)
        {
            string source = "var := if false then 10 else 5";
            if (type == "JoinCalc") source = "DS_r := inner_join(DS_1 as ds1 calc Me_2 := if false then 10 else 5)";

            List<Exception> errors;
            string sql = this.TranslateVtl(source, out errors);

            Assert.Empty(errors);

            if (type == "Standard")
            {
                int expected = 5;
                DataTable var = this.RunSql(sql, "@var");

                Assert.Single(var.Columns);
                Assert.Single(var.Rows);
                Assert.Equal(expected, int.Parse(var.Rows[0].Field<string>("value")));
            }
            else
            {
                DataTable expected = new DataTable();
                expected.Columns.Add("Id_1");
                expected.Columns.Add("Id_2");
                expected.Columns.Add("Id_3");
                expected.Columns.Add("Me_1");
                expected.Columns.Add("Me_2");
                expected.LoadDataRow(new object[] { 1, "A", "Custom", false, 5 }, false);
                expected.LoadDataRow(new object[] { 1, "B", "Custom", false, 5 }, false);
                expected.LoadDataRow(new object[] { 2, "A", "Custom", false, 5 }, false);
                expected.LoadDataRow(new object[] { 2, "B", "Custom", true, 5 }, false);
                expected.LoadDataRow(new object[] { 3, "A", "Custom", true, 5 }, false);
                expected.LoadDataRow(new object[] { 3, "B", "Custom", false, 5 }, false);
                expected.LoadDataRow(new object[] { 4, "A", "Custom", true, 5 }, false);
                expected.LoadDataRow(new object[] { 4, "B", "Custom", true, 5 }, false);

                DataTable ds_r = this.RunSql(sql, "#DS_r");

                Assert.True(expected.EqualsTo(ds_r));
            }
        }
    }
}
