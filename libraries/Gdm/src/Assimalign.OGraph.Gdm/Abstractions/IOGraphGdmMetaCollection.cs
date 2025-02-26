using System;
using System.Collections.Generic;

namespace Assimalign.OGraph.Gdm;

/// <summary>
/// 
/// </summary>
public interface IOGraphGdmMetaCollection : IDictionary<GdmMetaKey, IOGraphGdmMetaValue>
{
    // TODO: Revisit a more robust metadata structure
    T GetValue<T>(GdmMetaKey key);
}

public interface IOGraphGdmMetaValue : IOGraphGdmElement
{

}