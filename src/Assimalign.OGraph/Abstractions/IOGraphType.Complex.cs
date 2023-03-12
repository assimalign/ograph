using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Text.Json;
using System.Threading.Tasks;

namespace Assimalign.OGraph;

public interface IOGraphComplexType : IOGraphType
{
    /// <summary>
    /// 
    /// </summary>
    IOGraphPropertyCollection Properties { get; }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="reader"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    bool TryReadXml(XmlReader reader, out OGraphObject item);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="writer"></param>
    /// <returns></returns>
    bool TryWriteXml(XmlWriter writer, OGraphObject item);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="reader"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    bool TryReadJson(Utf8JsonReader reader, out OGraphObject item);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="writer"></param>
    /// <returns></returns>
    bool TryWriteJson(Utf8JsonWriter writer, OGraphObject item);
}
