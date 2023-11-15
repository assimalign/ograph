namespace Assimalign.OGraph;

public interface IOGraphObjectResult<T> : IOGraphObjectResult 
    where T : class
{
    /// <summary>
    /// 
    /// </summary>
    new T Data { get; }
}
