using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml;

namespace Assimalign.OGraph.Gdm;

public sealed class GdmTimeType : GdmPrimitiveType<TimeOnly>
{
    public GdmTimeType() : base("Time")
    {
        
    }

    public override TimeOnly Read(ref Utf8JsonReader reader)
    {
        throw new NotImplementedException();
    }

    public override TimeOnly Read(XmlReader reader)
    {
        throw new NotImplementedException();
    }

    public override void Write(Utf8JsonWriter writer, TimeOnly value)
    {
        throw new NotImplementedException();
    }

    public override void Write(XmlWriter writer, TimeOnly value)
    {
        throw new NotImplementedException();
    }
}