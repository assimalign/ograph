using System;
using System.Collections.Generic;
using System.Linq;

namespace Assimalign.OGraph.Syntax;

public abstract class EdgeNode : QueryNode
{
    public EdgeNode() { }

    /// <summary>
    /// Represents the Edge Path from the Parent Node.
    /// </summary>
    public string? Path { get; init; }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public Name? GetEdgeName()
    {
        return GetSegments().Last();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public string[] GetSegments()
    {
        return Path?.Split('/') ?? new string[0];
    }
}
