using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Modeling;

/// <summary>
/// Types represent primitive or complex structure that can be 
/// used to define a property, inputs, and outputs of operations within the graph.
/// </summary>
public interface IOGraphType
{
    
    OGraphTypeDescriptor Descriptor { get; }
}
