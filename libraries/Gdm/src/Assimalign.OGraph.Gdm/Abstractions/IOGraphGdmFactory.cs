namespace Assimalign.OGraph.Gdm;

/// <summary>
/// 
/// </summary>
public interface IOGraphGdmFactory
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="label">represent the domain name.</param>
    /// <returns></returns>
    IOGraphGdm Create(Label label); 
}
