using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Query;

public sealed partial class ExpressionVisitor : IQueryVisitor<Expression>
{
    public Expression Visit(QueryNode node)
    {
        throw new NotImplementedException();
    }

    public Expression Visit(RootNode node)
    {
        throw new NotImplementedException();
    }

    

    
}
