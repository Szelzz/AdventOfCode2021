

class Entry
{
    private List<string> patternSegments = new List<string>();
    private List<string> outputSegments = new List<string>();
    public List<int> PatternCount { get; set; }
        = new List<int>();

    public List<int> OutputCount { get; set; }
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
            {
                outputSegments.Add(part);
                OutputCount.Add(part.Length);
            }
            else
            {
                patternSegments.Add(part);
                PatternCount.Add(part.Length);
            }
        }
    }

    public int DecodeOutput()
    {
        var map = SegmentMap();
        var number = "";
        foreach (var segment in outputSegments)
        {
            number += map.First(m => m.IsMatch(segment)).Value.ToString();
        }

        return int.Parse(number);
    }

    private Digit[] SegmentMap()
    {
        /*
            1 (2 seg)
            4 (4 seg)
            7 (3 seg)
            8 (7 seg)

            3 (5 seg, zawiera 1)
            9 (6 seg, zawiera 3)
            0 (6 seg, zawiera 7)
            6 (6 seg)
            5 (5 seg, 5 zawiera 6)
            2 (ostatnie)
        */

        var map = new Dictionary<char, char>();

        var digits = new Digit[10];
        // 1
        var one = patternSegments.First(s => s.Length == 2);
        patternSegments.Remove(one);
        digits[1] = new Digit(1, one);
        // 4 
        var four = patternSegments.First(s => s.Length == 4);
        patternSegments.Remove(four);
        digits[4] = new Digit(4, four);
        // 7
        var seven = patternSegments.First(s => s.Length == 3);
        patternSegments.Remove(seven);
        digits[7] = new Digit(7, seven);
        // 8
        var eight = patternSegments.First(s => s.Length == 7);
        patternSegments.Remove(eight);
        digits[8] = new Digit(8, eight);

        // 3
        var three = patternSegments.First(s => s.Length == 5 && digits[1].IsContaining(s));
        patternSegments.Remove(three);
        digits[3] = new Digit(3, three);

        // 9
        var nine = patternSegments.First(s => s.Length == 6 && digits[3].IsContaining(s));
        patternSegments.Remove(nine);
        digits[9] = new Digit(9, nine);

        // 0
        var zero = patternSegments.First(s => s.Length == 6 && digits[7].IsContaining(s));
        patternSegments.Remove(zero);
        digits[0] = new Digit(0, zero);

        // 6
        var six = patternSegments.First(s => s.Length == 6);
        patternSegments.Remove(six);
        digits[6] = new Digit(6, six);

        // 5
        var five = patternSegments.First(s => s.Length == 5 && digits[6].Contains(s));
        patternSegments.Remove(five);
        digits[5] = new Digit(5, five);

        // 2
        digits[2] = new Digit(2, patternSegments[0]);

        return digits;
    }
}

class Digit
{
    public Digit(int value, string panels)
    {
        Value = value;
        Panels = panels;
    }

    public bool IsMatch(string panelsInput)
    {
        if (panelsInput.Length != Panels.Length)
            return false;

        return Panels.All(p => panelsInput.Contains(p));
    }

    public bool IsContaining(string panelsInput)
    {
        return Panels.All(p => panelsInput.Contains(p));
    }

    public bool Contains(string panelsInput)
    {
        return panelsInput.All(p => Panels.Contains(p));
    }

    public int Value { get; }
    public string Panels { get; }
}