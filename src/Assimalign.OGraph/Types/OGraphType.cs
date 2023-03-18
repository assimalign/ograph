using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph;

public abstract class OGraphType : IOGraphType
{
    public abstract Name TypeName { get; }
    public abstract OGraphTypeIdentifier TypeIdentifier { get; }
    public abstract Type? RuntimeType { get; }
    public abstract bool IsRoot { get; }

    public abstract bool IsCollectionType(out IOGraphCollectionType collectionType);
    public abstract bool IsComplexType(out IOGraphComplexType complexType);
    public abstract bool IsPrimitiveType(out IOGraphPrimitiveType primitiveType);
}
