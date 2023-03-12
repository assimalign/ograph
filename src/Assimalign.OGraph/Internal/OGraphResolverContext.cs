
using System;
using System.IO;
using System.Xml;
using System.Text.Json;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Internal;

using Assimalign.OGraph.Execution;


internal class OGraphResolverContext :
    IOGraphEdgeResolverContext,
    IOGraphPropertyResolverContext,
    IOGraphOperationResolverContext
{
    #region Request Information
    internal Route RequestRoute { get; init; }
    internal Stream? RequestBody { get; init; }
    internal IOGraphType RequestType { get; init; }
    internal IOGraphQueryCollection RequestQuery { get; init; }
    internal IOGraphHeaderCollection? RequestHeaders { get; init; }

    
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

            if (RequestHeaders.ContentType == OGraphMediaType.Json)
            {
                return GetJsonBodyValue<T>();
            }
            if (RequestHeaders.ContentType == OGraphMediaType.Xml)
            {
                return GetXmlBodyValue<T>();
            }

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

            if (!complexType.TryReadJson(reader, out var value))
            {
                throw new Exception();
            }
        }


        throw new Exception();
    }
    private T GetXmlBodyValue<T>()
    {
        if (RequestType is IOGraphComplexType complexType)
        {
            var reader = XmlReader.Create(RequestBody);

            if (!complexType.TryReadXml(reader, out var value))
            {
                throw new Exception();
            }
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
}
