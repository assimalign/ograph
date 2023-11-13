namespace Assimalign.OGraph;

/// <summary>
/// 
/// </summary>
public interface IOGraphFactory
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="domain"></param>
    /// <returns></returns>
    IOGraph Create(Label domain);
}
