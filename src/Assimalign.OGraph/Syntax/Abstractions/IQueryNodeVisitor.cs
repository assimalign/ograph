using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Assimalign.OGraph.Syntax;

public interface IQueryNodeVisitor<T>
{
    T Visit(QueryNode token);
    T Visit(RootQueryNode token);
    T Visit(FilterQueryNode token);
    T Visit(ProjectionQueryNode token);
    T Visit(PageQueryNode token);
    T Visit(SortQueryNode token);
    T Visit(BinaryQueryNode token);
    T Visit(PropertyQueryNode token);
    T Visit(ParameterQueryNode token);
    T Visit(FunctionQueryNode token);
    T Visit(ConstantQueryNode token);
}