using System.Text;

namespace Core.App.VtlSources
{
    public static class Example
    {
        public static string Source = new StringBuilder()
            .AppendLine(@"DS1 := 10 / 2.5;")
            .AppendLine(@"DS2 := 5 * 3;")
            .AppendLine(@"DS3 := Regular\R1 - 5 * DS1;")
            .AppendLine(@"DS4 := Json\X * Y;")
            .AppendLine(@"DS5 := Y * Y + 200;")
            .AppendLine(@"Y <- DS4 + 100.0;")
            .AppendLine(@"Regular\R1 <- 2 / DS3;")
            .ToString();
    }
}
