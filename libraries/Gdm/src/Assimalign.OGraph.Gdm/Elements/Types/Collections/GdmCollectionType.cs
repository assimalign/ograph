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
    where TCollection : IEnumerable<T>
{
    
    public abstract IOGraphGdmType ItemType { get; }
    public override GdmTypeKind Kind => GdmTypeKind.Collection;
}
