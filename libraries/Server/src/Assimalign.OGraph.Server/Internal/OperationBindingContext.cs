using System;
using System.Xml;
using System.Text.Json;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Internal;

using Assimalign.OGraph.Gdm;
using Assimalign.OGraph.Syntax;


internal class OperationBindingContext : IOGraphOperationBindingContext, IOGraphOperationBindingResolverContext
{

    public volatile object Parent;
    //public Either<XmlReader, Utf8JsonReader> Reader { get; init; }
    public Either<XmlWriter, Utf8JsonWriter> Writer { get; init; }
    public IOGraphGdmVertex Element { get; init; } = default!;
    public IOGraphRequest Request { get; init; } = default!;
    public IOGraphResponse Response { get; init; } = default!;
    public IServiceProvider ServiceProvider { get; init; } = default!;

    IOGraphGdmElement IOGraphGdmBindingContext.Element => Element;

    /// <inheritdoc />
    public T GetParent<T>()
    {
        if (Parent is T parent)
        {
            return parent;
        }
        throw new InvalidOperationException();
    }

    public QueryDocument GetQuery()
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
        throw new NotImplementedException();
    }

    public T GetService<T>()
    {
        throw new NotImplementedException();
    }
}