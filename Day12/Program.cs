var caves = new List<Cave>();

foreach (var line in File.ReadLines("input.txt"))
{
    var split = line.Split('-');

    var cave1 = caves.FirstOrDefault(c => c.Name == split[0]);
    if (cave1 == null)
    {
        cave1 = new Cave(split[0]);
        caves.Add(cave1);
    }
    var cave2 = caves.FirstOrDefault(c => c.Name == split[1]);
    if (cave2 == null)
    {
        cave2 = new Cave(split[1]);
        caves.Add(cave2);
    }
    cave1.AddConnetion(cave2);
}

var start = caves.First(c => c.IsStart);
var end = caves.First(c => c.IsEnd);

var allPaths = start.FindEnd() ?? throw new Exception();


Console.WriteLine("Part 1: {0}", allPaths.Count);