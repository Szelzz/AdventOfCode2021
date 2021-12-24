var input = File.ReadLines("input.txt");
var scanners = new List<Scanner>();

Scanner? currentScanner = null;
foreach (var line in input)
{
    if (line == "")
        continue;
    if (line.StartsWith("--- scanner"))
    {
        currentScanner = new Scanner(line);
        scanners.Add(currentScanner);
    }
    else
    {
        currentScanner?.Beacons.Add(new Beacon(new Position(line)));
    }
}

//foreach (var scanner in scanners)
//{
//    Console.WriteLine(scanner.Beacons.Count);
//}

scanners[0].Beacons[0].Position.AllRotations();

class Position
{
    public Position(string input)
    {
        var parts = input.Split(',');
        X = int.Parse(parts[0]);
        Y = int.Parse(parts[1]);
        Z = int.Parse(parts[2]);
    }

    public Position(int x, int y, int z)
    {
        X = x;
        Y = y;
        Z = z;
    }

    public int X { get; set; }
    public int Y { get; set; }
    public int Z { get; set; }

    public override bool Equals(object? obj)
    {
        if (obj == null || !(obj is Position p))
            return false;
        return p.X == X && p.Y == Y && p.Z == Z;
    }

    public override int GetHashCode()
    {
        return X.GetHashCode() ^ Y.GetHashCode() ^ Z.GetHashCode();
    }



    public List<Position> AllRotations()
    {
        var positions = new List<Position>()
        {
            new Position(X,Y,Z),

            new Position(-X,Y,Z),
            new Position(X,-Y,Z),
            new Position(X,Y,-Z),

            new Position(-X,-Y,Z),
            new Position(X,-Y,-Z),
            new Position(-X,Y,-Z),

            new Position(-X,-Y,-Z),


            new Position(X,Z,Y),

            new Position(-X,Z,Y),
            new Position(X,-Z,Y),
            new Position(X,Z,-Y),

            new Position(-X,-Z,Y),
            new Position(X,-Z,-Y),
            new Position(-X,Z,-Y),

            new Position(-X,-Z,-Y),


            new Position(Y,X,Z),

            new Position(-Y,X,Z),
            new Position(Y,-X,Z),
            new Position(Y,X,-Z),

            new Position(-Y,-X,Z),
            new Position(Y,-X,-Z),
            new Position(-Y,X,-Z),

            new Position(-Y,-X,-Z),


            new Position(Y,Z,X),

            new Position(-Y,Z,X),
            new Position(Y,-Z,X),
            new Position(Y,Z,-X),

            new Position(-Y,-Z,X),
            new Position(Y,-Z,-X),
            new Position(-Y,Z,-X),

            new Position(-Y,-Z,-X),


            new Position(Z,X,Y),

            new Position(-Z,X,Y),
            new Position(Z,-X,Y),
            new Position(Z,X,-Y),

            new Position(-Z,-X,Y),
            new Position(Z,-X,-Y),
            new Position(-Z,X,-Y),

            new Position(-Z,-X,-Y),


            new Position(Z,Y,X),

            new Position(-Z,Y,X),
            new Position(Z,-Y,X),
            new Position(Z,Y,-X),

            new Position(-Z,-Y,X),
            new Position(Z,-Y,-X),
            new Position(-Z,Y,-X),

            new Position(-Z,-Y,-X),
        };

        return positions;

        //var p000 = new Position(X, Y, Z);
        //// rotate on Y
        //var p010 = new Position(-Z, Y, X);
        //var p020 = new Position(-X, Y, -Z);
        //var p030 = new Position(Z, Y, -X);

        //// rotate on 

        //var p001 = new Position(Y, -X, Z);
        //var p002 = new Position(-X, -Y, Z);
        //var p003 = new Position(-Y, -Y, Z);


        //var p100 = new Position(-Z, -X, Y);
        //var p200 = new Position(Z, X, -Y);


    }
}

class Beacon
{
    public Beacon(Position position)
    {
        Position = position;
    }

    public Position Position { get; set; }

    public List<Beacon> AllDirections()
    {
        throw new NotImplementedException();
    }
}

class Scanner
{
    public Scanner(string name)
    {
        Name = name;
    }

    public string Name { get; set; }

    public List<Beacon> Beacons { get; set; }
        = new List<Beacon>();


}