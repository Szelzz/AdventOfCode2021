

class Entry
{
    public List<int> Pattern { get; set; }
        = new List<int>();

    public List<int> Output { get; set; }
        = new List<int>();

    public Entry(string line)
    {
        var parts = line.Split(' ');
        var outputSwitch = false;
        foreach (var part in parts)
        {
            if (part == "|")
            {
                outputSwitch = true;
                continue;
            }
            if (outputSwitch)
                Output.Add(part.Length);
            else
                Pattern.Add(part.Length);
        }
    }
}