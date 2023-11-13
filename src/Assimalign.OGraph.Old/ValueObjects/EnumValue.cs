namespace Assimalign.OGraph;

/// <summary>
/// 
/// </summary>
public struct EnumValue
{
    public EnumValue(string name, object value)
    {
        Name = name;
        Value = value;
    }
    /// <summary>
    /// 
    /// </summary>
    public string Name { get; }
    /// <summary>
    /// 
    /// </summary>
    public object Value { get; }
}