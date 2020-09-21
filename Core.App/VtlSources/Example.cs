namespace Core.App.VtlSources
{
    using System.Text;

    public static class Example
    {
        public static string Source = new StringBuilder()
            .AppendLine("DS_1 := inner_join(X as ds1, Y as ds2 apply isnull(ds1) or ds1 * ds2 > 5);")
            .ToString();

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
