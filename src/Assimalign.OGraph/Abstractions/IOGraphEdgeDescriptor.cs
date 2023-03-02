using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph;

public interface IOGraphEdgeDescriptor
{
    IOGraphEdgeDescriptor UseNode(Label label);
    IOGraphEdgeDescriptor UseNode<TNode>() where TNode : IOGraphNode;
    IOGraphEdgeDescriptor UseMiddleware(IOGraphEdgeMiddleware middleware);
}


