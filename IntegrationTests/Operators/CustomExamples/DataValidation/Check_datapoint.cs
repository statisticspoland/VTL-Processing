namespace VtlProcessing.IntegrationTests.TSQL.Operators.CustomExamples.DataValidation
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Text;
    using Xunit;

    public class Check_datapoint : TSQLTestBase
    {
        private readonly string rulesetsSource;

        public Check_datapoint() : base("Custom_Check_datapoint")
        {
            List<object[]> ds1 = new List<object[]>
            {
                new object[] { 1, "Jack", 14, 178.6, 1, "UK", "Tall" },
                new object[] { 2, "Sophia", 16, 152.4, 2, "Germany", "Short" },
                new object[] { 3, "Martin", 13, 164.2, 1, "UK", "Short" },
                new object[] { 4, "frank", 13, 189.1, 1, "UK", "Short" },
                new object[] { 5, "Penny", 14, 158.3, 2, "USA", "Tall" },
                new object[] { 6, "Annie", 12, 170.2, 2, "Germany", "Tall" }
            };

            this.SqlFillData("[Custom_Check_datapoint].DS_1", ds1);

            this.rulesetsSource = new StringBuilder()
                .AppendLine("define datapoint ruleset polish_names_dpr1 ( variable name, gender ) is")
                .AppendLine("     first_big_lettter: substr(name, 1, 1) = upper(substr(name, 1, 1)) errorcode \"Name doesn't start with big letter\"")
                .AppendLine("   ; girl_ends_with_a:  when gender = 2 then substr(name, length(name)) = \"a\" errorcode \"Name doesn't end with 'a'\"")
                .AppendLine("end datapoint ruleset;")
                .AppendLine()
                .AppendLine("define datapoint ruleset polish_names_dpr2 ( valuedomain string_default as name, integer_default as gender ) is")
                .AppendLine("     first_big_lettter: substr(name, 1, 1) = upper(substr(name, 1, 1)) errorcode \"Name doesn't start with big letter\"")
                .AppendLine("   ; girl_ends_with_a:  when gender = 2 then substr(name, length(name)) = \"a\" errorcode \"Name doesn't end with 'a'\"")
                .AppendLine("end datapoint ruleset;")
                .AppendLine()
                .AppendLine("define datapoint ruleset heigth_dpr1 ( variable Me_2, Me_3 as gender, At_2 ) is")
                .AppendLine("     when Me_2 < 170 then At_2 = \"Short\" errorcode \"Wrong heigth description\" errorlevel 1")
                .AppendLine("   ; when Me_2 >= 170 then At_2 = \"Tall\" errorcode \"Wrong heigth description\" errorlevel 1")
                .AppendLine("   ; when Me_2 < 170 then gender = 2 errorcode \"Short boy\" errorlevel 2")
                .AppendLine("   ; when Me_2 >= 170 then gender = 1 errorcode \"Tall girl\" errorlevel 2")
                .AppendLine("end datapoint ruleset;")
                .AppendLine()
                .AppendLine("define datapoint ruleset heigth_dpr2 ( valuedomain number_default as heigth ) is")
                .AppendLine("     above_155.5: heigth > 155.5 errorcode \"Below 155.5\"")
                .AppendLine("   ; below_186.6: heigth < 186.6 errorcode \"Above 186.6\"")
                .AppendLine("end datapoint ruleset;")
                .ToString();
        }

        [Fact]
        public void Variables_CompsListInvalid_WithAttributes()
        {
            DataTable expected = new DataTable();
            expected.Columns.Add("Id_1");
            expected.Columns.Add("Id_2");
            expected.Columns.Add("ruleid");
            expected.Columns.Add("Me_1");
            expected.Columns.Add("Me_2");
            expected.Columns.Add("Me_3");
            expected.Columns.Add("errorcode");
            expected.Columns.Add("errorlevel");
            expected.Columns.Add("At_1");
            expected.LoadDataRow(new object[] { 4, "frank", "first_big_lettter", 13, 189.1, 1, "Name doesn't start with big letter", null, "UK" }, false);
            expected.LoadDataRow(new object[] { 5, "Penny", "girl_ends_with_a", 14, 158.3, 2, "Name doesn't end with 'a'", null, "USA" }, false);
            expected.LoadDataRow(new object[] { 6, "Annie", "girl_ends_with_a", 12, 170.2, 2, "Name doesn't end with 'a'", null, "Germany" }, false);

            string source = "DS_r := check_datapoint(DS_1, polish_names_dpr1 components Id_2, Me_3 invalid)";

            List<Exception> errors;
            string sql = this.TranslateVtl($"{this.rulesetsSource}{source}", out errors);

            Assert.Empty(errors);

            DataTable ds_r = this.RunSql(sql, "#DS_r");

            Assert.True(expected.EqualsTo(ds_r));
        }

        [Fact]
        public void Variables_CompsListAll_WithAttributes()
        {
            DataTable expected = new DataTable();
            expected.Columns.Add("Id_1");
            expected.Columns.Add("Id_2");
            expected.Columns.Add("ruleid");
            expected.Columns.Add("bool_var");
            expected.Columns.Add("errorcode");
            expected.Columns.Add("errorlevel");
            expected.Columns.Add("At_1");
            expected.LoadDataRow(new object[] { 1, "Jack", "first_big_lettter", 1, null, null, "UK" }, false);
            expected.LoadDataRow(new object[] { 2, "Sophia", "first_big_lettter", 1, null, null, "Germany" }, false);
            expected.LoadDataRow(new object[] { 3, "Martin", "first_big_lettter", 1, null, null, "UK" }, false);
            expected.LoadDataRow(new object[] { 4, "frank", "first_big_lettter", 0, "Name doesn't start with big letter", null, "UK" }, false);
            expected.LoadDataRow(new object[] { 5, "Penny", "first_big_lettter", 1, null, null, "USA" }, false);
            expected.LoadDataRow(new object[] { 6, "Annie", "first_big_lettter", 1, null, null, "Germany", }, false);
            expected.LoadDataRow(new object[] { 1, "Jack", "girl_ends_with_a", 1, null, null, "UK" }, false);
            expected.LoadDataRow(new object[] { 2, "Sophia", "girl_ends_with_a", 1, null, null, "Germany" }, false);
            expected.LoadDataRow(new object[] { 3, "Martin", "girl_ends_with_a", 1, null, null, "UK" }, false);
            expected.LoadDataRow(new object[] { 4, "frank", "girl_ends_with_a", 1, null, null, "UK" }, false);
            expected.LoadDataRow(new object[] { 5, "Penny", "girl_ends_with_a", 0, "Name doesn't end with 'a'", null, "USA" }, false);
            expected.LoadDataRow(new object[] { 6, "Annie", "girl_ends_with_a", 0, "Name doesn't end with 'a'", null, "Germany" }, false);
            // Fourth values are boolean

            string source = "DS_r := check_datapoint(DS_1, polish_names_dpr1 components Id_2, Me_3 all)";

            List<Exception> errors;
            string sql = this.TranslateVtl($"{this.rulesetsSource}{source}", out errors);

            Assert.Empty(errors);

            DataTable ds_r = this.RunSql(sql, "#DS_r");

            Assert.True(expected.EqualsTo(ds_r));
        }

        [Fact]
        public void Variables_CompsListAllMeasures_WithAttributes()
        {
            DataTable expected = new DataTable();
            expected.Columns.Add("Id_1");
            expected.Columns.Add("Id_2");
            expected.Columns.Add("ruleid");
            expected.Columns.Add("Me_1");
            expected.Columns.Add("Me_2");
            expected.Columns.Add("Me_3");
            expected.Columns.Add("bool_var");
            expected.Columns.Add("errorcode");
            expected.Columns.Add("errorlevel");
            expected.Columns.Add("At_1");
            expected.LoadDataRow(new object[] { 1, "Jack", "first_big_lettter", 14, 178.6, 1, 1, null, null, "UK" }, false);
            expected.LoadDataRow(new object[] { 2, "Sophia", "first_big_lettter", 16, 152.4, 2, 1, null, null, "Germany" }, false);
            expected.LoadDataRow(new object[] { 3, "Martin", "first_big_lettter", 13, 164.2, 1, 1, null, null, "UK" }, false);
            expected.LoadDataRow(new object[] { 4, "frank", "first_big_lettter", 13, 189.1, 1, 0, "Name doesn't start with big letter", null, "UK" }, false);
            expected.LoadDataRow(new object[] { 5, "Penny", "first_big_lettter", 14, 158.3, 2, 1, null, null, "USA" }, false);
            expected.LoadDataRow(new object[] { 6, "Annie", "first_big_lettter", 12, 170.2, 2, 1, null, null, "Germany" }, false);
            expected.LoadDataRow(new object[] { 1, "Jack", "girl_ends_with_a", 14, 178.6, 1, 1, null, null, "UK" }, false);
            expected.LoadDataRow(new object[] { 2, "Sophia", "girl_ends_with_a", 16, 152.4, 2, 1, null, null, "Germany" }, false);
            expected.LoadDataRow(new object[] { 3, "Martin", "girl_ends_with_a", 13, 164.2, 1, 1, null, null, "UK" }, false);
            expected.LoadDataRow(new object[] { 4, "frank", "girl_ends_with_a", 13, 189.1, 1, 1, null, null, "UK" }, false);
            expected.LoadDataRow(new object[] { 5, "Penny", "girl_ends_with_a", 14, 158.3, 2, 0, "Name doesn't end with 'a'", null, "USA" }, false);
            expected.LoadDataRow(new object[] { 6, "Annie", "girl_ends_with_a", 12, 170.2, 2, 0, "Name doesn't end with 'a'", null, "Germany" }, false);
            // Seventh values are boolean

            string source = "DS_r := check_datapoint(DS_1, polish_names_dpr1 components Id_2, Me_3 all_measures)";

            List<Exception> errors;
            string sql = this.TranslateVtl($"{this.rulesetsSource}{source}", out errors);

            Assert.Empty(errors);

            DataTable ds_r = this.RunSql(sql, "#DS_r");

            Assert.True(expected.EqualsTo(ds_r));
        }

        [Fact]
        public void ValueDomains_CompsListInvalid_WithAttributes()
        {
            DataTable expected = new DataTable();
            expected.Columns.Add("Id_1");
            expected.Columns.Add("Id_2");
            expected.Columns.Add("ruleid");
            expected.Columns.Add("Me_1");
            expected.Columns.Add("Me_2");
            expected.Columns.Add("Me_3");
            expected.Columns.Add("errorcode");
            expected.Columns.Add("errorlevel");
            expected.Columns.Add("At_1");
            expected.LoadDataRow(new object[] { 4, "frank", "first_big_lettter", 13, 189.1, 1, "Name doesn't start with big letter", null, "UK" }, false);
            expected.LoadDataRow(new object[] { 5, "Penny", "girl_ends_with_a", 14, 158.3, 2, "Name doesn't end with 'a'", null, "USA" }, false);
            expected.LoadDataRow(new object[] { 6, "Annie", "girl_ends_with_a", 12, 170.2, 2, "Name doesn't end with 'a'", null, "Germany" }, false);

            string source = "DS_r := check_datapoint(DS_1, polish_names_dpr2 components Id_2, Me_3 invalid)";

            List<Exception> errors;
            string sql = this.TranslateVtl($"{this.rulesetsSource}{source}", out errors);

            Assert.Empty(errors);

            DataTable ds_r = this.RunSql(sql, "#DS_r");

            Assert.True(expected.EqualsTo(ds_r));
        }

        [Fact]
        public void ValueDomains_CompsListAll_WithAttributes()
        {
            DataTable expected = new DataTable();
            expected.Columns.Add("Id_1");
            expected.Columns.Add("Id_2");
            expected.Columns.Add("ruleid");
            expected.Columns.Add("bool_var");
            expected.Columns.Add("errorcode");
            expected.Columns.Add("errorlevel");
            expected.Columns.Add("At_1");
            expected.LoadDataRow(new object[] { 1, "Jack", "first_big_lettter", 1, null, null, "UK" }, false);
            expected.LoadDataRow(new object[] { 2, "Sophia", "first_big_lettter", 1, null, null, "Germany" }, false);
            expected.LoadDataRow(new object[] { 3, "Martin", "first_big_lettter", 1, null, null, "UK" }, false);
            expected.LoadDataRow(new object[] { 4, "frank", "first_big_lettter", 0, "Name doesn't start with big letter", null, "UK" }, false);
            expected.LoadDataRow(new object[] { 5, "Penny", "first_big_lettter", 1, null, null, "USA" }, false);
            expected.LoadDataRow(new object[] { 6, "Annie", "first_big_lettter", 1, null, null, "Germany" }, false);
            expected.LoadDataRow(new object[] { 1, "Jack", "girl_ends_with_a", 1, null, null, "UK" }, false);
            expected.LoadDataRow(new object[] { 2, "Sophia", "girl_ends_with_a", 1, null, null, "Germany" }, false);
            expected.LoadDataRow(new object[] { 3, "Martin", "girl_ends_with_a", 1, null, null, "UK" }, false);
            expected.LoadDataRow(new object[] { 4, "frank", "girl_ends_with_a", 1, null, null, "UK" }, false);
            expected.LoadDataRow(new object[] { 5, "Penny", "girl_ends_with_a", 0, "Name doesn't end with 'a'", null, "USA" }, false);
            expected.LoadDataRow(new object[] { 6, "Annie", "girl_ends_with_a", 0, "Name doesn't end with 'a'", null, "Germany" }, false);
            // Fourth values are boolean

            string source = "DS_r := check_datapoint(DS_1, polish_names_dpr2 components Id_2, Me_3 all)";

            List<Exception> errors;
            string sql = this.TranslateVtl($"{this.rulesetsSource}{source}", out errors);

            Assert.Empty(errors);

            DataTable ds_r = this.RunSql(sql, "#DS_r");

            Assert.True(expected.EqualsTo(ds_r));
        }

        [Fact]
        public void ValueDomains_CompsListAllMeasures_WithAttributes()
        {
            DataTable expected = new DataTable();
            expected.Columns.Add("Id_1");
            expected.Columns.Add("Id_2");
            expected.Columns.Add("ruleid");
            expected.Columns.Add("Me_1");
            expected.Columns.Add("Me_2");
            expected.Columns.Add("Me_3");
            expected.Columns.Add("bool_var");
            expected.Columns.Add("errorcode");
            expected.Columns.Add("errorlevel");
            expected.Columns.Add("At_1");
            expected.LoadDataRow(new object[] { 1, "Jack", "first_big_lettter", 14, 178.6, 1, 1, null, null, "UK" }, false);
            expected.LoadDataRow(new object[] { 2, "Sophia", "first_big_lettter", 16, 152.4, 2, 1, null, null, "Germany" }, false);
            expected.LoadDataRow(new object[] { 3, "Martin", "first_big_lettter", 13, 164.2, 1, 1, null, null, "UK" }, false);
            expected.LoadDataRow(new object[] { 4, "frank", "first_big_lettter", 13, 189.1, 1, 0, "Name doesn't start with big letter", null, "UK" }, false);
            expected.LoadDataRow(new object[] { 5, "Penny", "first_big_lettter", 14, 158.3, 2, 1, null, null, "USA" }, false);
            expected.LoadDataRow(new object[] { 6, "Annie", "first_big_lettter", 12, 170.2, 2, 1, null, null, "Germany" }, false);
            expected.LoadDataRow(new object[] { 1, "Jack", "girl_ends_with_a", 14, 178.6, 1, 1, null, null, "UK" }, false);
            expected.LoadDataRow(new object[] { 2, "Sophia", "girl_ends_with_a", 16, 152.4, 2, 1, null, null, "Germany" }, false);
            expected.LoadDataRow(new object[] { 3, "Martin", "girl_ends_with_a", 13, 164.2, 1, 1, null, null, "UK" }, false);
            expected.LoadDataRow(new object[] { 4, "frank", "girl_ends_with_a", 13, 189.1, 1, 1, null, null, "UK" }, false);
            expected.LoadDataRow(new object[] { 5, "Penny", "girl_ends_with_a", 14, 158.3, 2, 0, "Name doesn't end with 'a'", null, "USA" }, false);
            expected.LoadDataRow(new object[] { 6, "Annie", "girl_ends_with_a", 12, 170.2, 2, 0, "Name doesn't end with 'a'", null, "Germany" }, false);
            // Seventh values are boolean

            string source = "DS_r := check_datapoint(DS_1, polish_names_dpr2 components Id_2, Me_3 all_measures)";

            List<Exception> errors;
            string sql = this.TranslateVtl($"{this.rulesetsSource}{source}", out errors);

            Assert.Empty(errors);

            DataTable ds_r = this.RunSql(sql, "#DS_r");

            Assert.True(expected.EqualsTo(ds_r));
        }

        [Fact]
        public void Variables_Invalid_WithAttributes()
        {
            DataTable expected = new DataTable();
            expected.Columns.Add("Id_1");
            expected.Columns.Add("Id_2");
            expected.Columns.Add("ruleid");
            expected.Columns.Add("Me_1");
            expected.Columns.Add("Me_2");
            expected.Columns.Add("Me_3");
            expected.Columns.Add("errorcode");
            expected.Columns.Add("errorlevel");
            expected.Columns.Add("At_1");
            expected.Columns.Add("At_2");
            expected.LoadDataRow(new object[] { 5, "Penny", "heigth_dpr1_1", 14, 158.3, 2, "Wrong heigth description", 1, "USA", "Tall" }, false);
            expected.LoadDataRow(new object[] { 4, "frank", "heigth_dpr1_2", 13, 189.1, 1, "Wrong heigth description", 1, "UK", "Short" }, false);
            expected.LoadDataRow(new object[] { 3, "Martin", "heigth_dpr1_3", 13, 164.2, 1, "Short boy", 2, "UK", "Short" }, false);
            expected.LoadDataRow(new object[] { 6, "Annie", "heigth_dpr1_4", 12, 170.2, 2, "Tall girl", 2, "Germany", "Tall" }, false);

            string source = "DS_r := check_datapoint(DS_1, heigth_dpr1 invalid)";

            List<Exception> errors;
            string sql = this.TranslateVtl($"{this.rulesetsSource}{source}", out errors);

            Assert.Empty(errors);

            DataTable ds_r = this.RunSql(sql, "#DS_r");

            Assert.True(expected.EqualsTo(ds_r));
        }

        [Fact]
        public void Variables_All_WithAttributes()
        {
            DataTable expected = new DataTable();
            expected.Columns.Add("Id_1");
            expected.Columns.Add("Id_2");
            expected.Columns.Add("ruleid");
            expected.Columns.Add("bool_var");
            expected.Columns.Add("errorcode");
            expected.Columns.Add("errorlevel");
            expected.Columns.Add("At_1");
            expected.Columns.Add("At_2");
            expected.LoadDataRow(new object[] { 1, "Jack", "heigth_dpr1_1", 1, null, null, "UK", "Tall" }, false);
            expected.LoadDataRow(new object[] { 2, "Sophia", "heigth_dpr1_1", 1, null, null, "Germany", "Short" }, false);
            expected.LoadDataRow(new object[] { 3, "Martin", "heigth_dpr1_1", 1, null, null, "UK", "Short" }, false);
            expected.LoadDataRow(new object[] { 4, "frank", "heigth_dpr1_1", 1, null, null, "UK", "Short" }, false);
            expected.LoadDataRow(new object[] { 5, "Penny", "heigth_dpr1_1", 0, "Wrong heigth description", 1, "USA", "Tall" }, false);
            expected.LoadDataRow(new object[] { 6, "Annie", "heigth_dpr1_1", 1, null, null, "Germany", "Tall" }, false);
            expected.LoadDataRow(new object[] { 1, "Jack", "heigth_dpr1_2", 1, null, null, "UK", "Tall" }, false);
            expected.LoadDataRow(new object[] { 2, "Sophia", "heigth_dpr1_2", 1, null, null, "Germany", "Short" }, false);
            expected.LoadDataRow(new object[] { 3, "Martin", "heigth_dpr1_2", 1, null, null, "UK", "Short" }, false);
            expected.LoadDataRow(new object[] { 4, "frank", "heigth_dpr1_2", 0, "Wrong heigth description", 1, "UK", "Short" }, false);
            expected.LoadDataRow(new object[] { 5, "Penny", "heigth_dpr1_2", 1, null, null, "USA", "Tall" }, false);
            expected.LoadDataRow(new object[] { 6, "Annie", "heigth_dpr1_2", 1, null, null, "Germany", "Tall" }, false);
            expected.LoadDataRow(new object[] { 1, "Jack", "heigth_dpr1_3", 1, null, null, "UK", "Tall" }, false);
            expected.LoadDataRow(new object[] { 2, "Sophia", "heigth_dpr1_3", 1, null, null, "Germany", "Short" }, false);
            expected.LoadDataRow(new object[] { 3, "Martin", "heigth_dpr1_3", 0, "Short boy", 2, "UK", "Short" }, false);
            expected.LoadDataRow(new object[] { 4, "frank", "heigth_dpr1_3", 1, null, null, "UK", "Short" }, false);
            expected.LoadDataRow(new object[] { 5, "Penny", "heigth_dpr1_3", 1, null, null, "USA", "Tall" }, false);
            expected.LoadDataRow(new object[] { 6, "Annie", "heigth_dpr1_3", 1, null, null, "Germany", "Tall" }, false);
            expected.LoadDataRow(new object[] { 1, "Jack", "heigth_dpr1_4", 1, null, null, "UK", "Tall" }, false);
            expected.LoadDataRow(new object[] { 2, "Sophia", "heigth_dpr1_4", 1, null, null, "Germany", "Short" }, false);
            expected.LoadDataRow(new object[] { 3, "Martin", "heigth_dpr1_4", 1, null, null, "UK", "Short" }, false);
            expected.LoadDataRow(new object[] { 4, "frank", "heigth_dpr1_4", 1, null, null, "UK", "Short" }, false);
            expected.LoadDataRow(new object[] { 5, "Penny", "heigth_dpr1_4", 1, null, null, "USA", "Tall" }, false);
            expected.LoadDataRow(new object[] { 6, "Annie", "heigth_dpr1_4", 0, "Tall girl", 2, "Germany", "Tall" }, false);
            // Fourth values are boolean

            string source = "DS_r := check_datapoint(DS_1, heigth_dpr1 all)";

            List<Exception> errors;
            string sql = this.TranslateVtl($"{this.rulesetsSource}{source}", out errors);

            Assert.Empty(errors);

            DataTable ds_r = this.RunSql(sql, "#DS_r");

            Assert.True(expected.EqualsTo(ds_r));
        }

        [Fact]
        public void Variables_AllMeasures_WithAttributes()
        {
            DataTable expected = new DataTable();
            expected.Columns.Add("Id_1");
            expected.Columns.Add("Id_2");
            expected.Columns.Add("ruleid");
            expected.Columns.Add("Me_1");
            expected.Columns.Add("Me_2");
            expected.Columns.Add("Me_3");
            expected.Columns.Add("bool_var");
            expected.Columns.Add("errorcode");
            expected.Columns.Add("errorlevel");
            expected.Columns.Add("At_1");
            expected.Columns.Add("At_2");
            expected.LoadDataRow(new object[] { 1, "Jack", "heigth_dpr1_1", 14, 178.6, 1, 1, null, null, "UK", "Tall"}, false);
            expected.LoadDataRow(new object[] { 2, "Sophia", "heigth_dpr1_1", 16, 152.4, 2, 1, null, null, "Germany", "Short"}, false);
            expected.LoadDataRow(new object[] { 3, "Martin", "heigth_dpr1_1", 13, 164.2, 1, 1, null, null, "UK", "Short"}, false);
            expected.LoadDataRow(new object[] { 4, "frank", "heigth_dpr1_1", 13, 189.1, 1, 1, null, null, "UK", "Short"}, false);
            expected.LoadDataRow(new object[] { 5, "Penny", "heigth_dpr1_1", 14, 158.3, 2, 0, "Wrong heigth description", 1, "USA", "Tall"}, false);
            expected.LoadDataRow(new object[] { 6, "Annie", "heigth_dpr1_1", 12, 170.2, 2, 1, null, null, "Germany", "Tall"}, false);
            expected.LoadDataRow(new object[] { 1, "Jack", "heigth_dpr1_2", 14, 178.6, 1, 1, null, null, "UK", "Tall" }, false);
            expected.LoadDataRow(new object[] { 2, "Sophia", "heigth_dpr1_2", 16, 152.4, 2, 1, null, null, "Germany", "Short"}, false);
            expected.LoadDataRow(new object[] { 3, "Martin", "heigth_dpr1_2", 13, 164.2, 1, 1, null, null, "UK", "Short" }, false);
            expected.LoadDataRow(new object[] { 4, "frank", "heigth_dpr1_2", 13, 189.1, 1, 0, "Wrong heigth description", 1, "UK", "Short" }, false);
            expected.LoadDataRow(new object[] { 5, "Penny", "heigth_dpr1_2", 14, 158.3, 2, 1, null, null, "USA", "Tall" }, false);
            expected.LoadDataRow(new object[] { 6, "Annie", "heigth_dpr1_2", 12, 170.2, 2, 1, null, null, "Germany", "Tall" }, false);
            expected.LoadDataRow(new object[] { 1, "Jack", "heigth_dpr1_3", 14, 178.6, 1, 1, null, null, "UK", "Tall" }, false);
            expected.LoadDataRow(new object[] { 2, "Sophia", "heigth_dpr1_3", 16, 152.4, 2, 1, null, null, "Germany", "Short" }, false);
            expected.LoadDataRow(new object[] { 3, "Martin", "heigth_dpr1_3", 13, 164.2, 1, 0, "Short boy", 2, "UK", "Short" }, false);
            expected.LoadDataRow(new object[] { 4, "frank", "heigth_dpr1_3", 13, 189.1, 1, 1, null, null, "UK", "Short" }, false);
            expected.LoadDataRow(new object[] { 5, "Penny", "heigth_dpr1_3", 14, 158.3, 2, 1, null, null, "USA", "Tall" }, false);
            expected.LoadDataRow(new object[] { 6, "Annie", "heigth_dpr1_3", 12, 170.2, 2, 1, null, null, "Germany", "Tall" }, false);
            expected.LoadDataRow(new object[] { 1, "Jack", "heigth_dpr1_4", 14, 178.6, 1, 1, null, null, "UK", "Tall" }, false);
            expected.LoadDataRow(new object[] { 2, "Sophia", "heigth_dpr1_4", 16, 152.4, 2, 1, null, null, "Germany", "Short" }, false);
            expected.LoadDataRow(new object[] { 3, "Martin", "heigth_dpr1_4", 13, 164.2, 1, 1, null, null, "UK", "Short" }, false);
            expected.LoadDataRow(new object[] { 4, "frank", "heigth_dpr1_4", 13, 189.1, 1, 1, null, null, "UK", "Short" }, false);
            expected.LoadDataRow(new object[] { 5, "Penny", "heigth_dpr1_4", 14, 158.3, 2, 1, null, null, "USA", "Tall" }, false);
            expected.LoadDataRow(new object[] { 6, "Annie", "heigth_dpr1_4", 12, 170.2, 2, 0, "Tall girl", 2, "Germany", "Tall" }, false);
            // Seventh values are boolean

            string source = "DS_r := check_datapoint(DS_1, heigth_dpr1 all_measures)";

            List<Exception> errors;
            string sql = this.TranslateVtl($"{this.rulesetsSource}{source}", out errors);

            Assert.Empty(errors);

            DataTable ds_r = this.RunSql(sql, "#DS_r");

            Assert.True(expected.EqualsTo(ds_r));
        }

        [Fact]
        public void ValueDomains_Invalid_WithAttributes()
        {
            DataTable expected = new DataTable();
            expected.Columns.Add("Id_1");
            expected.Columns.Add("Id_2");
            expected.Columns.Add("ruleid");
            expected.Columns.Add("Me_1");
            expected.Columns.Add("Me_2");
            expected.Columns.Add("Me_3");
            expected.Columns.Add("errorcode");
            expected.Columns.Add("errorlevel");
            expected.Columns.Add("At_1");
            expected.LoadDataRow(new object[] { 2, "Sophia", "above_155.5", 16, 152.4, 2, "Below 155.5", null, "Germany" }, false);
            expected.LoadDataRow(new object[] { 4, "frank", "below_186.6", 13, 189.1, 1, "Above 186.6", null, "UK" }, false);

            string source = "DS_r := check_datapoint(DS_1, heigth_dpr2 invalid)";

            List<Exception> errors;
            string sql = this.TranslateVtl($"{this.rulesetsSource}{source}", out errors);

            Assert.Empty(errors);

            DataTable ds_r = this.RunSql(sql, "#DS_r");

            Assert.True(expected.EqualsTo(ds_r));
        }

        [Fact]
        public void ValueDomains_All_WithAttributes()
        {
            DataTable expected = new DataTable();
            expected.Columns.Add("Id_1");
            expected.Columns.Add("Id_2");
            expected.Columns.Add("ruleid");
            expected.Columns.Add("bool_var");
            expected.Columns.Add("errorcode");
            expected.Columns.Add("errorlevel");
            expected.Columns.Add("At_1");
            expected.LoadDataRow(new object[] { 1, "Jack", "above_155.5", 1, null, null, "UK" }, false);
            expected.LoadDataRow(new object[] { 2, "Sophia", "above_155.5", 0, "Below 155.5", null, "Germany" }, false);
            expected.LoadDataRow(new object[] { 3, "Martin", "above_155.5", 1, null, null, "UK" }, false);
            expected.LoadDataRow(new object[] { 4, "frank", "above_155.5", 1, null, null, "UK" }, false);
            expected.LoadDataRow(new object[] { 5, "Penny", "above_155.5", 1, null, null, "USA" }, false);
            expected.LoadDataRow(new object[] { 6, "Annie", "above_155.5", 1, null, null, "Germany" }, false);
            expected.LoadDataRow(new object[] { 1, "Jack", "below_186.6", 1, null, null, "UK" }, false);
            expected.LoadDataRow(new object[] { 2, "Sophia", "below_186.6", 1, null, null, "Germany" }, false);
            expected.LoadDataRow(new object[] { 3, "Martin", "below_186.6", 1, null, null, "UK" }, false);
            expected.LoadDataRow(new object[] { 4, "frank", "below_186.6", 0, "Above 186.6", null, "UK" }, false);
            expected.LoadDataRow(new object[] { 5, "Penny", "below_186.6", 1, null, null, "USA" }, false);
            expected.LoadDataRow(new object[] { 6, "Annie", "below_186.6", 1, null, null, "Germany" }, false);
            // Fourth values are boolean

            string source = "DS_r := check_datapoint(DS_1, heigth_dpr2 all)";

            List<Exception> errors;
            string sql = this.TranslateVtl($"{this.rulesetsSource}{source}", out errors);

            Assert.Empty(errors);

            DataTable ds_r = this.RunSql(sql, "#DS_r");

            Assert.True(expected.EqualsTo(ds_r));
        }

        [Fact]
        public void ValueDomains_AllMeasures_WithAttributes()
        {
            DataTable expected = new DataTable();
            expected.Columns.Add("Id_1");
            expected.Columns.Add("Id_2");
            expected.Columns.Add("ruleid");
            expected.Columns.Add("Me_1");
            expected.Columns.Add("Me_2");
            expected.Columns.Add("Me_3");
            expected.Columns.Add("bool_var");
            expected.Columns.Add("errorcode");
            expected.Columns.Add("errorlevel");
            expected.Columns.Add("At_1");
            expected.LoadDataRow(new object[] { 1, "Jack", "above_155.5", 14, 178.6, 1, 1, null, null, "UK" }, false);
            expected.LoadDataRow(new object[] { 2, "Sophia", "above_155.5", 16, 152.4, 2, 0, "Below 155.5", null, "Germany" }, false);
            expected.LoadDataRow(new object[] { 3, "Martin", "above_155.5", 13, 164.2, 1, 1, null, null, "UK" }, false);
            expected.LoadDataRow(new object[] { 4, "frank", "above_155.5", 13, 189.1, 1, 1, null, null, "UK" }, false);
            expected.LoadDataRow(new object[] { 5, "Penny", "above_155.5", 14, 158.3, 2, 1, null, null, "USA" }, false);
            expected.LoadDataRow(new object[] { 6, "Annie", "above_155.5", 12, 170.2, 2, 1, null, null, "Germany" }, false);
            expected.LoadDataRow(new object[] { 1, "Jack", "below_186.6", 14, 178.6, 1, 1, null, null, "UK" }, false);
            expected.LoadDataRow(new object[] { 2, "Sophia", "below_186.6", 16, 152.4, 2, 1, null, null, "Germany" }, false);
            expected.LoadDataRow(new object[] { 3, "Martin", "below_186.6", 13, 164.2, 1, 1, null, null, "UK" }, false);
            expected.LoadDataRow(new object[] { 4, "frank", "below_186.6", 13, 189.1, 1, 0, "Above 186.6", null, "UK" }, false);
            expected.LoadDataRow(new object[] { 5, "Penny", "below_186.6", 14, 158.3, 2, 1, null, null, "USA" }, false);
            expected.LoadDataRow(new object[] { 6, "Annie", "below_186.6", 12, 170.2, 2, 1, null, null, "Germany" }, false);
            // Seventh values are boolean

            string source = "DS_r := check_datapoint(DS_1, heigth_dpr2 all_measures)";

            List<Exception> errors;
            string sql = this.TranslateVtl($"{this.rulesetsSource}{source}", out errors);

            Assert.Empty(errors);

            DataTable ds_r = this.RunSql(sql, "#DS_r");

            Assert.True(expected.EqualsTo(ds_r));
        }
    }
}
