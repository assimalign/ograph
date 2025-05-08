using System;
using System.Text.Json;
using System.Xml;

namespace Assimalign.OGraph.Gdm.Tests;

using Elements;
using Objects;

public class GdmEmployeeId : GdmValueScalarType<EmployeeId>
{
    public GdmEmployeeId(GdmGraph graph) : base(graph)
    {
    }

    public override GdmPrimitiveType PrimitiveType => throw new NotImplementedException();

    public override EmployeeId Parse(string? value)
    {
        throw new NotImplementedException();
    }

    public override EmployeeId Read(ref Utf8JsonReader reader)
    {
        throw new NotImplementedException();
    }

    public override EmployeeId Read(XmlReader reader)
    {
        throw new NotImplementedException();
    }

    public override bool TryParse(string? value, out EmployeeId result)
    {
        throw new NotImplementedException();
    }

    public override void Write(Utf8JsonWriter writer, EmployeeId value)
    {
        throw new NotImplementedException();
    }

    public override void Write(XmlWriter writer, EmployeeId value)
    {
        throw new NotImplementedException();
    }
}
