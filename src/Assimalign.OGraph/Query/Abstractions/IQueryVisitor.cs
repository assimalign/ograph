using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Query;

public interface IQueryVisitor<out T>
{
    T Visit(QueryNode node);
    T Visit(RootNode node);
    T Visit(BinaryNode node);
    T Visit(ConstantNode node);
    T Visit(MemberNode node);
}
