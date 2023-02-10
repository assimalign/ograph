using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Internal;

internal class OGraph : IOGraph
{
    public Name Name { get; set; }
    public IOGraphNodeCollection Nodes { get; set; }
    public IOGraphEventCollection Events { get; set; }
    public IOGraphOperationCollection Operations { get; set; }
}
