// 0 - 6 seg
// 1 - 2 seg -
// 2 - 5 seg
// 3 - 5 seg
// 4 - 4 seg - 
// 5 - 5 seg
// 6 - 6 seg
// 7 - 3 seg - 
// 8 - 7 seg -
// 9 - 6 seg

var entries = new List<Entry>();
foreach (var line in File.ReadLines("input.txt"))
{
    entries.Add(new Entry(line));
}

var count1 = entries.Sum(
    e => e.Output.Where(o => new[] { 2, 4, 3, 7 }.Contains(o)).Count());

Console.WriteLine("Part 1: {0}", count1);


