class Group
{
    public Group(int value){
        AddValue(value);
    }
    private int count = 0;
    public int Value { get; private set; }

    public bool IsFull()
        => count == 3;

    // returns true if group full
    public void AddValue(int value)
    {
        if (IsFull())
            return;

        count++;
        Value += value;
    }
}