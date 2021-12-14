var dots = new List<Dot>();
var folds = new List<Fold>();
var foldsFlag = false;
foreach (var line in File.ReadLines("input.txt"))
{
    if (line == "")
    {
        foldsFlag = true;
        continue;
    }

    if (foldsFlag)
        folds.Add(new Fold(line));
    else
    {
        var split = line.Split(',');
        dots.Add(
            new Dot(int.Parse(split[0]), int.Parse(split[1])));
    }
}

var dotsAfterFirstFold = dots.Select(folds[0].Transform).Distinct().ToList();
Console.WriteLine("Part 1: {0}", dotsAfterFirstFold.Count);

var dotsAfterFolding = dots.ToList();
foreach (var fold in folds)
{
    dotsAfterFolding = dotsAfterFolding
        .Select(fold.Transform)
        .Distinct().ToList();
}

void VisualizeDots(List<Dot> dots)
{
    var maxX = dots.Max(d=>d.X);
    var maxY = dots.Max(d=>d.Y);

    for (int y = 0; y <= maxY; y++)
    {
        for (int x = 0; x <= maxX; x++)
        {
            if (dots.Any(d => d.X == x && d.Y == y))
                Console.Write("#");
            else
                Console.Write(" ");
        }
        Console.WriteLine();
    }
}

Console.WriteLine("Part 2:");
VisualizeDots(dotsAfterFolding);