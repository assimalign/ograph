namespace Assimalign.OGraph.Gdm;

/// <summary>
/// 
/// </summary>
public interface IOGraphGdmParameter : IOGraphGdmLabeledElement
{
    /// <summary>
    /// 
    /// </summary>
    bool IsRequired { get; }

    /// <summary>
    /// The parameter type reference.
    /// </summary>
    IOGraphGdmType Type { get; }


}