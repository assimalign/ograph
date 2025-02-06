namespace Assimalign.OGraph;

/// <summary>
/// 
/// </summary>
public readonly struct GdmEnumValue
{
    GdmEnumValue(string name, object value)
    {
        Name = name;
        Value = value;
    }


    /// <summary>
    /// The string name of the enum.
    /// </summary>
    public string Name { get; }

    /// <summary>
    /// The underlying value of the enum.
    /// </summary>
    public object Value { get; }

    /* 
     * 
     */
    public static GdmEnumValue Create(string name, byte value) => new(name, value);
    public static GdmEnumValue Create(string name, sbyte value) => new(name, value);
    public static GdmEnumValue Create(string name, short value) => new(name, value);
    public static GdmEnumValue Create(string name, ushort value) => new(name, value);
    public static GdmEnumValue Create(string name, int value) => new(name, value);
    public static GdmEnumValue Create(string name, uint value) => new(name, value);
    public static GdmEnumValue Create(string name, long value) => new(name, value);
    public static GdmEnumValue Create(string name, ulong value) => new(name, value);
}