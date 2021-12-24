var player1StartingPosition = int.Parse(File.ReadLines("input.txt").First()[^2..]);
var player2StartingPosition = int.Parse(File.ReadLines("input.txt").Last()[^2..]);

//player2StartingPosition = 8;

var dice = new Dice();
var player1 = new Player(player1StartingPosition);
var player2 = new Player(player2StartingPosition);

while (true)
{
    player1.Move(dice);
    if (player1.Score >= 1000)
        break;

    player2.Move(dice);
    if (player2.Score >= 1000)
        break;
}
var looserScore = Math.Min(player1.Score, player2.Score);
Console.WriteLine("Part 1: {0}", looserScore * dice.RolledCount);

class Player
{
    public Player(int position)
    {
        this.position = position - 1;
    }

    public int Score { get; private set; }

    // keep 0 based position for easier calculations
    private int position;
    public int Position => position + 1;

    public void Move(Dice dice)
    {
        var value = position + dice.Roll() + dice.Roll() + dice.Roll();
        position = value % 10;
        Score += Position;
    }
}

class Dice
{
    public long RolledCount { get; private set; }

    int value = 1;
    public int Roll()
    {
        if (value == 101)
            value = 1;

        RolledCount++;
        return value++;
    }
}