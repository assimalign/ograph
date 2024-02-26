namespace Assimalign.OGraph.Syntax;

public interface IQueryNodeVisitor<T>
{
    T Visit(QueryNode queryNode);
    T Visit(VertexNode queryNode);
    T Visit(FilterNode queryNode);
    T Visit(ProjectNode queryNode);
    T Visit(PageNode queryNode);
    T Visit(SortNode queryNode);
    T Visit(BinaryNode queryNode);
    T Visit(PropertyNode queryNode);
    T Visit(ParameterNode queryNode);
    T Visit(FunctionCallNode queryNode);
    T Visit(ConstantNode queryNode);
    T Visit(EdgeNode queryNode);
}