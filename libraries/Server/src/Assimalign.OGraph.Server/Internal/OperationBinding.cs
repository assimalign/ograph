using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Assimalign.OGraph.Internal;

using Assimalign.OGraph.Gdm;
using Assimalign.OGraph.Syntax;
using System.Collections;
using System.Text.Json;
using System.Xml;

internal class OperationBinding : IOGraphOperationBinding
{
    private static readonly IList<OperationBindingResultStrategy> strategies = new List<OperationBindingResultStrategy>();

    static OperationBinding()
    {

    }


    public OperationBinding()
    {
        
    }

    public Label Label { get; set; }
    public Route Route { get; set; }
    public Method Method { get; set; }
    public OGraphQueryOptions QueryOptions { get; set; }
    public IOGraphQueryProvider QueryProvider { get; set; }
    public IOGraphOperationBindingResolver Resolver { get; set; } = default!;
    public IOGraphOperationBindingMiddlewareQueue Middleware { get; }

    public IOGraphGdmTypeReference RequestType => throw new NotImplementedException();

    public IOGraphGdmTypeReference ResponseType => throw new NotImplementedException();

    public IOGraphOperationBindingHeaders Headers => throw new NotImplementedException();

    public IOGraphOperationBindingQueryParams QueryParams => throw new NotImplementedException();

    public OperationType OperationType => throw new NotImplementedException();

    private OGraphGdmVertexHandler GetHandler()
    {
        var index = 0;
        var root = new OGraphGdmVertexHandler(Resolver.InvokeAsync);

        if (Middleware.Count == 0)
        {
            return root;
        }

        return Chain(root);

        OGraphGdmVertexHandler Chain(OGraphGdmVertexHandler root)
        {
            var middleware = Middleware.Reverse().Skip(index).First();
            var next = new OGraphGdmVertexHandler((context, cancellationToken) =>
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


    public async Task ExecuteAsync(IOGraphOperationBindingContext context, CancellationToken cancellationToken = default)
    {
        if (context is not OperationBindingContext operationContext)
        {
            throw new ArgumentException();
        }
        try
        {
            var vertex = context.Element;
            var result = await GetHandler().Invoke(operationContext, cancellationToken);

            switch (result)
            {
                case IOGraphErrorResult errorResult:
                    {
                        break;
                    }
                case IOGraphQueryResult queryResult:
                    {
                        break;
                    }
                case IOGraphObjectResult objectResult:
                    {
                        break;
                    }
                
                default:
                    {
                        throw new Exception("Invalid result type");
                    }
            }
        }
        catch(Exception exception)
        {

        }
    }





    private async Task ProcessQueryResultAsync(OperationBindingContext context, IOGraphQueryResult result, CancellationToken cancellationToken = default)
    {

        var writer = new Either<XmlWriter, Utf8JsonWriter>(new Utf8JsonWriter(context.Response.Body));

        var element         = context.Element;
        var elementEntity   = element.GetGdmEntityType();

        var query           = context.GetQuery();
        var queryOptions    = context.GetQueryOptions();

        var vertexNode      = (query.Root as VertexNode)!;
        var projectionNode  = vertexNode.Nodes.OfType<ProjectionNode>().FirstOrDefault();
        var edgeNodes       = vertexNode.Nodes.OfType<EdgeNode>();

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
                writer.Switch(xml => { }, json => json.WriteStartObject());

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


                writer.Switch(xml => { }, json => json.WriteEndObject());
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


    Task IOGraphGdmBinding.ExecuteAsync(IOGraphGdmBindingContext context, CancellationToken cancellationToken = default)
    {
        if (context is not IOGraphOperationBindingContext)
        {
            ThrowHelper.ThrowInvalidOperationException("");
        }
        return ExecuteAsync((IOGraphOperationBindingContext)context, cancellationToken);
    }
}
