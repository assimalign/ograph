namespace Assimalign.OGraph.Query;

/// <summary>
/// 
/// </summary>
public interface IQueryVisitorExecutor
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="root"></param>
    /// <param name="visitor"></param>
    void Execute<T>(RootNode root, IQueryVisitor<T> visitor);
}
