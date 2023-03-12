using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph;

public abstract class CollectionType<TCollection> : IOGraphCollectionType
    where TCollection : IEnumerable
{
    public virtual Name TypeName => nameof(TCollection);
    public abstract IOGraphType ItemType { get; }
    public OGraphTypeIdentifier TypeIdentifier => OGraphTypeIdentifier.Collection;

    public Type? RuntimeType => throw new NotImplementedException();

    public bool IsRuntimeType => throw new NotImplementedException();
}
