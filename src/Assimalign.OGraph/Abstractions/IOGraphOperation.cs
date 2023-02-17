using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph;

/// <summary>
/// Represents a single HTTP REST operation.
/// </summary>
public interface IOGraphOperation
{
    /// <summary>
    /// The name of the command.
    /// </summary>
    Name Name { get; }
    /// <summary>
    /// The route associated with this operation.
    /// </summary>
    Route Route { get; }
    /// <summary>
    /// 
    /// </summary>
    Method Method { get; }
    /// <summary>
    /// 
    /// </summary>
    bool IsEnabled { get; }
    /// <summary>
    /// 
    /// </summary>
    IOGraphType? RequestType { get; }
    /// <summary>
    /// 
    /// </summary>
    IOGraphType? ResponseType { get; }
    /// <summary>
    /// 
    /// </summary>
    IOGraphOperationResolver Resolver { get; }
}
