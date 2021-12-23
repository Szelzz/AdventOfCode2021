interface ISNumber
{
    ComplexNumber? Parent { get; set; }
    bool Split();
    long Magnitude();
}
