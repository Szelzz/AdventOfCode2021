var closingBrackets = "]})>";
var bracketMap = new Dictionary<char, char>()
{
    { '(', ')' },
    { '[', ']' },
    { '{', '}' },
    { '<', '>' }
};

var invalidBrackets = new List<char>();
foreach (var line in File.ReadLines("input.txt"))
{
    var brackets = new List<char>();
    foreach (var bracket in line)
    {
        if (closingBrackets.Contains(bracket))
        {
            if (bracketMap[brackets.Last()] != bracket)
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

var missingBrackets = new List<string>();
foreach (var line in File.ReadLines("input.txt"))
{
    var brackets = new List<char>();
    var invalidLine = false;
    foreach (var bracket in line)
    {
        if (closingBrackets.Contains(bracket))
        {
            if (bracketMap[brackets.Last()] != bracket)
            {
                invalidLine = true;
                break;
            }
            brackets.RemoveAt(brackets.Count - 1);
        }
        else
            brackets.Add(bracket);
    }
    if (invalidLine)
        continue;

    brackets.Reverse();
    missingBrackets.Add(
        new string(
            brackets.Select(b => bracketMap[b])
                .ToArray()));
}

long MissingBraketsScoring(string missingBrackets)
{
    long score = 0;
    foreach (var bracket in missingBrackets)
    {
        checked
        {
            score *= 5;
            score += bracket switch
            {
                ')' => 1,
                ']' => 2,
                '}' => 3,
                '>' => 4,
                _ => 0
            };
        }
    }
    return score;
}

var scores = missingBrackets.Select(MissingBraketsScoring)
    .OrderBy(b => b)
    .ToList();

var score = scores[scores.Count / 2];
Console.WriteLine("Part 2: {0}", score);