namespace Assimalign.OGraph.Gdm;

/// <summary>
/// 
/// </summary>
public interface IOGraphGdmParameter : IOGraphGdmLabeledElement
{
    /// <summary>
    /// The parameter type reference.
    /// </summary>
    IOGraphGdmTypeReference Type { get; }

    /// <summary>
    /// 
    /// </summary>
    bool IsRequired { get; }
}