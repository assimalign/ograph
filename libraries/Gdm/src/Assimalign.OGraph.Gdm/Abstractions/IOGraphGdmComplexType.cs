namespace Assimalign.OGraph.Gdm;

/// <summary>
/// 
/// </summary>
public interface IOGraphGdmComplexType : IOGraphGdmType
{
    /// <summary>
    /// Gets the collection of members.
    /// </summary>
    IOGraphGdmMemberCollection Members { get; }
}