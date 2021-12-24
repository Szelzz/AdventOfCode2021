var input = File.ReadLines("input.txt");

var algorythm = input.First();

var imageSize = input.Count() - 2;
var image = new bool[imageSize, imageSize];
var ypos = 0;

foreach (var line in input.Skip(2))
{
    for (int x = 0; x < imageSize; x++)
        image[x, ypos] = line[x] == '#';
    ypos++;
}


int CountLit(bool[,] image)
{
    var counter = 0;
    for (int i = 0; i < image.GetLength(0); i++)
    {
        for (int j = 0; j < image.GetLength(1); j++)
        {
            if (image[i, j])
                counter++;
        }
    }
    return counter;
}

bool[,] DecodeImage(bool[,] image, ref bool outsideValue)
{
    var imageSize = image.GetLength(0);
    var newImage = new bool[imageSize + 2, imageSize + 2];
    for (int y = -1; y <= imageSize; y++)
    {
        for (int x = -1; x <= imageSize; x++)
        {
            newImage[x + 1, y + 1] = algorythm[TakeSquare(image, x, y, outsideValue)] == '#';
        }
    }
    outsideValue = !outsideValue;

    return newImage;
}

int TakeSquare(bool[,] image, int x, int y, bool outsideValue)
{
    // image is square so max size for x and y are equal
    var maxSize = image.GetLength(0);
    var number = 0;
    for (int yt = -1; yt <= 1; yt++)
    {
        for (int xt = -1; xt <= 1; xt++)
        {
            var newX = x + xt;
            var newY = y + yt;
            if (newX < 0 || newY < 0 || newX >= maxSize || newY >= maxSize)
            {
                if (outsideValue)
                    number |= 1;
            }
            else if (image[newX, newY])
            {
                number |= 1;
            }
            number <<= 1;
        }
    }
    number >>= 1;
    return number;
}

var outsideValue = false;
image = DecodeImage(image, ref outsideValue);
image = DecodeImage(image, ref outsideValue);

Console.WriteLine("Part 1: {0}", CountLit(image));

// Part 2

for (int i = 0; i < 48; i++)
{
    image = DecodeImage(image, ref outsideValue);
}

Console.WriteLine("Part 1: {0}", CountLit(image));