using System.Text.RegularExpressions;

class Vent
{
    public bool IsLine { get; }
    public Point Point2 { get; }
    public Point Point1 { get; }

    public Vent(string points)
    {
        // 578,391 -> 578,322
        var match = Regex.Match(points, "([0-9]+),([0-9]+) -> ([0-9]+),([0-9]+)");
        Point1 = new Point(int.Parse(match.Groups[1].Value), int.Parse(match.Groups[2].Value));
        Point2 = new Point(int.Parse(match.Groups[3].Value), int.Parse(match.Groups[4].Value));

        IsLine = Point1.X == Point2.X || Point1.Y == Point2.Y;
    }

    public List<Point> GetAllPoints(bool onlyLines)
    {
        var points = new List<Point>();
        if (!IsLine && onlyLines)
            return points;

        var xRatio = (Point1.X - Point2.X) switch
        {
            0 => 0,
            < 0 => 1,
            > 0 => -1
        };
        var yRatio = (Point1.Y - Point2.Y) switch
        {
            0 => 0,
            < 0 => 1,
            > 0 => -1
        };
        var differentce = Math.Max(Math.Abs(Point1.X - Point2.X), Math.Abs(Point1.Y - Point2.Y));
        for (int i = 0; i <= differentce; i++)
        {
            points.Add(new Point(Point1.X + xRatio * i, Point1.Y + yRatio * i));
        }

        return points;
    }
}

record Point(int X, int Y);