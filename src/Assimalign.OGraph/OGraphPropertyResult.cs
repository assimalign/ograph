using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;

namespace Assimalign.OGraph;

public sealed class OGraphPropertyResult : IOGraphPropertyResult
{


    public bool IsSuccess => this.Error is null;

    public IOGraphError? Error { get; init; }

    public ValueTask ExecuteAsync(XmlWriter writer, IOGraphPropertyContext context, CancellationToken cancellationToken = default)
    {
        var propertyType = context.GetPropertyType();

        if (propertyType.IsComplexType(out var complexType))
        {
            complexType.TryWriteXml()
        }
        throw new NotImplementedException();
    }

    public ValueTask ExecuteAsync(Utf8JsonWriter writer, IOGraphPropertyContext context, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}
