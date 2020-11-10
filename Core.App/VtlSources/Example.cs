namespace Core.App.VtlSources
{
    using System.Text;

    public static class Example
    {
        public static string Source = new StringBuilder()
            .AppendLine("DS1 := 3 in { 4, 5, 6 };")
            .AppendLine("DS3 := inner_join(X as ds1, Y as ds2 calc Me3 := ds2#At_string in {\"123\", \"dfefe\" });")
            .AppendLine("DS4 := inner_join(X as ds1, Y as ds2 apply ds1 in { 1, 3, 4 } or true);")
            .ToString();

        //public static string Source = new StringBuilder()
        //    .AppendLine("X2 := inner_join(X calc Me1 := true drop Me2 rename Me1 to bool_var);")
        //    .AppendLine("Y2 := inner_join(X as ds1, Y as ds2 filter ds1#Me1 > 2 keep ds2#Me2);")
        //    .AppendLine("Y3 := isnull(Y2);")
        //    .AppendLine("DS_1 := not X2 = Y3;")
        //    .AppendLine("DS_2 := X2 and false or DS_1 < Y3;")
        //    .AppendLine("DS_3 := X#Id1 = 5 <> Y3;")
        //    .ToString();

        //public static string Source = new StringBuilder()
        //    .AppendLine(@"DS1 := 10 / 2.5;")
        //    .AppendLine(@"DS2 := 5 * 3;")
        //    .AppendLine(@"DS3 := Regular\R1 - 5 * DS1;")
        //    .AppendLine(@"DS4 := Json\X * Y;")
        //    .AppendLine(@"DS5 := Y * Y + 200;")
        //    .AppendLine(@"Y <- DS4 + 100.0;")
        //    .AppendLine(@"Regular\R1 <- 2 / DS3;")
        //    .ToString();
    }
}
