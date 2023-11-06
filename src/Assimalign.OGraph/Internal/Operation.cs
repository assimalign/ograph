using Assimalign.OGraph.Gdm;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Internal;

internal class Operation : IOGraphOperation
{
    public Operation()
    {
        
    }
    public Label Label { get; set; }
    public Route Route { get; set; }
    public Method Method { get; set; }
    public IOGraphOperationResolver Resolver { get; set; } = default!;
    public IOGraphOperationMiddlewareQueue Middleware { get; }

    public OGraphOperationHandler GetHandlerChain()
    {
        throw new NotImplementedException();
    }

    public async Task InvokeAsync(IOGraphGdmVertexBindingContext context, CancellationToken cancellationToken = default)
    {
        if (context is not IOGraphOperationResolverContext opContext)
        {
            throw new Exception();
        }

        var result = await GetHandlerChain().Invoke(opContext, cancellationToken);

        var vertex = opContext.Vertex;
        var vertexType = vertex.Type.Definition as IOGraphGdmEntityType;

        var writer = new Utf8JsonWriter(default(Stream), new()
        {
            Indented = false,
            SkipValidation = true // OGraph Typing should protect this
        });

        writer.WriteStartObject();

        foreach (var property in vertexType.Properties)
        {
            var propertyResolver = property.GetBindings()
                .OfType<IOGraphPropertyResolver>()
                .First();

            var propertyResult =  await propertyResolver.InvokeAsync(default!, cancellationToken);
        }

        switch (result)
        {
            case IOGraphQueryResult query:
                {
                    
                    break;
                }
            case IOGraphErrorResult error:
                {
                    break;
                }
        }

        writer.WriteEndObject();
    }


    private async Task HandleQueryResultAsync(IOGraphQueryResult result)
    {
        
    }
}
