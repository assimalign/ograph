namespace Assimalign.OGraph.Gdm;

public interface IOGraphGdmPath
{
    /// <summary>
    /// 
    /// </summary>
    GdmPathValue Value { get; }

    /// <summary>
    /// 
    /// </summary>
    IOGraphGdmOperationCollection Operations { get; }
}
