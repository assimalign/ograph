
using System;

namespace Assimalign.OGraph;

using Assimalign.OGraph.Internal;

public class ComplexType : IOGraphComplexType
{
    public ComplexType()
    {
        this.Properties = new OGraphPropertyCollection();
    }
    public Name TypeName { get; init; }
    public OGraphTypeIdentifier TypeIdentifier => OGraphTypeIdentifier.Complex;
    public IOGraphPropertyCollection Properties { get; }
    public Type? RuntimeType { get; init; }

    public bool IsRoot => throw new NotImplementedException();

    public bool IsCollectionType(out IOGraphCollectionType collectionType)
    {
        collectionType = default;
        return false;
    }

    public bool IsPrimitiveType(out IOGraphPrimitiveType primitiveType)
    {
        throw new NotImplementedException();
    }

    public bool IsComplexType(out IOGraphComplexType complexType)
    {
        throw new NotImplementedException();
    }
}
