namespace Assimalign.OGraph.Gdm;

public interface IOGraphGdmFactory
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="label">represent the domain name.</param>
    /// <returns></returns>
    IOGraphGdm Create(Label label); 
}
