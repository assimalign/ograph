using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Query;

public interface IQueryVisitor
{
    void Visit(QueryNode node);
    void Visit(RootNode node);
    void Visit(FilterNode node);
    void Visit(SelectNode node);
    void Visit(SortNode node);
    void Visit(BinaryNode node);
    void Visit(ConstantNode node);
    void Visit(MemberNode node);
}

public interface IQueryVisitor<out T>
{
    T Visit(QueryNode node);
    T Visit(RootNode node);
    T Visit(FilterNode node);
    T Visit(SelectNode node);
    T Visit(SortNode node);
    T Visit(BinaryNode node);
    T Visit(ConstantNode node);
    T Visit(MemberNode node);
}
