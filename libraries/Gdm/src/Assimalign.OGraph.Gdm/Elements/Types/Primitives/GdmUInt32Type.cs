using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml;

namespace Assimalign.OGraph.Gdm;

public sealed class GdmUInt32Type : GdmPrimitiveType<UInt32>
{
    public override uint Read(ref Utf8JsonReader reader)
    {
        throw new NotImplementedException();
    }

    public override uint Read(XmlReader reader)
    {
        throw new NotImplementedException();
    }

    public override void Write(Utf8JsonWriter writer, uint value)
    {
        throw new NotImplementedException();
    }

    public override void Write(XmlWriter writer, uint value)
    {
        throw new NotImplementedException();
    }
}
