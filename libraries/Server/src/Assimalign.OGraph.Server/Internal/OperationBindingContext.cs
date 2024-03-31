using System;
using System.Xml;
using System.Text.Json;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Claims;

namespace Assimalign.OGraph.Internal;

using Assimalign.OGraph.Gdm;
using Assimalign.OGraph.Syntax;

internal class OperationBindingContext : IOGraphOperationBindingContext
{
    public IOGraphGdmVertex Element { get; init; } = default!;
    public IOGraphExecutorRequest Request { get; init; } = default!;
    public IOGraphExecutorResponse Response { get; init; } = default!;
    public IOGraphGdmType RequestType { get; init; } = default!;
    public IServiceProvider ServiceProvider { get; init; } = default!;
    public QueryParser Parser { get; init; } = default!;
    IOGraphGdmLabeledElement IOGraphGdmBindingContext.Element => Element;


    public IOGraphOperationBinding Binding { get; init; } = default!;



    public Either<XmlWriter, Utf8JsonWriter> GetWriter()
    {



        throw new Exception();
    }

    public ClaimsPrincipal GetClaimsPrincipal()
    {
        throw new NotImplementedException();
    }

    public QueryDocument GetQueryDocument()
    {
        throw new NotImplementedException();
    }

    public OGraphQueryOptions GetQueryOptions()
    {
        throw new NotImplementedException();
    }

    public T GetQueryParam<T>()
    {
        throw new NotImplementedException();
    }

    public IOGraphQueryProvider GetQueryProvider()
    {
        throw new NotImplementedException();
    }

    public T GetRequestBody<T>()
    {
        return Request.Headers.Accept.Value switch
        {
            OGraphMediaType.Xml => GetXmlRequestBody<T>(),
            OGraphMediaType.Json => GetJsonRequestBody<T>()
        };
    }

    private T GetXmlRequestBody<T>()
    {
        throw new NotImplementedException();
    }

    private T GetJsonRequestBody<T>()
    {
        var buffer = new byte[0];
        var reader = new Utf8JsonReader();

        if (RequestType.Read(ref reader) is T value)
        {
            return value;
        }
        throw new Exception();
    }

    public T GetService<T>()
    {
        if (ServiceProvider.GetService(typeof(T)) is T service)
        {
            return service;
        }

        throw new Exception();
    }

    public T GetRouteValue<T>(string paramName)
    {
        var routeSegments = Binding.Route.Segments;
        var pathSegments = Request.Path.Segments;

        for (int i = 0; i < pathSegments.Length; i++)
        {
            var rs = routeSegments[i];
            var ps = pathSegments[i];

            if (rs.SegmentType == RouteSegmentType.Parameter && r) 
        }
    }
}