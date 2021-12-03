var gammaList = new int[12];

foreach (var line in File.ReadLines("input.txt"))
{
    for (int i = 0; i < gammaList.Length; i++)
    {
        if (line[i] == '0')
            gammaList[i]--;
        else
            gammaList[i]++;
    }
}

var binary = string.Concat(gammaList.Select(d => d < 0 ? '0' : '1'));
var gammaRate = Convert.ToUInt32(binary, 2);
var epsilonRate = ~gammaRate & 0x00000FFF;

System.Console.WriteLine("Part One: {0}", gammaRate * epsilonRate);

// Part TWO

string CalculateOxygen(IEnumerable<string> input, int index, bool takeBigger)
{
    var group0 = new List<string>();
    var group1 = new List<string>();
    foreach (var line in input)
    {
        if (line[index] == '0')
            group0.Add(line);
        else
            group1.Add(line);
    }
    List<string> returnGroup;
    if (takeBigger)
        returnGroup = group0.Count > group1.Count ? group0 : group1;
    else
        returnGroup = group0.Count <= group1.Count ? group0 : group1;

    if (returnGroup.Count == 1 || index == 12)
        return returnGroup[0];

    return CalculateOxygen(returnGroup, index + 1, takeBigger);
}

var oxygenBinary = CalculateOxygen(File.ReadLines("input.txt"), 0, true);
var co2Binary = CalculateOxygen(File.ReadLines("input.txt"), 0, false);

var oxygen = Convert.ToInt32(oxygenBinary, 2);
var co2 = Convert.ToInt32(co2Binary, 2);

System.Console.WriteLine("Part Two: {0}", oxygen * co2);
