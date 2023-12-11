using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml;

namespace Assimalign.OGraph.Gdm;

public sealed class GdmInt64Type : GdmPrimitiveType<Int64>
{
    public override long Read(ref Utf8JsonReader reader)
    {
        throw new NotImplementedException();
    }

    public override long Read(XmlReader reader)
    {
        throw new NotImplementedException();
    }

    public override void Write(Utf8JsonWriter writer, long value)
    {
        throw new NotImplementedException();
    }

    public override void Write(XmlWriter writer, long value)
    {
        throw new NotImplementedException();
    }
}
