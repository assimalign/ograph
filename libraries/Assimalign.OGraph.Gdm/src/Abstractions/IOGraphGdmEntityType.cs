namespace Assimalign.OGraph.Gdm;

/// <summary>
/// Represents a GdmEntity.
/// </summary>
public interface IOGraphGdmEntityType : IOGraphGdmComplexType
{
    /// <summary>
    /// The The entity key reference.
    /// </summary>
    IOGraphGdmEntityKey Key { get; }
}