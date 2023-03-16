
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Internal;

using Assimalign.OGraph.Syntax;

internal class QueryableQueryProvider<T> : IOGraphQueryProvider
{
    public Type ElementType => typeof(IQueryable<T>);

    public IQueryable<T> Queryable { get; set; }

    public Task<IOGraphQueryResult> ExecuteAsync(IOGraphQueryContext context, CancellationToken cancellationToken = default)
    {
        var visitor = new QueryExpressionVisitor(Queryable.Expression)
        {
            ElementType = Queryable.ElementType
        };

        var expression = visitor.Visit(context.Query.Root);
        var expressionResult = Queryable.Provider.Execute(expression);

        if (expressionResult is not IEnumerable enumerable)
        {
            throw new Exception();
        }
        foreach (var item in enumerable)
        {

        }



        throw new NotImplementedException();
    }
   
}