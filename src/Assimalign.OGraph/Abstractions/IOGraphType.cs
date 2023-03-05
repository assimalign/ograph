using System;
using System.Xml;
using System.Text.Json;

namespace Assimalign.OGraph;

/// <summary>
/// Types represent primitive, complex, or collection structure that can be 
/// used to define a property, inputs, and outputs of operations within the graph.
/// </summary>
public interface IOGraphType
{
    /// <summary>
    /// The name of the type.
    /// </summary>
    Name TypeName { get; }
    /// <summary>
    /// 
    /// </summary>
    OGraphTypeIdentifier TypeIdentifier { get; }
    /// <summary>
    /// 
    /// </summary>
    Type? RuntimeType { get; }
    /// <summary>
    /// 
    /// </summary>
    bool IsRuntimeType { get; }
    ///// <summary>
    ///// 
    ///// </summary>
    ///// <param name="reader"></param>
    ///// <param name="value"></param>
    ///// <returns></returns>
    //bool TryReadXml(XmlReader reader, out object value);
    ///// <summary>
    ///// 
    ///// </summary>
    ///// <param name="writer"></param>
    ///// <returns></returns>
    //bool TryWriteXml(XmlWriter writer, object value);
    ///// <summary>
    ///// 
    ///// </summary>
    ///// <param name="reader"></param>
    ///// <param name="value"></param>
    ///// <returns></returns>
    //bool TryReadJson(Utf8JsonReader reader, out object value);
    ///// <summary>
    ///// 
    ///// </summary>
    ///// <param name="writer"></param>
    ///// <returns></returns>
    //bool TryWriteJson(Utf8JsonWriter writer, object value);
}
