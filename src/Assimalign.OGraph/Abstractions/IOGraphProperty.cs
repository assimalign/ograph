using System;

namespace Assimalign.OGraph;

public interface IOGraphProperty
{
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




    bool IsNullable { get; }
    bool IsFilterable { get; }
    bool IsPagable { get; }
    bool IsSortable { get; }
}
