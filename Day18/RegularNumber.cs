class RegularNumber : ISNumber
{
    public RegularNumber(int value, ComplexNumber parent)
    {
        Value = value;
        Parent = parent;
    }
    public RegularNumber(char value, ComplexNumber parent)
    {
        Value = value - '0';
        Parent = parent;
    }

    public int Value { get; set; }
    public ComplexNumber? Parent { get; set; }

    public bool Split()
    {
        if (Parent == null || Value < 10)
            return false;

        var newComplex = new ComplexNumber(Value / 2, (int)Math.Ceiling(Value / 2.0), Parent);
        if (Parent.Left == this)
            Parent.Left = newComplex;
        else
            Parent.Right = newComplex;

        return true;
    }

    public long Magnitude() => Value;
    public override string ToString() => Value.ToString();
}