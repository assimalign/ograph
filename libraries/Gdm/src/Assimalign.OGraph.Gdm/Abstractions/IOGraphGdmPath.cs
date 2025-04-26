namespace Assimalign.OGraph.Gdm;

/// <summary>
/// 
/// </summary>
public interface IOGraphGdmPath : IOGraphGdmElement
{
    /// <summary>
    /// 
    /// </summary>
    GdmPathValue Value { get; }

    /// <summary>
    /// 
    /// </summary>
    IOGraphGdmOperationCollection Operations { get; }

    /// <summary>
    /// 
    /// </summary>
    IOGraphGdmVertex Vertex { get; }
}
