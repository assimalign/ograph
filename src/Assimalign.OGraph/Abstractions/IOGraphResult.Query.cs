using System;
using System.Collections.Generic;

namespace Assimalign.OGraph;

/// <summary>
/// 
/// </summary>
public interface IOGraphQueryResult : IOGraphResult
{
    /// <summary>
    /// The query error the occurred.
    /// </summary>
    IOGraphError Error { get; }
    /// <summary>
    /// 
    /// </summary>
    IOGraphVertexCollection Nodes { get; }
}





public interface IOGraphEntryCollection : IEnumerable<IOGraphNodeCo>
{
    
}
public interface IOGraphNodeCo : IEnumerable<IOGraphQueryResultNodeEntry>
{
    IOGraphQueryResultNodeEdges Edges { get; }
}

public interface IOGraphQueryResultNodeEntry
{
    Name Key { get; }
    object Value { get; }
}
public interface IOGraphQueryResultNodeEdges : IDictionary<Name, IOGraphQueryResult>
{

}

