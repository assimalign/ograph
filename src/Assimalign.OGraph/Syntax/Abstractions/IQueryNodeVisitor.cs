using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Assimalign.OGraph.Syntax;

public interface IQueryNodeVisitor<T>
{
    T Visit(QueryNode token);
    T Visit(RootNode token);
    T Visit(FilterNode token);
    T Visit(ProjectionNode token);
    T Visit(PageNode token);
    T Visit(SortNode token);
    T Visit(BinaryNode token);
    T Visit(PropertyNode token);
    T Visit(ParameterNode token);
    T Visit(FunctionQueryNode token);
    T Visit(ConstantNode token);
}