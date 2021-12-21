using System.Collections;

static class BitArrayExtension
{
    public static bool GetTrueValue(this BitArray bits, int index)
    {
        var position = ((index / 8) * 8) + (8 - (index % 8)) - 1;
        return bits[position];
    }

    public static byte ToByte(this BitArray bits, int start, int length)
    {
        if (length > 8)
            throw new ArgumentException("Value too big", nameof(length));

        byte result = 0;

        for (int i = 0; i < length; i++)
        {
            var index = start + i;
            var b = bits.GetTrueValue(index);
            if (b)
                result |= (byte)(1 << (length - i - 1));
        }

        return result;
    }
}