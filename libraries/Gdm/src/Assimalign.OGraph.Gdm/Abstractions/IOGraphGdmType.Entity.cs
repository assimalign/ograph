namespace Assimalign.OGraph.Gdm;

/// <summary>
/// 
/// </summary>
public interface IOGraphGdmEntityType : IOGraphGdmComplexType
{
    /// <summary>
    /// 
    /// </summary>
    IOGraphGdmEntityKey Key { get; }
}