using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml;

namespace Assimalign.OGraph;

/// <summary>
/// 
/// </summary>
public interface IOGraphCollectionType : IOGraphType
{
    /// <summary>
    /// Represents the item type that is contained inside the collection.
    /// </summary>
    IOGraphType ItemType { get; }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="reader"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    bool TryReadXml(XmlReader reader, out OGraphCollection collection);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="writer"></param>
    /// <returns></returns>
    bool TryWriteXml(XmlWriter writer, OGraphCollection collection);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="reader"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    bool TryReadJson(Utf8JsonReader reader, out OGraphCollection collection);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="writer"></param>
    /// <returns></returns>
    bool TryWriteJson(Utf8JsonWriter writer, OGraphCollection collection);
}
