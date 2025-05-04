using System;
using System.Collections;
using System.Collections.Generic;

namespace Assimalign.OGraph.Gdm.Elements;


public class GdmCollectionType : GdmType, IOGraphGdmCollectionType
{
    private readonly Type _runtimeType;

    public GdmCollectionType()
    {
        
    }

    public GdmType ItemType { get; }
    public override GdmName Name => throw new NotImplementedException();
    public override Type RuntimeType => _runtimeType;
    public sealed override GdmTypeKind Kind { get; } = GdmTypeKind.Collection;
    IOGraphGdmType IOGraphGdmCollectionType.ItemType => ItemType;
}

public abstract class GdmCollectionType<TCollection, T> : GdmType<TCollection>, IOGraphGdmCollectionType
    where TCollection : IEnumerable<T>
{
    public abstract GdmType ItemType { get; internal set; }
    public override GdmTypeKind Kind => GdmTypeKind.Collection;
    IOGraphGdmType IOGraphGdmCollectionType.ItemType => ItemType;
}
