var vents = new List<Vent>();
foreach (var line in File.ReadLines("input.txt"))
{
    vents.Add(new Vent(line));
}

var points = vents.SelectMany(v => v.GetAllPoints(true))
    .GroupBy(v => v).Where(g => g.Count() > 1);

Console.WriteLine("Part 1: {0}", points.Count());

// Part Two

var points2 = vents.SelectMany(v => v.GetAllPoints(false))
    .GroupBy(v => v).Where(g => g.Count() > 1);

Console.WriteLine("Part 2: {0}", points2.Count());