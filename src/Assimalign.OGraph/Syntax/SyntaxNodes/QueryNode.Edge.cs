using System;
using System.Linq;

namespace Assimalign.OGraph.Syntax;

public sealed class EdgeNode : IdentifierNode
{
    internal EdgeNode() { }
    public EdgeNode(string name)
    {
        this.Name = name;
    }

    /// <inheritdoc />
    public override QueryNodeType NodeType => QueryNodeType.Edge;

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
        return Name?.Split('/') ?? new string[0];
    }
}
