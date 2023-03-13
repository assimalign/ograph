using System;
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