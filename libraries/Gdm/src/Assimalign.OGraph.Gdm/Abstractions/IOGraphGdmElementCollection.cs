using System;
using System.Collections.Generic;

namespace Assimalign.OGraph.Gdm;

/// <summary>
/// 
/// </summary>
public interface IOGraphGdmElementCollection : ICollection<IOGraphGdmElement>
{
    /// <summary>
    /// Get's an element with the specified label that is of type <typeparamref name="TGdmElement"/>.
    /// </summary>
    /// <typeparam name="TGdmElement"></typeparam>
    /// <param name="label"></param>
    /// <returns></returns>
    TGdmElement Get<TGdmElement>(Label label) where TGdmElement : IOGraphGdmLabeledElement;
}