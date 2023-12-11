using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml;

namespace Assimalign.OGraph.Gdm;

public sealed class GdmFloatType : GdmPrimitiveType<Single>
{
    public override float Read(ref Utf8JsonReader reader)
    {
        throw new NotImplementedException();
    }

    public override float Read(XmlReader reader)
    {
        throw new NotImplementedException();
    }

    public override void Write(Utf8JsonWriter writer, float value)
    {
        throw new NotImplementedException();
    }

    public override void Write(XmlWriter writer, float value)
    {
        throw new NotImplementedException();
    }
}
