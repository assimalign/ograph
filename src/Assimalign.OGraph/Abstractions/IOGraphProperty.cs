using System;

namespace Assimalign.OGraph;

public interface IOGraphProperty
{
    //bool IsKey { get; }
    //bool IsComputed { get; }
    //bool IsPagable { get; }
    //bool IsFilterable { get; }

    /// <summary>
    /// The name of the property.
    /// </summary>
    Name Name { get; }
    /// <summary>
    /// The OGraph Property Type.
    /// </summary>
    IOGraphType Type { get; }
    /// <summary>
    /// 
    /// </summary>
    IOGraphMetadata Metadata { get; }
    /// <summary>
    /// 
    /// </summary>
    IOGraphPropertyResolver Resolver { get; }
    /// <summary>
    /// 
    /// </summary>
    IOGraphPropertyMiddlewareQueue Middleware { get; }



    Type? RuntimeType { get; }

    string? RuntimeName { get; }
}
