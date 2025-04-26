namespace Assimalign.OGraph.Gdm;

/// <summary>
/// Represents a GdmEntity.
/// </summary>
public interface IOGraphGdmEntityType : IOGraphGdmComplexType
{
    /// <summary>
    /// The key of the entity.
    /// </summary>
    IOGraphGdmEntityKey Key { get; }
}