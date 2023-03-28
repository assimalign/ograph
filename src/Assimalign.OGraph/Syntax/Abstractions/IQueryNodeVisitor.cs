namespace Assimalign.OGraph.Syntax;

public interface IQueryNodeVisitor
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="queryNode"></param>
    void Visit(QueryNode queryNode);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="queryNode"></param>
    void Visit(RootNode queryNode);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="queryNode"></param>
    void Visit(FilterNode queryNode);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="queryNode"></param>
    void Visit(ProjectionNode queryNode);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="queryNode"></param>
    void Visit(PageNode queryNode);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="queryNode"></param>
    void Visit(SortNode queryNode);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="queryNode"></param>
    void Visit(BinaryNode queryNode);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="queryNode"></param>
    void Visit(PropertyNode queryNode);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="queryNode"></param>
    void Visit(ParameterNode queryNode);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="queryNode"></param>
    void Visit(FunctionCallNode queryNode);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="queryNode"></param>
    void Visit(ConstantNode queryNode);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="queryNode"></param>
    void Visit(EdgeNode queryNode);
}
