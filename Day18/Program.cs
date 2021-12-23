ComplexNumber? result = null;

var input = File.ReadLines("input.txt");
foreach (var line in input)
{
    var number = new ComplexNumber(line);
    if (result == null)
        result = number;
    else
        result = result.Add(number);

    Console.WriteLine("After add");
    Console.WriteLine(result);

    result.Reduce();
}

Console.WriteLine("Part 1: {0}", result?.Magnitude());

