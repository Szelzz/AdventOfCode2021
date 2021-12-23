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
for (int currentYVelocity = maxYVelocity; currentYVelocity > 0; currentYVelocity--)
    maxHeight += currentYVelocity;

Console.WriteLine("Part 1: {0}", maxHeight);

// Part 2
var maxSteps = maxYVelocity * 2 + 2;
var maxXVelocity = target.X2;

var hits2 = 0;
for (int xVel = 1; xVel <= maxXVelocity; xVel++)
{
    for (int yVel = target.Y1; yVel <= maxYVelocity; yVel++)
    {
        var xPos = 0;
        var yPos = 0;
        var currentXVel = xVel;
        var currentYVel = yVel;

        for (int step = 1; step <= maxSteps; step++)
        {
            xPos += currentXVel;
            yPos += currentYVel;

            if (xPos >= target.X1 && xPos <= target.X2
                && yPos >= target.Y1 && yPos <= target.Y2)
            {
                hits2++;
                break;
            }

            currentYVel--;
            if (currentXVel != 0)
                currentXVel--;
        }
    }
}

Console.WriteLine("Part 2: {0}", hits2);

record Area(int X1, int X2, int Y1, int Y2);