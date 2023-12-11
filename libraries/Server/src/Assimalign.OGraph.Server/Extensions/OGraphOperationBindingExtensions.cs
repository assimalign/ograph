using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph;

public static class OGraphOperationBindingExtensions
{

    public static IOGraphOperationBindingDescriptor UseResolver<T>(
        this IOGraphOperationBindingDescriptor descriptor, 
        Func<IOGraphOperationBindingContext, Either<IQueryable<T>, IOGraphErrorResult>> resolver)
    {


        return descriptor

            .UseResolver(async (context, cancellationToken) =>
            {
                var either = resolver.Invoke(context);

                if (either.If(out IOGraphErrorResult error, out IQueryable<T> queryable))
                {
                    return error;
                }
                else
                {
                    var queryProvider = context.GetQueryProvider();
                    var queryOptions = context.GetQueryOptions();


                    return await queryProvider.ExecuteAsync(default, queryOptions, cancellationToken);

                }
            });
    }
}
