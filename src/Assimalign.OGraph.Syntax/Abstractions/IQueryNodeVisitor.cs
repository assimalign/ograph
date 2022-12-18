using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Assimalign.OGraph.Syntax;

public interface IQueryNodeVisitor<T>
{
    T Visit(QueryNode node);
    T Visit(RootQueryNode node);
    T Visit(FilterQueryNode node);
    T Visit(ProjectionQueryNode node);
    T Visit(PageQueryNode node);
    T Visit(SortQueryNode node);
    T Visit(BinaryQueryNode node);
    T Visit(FieldQueryNode node);
    T Visit(MemberQueryNode node);
    T Visit(ParameterQueryNode node);
    T Visit(FunctionQueryNode node);
    T Visit(ConstantQueryNode node);
}