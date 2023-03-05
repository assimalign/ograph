using System;
using System.Xml;
using System.Text.Json;
using System.Threading.Tasks;

namespace Assimalign.OGraph;

public abstract class ValueType<T> : IOGraphType
    where T : struct
{
    public Name TypeName => throw new NotImplementedException();

    public OGraphTypeIdentifier TypeIdentifier => throw new NotImplementedException();

    public Type? RuntimeType => throw new NotImplementedException();

    public bool IsRuntimeType => throw new NotImplementedException();

    public virtual bool TryReadJson(Utf8JsonReader reader, out object value)
    {
        value = default;

        
        throw new NotImplementedException();
    }
    public virtual bool TryWriteJson(Utf8JsonWriter writer, object value)
    {
        throw new NotImplementedException();
    }

    public virtual bool TryReadXml(XmlReader reader, out object value)
    {
        throw new NotImplementedException();
    }

    

    public virtual bool TryWriteXml(XmlWriter writer, object value)
    {
        throw new NotImplementedException();
    }
}
