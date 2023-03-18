namespace Assimalign.OGraph.Internal;

/// <summary>
/// 
/// </summary>
internal readonly struct OGraphValue
{
    internal OGraphValue(object value)
    {
        this.Value = value;
    }
    /// <summary>
    /// 
    /// </summary>
    public object Value { get; }

    public static OGraphValue Create<T>(T value) where T : struct
    {
        return new OGraphValue(value);
    }
}