using System.Text;

var startTemplate = File.ReadLines("input.txt").First();

var rules = new List<Rule>();
var rules2 = new Dictionary<string, char>();
foreach (var line in File.ReadLines("input.txt").Skip(2))
{
    rules.Add(new Rule(line));
    var parts = line.Split(" -> ");
    rules2.Add(parts[0], parts[1][0]);
}

string Transform(string template)
{
    var position = 0;
    var newTemplate = new StringBuilder(template[0].ToString());

    while (true)
    {
        if (position == template.Length - 1)
            break;
        // take pair
        var pair = template[position].ToString() + template[position + 1].ToString();
        var rule = rules2[pair];
        newTemplate.Append(rule);
        newTemplate.Append(pair[1]);
        position++;
    }

    return newTemplate.ToString();
}

var part1Template = startTemplate;
for (int i = 0; i < 10; i++)
    part1Template = Transform(part1Template);

var orderedElements = part1Template.GroupBy(c => c)
    .OrderBy(c => c.Count())
    .ToList();
var mostCommonElement = orderedElements.Last().Count();
var leastCommonElement = orderedElements.First().Count();
Console.WriteLine("Part 1: {0}", mostCommonElement - leastCommonElement);

// Part 2
string[] pairTransform(string pair)
{
    var link = rules2[pair];
    return new[]{
        string.Concat(pair[0], link),
        string.Concat(link, pair[1]) };
}

var pairs = new List<string>();
for (int i = 0; i < startTemplate.Length - 1; i++)
    pairs.Add(startTemplate[i..(i + 2)]);

var pairCount = pairs.GroupBy(c => c).ToDictionary(k => k.Key, c => (long)c.Count());

Dictionary<string, long> TransformStep(Dictionary<string, long> pairCount)
{
    var pairCountAfter = new Dictionary<string, long>();
    foreach (var pair in pairCount)
    {
        var newPairs = pairTransform(pair.Key);
        pairCountAfter[newPairs[0]] = pairCountAfter.GetValueOrDefault(newPairs[0]) + pair.Value;
        pairCountAfter[newPairs[1]] = pairCountAfter.GetValueOrDefault(newPairs[1]) + pair.Value;
    }
    return pairCountAfter;
}

Dictionary<char, long> SumElements(Dictionary<string, long> pairCount)
{
    var sum = new Dictionary<char, long>();
    foreach (var pair in pairCount)
        sum[pair.Key[0]] = sum.GetValueOrDefault(pair.Key[0]) + pair.Value;

    return sum;
}

for (int i = 0; i < 40; i++)
    pairCount = TransformStep(pairCount);

var sum = SumElements(pairCount);
sum[startTemplate.Last()]++;

var result = sum.Values.Max() - sum.Values.Min();
Console.WriteLine("Part 2: {0}", result);