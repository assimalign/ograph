using System;
using System.Xml;
using System.Text.Json;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Gdm;

public sealed class GdmUInt64Type : GdmPrimitiveType<UInt64>
{
    public override ulong Read(ref Utf8JsonReader reader)
    {
        throw new NotImplementedException();
    }

    public override ulong Read(XmlReader reader)
    {
        throw new NotImplementedException();
    }

    public override void Write(Utf8JsonWriter writer, ulong value)
    {
        throw new NotImplementedException();
    }

    public override void Write(XmlWriter writer, ulong value)
    {
        throw new NotImplementedException();
    }
}
