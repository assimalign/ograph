using System;
using System.Xml;
using System.Text.Json;
using System.Collections;
using Assimalign.OGraph.Internal;

namespace Assimalign.OGraph;

public class CollectionType<TCollection> : IOGraphCollectionType
    where TCollection : IEnumerable
{
    public virtual Name TypeName
    {
        get
        {
            if (typeof(TCollection).IsEnumerableType(out var enumerableType))
            {
                return $"{enumerableType.Name}Collection";
            }

            return nameof(TCollection);
        }
    }
    public virtual IOGraphType ItemType { get; internal set; }
    public OGraphTypeIdentifier TypeIdentifier => OGraphTypeIdentifier.Collection;
    public Type RuntimeType => typeof(TCollection);
    public bool IsNullable => true;
}
