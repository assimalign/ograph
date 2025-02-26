namespace Assimalign.OGraph.Gdm;

/// <summary>
/// 
/// </summary>
public interface IOGraphGdmParameter : IOGraphGdmNamedElement
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