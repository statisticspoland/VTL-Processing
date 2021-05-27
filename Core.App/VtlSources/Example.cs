namespace Core.App.VtlSources
{
    using System.Text;

    public static class Example
    {
        public const string Source = "define datapoint ruleset dpr1 ( variable x, Id1, Me2 as var ) is " +
            "x = \"123\"; " +
            "var >= 15 errorcode \"Bad credit\" errorlevel 5; " +
            "rule123: when Id1 = Id1 and Id1 > 2 then var >= 15; " +
            "var >= 20 errorcode \"Bad debit\" " +
            "end datapoint ruleset;" +
            "" +
            "define datapoint ruleset dpr2 ( valuedomain integer_default, string_default as a ) is " +
            "when integer_default = integer_default then a = \"321\" errorcode \"Bad credit\" errorlevel 5; " +
            "rule123: when integer_default > 5 and integer_default < 10.0 then match_characters(a, \"123\"); " +
            "a <> \"123\" errorcode \"Bad debit\" " +
            "end datapoint ruleset;" +
            "" +
            "DS_r := check_datapoint(Y, dpr2 components Id1, At_string)";
    }
}
