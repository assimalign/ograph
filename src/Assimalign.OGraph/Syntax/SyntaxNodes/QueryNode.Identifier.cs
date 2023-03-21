using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Syntax;

public abstract class IdentifierNode : QueryNode
{
    /// <summary>
    /// 
    /// </summary>
    public string? Name { get; init; }
}
