class ComplexNumber : ISNumber
{
    public ComplexNumber(ISNumber left, ISNumber right, ComplexNumber? parent = null)
    {
        Left = left;
        Left.Parent = this;
        Right = right;
        Right.Parent = this;
        Parent = parent;
    }

    public ComplexNumber(int left, int right, ComplexNumber parent)
    {
        Left = new RegularNumber(left, this);
        Right = new RegularNumber(right, this);
        Parent = parent;
    }

    public ComplexNumber(string number, ComplexNumber? parent = null)
    {
        Parent = parent;

        var left = number[1];
        var right = number[^2];

        // scan for comma
        var i = 1;
        for (; i < number.Length - 1; i++)
        {
            var leftSide = number[..i];
            var rightSide = number[i..];
            var leftBrackets = leftSide.Count(c => c == '[') - leftSide.Count(c => c == ']');
            var rightBrackets = rightSide.Count(c => c == ']') - rightSide.Count(c => c == '[');
            if (leftBrackets == 1 && rightBrackets == 1 && number[i] == ',')
                break;
        }

        Left = left == '[' ?
            new ComplexNumber(number[1..i], this) : new RegularNumber(left, this);
        Right = right == ']' ?
            new ComplexNumber(number[(i + 1)..^1], this) : new RegularNumber(right, this);
    }

    public ISNumber Left { get; set; }
    public ISNumber Right { get; set; }

    public ComplexNumber? Parent { get; set; }

    public RegularNumber? FindClosestLeft()
    {
        if (Parent == null)
            return null;

        if (Parent.Left is RegularNumber l)
            return l;

        if (Parent.Left == this)
            return Parent.FindClosestLeft();

        return ((ComplexNumber)Parent.Left).GetFirstRegularRight(); ;
    }

    public RegularNumber? FindClosestRight()
    {
        if (Parent == null)
            return null;

        if (Parent.Right is RegularNumber r)
            return r;

        if (Parent.Right == this)
            return Parent.FindClosestRight();

        return ((ComplexNumber)Parent.Right).GetFirstRegularLeft();
    }

    public RegularNumber GetFirstRegularLeft()
    {
        if (Left is RegularNumber n)
            return n;

        return ((ComplexNumber)Left).GetFirstRegularLeft();
    }

    public RegularNumber GetFirstRegularRight()
    {
        if (Right is RegularNumber n)
            return n;

        return ((ComplexNumber)Right).GetFirstRegularRight();
    }

    public ComplexNumber? ScanForExplode(int nestingLevel = 0)
    {
        ComplexNumber? result = null;
        if (Left is ComplexNumber l)
            result = l.ScanForExplode(nestingLevel + 1);

        if (result != null)
            return result;

        if (Right is ComplexNumber r)
            result = r.ScanForExplode(nestingLevel + 1);

        if (result != null)
            return result;

        if (nestingLevel >= 4)
            return this;

        return null;
    }

    private void RemoveFromParent()
    {
        if (Parent == null)
            return;

        if (Parent.Left == this)
            Parent.Left = new RegularNumber(0, Parent);
        else if (Parent.Right == this)
            Parent.Right = new RegularNumber(0, Parent);
    }

    public void Reduce()
    {
        var toExplode = ScanForExplode();
        var split = false;
        while (toExplode != null || split)
        {
            while (toExplode != null)
            {
                var newLeft = toExplode.FindClosestLeft();
                if (newLeft != null)
                    newLeft.Value += ((RegularNumber)toExplode.Left).Value;

                var newRight = toExplode.FindClosestRight();
                if (newRight != null)
                    newRight.Value += ((RegularNumber)toExplode.Right).Value;

                toExplode.RemoveFromParent();
                toExplode = ScanForExplode();
            }

            split = Split();
            toExplode = ScanForExplode();
        }

    }

    public bool Split()
        => Left.Split() || Right.Split();

    public ComplexNumber Add(ISNumber number2)
        => new ComplexNumber(this, number2);

    public long Magnitude()
        => Left.Magnitude() * 3 + Right.Magnitude() * 2;

    public override string ToString()
        => $"[{Left},{Right}]";
}
