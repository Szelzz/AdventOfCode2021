var horizontal = 0;
var depth = 0;
foreach (var line in File.ReadLines("input.txt"))
{
    var number = int.Parse(line.Split(" ")[1]);
    if(line.StartsWith("forward")){
        horizontal+= number;
    }else if(line.StartsWith("down")){
        depth += number;
    }else if(line.StartsWith("up")){
        depth -= number;
    }
}

Console.WriteLine("Part 1: {0}", depth*horizontal);

// Part Two

horizontal = 0;
depth = 0;
var aim = 0;
foreach (var line in File.ReadLines("input.txt"))
{
    var number = int.Parse(line.Split(" ")[1]);
    if(line.StartsWith("forward")){
        horizontal+= number;
        depth += aim * number;
    }else if(line.StartsWith("down")){
        aim += number;
    }else if(line.StartsWith("up")){
        aim -= number;
    }
}
Console.WriteLine("Part 2: {0}", depth*horizontal);
