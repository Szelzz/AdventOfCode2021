var input = File.ReadAllLines("input.txt");

var number = 99_999_999_999_999;
while (true)
{
    //Console.WriteLine("Checking: {0}", number);
    if (CheckIsValid(number))
        break;

    number--;
}
Console.WriteLine("Part 1: {0}", number);

bool CheckIsValid(long number)
{
    var inputNumberEnumerator = number.ToString()
        .Select(n => n - '0')
        .ToList().GetEnumerator();
    var variables = new Dictionary<string, Variable>()
    {
        {"w", new W() },
        {"x", new X() },
        {"y", new Y() },
        {"z", new Z() },
    };

    foreach (var line in input)
    {
        var tokens = line.Split(' ');
        switch (tokens[0])
        {
            case "inp":
                inputNumberEnumerator.MoveNext();
                variables[tokens[1]].Value = inputNumberEnumerator.Current;
                break;
            case "add":
                variables[tokens[1]].Value += GetValue(tokens[2]);
                break;
            case "mul":
                variables[tokens[1]].Value *= GetValue(tokens[2]);
                break;
            case "div":
                variables[tokens[1]].Value /= GetValue(tokens[2]);
                break;
            case "mod":
                variables[tokens[1]].Value %= GetValue(tokens[2]);
                break;
            case "eql":
                variables[tokens[1]].Value = variables[tokens[1]].Value == GetValue(tokens[2]) ? 1 : 0;
                break;
            default:
                break;
        }
    }

    return variables["z"].Value == 0;

    long GetValue(string token)
    {
        if (token[0] > '9')
            return variables[token].Value;
        return long.Parse(token);
    }
}


Console.WriteLine();
abstract class Variable
{
    public Variable(string name)
    {
        Name = name;
    }

    public string Name { get; }
    public long Value { get; set; }
}

class W : Variable
{
    public W() : base("w") { }
}

class X : Variable
{
    public X() : base("x") { }
}

class Y : Variable
{
    public Y() : base("y") { }
}
class Z : Variable
{
    public Z() : base("z") { }
}