namespace Assimalign.OGraph;

public interface IOGraphCommandOperation : IOGraphOperation
{
    /// <summary>
    /// 
    /// </summary>
    IOGraphType RequestType { get; }
    /// <summary>
    /// 
    /// </summary>
    IOGraphType ResponseType { get; }
}