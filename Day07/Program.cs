var positions = File.ReadAllText("input.txt")
    .Split(',')
    .Select(int.Parse)
    .ToList();

positions.Sort();
var closest = positions[positions.Count / 2];

var fuelLost = positions.Select(p => Math.Abs(p - closest)).Sum();
Console.WriteLine("Part 1: {0}", fuelLost);

var closest2 = Math.Round(positions.Average(), MidpointRounding.ToZero);
var fuelLost2 = positions.Select(p => Math.Abs(p - closest2)).
    Select(d => (d * (d + 1)) / 2)
    .Sum();

Console.WriteLine("Part 2: {0}", fuelLost2);