var startTemplate = File.ReadLines("input.txt").First();

var rules = new List<Rule>();
foreach (var line in File.ReadLines("input.txt").Skip(2))
{
    rules.Add(new Rule(line));
}

string Transform(string template)
{
    var position = 0;
    var insertCount = 0;
    var newTemplate = template[0].ToString();
    while (true)
    {
        if (position == template.Length - 1)
            break;
        // take pair
        var pair = template[position..(position + 2)];
        var rule = rules.FirstOrDefault(r => r.Pair == pair);
        if (rule != null)
        {
            newTemplate += rule.Insert;
            insertCount++;
        }
        newTemplate += template[position + 1];
        position++;
    }

    return newTemplate;
}

var part1Template = startTemplate;
for (int i = 0; i < 10; i++)
{
    part1Template = Transform(part1Template);
}
var orderedElements = startTemplate.GroupBy(c => c)
    .OrderBy(c => c.Count())
    .ToList();
var mostCommonElement = orderedElements.Last().Count();
var leastCommonElement = orderedElements.First().Count();
Console.WriteLine("Part 1: {0}", mostCommonElement - leastCommonElement);

// Part 2
var part2Template = startTemplate;
for (int i = 0; i < 40; i++)
{
    part2Template = Transform(part2Template);
    Console.WriteLine("{0}: {1}", i, part2Template.Length);
}

Console.WriteLine("Part 2: ");