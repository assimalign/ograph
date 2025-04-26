namespace Assimalign.OGraph.Gdm;

/// <summary>
/// Represents an element that accepts bindings
/// </summary>
/// <remarks>
/// Bindings are a way to maintain Open-Close principal by 
/// allowing us to extend the functionality of the model without 
/// opening it up for modification.
/// </remarks>
public interface IOGraphGdmBindableElement : IOGraphGdmNamedElement
{
    /// <summary>
    /// Specifies whther the elemet has been bound.
    /// </summary>
    bool IsBound { get; }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="binding"></param>
    //void Set(IOGraphGdmBinding binding);

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    //IOGraphGdmBinding Get();

    /// <summary>
    /// 
    /// </summary>
    // IOGraphGdmDirectiveCollection Directives { get; }

    //// <summary>
    //// 
    //// </summary>
    //// <param name="binding"></param>
    // void Bind(IOGraphGdmBinding binding);
}