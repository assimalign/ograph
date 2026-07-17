namespace Assimalign.OGraph.Gdm;

/// <summary>
/// 
/// </summary>
public interface IOGraphGdmComplexType : IOGraphGdmSerializableType
{
    /// <summary>
    /// Gets the collection of members.
    /// </summary>
    IOGraphGdmMemberCollection Members { get; }
}