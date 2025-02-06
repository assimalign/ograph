using System;
using System.Collections.Generic;

namespace Assimalign.OGraph.Gdm;

/// <summary>
/// 
/// </summary>
public interface IOGraphGdmElementCollection : ICollection<IOGraphGdmElement>
{
    /// <summary>
    /// Get's an element with the specified label that is of type <typeparamref name="TElement"/>.
    /// </summary>
    /// <typeparam name="TElement"></typeparam>
    /// <param name="label"></param>
    /// <returns></returns>
    TElement Find<TElement>(Label label) where TElement : IOGraphGdmLabeledElement;
}