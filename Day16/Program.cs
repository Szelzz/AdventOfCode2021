using System.Collections;

var messageHex = File.ReadAllText("input.txt");
messageHex = "9C0141080250320F1802104A08";
var messageBytes = new byte[messageHex.Length / 2];
for (int i = 0; i < messageHex.Length; i += 2)
{
    messageBytes[i / 2] = Convert.ToByte(messageHex.Substring(i, 2), 16);
}

var message = new BitArray(messageBytes);

var allPackets = new List<Packet>();
var position = 0;
while (position <= (message.Length - 8))
{
    var packet = ParsePacket(message, ref position);
    allPackets.Add(packet);
}

Console.WriteLine("Part 1: {0}", allPackets.Sum(p => p.Version));

// Part 2



var operationStack = new Stack<Packet>();
var numbers = new Stack<long>();
var packetLength = 0;
foreach (var packet in allPackets)
{
    packetLength += packet.Size;
    if (packet.Type == PacketType.Number)
    {
        numbers.Push(packet.Value);
        var lastOperation = operationStack.Peek();

        if ((lastOperation.LengthType == LengthType.Packet
                && lastOperation.SubpacketsSize <= numbers.Count)
            || (lastOperation.LengthType == LengthType.Length
                && lastOperation.SubpacketsSize <= packetLength - lastOperation.Position - lastOperation.Size))
        {
            var operation = operationStack.Pop();
            var numbersForOperation = new Stack<long>();
            var max = numbers.Count;
            for (int i = 0; i < max; i++)
                numbersForOperation.Push(numbers.Pop());

            numbers.Push(operation.Evaluate(numbersForOperation));
        }
    }
    else
    {
        operationStack.Push(packet);
    }
}

if (numbers.Count != 1)
    throw new Exception();

Console.WriteLine("Part 2: {0}", numbers.Pop());


static Packet ParsePacket(BitArray message, ref int position)
{
    var startPosition = position;
    var packetVersion = message.ToByte(position, 3);
    position += 3;
    var packetType = (PacketType)message.ToByte(position, 3);
    position += 3;
    if (packetType == PacketType.Number)
    {
        var valueBytes = new List<byte>();
        for (int i = 0; ; i += 5)
        {
            var isEnd = !message.GetTrueValue(position++);
            valueBytes.Add(message.ToByte(position, 4));

            position += 4;
            if (isEnd)
                break;
        }
        long value = 0;
        if (valueBytes.Count > 8)
            throw new InvalidOperationException();
        for (int i = 0; i < valueBytes.Count; i++)
        {
            value <<= 4;
            value |= ((long)valueBytes[i]);
        }

        return new Packet()
        {
            Type = packetType,
            Version = packetVersion,
            Value = value,
            Size = position - startPosition,
            Position = startPosition
        };
    }
    else
    {
        // operator
        var lengthType = message.GetTrueValue(position++) ? LengthType.Packet : LengthType.Length;
        int length;
        if (lengthType == LengthType.Packet)
        {
            //11 bits
            var byteArray = new byte[]
            {
                0,0,
                message.ToByte(position, 3),
                message.ToByte(position + 3, 8)
            };
            position += 11;
            if (BitConverter.IsLittleEndian)
                Array.Reverse(byteArray);

            length = BitConverter.ToInt32(byteArray, 0);
        }
        else
        {
            //15 bits
            var byteArray = new byte[]
            {
                0,0,
                message.ToByte(position, 7),
                message.ToByte(position + 7, 8)
            };
            position += 15;
            if (BitConverter.IsLittleEndian)
                Array.Reverse(byteArray);

            length = BitConverter.ToInt32(byteArray, 0);
        }

        return new Packet()
        {
            Type = packetType,
            Version = packetVersion,
            SubpacketsSize = length,
            Size = position - startPosition,
            Position = startPosition,
            LengthType = lengthType
        };
    }
}