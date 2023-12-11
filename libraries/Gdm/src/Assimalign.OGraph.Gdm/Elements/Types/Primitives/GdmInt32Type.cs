using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml;

namespace Assimalign.OGraph.Gdm;

public sealed class GdmInt32Type : GdmPrimitiveType<Int32>
{
    public override int Read(ref Utf8JsonReader reader)
    {
        throw new NotImplementedException();
    }

    public override int Read(XmlReader reader)
    {
        throw new NotImplementedException();
    }

    public override void Write(Utf8JsonWriter writer, int value)
    {
        throw new NotImplementedException();
    }

    public override void Write(XmlWriter writer, int value)
    {
        throw new NotImplementedException();
    }
}
