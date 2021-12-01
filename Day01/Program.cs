using System.IO;

var last = 0;
var increaseCount = -1;
foreach (var line in File.ReadLines("input.txt"))
{
    var current = int.Parse(line);
    if (current > last)
        increaseCount++;

    last = current;
}

System.Console.WriteLine("Part one: {0}", increaseCount);

// Part Two

var lastGroupResult = 0;
increaseCount = -1;
var currentGroups = new List<Group>();
foreach (var line in File.ReadLines("input.txt"))
{
    var value = int.Parse(line);
    currentGroups.ForEach(g => g.AddValue(value));
    if (currentGroups.Count < 3)
        currentGroups.Add(new Group(value));

    var fullGroup = currentGroups.FirstOrDefault(g => g.IsFull());
    if (fullGroup != null)
    {
        if (fullGroup.Value > lastGroupResult)
            increaseCount++;

        lastGroupResult = fullGroup.Value;
        currentGroups.Remove(fullGroup);
    }
}

System.Console.WriteLine("Part two: {0}", increaseCount);
