using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph;

public interface IOGraphEnumType : IOGraphType
{
    /// <summary>
    /// 
    /// </summary>
    public EnumValue[] Values { get; }
}

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