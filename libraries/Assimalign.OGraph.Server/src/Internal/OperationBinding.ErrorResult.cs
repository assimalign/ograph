using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Internal;

internal partial class OperationBinding
{
    private async Task ProcessErrorResultAsync(IOGraphErrorResult result, OperationBindingContext context)
    {
        var hasAcceptHeader = context.Request.Headers.TryGetValue("Accept", out var accept);
        if (hasAcceptHeader)
        {
            if (accept.Equals(new[] { OGraphMediaType.Xml, OGraphMediaType.Json}))
            {

            }
            if (accept.Equals(OGraphMediaType.Json))
            {

            }
            if (accept.Equals(OGraphMediaType.Xml))
            {

            }
        }
    }

    private Task ProcessErrorResultAsJsonAsync(IOGraphErrorResult result, OperationBindingContext context)
    {
        var writer = new Utf8JsonWriter(context.Response.Body);

        writer.WriteStartObject();
        writer.WriteNumber("@ograph.status", result.StatusCode);
        writer.WritePropertyName("error");
        writer.WriteStartObject();
        writer.WriteString("code", result.Error.Code);
        writer.WriteString("message", result.Error.Message);

        if (result.Error.Details is not null && result.Error.Details.Any())
        {
            writer.WriteStartArray();
            foreach (var detail in result.Error.Details)
            {
                writer.WriteStartObject();
                writer.WriteString("title", detail.Title);
                writer.WriteString("message", detail.Message);
                writer.WriteEndObject();
            }
            writer.WriteEndArray();
        }

        writer.WriteEndObject();
        writer.WriteEndObject();

        return Task.CompletedTask;
    }
}
