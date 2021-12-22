class Packet
{
    public PacketType Type { get; set; }
    public int Version { get; set; }
    public int SubpacketsSize { get; set; }
    public long Value { get; set; }
    public int Position { get; set; }
    public int Size { get; set; }
    public LengthType LengthType { get; set; }


    public long Evaluate(Stack<long> numbersForOperation)
    {
        checked
        {
            return Type switch
            {
                PacketType.Sum => numbersForOperation.Sum(),
                PacketType.Product => numbersForOperation.Aggregate((a, b) =>   a * b),
                PacketType.Minimum => numbersForOperation.Min(),
                PacketType.Maximum => numbersForOperation.Max(),
                PacketType.GreaterThen => numbersForOperation.Pop() > numbersForOperation.Pop() ? 1 : 0,
                PacketType.LessThen => numbersForOperation.Pop() < numbersForOperation.Pop() ? 1 : 0,
                PacketType.EqualTo => numbersForOperation.Pop() == numbersForOperation.Pop() ? 1 : 0,
                _ => throw new InvalidOperationException(),
            };
        }
    }
}

enum LengthType
{
    Length,
    Packet
}

enum PacketType
{
    Sum,
    Product,
    Minimum,
    Maximum,
    Number,
    GreaterThen,
    LessThen,
    EqualTo
}