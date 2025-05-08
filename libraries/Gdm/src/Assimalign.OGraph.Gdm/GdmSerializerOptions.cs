using System.Collections.Generic;

namespace Assimalign.OGraph.Gdm;

/// <summary>
/// 
/// </summary>
public class GdmSerializerOptions
{
    /// <summary>
    /// The serialization version
    /// </summary>
    public GdmVersion Version { get; set; } = GdmVersion.Version1;

    /// <summary>
    /// Meta providers.
    /// </summary>
    public List<GdmMetaConvertor> MetaConvertors { get; } = new List<GdmMetaConvertor>();
}
