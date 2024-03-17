using System.Collections.Generic;

namespace Assimalign.OGraph.Gdm;

/// <summary>
/// Represents an element that accepts bindings
/// </summary>
/// <remarks>
/// Bindings are a way to maintain Open-Close principal by 
/// allowing us to extend the functionality of the model without 
/// opening it up for modification.
/// </remarks>
public interface IOGraphGdmBindingElement : IOGraphGdmElement
{
    /// <summary>
    /// Gets the collection of bindings.
    /// </summary>
    IEnumerable<IOGraphGdmBinding> Bindings { get; }
    /// <summary>
    /// Binds an object to the element.
    /// </summary>
    /// <param name="binding"></param>
    void Bind(IOGraphGdmBinding binding);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="binding"></param>
    //void Unbind(IOGraphGdmBinding binding);
}