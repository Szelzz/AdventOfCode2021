ComplexNumber? result = null;
var input = File.ReadAllLines("input.txt");
foreach (var line in input)
{
    var number = new ComplexNumber(line);
    if (result == null)
        result = number;
    else
        result = result.Add(number);

    result.Reduce();
}

Console.WriteLine("Part 1: {0}", result?.Magnitude());

var numbers = input.Select(x => new ComplexNumber(x)).ToList();

var maxMagnitude = 0L;
for (int i = 0; i < numbers.Count; i++)
{
    for (int j = 0; j < numbers.Count; j++)
    {
        var num1 = numbers[i];
        var num2 = numbers[j];
        if (num1 == num2)
            continue;

        var sum = num1.Add(num2);
        sum.Reduce();
        var magnitude = sum.Magnitude();
        if (magnitude > maxMagnitude)
            maxMagnitude = magnitude;

        // reload input because numbers has been altered
        numbers = input.Select(x => new ComplexNumber(x)).ToList();
    }
}

Console.WriteLine("Part 2: {0}", maxMagnitude);