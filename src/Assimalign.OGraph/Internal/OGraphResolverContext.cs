
using System;
using System.IO;
using System.Xml;
using System.Text.Json;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Assimalign.OGraph.Syntax;

namespace Assimalign.OGraph.Internal;

internal class OGraphResolverContext :
    IOGraphEdgeContext,
    IOGraphPropertyContext,
    IOGraphOperationContext
{
    #region Request Information
    internal Route RequestRoute { get; init; }
    internal Stream? RequestBody { get; init; }
    internal IOGraphType RequestType { get; init; }

    
    internal IServiceProvider? ServiceProvider { get; init; }

    #endregion

    internal volatile object? Parent;


    public T GetBodyValue<T>()
    {
        var type = typeof(T);

        if (RequestType.RuntimeType != type)
        {
            throw new ArgumentException("Type mismatch");
        }

        var memoise = Cacher<Type, T>.Memoise(type =>
        {
            RequestBody.Position = 0;


            throw new Exception("Unsupported Media Type");
        });

        return memoise.Invoke(type);
    }

    private T GetJsonBodyValue<T>()
    {
        if (RequestType is IOGraphComplexType complexType)
        {
            var buffer = new byte[RequestBody.Length];
            var span = new Span<byte>(buffer);

            RequestBody.Read(span);
            
            var reader = new Utf8JsonReader(span);

    
        }


        throw new Exception();
    }
    private T GetXmlBodyValue<T>()
    {
        if (RequestType is IOGraphComplexType complexType)
        {
            var reader = XmlReader.Create(RequestBody);

   
        }

        throw new Exception();
    }

    public ClaimsPrincipal GetClaimsPrincipal()
    {
        throw new NotImplementedException();
    }

    public T GetHeaderValue<T>(string headerName)
    {
        throw new NotImplementedException();
    }

    public T GetParent<T>()
    {
        if (Parent is T instance)
        {
            return instance;
        }
        throw new InvalidOperationException("");
    }

    public T GetQueryValue<T>(string parameterName)
    {
        throw new NotImplementedException();
    }

    public T GetRouteValue<T>(string parameterName)
    {
        throw new NotImplementedException();
    }

    public T GetService<T>()
    {
        throw new NotImplementedException();
    }

    public IOGraph GetGraph()
    {
        throw new NotImplementedException();
    }

    public IOGraphEdge GetEdge()
    {
        throw new NotImplementedException();
    }

    public QueryDocument GetQuery()
    {
        throw new NotImplementedException();
    }

    public IOGraphQueryProvider GetQueryProvider()
    {
        throw new NotImplementedException();
    }

    public IOGraphType GetPropertyType()
    {
        throw new NotImplementedException();
    }

    public IOGraphNode GetNode()
    {
        throw new NotImplementedException();
    }

    public IOGraphEdge GetEdge(Name name)
    {
        throw new NotImplementedException();
    }

    public OGraphQueryOptions GetQueryOptions()
    {
        throw new NotImplementedException();
    }

    public T GetRequestHeader<T>(string headerName)
    {
        throw new NotImplementedException();
    }

    public T GetRequestRouteValue<T>(string parameterName) where T : struct
    {
        return RequestRoute.GetRouteValue<T>(parameterName);
    }

    public T GetRequestQueryValue<T>(string parameterName)
    {
        throw new NotImplementedException();
    }

    public T GetRequestBody<T>()
    {
        throw new NotImplementedException();
    }

    public Stream GetRequestBody()
    {
        throw new NotImplementedException();
    }

    public Stream GetResponseBody()
    {
        throw new NotImplementedException();
    }
}
