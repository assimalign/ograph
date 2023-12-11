using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml;

namespace Assimalign.OGraph.Gdm;

public sealed class GdmHalfType : GdmPrimitiveType<Half>
{
    public override Half Read(ref Utf8JsonReader reader)
    {
        throw new NotImplementedException();
    }

    public override Half Read(XmlReader reader)
    {
        throw new NotImplementedException();
    }

    public override void Write(Utf8JsonWriter writer, Half value)
    {
        throw new NotImplementedException();
    }

    public override void Write(XmlWriter writer, Half value)
    {
        throw new NotImplementedException();
    }
}
