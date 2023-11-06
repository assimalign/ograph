using System;
using System.IO;

namespace Assimalign.OGraph;

using Assimalign.OGraph.Gdm;
using Assimalign.OGraph.Syntax;

/// <summary>
/// 
/// </summary>
public interface IOGraphQueryContext
{
    /// <summary>
    /// The starting node/vertex of the query.
    /// </summary>
    IOGraphGdmVertex Vertex { get;  }
    /// <summary>
    /// The parsed query from the request.
    /// </summary>
    QueryDocument Query { get; }
    /// <summary>
    /// 
    /// </summary>
    IServiceProvider ServiceProvider { get; }
    /// <summary>
    /// 
    /// </summary>
    Stream Stream { get; }
}
