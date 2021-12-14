class Rule
{
    public string Pair { get; }
    public string Insert { get; }

    public Rule(string input)
    {
        var parts = input.Split(" -> ");
        Pair = parts[0];
        Insert = parts[1];
    }
}
