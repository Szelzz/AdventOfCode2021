var firstLine = File.ReadLines("input.txt").First();
var numbers = firstLine.Split(',', StringSplitOptions.RemoveEmptyEntries).Select(n => int.Parse(n)).ToList();

var boards = new List<Board>();
var currentGrid = new int[5, 5];
var currentRow = 0;
foreach (var line in File.ReadLines("input.txt").Skip(2))
{
    if (line == "")
        continue;

    var rowNumbers = line.Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(n => int.Parse(n)).ToList();
    for (int colNumber = 0; colNumber < 5; colNumber++)
    {
        currentGrid[currentRow, colNumber] = rowNumbers[colNumber];
    }
    currentRow++;
    if (currentRow == 5)
    {
        var b = new Board(currentGrid);
        boards.Add(b);
        currentGrid = new int[5, 5];
        currentRow = 0;
    }
}

foreach (var number in numbers)
{
    foreach (var board in boards)
    {
        if(board.NextNumber(number)){
            System.Console.WriteLine("Part 1: {0}", board.CalculateSum() * number);
            return;
        }
    }
}
