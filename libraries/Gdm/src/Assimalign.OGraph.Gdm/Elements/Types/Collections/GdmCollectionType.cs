using System;
using System.Collections;
using System.Collections.Generic;

namespace Assimalign.OGraph.Gdm;

/// <summary>
/// 
/// </summary>
/// <typeparam name="TCollection"></typeparam>
/// <typeparam name="T"></typeparam>
public abstract class GdmCollectionType<TCollection, T> : GdmType<TCollection>, 
    IOGraphGdmCollectionType
    where TCollection : IEnumerable<T>, new()
    where T : new()
{
    
    IOGraphGdmType IOGraphGdmCollectionType.ItemType => ItemType;
    public abstract GdmType<T> ItemType { get; }
    public override GdmTypeKind Kind => GdmTypeKind.Collection;
}
