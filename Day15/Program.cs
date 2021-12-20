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

var start = coordinates[new Coordinate(0, 0)];
var end = coordinates[new Coordinate(99, 99)];
var shortest = ShortestPath(coordinates, start, end);
Console.WriteLine("Part 1: {0}", shortest);

// Part 2
var coordinates2 = new Dictionary<Coordinate, DijkstraCoordinate>();
for (int x = 0; x < 100 * 5; x++)
{
    for (int y2 = 0; y2 < 100 * 5; y2++)
    {
        var sourceCoordinate = coordinates[new Coordinate(x % 100, y2 % 100)];
        var tileDistance = (x / 100) + (y2 / 100);
        var risk = sourceCoordinate.Risk + tileDistance;
        if (risk > 9)
            risk -= 9;
        var c = new Coordinate(x, y2);
        coordinates2.Add(c, new DijkstraCoordinate(c, risk));
    }
}
start = coordinates2[new Coordinate(0, 0)];
end = coordinates2[new Coordinate(499, 499)];
shortest = ShortestPath(coordinates2, start, end);
Console.WriteLine("Part 2: {0}", shortest);


static List<Coordinate> Adjacent(Coordinate coordinate, int maxValue)
{
    var coordinates = new List<Coordinate>();
    if (coordinate.X != 0)
        coordinates.Add(new Coordinate(coordinate.X - 1, coordinate.Y));
    if (coordinate.X != maxValue)
        coordinates.Add(new Coordinate(coordinate.X + 1, coordinate.Y));
    if (coordinate.Y != 0)
        coordinates.Add(new Coordinate(coordinate.X, coordinate.Y - 1));
    if (coordinate.Y != maxValue)
        coordinates.Add(new Coordinate(coordinate.X, coordinate.Y + 1));

    return coordinates;
}

static long ShortestPath(Dictionary<Coordinate, DijkstraCoordinate> coordinates,
    DijkstraCoordinate start,
    DijkstraCoordinate end)
{
    start.Distance = 0;

    var current = start;
    var next = new List<DijkstraCoordinate>();
    var mapLength = coordinates.OrderByDescending(c => c.Key.X).First().Key.X;
    while (true)
    {
        var adjacent = Adjacent(current.Coordinate, mapLength);

        foreach (var neighbor in adjacent)
        {
            var coordinate = coordinates[neighbor];
            if (coordinate.Visited)
                continue;

            coordinate.CalculateDistance(current);
            if (!next.Contains(coordinate))
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
    return current.Distance;
}