using System;
using System.Collections;
using System.Collections.Generic;

namespace Assimalign.OGraph.Gdm.Elements;

public abstract class GdmCollectionType<TCollection, T> : GdmType<TCollection>, IOGraphGdmCollectionType
    where TCollection : IEnumerable<T>
{
    public abstract GdmType ItemType { get; internal set; }
    public override GdmTypeKind Kind => GdmTypeKind.Collection;
    IOGraphGdmType IOGraphGdmCollectionType.ItemType => ItemType;
}
