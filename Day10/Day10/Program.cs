var closingBrackets = "]})>";
var bracketMap = new Dictionary<char, char>()
{
    { ']', '[' },
    { ')', '(' },
    { '}', '{' },
    { '>', '<' }
};

var invalidBrackets = new List<char>();
foreach (var line in File.ReadLines("input.txt"))
{
    var brackets = new List<char>();
    foreach (var bracket in line)
    {
        if (closingBrackets.Contains(bracket))
        {
            if (brackets.Last() != bracketMap[bracket])
            {
                invalidBrackets.Add(bracket);
                break;
            }
            brackets.RemoveAt(brackets.Count - 1);
        }
        else
            brackets.Add(bracket);
    }
}

int BracketScoring(char c)
{
    return c switch
    {
        ')' => 3,
        ']' => 57,
        '}' => 1197,
        '>' => 25137,
        _ => throw new ArgumentException(),
    };
}
var sum = invalidBrackets.Select(BracketScoring).Sum();

Console.WriteLine("Part 1: {0}", sum);

// Part 2