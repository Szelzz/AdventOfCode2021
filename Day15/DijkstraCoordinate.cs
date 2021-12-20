record Coordinate(int X, int Y);

class DijkstraCoordinate
{
    public DijkstraCoordinate(int x, int y, int risk)
    {
        X = x;
        Y = y;
        Risk = risk;
        Coordinate = new Coordinate(x, y);
    }

    public DijkstraCoordinate(Coordinate coordinate, int risk)
    {
        X = coordinate.X;
        Y = coordinate.Y;
        Coordinate = coordinate;
        Risk = risk;
    }

    public int X { get; }
    public int Y { get; }
    public int Risk { get; }

    public bool IsStart => X == 0 && Y == 0;
    public bool IsEnd => X == 99 && Y == 99;
    public bool Visited { get; set; } = false;
    public long Distance { get; set; } = long.MaxValue;

    public Coordinate Coordinate { get; }

    public bool IsCoordinate(Coordinate coordinate)
    {
        return coordinate.X == X && coordinate.Y == Y;
    }

    internal void CalculateDistance(DijkstraCoordinate sourceCoordinate)
    {
        var newDistance = sourceCoordinate.Distance + Risk;
        if (newDistance < Distance)
            Distance = newDistance;
    }
}