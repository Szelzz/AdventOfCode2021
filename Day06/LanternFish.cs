public class LanternFish
{
    public int Counter { get; private set; }
    public LanternFish(int counter)
    {
        Counter = counter;
    }

    public LanternFish()
        : this(8)
    {
    }

    public bool NextDay()
    {
        Counter--;
        if (Counter == -1)
        {
            Counter = 6;
            return true;
        }
        return false;
    }
}
