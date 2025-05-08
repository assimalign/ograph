using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml;

namespace Assimalign.OGraph.Gdm.Elements;

public sealed class GdmHalfType : GdmValueScalarType<Half>
{
    public GdmHalfType(GdmGraph graph) : base(graph) { }
    public override GdmPrimitiveType PrimitiveType { get; } = GdmPrimitiveType.Float;
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
    public override Half Parse(string? value)
    {
        return Half.Parse(value!);
    }
    public override bool TryParse(string? value, out Half result)
    {
        return Half.TryParse(value!, out result);  
    }
}
