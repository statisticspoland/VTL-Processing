namespace ExampleApp.VtlSources
{
    public static class Example
    {
        public static readonly string Simple = "A := X + Y";
        public static readonly string WithJoin = "B := inner_join(X as ds1, Y as ds2 filter Id1 = 2 and Me1 > 3 calc Id1 := 3, Me1 := 5 keep Me1 rename Id1 to Id3, Me1 to Me3)";
    }
}
