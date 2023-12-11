using System;

namespace Assimalign.OGraph;

using Assimalign.OGraph.Gdm;
using Assimalign.OGraph.Syntax;

/// <summary>
/// 
/// </summary>
public interface IOGraphQueryProviderContext
{
    object QueryItem { get; }

    /// <summary>
    /// The parsed query from the request.
    /// </summary>
    QueryDocument QueryDocument { get; }
    /// <summary>
    /// The starting node/vertex of the query.
    /// </summary>
    IOGraphGdmElement Element { get; }
    /// <summary>
    /// The service provider to be used in the query.
    /// </summary>
    IServiceProvider ServiceProvider { get; }
}