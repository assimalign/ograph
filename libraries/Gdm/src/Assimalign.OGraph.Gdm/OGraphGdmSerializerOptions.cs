using System.Collections.Generic;

namespace Assimalign.OGraph.Gdm;

/// <summary>
/// 
/// </summary>
public class OGraphGdmSerializerOptions
{
    /// <summary>
    /// The serialization version
    /// </summary>
    public OGraphVersion Version { get; set; } = OGraphVersion.Version1;

    /// <summary>
    /// Meta providers;
    /// </summary>
    public List<OGraphGdmMetaProvider> MetaProviders { get; } = new List<OGraphGdmMetaProvider>();
}
