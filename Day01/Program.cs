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

System.Console.WriteLine(increaseCount);