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
    public Name Name { get; set; }
    public IOGraphNodeCollection Nodes { get; set; } = new OGraphNodeCollection();
    public IOGraphEventCollection Events { get; set; }
    public IOGraphOperationCollection Operations { get; set; }

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
