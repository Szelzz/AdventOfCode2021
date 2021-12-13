
class Dumbo
{
    public Dumbo(int energy, int x, int y)
    {
        Energy = energy;
        this.X = x;
        this.Y = y;
    }

    public int Energy { get; private set; }
    public int FlashCount { get; private set; }

    public List<Dumbo> Neighbors { get; }
        = new List<Dumbo>();

    public int X { get; }
    public int Y { get; }

    public void AddNeighbor(Dumbo dumbo)
    {
        if (Math.Abs(X - dumbo.X) <= 1 && Math.Abs(Y - dumbo.Y) <= 1)
        {
            Neighbors.Add(dumbo);
            dumbo.Neighbors.Add(this);
        }
    }

    public void AddEnergy()
    {
        if (Energy > 9)
            return;

        Energy++;
        if (Energy == 10)
        {
            FlashCount++;
            foreach (var neighbor in Neighbors)
            {
                neighbor.AddEnergy();
            }
        }
    }

    public void Reset()
    {
        if (Energy > 9)
            Energy = 0;
    }
}

