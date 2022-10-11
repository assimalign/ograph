using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Query;

public sealed partial class ExpressionVisitor<T>
{
    public Expression Visit(ConstantNode node) => Expression.Constant(node.Value);
}
