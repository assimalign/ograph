namespace Assimalign.OGraph.Gdm;

/// <summary>
/// 
/// </summary>
public interface IOGraphGdmStep : IOGraphGdmElement
{
    /// <summary>
    /// 
    /// </summary>
    GdmStepValue Value { get; }

    /// <summary>
    /// 
    /// </summary>
    IOGraphGdmOperationCollection Operations { get; }

    /// <summary>
    /// 
    /// </summary>
    IOGraphGdmEdge Edge { get; }
}
