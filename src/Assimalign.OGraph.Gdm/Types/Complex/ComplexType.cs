using System;
using System.Text.Json;
using System.Xml;

namespace Assimalign.OGraph.Gdm;

//using Assimalign.OGraph.Internal;

public class ComplexType : IOGraphGdmComplexType
{
    public ComplexType()
    {
        //this.Properties = new PropertyCollection();
    }

    public Label Label { get; init; }
    public TypeKind Kind => TypeKind.Complex;
    public IOGraphGdmPropertyCollection Properties { get; }
    public Type? RuntimeType { get; init; }
    public virtual bool IsAssignableTo(IOGraphGdmType type)
    {
        return RuntimeType!.IsAssignableFrom(type.RuntimeType);
    }

    public object Read(ref Utf8JsonReader reader)
    {
        throw new NotImplementedException();
    }

    public object Read(XmlReader reader)
    {
        throw new NotImplementedException();
    }

    public void Write(Utf8JsonWriter writer, object value)
    {
        throw new NotImplementedException();
    }

    public void Write(XmlWriter writer, object value)
    {
        throw new NotImplementedException();
    }
}
