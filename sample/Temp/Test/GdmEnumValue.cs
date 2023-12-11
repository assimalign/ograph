using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Temp.Test;

public struct GdmEnumValue
{
    public GdmEnumValue(string name, byte value)
        : this(name, value as object)
    {
    }
    public GdmEnumValue(string name, sbyte value)
        : this(name, value as object)
    {
    }
    public GdmEnumValue(string name, short value)
        : this(name, value as object)
    {
    }
    public GdmEnumValue(string name, ushort value)
        : this(name, value as object)
    {
    }
    public GdmEnumValue(string name, int value)
        : this(name, value as object)
    {
    }
    public GdmEnumValue(string name, uint value)
        : this(name, value as object)
    {
    }
    public GdmEnumValue(string name, long value)
        : this(name, value as object)
    {
    }
    public GdmEnumValue(string name, ulong value)
        : this(name, value as object)
    {
    }
    private GdmEnumValue(string name, object value)
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
}
