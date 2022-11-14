using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Syntax;

/// <summary>
/// The query tree represents the complete parsed expression tree.
/// </summary>
public sealed class QueryTree
{
    private IEnumerable<QueryNode> nodes = new List<QueryNode>();

    /// <summary>
    /// Represents the root nodes of the expression tree.
    /// </summary>
    public IEnumerable<QueryNode> Nodes => this.nodes;
}
