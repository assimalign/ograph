using System;
using System.Text.Json;
using System.Xml;

namespace Assimalign.OGraph.Gdm;

using Assimalign.OGraph.Gdm.Internal;

public sealed class GdmDecimalType : GdmPrimitiveType<Decimal>
{
    public override decimal Read(ref Utf8JsonReader reader)
    {
        if (reader.TokenType != JsonTokenType.Number)
        {
            GdmThrowHelper.ThrowInvalidContentException("");
        }
        if (!reader.TryGetDecimal(out var value))
        {
            GdmThrowHelper.ThrowInvalidContentException("");
        }
        return value;
    }

    public override decimal Read(XmlReader reader)
    {
        throw new NotImplementedException();
    }

    public override void Write(Utf8JsonWriter writer, decimal value)
    {
        throw new NotImplementedException();
    }

    public override void Write(XmlWriter writer, decimal value)
    {
        throw new NotImplementedException();
    }
}
