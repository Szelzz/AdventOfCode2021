var fish = File.ReadAllText("input.txt")
    .Split(',')
    .Select(n => new LanternFish(int.Parse(n)))
    .ToList();


for (int i = 0; i < 80; i++)
{
    Console.WriteLine("Day {0}, count {1}",i,fish.Count);
    foreach (var oneFish in fish.ToList())
    {
        if (oneFish.NextDay())
            fish.Add(new LanternFish());
    }
}

Console.WriteLine("Part 1: {0}", fish.Count);