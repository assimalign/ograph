using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml;

namespace Assimalign.OGraph.Gdm;

public sealed class GdmUInt16Type : GdmPrimitiveType<UInt16>
{
    public override ushort Read(ref Utf8JsonReader reader)
    {
        throw new NotImplementedException();
    }

    public override ushort Read(XmlReader reader)
    {
        throw new NotImplementedException();
    }

    public override void Write(Utf8JsonWriter writer, ushort value)
    {
        throw new NotImplementedException();
    }

    public override void Write(XmlWriter writer, ushort value)
    {
        throw new NotImplementedException();
    }
}
