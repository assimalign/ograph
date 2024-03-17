namespace Assimalign.OGraph.Gdm;

/// <summary>
/// 
/// </summary>
public interface IOGraphGdmEnumType : IOGraphGdmType
{
    /// <summary>
    /// The accepted enum values.
    /// </summary>
    public GdmEnumValue[] Values { get; }
}

