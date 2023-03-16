using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Syntax;

public sealed class QueryExpressionVisitor : QueryVisitor<Expression>
{
    private readonly Expression expression;

    public QueryExpressionVisitor(Expression expression)
    {
        this.expression = expression;
    }

    public Type ElementType { get; init; }
}
