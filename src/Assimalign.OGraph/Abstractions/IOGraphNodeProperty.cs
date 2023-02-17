using System;

namespace Assimalign.OGraph;

public interface IOGraphNodeProperty
{
    bool IsKey { get; }
    bool IsComputed { get; }
    bool IsPagable { get; }
    bool IsFilterable { get; }

    /// <summary>
    /// The name of the property.
    /// </summary>
    Name PropertyName { get; }
    /// <summary>
    /// The OGraph Property Type.
    /// </summary>
    IOGraphType PropertyType { get; }
    /// <summary>
    /// 
    /// </summary>
    IOGraphNodePropertyMetadata Metadata { get; }
    /// <summary>
    /// 
    /// </summary>
    IOGraphNodePropertyMiddlewareCollection Middleware { get; }



    Type? RuntimeType { get; }

    string? RuntimeName { get; }
}
