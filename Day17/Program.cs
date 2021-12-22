using System.Text.RegularExpressions;

// target area: x=257..286, y=-101..-57
var match = Regex.Match(
    File.ReadAllText("input.txt"),
    "target area: x=([-\\d]+)\\.\\.([-\\d]+), y=([-\\d]+)\\.\\.([-\\d]+)");

var target = new Area(
    int.Parse(match.Groups[1].Value),
    int.Parse(match.Groups[2].Value),
    int.Parse(match.Groups[3].Value),
    int.Parse(match.Groups[4].Value));

var maxYVelocity = -target.Y1 - 1;
var maxHeight = 0;
var currentYVelocity = maxYVelocity;
while (currentYVelocity > 0)
{
    maxHeight += currentYVelocity;
    currentYVelocity--;
}

Console.WriteLine("Part 1: {0}", maxHeight);


record Area(int X1, int X2, int Y1, int Y2);