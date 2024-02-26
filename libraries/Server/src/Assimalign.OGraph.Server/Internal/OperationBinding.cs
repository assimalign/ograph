using System;
using System.Linq;
using System.Xml;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Assimalign.OGraph.Internal;

using Assimalign.OGraph.Gdm;
using Assimalign.OGraph.Syntax;

internal partial class OperationBinding : IOGraphOperationBinding
{
    public OperationBinding()
    {
        Middleware = new OperationBindingMiddlewareQueue();
    }

    public Label Label { get; set; }
    public Route Route { get; set; }
    public Method Method { get; set; }
    public OGraphQueryOptions QueryOptions { get; set; } = OGraphQueryOptions.Default;
    public IOGraphQueryProvider QueryProvider { get; set; }
    public IOGraphOperationBindingResolver Resolver { get; set; } = default!;
    public IOGraphOperationBindingMiddlewareQueue Middleware { get; }
    public IOGraphGdmTypeReference RequestType { get; set; } = default!;
    public IOGraphGdmTypeReference ResponseType { get; set; } = default!;
    public IOGraphOperationBindingHeaders Headers { get; set; } = default!;
    public IOGraphOperationBindingQueryParams Query { get; set; } = default!;
    public OperationType OperationType { get; set; }
    public async Task ExecuteAsync(IOGraphOperationBindingContext context, CancellationToken cancellationToken = default)
    {
        if (context is not OperationBindingContext ctx)
        {
            ThrowHelper.ThrowArgumentException("");
        }
        else
        {
            try
            {
                var request = ctx.Request;
                var response = ctx.Response;

                // 2. 415 Check - Let's check Content Length & Type header
                if (request.Headers.TryGetValue(HeaderKey.ContentLength, out var contentLength))
                {
                    var length = long.Parse(contentLength!);

                    if (length > 0 && request.Headers.TryGetValue(HeaderKey.ContentType, out var contentType))
                    {
                        var collection = (contentType as ICollection<string>);

                        if (!collection.Contains(OGraphMediaType.Json) && collection.Contains(OGraphMediaType.Xml))
                        {

                        }
                    }
                    // Unsupported Media Type
                    else
                    {

                    }
                }

                // 3. 406 Check - Check for accept header
                if (request.Headers.TryGetValue(HeaderKey.Accept, out var accept))
                {
                    var collection = accept as ICollection<string>;

                    // Accepts either any content-type or both OGraph content-type.
                    if (collection.Contains("*/*") || (collection.Contains(OGraphMediaType.Xml) && collection.Contains(OGraphMediaType.Json)))
                    {

                    }
                    else if (collection.Contains(OGraphMediaType.Xml))
                    {

                    }
                    else if (collection.Contains(OGraphMediaType.Json))
                    {

                    }
                    // The user requested an Unsupported media type. - 406 (Not Acceptable)
                    else
                    {
                        return ProcessErrorResultAsJsonAsync(new OGraph)
                    }
                }
                else
                {


                    var vertex = context.Element;
                    var handler = GetChain();
                    var result = await handler(ctx, cancellationToken);

                    var task = result switch
                    {
                        IOGraphErrorResult error => ProcessErrorResultAsync(error, ctx),
                        IOGraphQueryResult query => Task.CompletedTask,
                        IOGraphObjectResult value => Task.CompletedTask
                    };

                    return task;
                }
            }
            catch (Exception exception)
            {

            }
        }
    }
    Task IOGraphGdmBinding.ExecuteAsync(IOGraphGdmBindingContext context, CancellationToken cancellationToken = default)
    {
        if (context is not IOGraphOperationBindingContext)
        {
            ThrowHelper.ThrowInvalidOperationException("");
        }
        return ExecuteAsync((IOGraphOperationBindingContext)context, cancellationToken);
    }

    private async Task ProcessQueryResultAsync(OperationBindingContext context, IOGraphQueryResult result, CancellationToken cancellationToken = default)
    {
        var writer = new Either<XmlWriter, Utf8JsonWriter>(new Utf8JsonWriter(context.Response.Body));

        var element = context.Element;
        var elementEntity = element.GetGdmEntityType();

        var query = context.GetQueryDocument();
        var queryOptions = context.GetQueryOptions();

        var vertexNode = (VertexNode)query.Root;
        var projectionNode = vertexNode.Nodes.OfType<ProjectNode>().FirstOrDefault();
        var edgeNodes = vertexNode.Nodes.OfType<EdgeNode>();

        writer.Switch(xml => xml.WriteStartElement(""), json => json.WriteStartObject());

        if (projectionNode is null)
        {
            if (!queryOptions.DefaultProjectAll)
            {
                return;
            }

            writer.Switch(
                xml =>
                {

                },
                json =>
                {
                    json.WriteNumber("@ograph.status", result.StatusCode);
                    json.WriteNumber("@ograph.total", result.Total);
                    json.WriteNumber("@ograph.count", result.Count);
                });

            foreach (var item in result)
            {
                foreach (var property in elementEntity.Properties)
                {
                    // Check for property binding
                    if (property!.HasBinding<IOGraphPropertyBinding>(out var binding))
                    {

                    }
                }

                //elementEntity.Write()
            }
        }
        else
        {
            writer.Switch(
                xml =>
                {

                },
                json =>
                {
                    json.WritePropertyName("data");
                    json.WriteStartArray();
                });

            var propertyBindingContext = new PropertyBindingContext()
            {
                
            };



            foreach (var item in result)
            {
                writer.Switch(
                    xml => xml.WriteStartElement(element.Label),
                    json => json.WriteStartObject());

                foreach (var propertyNode in projectionNode!.Properties)
                {
                    var propertyName = propertyNode.Name!;
                    var tasks = new List<Task>();

                    if (element.TryGetProperty(propertyName, out var property))
                    {
                        if (propertyNode.HasChildren)
                        {
                            if (property!.Type.Definition is IOGraphGdmComplexType complexType)
                            {

                            }
                            else if (property!.Type.Definition is IOGraphGdmCollectionType collectionType &&
                                collectionType.ItemType is IOGraphGdmComplexType complexType1)
                            {

                            }
                        }
                        // Check for property binding
                        if (property!.HasBinding<IOGraphPropertyBinding>(out var binding))
                        {
                            tasks.Add(binding!.ExecuteAsync(propertyBindingContext, cancellationToken));
                        }
                        else
                        {

                        }
                    }
                    else
                    {
                        throw new InvalidOperationException("Invalid projection");
                    }
                }
                foreach (var edgeNode in edgeNodes)
                {

                }


                writer.Switch(
                    xml => xml.WriteEndElement(),
                    json => json.WriteEndObject());
            }

            writer.Switch(
                xml =>
                {

                },
                json =>
                {
                    json.WriteEndArray();
                });
        }

        writer.Switch(xml => xml.WriteEndElement(), json => json.WriteEndObject());
    }



    private OGraphOperationBindingMiddlewareHandler GetChain()
    {
        var index = 0;
        var root = new OGraphOperationBindingMiddlewareHandler(Resolver.InvokeAsync);

        if (Middleware.Count == 0)
        {
            return root;
        }

        return Chain(root);

        OGraphOperationBindingMiddlewareHandler Chain(OGraphOperationBindingMiddlewareHandler root)
        {
            var middleware = Middleware.Reverse().Skip(index).First();
            var next = new OGraphOperationBindingMiddlewareHandler((context, cancellationToken) =>
            {
                return middleware.InvokeAsync(context, cancellationToken, root);
            });
            if (index < Middleware.Count - 1)
            {
                index++;
                return Chain(next);
            }
            return next;
        }
    }
}