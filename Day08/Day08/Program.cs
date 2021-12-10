// 0 - 6 seg -
// 1 - 2 seg -
// 2 - 5 seg -
// 3 - 5 seg -
// 4 - 4 seg - 
// 5 - 5 seg
// 6 - 6 seg -
// 7 - 3 seg - 
// 8 - 7 seg -
// 9 - 6 seg -
/*
    1 (2 seg)
    4 (4 seg)
    7 (3 seg)
    8 (7 seg)

    1 -> 3 (5 seg, zawiera 1)
    3 -> 9 (6 seg, zawiera 3)
    7 -> 0 (6 seg, zawiera 7)
    6 (6 seg)
    6 -> 5 (5 seg, 5 zawiera 6)
    2 (ostatnie)
*/
var entries = new List<Entry>();
foreach (var line in File.ReadLines("input.txt"))
{
    entries.Add(new Entry(line));
}

var count1 = entries.Sum(
    e => e.OutputCount.Where(o => new[] { 2, 4, 3, 7 }.Contains(o)).Count());

Console.WriteLine("Part 1: {0}", count1);

var sum = 0L;
foreach (var segment in entries)
{
    var number = segment.DecodeOutput();
    sum += number;
}

Console.WriteLine("Part 2: {0}", sum);