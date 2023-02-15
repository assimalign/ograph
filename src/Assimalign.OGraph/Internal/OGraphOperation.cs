using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Internal;

internal abstract class OGraphOperation : IOGraphOperation
{
    public Label Name { get; set; }
    public Route Route { get; set; }
    public Method Method { get; set; }
    public bool IsEnabled { get; set; }
    public IOGraphType? RequestType { get; set; }
    public IOGraphType? ResponseType { get; set; }
    public IOGraphOperationResolver? Resolver { get; set; } 
}
