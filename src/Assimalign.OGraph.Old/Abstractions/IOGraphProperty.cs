using System.Threading;
using System.Threading.Tasks;

namespace Assimalign.OGraph;

/// <summary>
/// 
/// </summary>
public interface IOGraphProperty
{
    /// <summary>
    /// The property name.
    /// </summary>
    Label Name { get; }
    /// <summary>
    /// The OGraph Property Type.
    /// </summary>
    IOGraphType Type { get; }
    /// <summary>
    /// Metadata of the property.
    /// </summary>
    IOGraphMetadata Metadata { get; }
    /// <summary>
    /// Specifies whether the property the primary key.
    /// </summary>
    bool IsKey { get; }
    /// <summary>
    /// Computed properties extend complex or entity types that 
    /// are invoked at runtime.
    /// </summary>
    bool IsComputed { get; }
    /// <summary>
    /// 
    /// </summary>
    bool IsNullable { get; }






    /// <summary>
    /// 
    /// </summary>
    IOGraphPropertyResolver Resolver { get; }
    /// <summary>
    /// The collection of middleware to execute before the resolver.
    /// </summary>
    IOGraphPropertyMiddlewareQueue Middleware { get; }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="context"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<IOGraphResult> ExecuteAsync(IOGraphPropertyContext context, CancellationToken cancellationToken = default);
}
