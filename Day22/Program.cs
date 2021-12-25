
using System.Text.RegularExpressions;

var input = File.ReadLines("input.txt");
List<Cuboid> cuboids = new List<Cuboid>();

foreach (var line in input)
{
    var cuboid = new Cuboid(line);
    if (cuboid.X.Start > 50 || cuboid.X.Start < -50)
        continue;

    cuboids.Add(new Cuboid(line));
}

var reactor = new Dictionary<Position, ReactorCube>();
var i = 0;
foreach (var cuboid in cuboids)
{
    Console.WriteLine("Step {0}", i++);
    var allPositions = cuboid.AllPositions();
    foreach (var position in allPositions)
    {
        if (reactor.ContainsKey(position))
            reactor[position].Activated = cuboid.Activate;
        else
            reactor.Add(position, new ReactorCube(position, cuboid.Activate));
    }
}

var activatedCubes = reactor.Values.Count(c => c.Activated);
Console.WriteLine("Part 1: {0}", activatedCubes);

record Range(int Start, int End);

record Position(int X, int Y, int Z);

class ReactorCube
{
    public ReactorCube(Position position, bool activate)
    {
        Position = position;
        Activated = activate;
    }

    public Position Position { get; set; }
    public bool Activated { get; set; }
}

class Cuboid
{
    public Cuboid(string line)
    {
        Activate = line.StartsWith("on");
        //on x=-25..22,y=-37..17,z=-38..8
        var match = Regex.Match(line, @"x=(-?\d+)\.\.(-?\d+),y=(-?\d+)\.\.(-?\d+),z=(-?\d+)\.\.(-?\d+)");
        X = new Range(int.Parse(match.Groups[1].Value), int.Parse(match.Groups[2].Value));
        Y = new Range(int.Parse(match.Groups[3].Value), int.Parse(match.Groups[4].Value));
        Z = new Range(int.Parse(match.Groups[5].Value), int.Parse(match.Groups[6].Value));
    }

    public Cuboid(Range x, Range y, Range z, bool activate)
    {
        X = x;
        Y = y;
        Z = z;
        Activate = activate;
    }

    public bool Activate { get; }
    public Range X { get; }
    public Range Y { get; }
    public Range Z { get; }

    public List<Position> AllPositions()
    {
        var result = new List<Position>();
        for (int x = X.Start; x <= X.End; x++)
            for (int y = Y.Start; y <= Y.End; y++)
                for (int z = Z.Start; z <= Z.End; z++)
                    result.Add(new Position(x, y, z));

        return result;
    }
}