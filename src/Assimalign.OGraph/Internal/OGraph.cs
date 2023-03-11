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

internal class OGraph : IOGraph, IXmlSerializable
{
    private readonly IOGraphNodeCollection nodes;
    private readonly IOGraphOperationCollection operations;

    public OGraph()
    {
        this.nodes = new OGraphNodeCollection();
        this.operations = new OGraphOperationCollection();
    }


    public Name Name { get; set; }
    public IOGraphNodeCollection Nodes => this.nodes;
    public IOGraphEventCollection Events { get; set; }
    public IOGraphOperationCollection Operations => this.operations;

    public XmlSchema? GetSchema()
    {
        throw new NotImplementedException();
    }

    public void ReadXml(XmlReader reader)
    {
        throw new NotImplementedException();
    }

    public void WriteXml(XmlWriter writer)
    {
        throw new NotImplementedException();
    }
}
