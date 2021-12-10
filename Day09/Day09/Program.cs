var size = File.ReadLines("input.txt").First().Count();
var input = new int[size, size];
var inputY = 0;
foreach (var line in File.ReadLines("input.txt"))
{
    for (int x = 0; x < size; x++)
        input[x, inputY] = int.Parse(line[x].ToString());

    inputY++;
}

var lowPoints = new List<int>();
for (int x = 0; x < size; x++)
{
    for (int y = 0; y < size; y++)
    {
        var adjacent = new List<int>();
        if (y - 1 >= 0)
            adjacent.Add(input[x, y - 1]);
        if (y + 1 < size)
            adjacent.Add(input[x, y + 1]);
        if (x - 1 >= 0)
            adjacent.Add(input[x - 1, y]);
        if (x + 1 < size)
            adjacent.Add(input[x + 1, y]);

        if (adjacent.All(a => a > input[x, y]))
            lowPoints.Add(input[x, y]);
    }
}

Console.WriteLine("Part 1: {0}", lowPoints.Select(p => p + 1).Sum());