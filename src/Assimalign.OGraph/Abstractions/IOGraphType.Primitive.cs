using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Text.Json;


namespace Assimalign.OGraph;

public interface IOGraphPrimitiveType : IOGraphType
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="reader"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    bool TryReadXml(XmlReader reader, out OGraphValue value);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="writer"></param>
    /// <returns></returns>
    bool TryWriteXml(XmlWriter writer, OGraphValue value);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="reader"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    bool TryReadJson(Utf8JsonReader reader, out OGraphValue value);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="writer"></param>
    /// <returns></returns>
    bool TryWriteJson(Utf8JsonWriter writer, OGraphValue value);
}



public readonly struct OGraphCollection
{

}

public readonly struct OGraphPropertyItem
{
    /// <summary>
    /// 
    /// </summary>
    public Name Name { get; init; }
    /// <summary>
    /// 
    /// </summary>
    public OGraphValue Value { get; init; } 
}

public readonly struct OGraphValue
{
    public OGraphValue(object value)
    {
        this.Value = value;
        this.ValueType = value.GetType();
    }
    public object? Value { get; init; }

    public Type ValueType { get; }
}
