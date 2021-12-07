using System.Numerics;

var fish = File.ReadAllText("input.txt")
    .Split(',')
    .Select(n => new LanternFish(int.Parse(n)))
    .ToList();

for (int i = 0; i < 80; i++)
{
    foreach (var oneFish in fish.ToList())
    {
        if (oneFish.NextDay())
            fish.Add(new LanternFish());
    }
}

Console.WriteLine("Part 1: {0}", fish.Count);


var fishCount = new BigInteger[9];
var fishCounters = File.ReadAllText("input.txt")
    .Split(',')
    .Select(n => int.Parse(n))
    .ToList();

for (int i = 0; i < 9; i++)
    fishCount[i] = new BigInteger(fishCounters.Count(c => c == i));

for (int day = 0; day < 256; day++)
{
    var fishCountNext = new BigInteger[9];
    for (int i = 1; i < fishCount.Length; i++)
    {
        fishCountNext[i - 1] = fishCount[i];
    }
    fishCountNext[6] = fishCountNext[6] + fishCount[0];
    fishCountNext[8] = fishCount[0];

    fishCount = fishCountNext;
}

BigInteger sum = new BigInteger();
for (int i = 0; i < fishCount.Length; i++)
{
    sum += fishCount[i];
}
Console.WriteLine("Part 2: {0}", sum);