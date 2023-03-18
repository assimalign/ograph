using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Assimalign.OGraph.Internal;

internal readonly struct OGraphCollectionItem : IXmlSerializable
{
    private readonly object value;

    public OGraphCollectionItem(OGraphCollection value) => this.value = value;
    public OGraphCollectionItem(OGraphObject value) => this.value = value;
    public OGraphCollectionItem(OGraphValue value) => this.value = value;

    

    public bool IsCollectionType(out OGraphCollection value) => IsMatch<OGraphCollection>(out value);
    public bool IsComplexType(out OGraphObject value) => IsMatch<OGraphObject>(out value);
    public bool IsPrimitiveType(out OGraphValue value) => IsMatch<OGraphValue>(out value);
    public XmlSchema? GetSchema()
    {
        throw new System.NotImplementedException();
    }
    public void ReadXml(XmlReader reader)
    {
        throw new System.NotImplementedException();
    }

    public void WriteXml(XmlWriter writer)
    {
        throw new System.NotImplementedException();
    }

    private bool IsMatch<T>(out T? value)
    {
        value = default;

        if (this.value is T item)
        {
            value = item;
            return true;
        }

        return false;
    }
}
