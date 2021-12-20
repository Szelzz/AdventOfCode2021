using System.Diagnostics;

var coordinates = new Dictionary<Coordinate, DijkstraCoordinate>();
var y = 0;
foreach (var line in File.ReadLines("input.txt"))
{
    for (int x = 0; x < 100; x++)
    {
        var c = new Coordinate(x, y);
        coordinates.Add(c, new DijkstraCoordinate(c, int.Parse(line[x].ToString())));
    }
    y++;
}

var start = coordinates[new Coordinate(0,0)];
var end = coordinates[new Coordinate(99, 99)];
start.Distance = 0;

var current = start;
var next = new List<DijkstraCoordinate>();
while (true)
{
    var adjacent = Adjacent(current.Coordinate);

    foreach (var neighbor in adjacent)
    {
        var coordinate = coordinates[neighbor];
        if (coordinate.Visited)
            continue;

        coordinate.CalculateDistance(current);
        if(!next.Contains(coordinate))
            next.Add(coordinate);
    }
    current.Visited = true;
    next.Remove(current);
    if (current == end)
        break;

    current = next
        .OrderBy(d => d.Distance)
        .First(d => !d.Visited);
}
Console.WriteLine("Part 1: {0}", current.Distance);

static List<Coordinate> Adjacent(Coordinate coordinate)
{
    var coordinates = new List<Coordinate>();
    if (coordinate.X != 0)
        coordinates.Add(new Coordinate(coordinate.X - 1, coordinate.Y));
    if (coordinate.X != 99)
        coordinates.Add(new Coordinate(coordinate.X + 1, coordinate.Y));
    if (coordinate.Y != 0)
        coordinates.Add(new Coordinate(coordinate.X, coordinate.Y - 1));
    if (coordinate.Y != 99)
        coordinates.Add(new Coordinate(coordinate.X, coordinate.Y + 1));

    return coordinates;
}