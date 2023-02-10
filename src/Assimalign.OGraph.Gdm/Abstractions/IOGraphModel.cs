using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Modeling;

public interface IOGraphModel
{
    /// <summary>
    /// 
    /// </summary>
    IOGraphNodeCollection Nodes { get; }
    /// <summary>
    /// 
    /// </summary>
    IOGraphTypeCollection Types { get; }
}
