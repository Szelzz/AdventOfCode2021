using System.Collections;

var messageHex = File.ReadAllText("input.txt");

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

static Packet ParsePacket(BitArray message, ref int position)
{
    var packetVersion = message.ToByte(position, 3);
    position += 3;
    var packetType = message.ToByte(position, 3);
    position += 3;
    if (packetType == 4)
    {
        for (int i = 0; ; i += 5)
        {
            var prefix = message.GetTrueValue(position);
            //var next = message.ToByte(position, 4
            position += 5;

            if (!prefix)
                break;
        }
        return new Packet()
        {
            Type = packetType,
            Version = packetVersion
        };
    }
    else
    {
        // operator
        var lengthType = message.GetTrueValue(position);
        position++;
        int length;
        if (lengthType)
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
            Version = packetVersion
        };
    }
}