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
           Point1= new Point(int.Parse(match.Groups[1].Value), int.Parse(match.Groups[2].Value));
           Point2= new Point(int.Parse(match.Groups[3].Value), int.Parse(match.Groups[4].Value));

        IsLine = Point1.X == Point2.X || Point1.Y == Point2.Y;

    }

    public List<Point> GetAllPoints(bool onlyLines)
    {
        var points = new List<Point>();
        if (IsLine)
        {
            if (Point1.X == Point2.X)
            {
                for (int i = Math.Min(Point1.Y, Point2.Y); i <= Math.Max(Point1.Y, Point2.Y); i++)
                {
                    points.Add(new Point(Point1.X, i));
                }
            }
            else
            {
                for (int i = Math.Min(Point1.X, Point2.X); i <= Math.Max(Point1.X, Point2.X); i++)
                {
                    points.Add(new Point(i, Point1.Y));
                }
            }
        }
        else if(!onlyLines)
        {
            for (int i = 0; i <= Math.Abs(Point1.X - Point2.X); i++)
			{
                points.Add(new Point(Math.Min(Point1.X, Point2.X) + i, 
                    Math.Min(Point1.Y, Point2.Y) + i));
			}
        }

        return points;
    }

}

record Point(int X, int Y);