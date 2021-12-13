class Point
{
    public int Value { get; }
    public Point? Left { get; private set; }
    public Point? Top { get; private set; }
    public Point? Right { get; private set; }
    public Point? Bottom { get; private set; }

    private Lazy<List<Point>> neighbours;
    public List<Point> Neighbours => neighbours.Value;

    private List<Point> InitNeighbours()
    {
        var l = new List<Point>();
        if (Left != null)
            l.Add(Left);
        if (Top != null)
            l.Add(Top);
        if (Right != null)
            l.Add(Right);
        if (Bottom != null)
            l.Add(Bottom);
        return l;
    }

    public int X { get; }
    public int Y { get; }

    public Point(int value, int x, int y)
    {
        Value = value;
        X = x;
        Y = y;

        neighbours = new Lazy<List<Point>>(InitNeighbours);
    }

    public void LoadPoint(Point point)
    {
        if (point.X == X)
        {
            if (point.Y == Y - 1)
            {
                Top = point;
                point.Bottom = this;
            }
            else if (point.Y == Y + 1)
            {
                Bottom = point;
                point.Top = this;
            }
        }
        else if (point.Y == Y)
        {
            if (point.X == X - 1)
            {
                Left = point;
                point.Right = this;
            }
            else if (point.X == X + 1)
            {
                Right = point;
                point.Left = this;
            }
        }
    }

    public List<Point> FindBasin(List<Point>? basin = null)
    {
        if(basin == null)
            basin = new List<Point>();

        if (Value == 9)
            return basin;

        basin.Add(this);
        foreach (var neighbour in Neighbours)
        {
            if (basin.Contains(neighbour))
                continue;

            neighbour.FindBasin(basin);
        }

        return basin;
    }
}
