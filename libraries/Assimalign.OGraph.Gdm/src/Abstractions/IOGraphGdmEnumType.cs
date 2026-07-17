namespace Assimalign.OGraph.Gdm;

/// <summary>
/// 
/// </summary>
public interface IOGraphGdmEnumType : IOGraphGdmSerializableType
{
    /// <summary>
    /// The accepted enum values.
    /// </summary>
    public GdmEnumValue[] Values { get; }
}