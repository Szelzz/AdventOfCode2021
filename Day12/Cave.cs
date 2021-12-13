class Cave
{
    public Cave(string name)
    {
        Name = name;
        if (name == name.ToUpper())
            IsBig = true;
    }

    public bool IsBig { get; }
    public string Name { get; }
    public bool IsEnd => Name == "end";
    public bool IsStart => Name == "start";

    public List<Cave> Connections { get; }
        = new List<Cave>();

    public void AddConnetion(Cave connection)
    {
        if (!Connections.Contains(connection))
            Connections.Add(connection);

        if (!connection.Connections.Contains(this))
            connection.Connections.Add(this);
    }

    public List<List<Cave>>? FindEnd(List<Cave>? path = null)
    {
        if (path == null)
            path = new List<Cave>();

        // small caves cannot be visited twice
        if (!IsBig && path.Contains(this))
            return null;

        path.Add(this);
        var allPaths = new List<List<Cave>>();
        if (IsEnd)
        {
            allPaths.Add(path);
            return allPaths;
        }

        foreach (var connection in Connections)
        {
            var newPaths = connection.FindEnd(path.ToList());
            if (newPaths != null)
                allPaths.AddRange(newPaths.Where(p => p.Any(c => c.IsEnd)));
        }

        return allPaths;
    }

    public List<List<Cave>>? FindEnd2(List<Cave>? path = null)
    {
        if (path == null)
            path = new List<Cave>();

        if (IsStart && path.Contains(this))
            return null;

        // only one small cave can be visited twice
        path.Add(this);
        var smallDuplicates = path.Where(c => !c.IsBig).Count() -
            path.Where(c => !c.IsBig).Distinct().Count();
        if (smallDuplicates > 1)
            return null;

        var allPaths = new List<List<Cave>>();
        if (IsEnd)
        {
            allPaths.Add(path);
            return allPaths;
        }

        foreach (var connection in Connections)
        {
            var newPaths = connection.FindEnd2(path.ToList());
            if (newPaths != null)
                allPaths.AddRange(newPaths.Where(p => p.Any(c => c.IsEnd)));
        }

        return allPaths;
    }

    public override string ToString()
    {
        return "Cave [" + Name + "]";
    }
}

