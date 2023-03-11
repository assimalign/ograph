using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Xml.Schema;

namespace Assimalign.OGraph.Internal;

internal class OGraph : IOGraph
{
    public OGraph()
    {
        this.Nodes = new OGraphNodeCollection();
        this.Operations = new OGraphOperationCollection();
    }


    public Name Name { get; set; }

    public OGraphNodeCollection Nodes { get; }
    IOGraphNodeCollection IOGraph.Nodes => this.Nodes;
    

    public OGraphOperationCollection Operations { get; }
    IOGraphOperationCollection IOGraph.Operations => this.Operations;


    public IOGraphEventCollection? Events { get; }

}
