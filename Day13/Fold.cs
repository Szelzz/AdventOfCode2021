class Fold
{
    readonly char axis;
    readonly int line;

    public Fold(string input)
    {
        var parts = input.Split('=');
        line = int.Parse(parts[1]);
        axis = parts[0][^1];
    }

    public Dot Transform(Dot dot)
    {
        if (axis == 'x')
        {
            if (dot.X < line)
                return dot;

            var space = dot.X - line;
            return new Dot(line - space, dot.Y);
        }
        else
        {
            if (dot.Y < line)
                return dot;

            var space = dot.Y - line;
            return new Dot(dot.X, line - space);
        }
    }
}
