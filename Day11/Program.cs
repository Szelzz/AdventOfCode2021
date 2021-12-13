var y = 0;

var octopuses = new List<Dumbo>();

foreach (var line in File.ReadLines("input.txt"))
{
    for (int x = 0; x < 10; x++)
    {
        var newDumbo = new Dumbo(int.Parse(line[x].ToString()), x, y);
        foreach (var octopus in octopuses)
        {
            octopus.AddNeighbor(newDumbo);
        }
        octopuses.Add(newDumbo);
    }
    y++;
}

for (int i = 0; i < 100; i++)
{
    foreach (var octopus in octopuses)
        octopus.AddEnergy();

    foreach (var octopus in octopuses)
        octopus.Reset();
}

var flashCount = octopuses.Sum(x => x.FlashCount);
Console.WriteLine("Part 1: {0}", flashCount);

var step = 101; // steps from part 1
while(true)
{
    foreach (var octopus in octopuses)
        octopus.AddEnergy();

    if (octopuses.All(o => o.Energy == 10))
        break;

    foreach (var octopus in octopuses)
        octopus.Reset();

    step++;
}

Console.WriteLine("Part 2: {0}", step);