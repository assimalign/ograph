namespace Assimalign.OGraph;

public readonly struct RouteSegment
{
    internal RouteSegment(string value, bool isParameter = false, bool isLiteral = false)
    {
        this.Value = value;
        this.IsParameter = isParameter;
        this.IsLiteral = isLiteral;
    }
    /// <summary>
    /// The raw segment value.
    /// </summary>
    public string Value { get; }
    /// <summary>
    /// A
    /// </summary>
    public bool IsParameter { get; }
    /// <summary>
    /// A literal value 
    /// </summary>
    public bool IsLiteral { get; }


    public override string ToString()
    {
        return IsParameter ? $"{{{Value}}}" : Value;
    }
}
