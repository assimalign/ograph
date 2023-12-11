using System;
using System.Xml;
using System.Text.Json;

namespace Assimalign.OGraph.Gdm;

public sealed class GdmDoubleType : GdmPrimitiveType<Double>
{
    public override double Read(ref Utf8JsonReader reader)
    {
        throw new NotImplementedException();
    }

    public override double Read(XmlReader reader)
    {
        throw new NotImplementedException();
    }

    public override void Write(Utf8JsonWriter writer, double value)
    {
        throw new NotImplementedException();
    }

    public override void Write(XmlWriter writer, double value)
    {
        throw new NotImplementedException();
    }
}