class Board
{
    public int[,] grid = new int[5, 5];
    private int[] rowScore = new int[5];
    private int[] colScore = new int[5];

    private bool BINGO = false;
    public Board(int[,] grid)
    {
        this.grid = grid;
    }

    public bool NextNumber(int number)
    {
        if (BINGO)
            return false;
        for (int r = 0; r < 5; r++)
        {
            for (int c = 0; c < 5; c++)
            {
                if (grid[r, c] == number)
                {
                    rowScore[r]++;
                    colScore[c]++;
                    grid[r, c] = -1; // mark number
                    if (rowScore[r] == 5 || colScore[c] == 5)
                    {
                        BINGO = true;
                        return true;
                    }
                }
            }
        }

        return false;
    }

    public int CalculateSum()
    {
        var sum = 0;
        for (int r = 0; r < 5; r++)
        {
            for (int c = 0; c < 5; c++)
            {
                if (grid[r, c] != -1)
                    sum += grid[r, c];
            }
        }

        return sum;
    }
}