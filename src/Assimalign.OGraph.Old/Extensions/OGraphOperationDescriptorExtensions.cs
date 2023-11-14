using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;


namespace Assimalign.OGraph;

using Assimalign.OGraph.Internal;


public static class OGraphOperationDescriptorExtensionsd
{

    /// <summary>
    /// A Synchronous 
    /// </summary>
    /// <param name="descriptor"></param>
    /// <param name="resolver"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    //public static IOGraphCommandOperationDescriptor UseResolver(
    //    this IOGraphCommandOperationDescriptor descriptor,
    //    Func<IOGraphOperationContext, IOGraphOperationResult> resolver)
    //{
    //    if (resolver is null)
    //    {
    //        throw new ArgumentNullException(nameof(resolver));
    //    }
    //    return descriptor.UseResolver(context =>
    //    {
    //        return Task.FromResult<IOGraphOperationResult>(resolver.Invoke(context));
    //    });
    //}
    /// <summary>
    /// 
    /// </summary>
    /// <param name="descriptor"></param>
    /// <param name="resolver"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    //public static IOGraphOperationDescriptor UseResolver(
    //    this IOGraphOperationDescriptor descriptor,
    //    Func<IOGraphOperationContext, IQueryable> resolver) 
    //{
    //    if (resolver is null)
    //    {
    //        throw new ArgumentNullException(nameof(resolver));
    //    }

    //    return descriptor.UseResolver(context =>
    //    {


    //        return default;
    //    });
    //}

    
    //public static IOGraphCommandOperationDescriptor UseEitherResolver<TQueryable, TResult>(
    //    this IOGraphCommandOperationDescriptor descriptor,
    //    Func<IOGraphOperationContext, Either<TQueryable, TResult>> resolver)
    //    where TQueryable : IQueryable
    //    where TResult : IOGraphOperationResult
    //{
    //    if (resolver is null)
    //    {
    //        throw new ArgumentNullException(nameof(resolver));
    //    }
    //    return descriptor.UseResolver(context =>
    //    {
    //        var results = resolver.Invoke(context);

    //        return results
    //            .Match((TResult result) =>  result as IOGraphOperationResult)
    //            .Match((TQueryable queryable) =>
    //            {
    //                return new QueryableResult() as IOGraphOperationResult;
    //            });
    //    });
    //}

    /// <summary>
    /// 
    /// </summary>
    /// <param name="descriptor"></param>
    /// <param name="resolver"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    //public static IOGraphOperationDescriptor UseResolver(
    //    this IOGraphOperationDescriptor descriptor,
    //    Func<IOGraphOperationContext, Task<IQueryable>> resolver)
    //{
    //    if (resolver is null)
    //    {
    //        throw new ArgumentNullException(nameof(resolver));
    //    }
    //    return descriptor.UseResolver(async context =>
    //    {
    //        var graphQueryable = await resolver.Invoke(context);

    //        return default;
    //    });
    //}

    //public static IOGraphOperationDescriptor UseResolver(
    //    this IOGraphOperationDescriptor descriptor,
    //    Func<IOGraphOperationContext, Either<IQueryable, IOGraphOperationResult>> resolver)
    //{
    //    if (resolver is null)
    //    {
    //        throw new ArgumentNullException(nameof(resolver));
    //    }
    //    return descriptor.UseResolver(async context =>
    //    {
    //        //var results = resolver.Invoke(context);

    //        //results.Match(async context => )
    //        //var graphQueryable = await resolver.Invoke(context);

    //        return default;
    //    });
    //}

    ///// <summary>
    ///// 
    ///// </summary>
    ///// <typeparam name="T"></typeparam>
    ///// <param name="descriptor"></param>
    ///// <param name="resolver"></param>
    ///// <returns></returns>
    ///// <exception cref="ArgumentNullException"></exception>
    //public static IOGraphOperationDescriptor UseResolver<T>(
    //    this IOGraphOperationDescriptor descriptor,
    //    Func<IOGraphOperationContext, IQueryable<T>> resolver)
    //{
    //    if (resolver is null)
    //    {  
    //        throw new ArgumentNullException(nameof(resolver));
    //    }

    //    return descriptor
    //        .UseQueryProvider<QueryableQueryProvider<T>>()
    //        .UseResolver(async context =>
    //        {
    //            // Get the binding to the operation
    //            var graphNode = context.GetNode();

    //            // Validate that the type matches the unerlying node type
    //            if (!typeof(T).IsAssignableTo(graphNode.Type.RuntimeType))
    //            {
    //                throw new Exception();
    //            }

    //            var graphQuery          = context.GetQuery();
    //            var graphQueryOptions   = context.GetQueryOptions();
    //            var graphQueryProvider  = context.GetQueryProvider();
    //            var graphQueryContext   = new QueryableQueryContext()
    //            {
    //                Query               = graphQuery,
    //                Node                = graphNode,
    //                ServiceProvider     = context.GetService<IServiceProvider>()
    //            };

    //            var graphQueryResults = await graphQueryProvider.ExecuteAsync(
    //                graphQueryContext, 
    //                graphQueryOptions);

    //            return new OGraphQueryResult(graphQueryResults);
    //        });
    //}

    ///// <summary>
    ///// 
    ///// </summary>
    ///// <typeparam name="T"></typeparam>
    ///// <param name="descriptor"></param>
    ///// <param name="resolver"></param>
    ///// <returns></returns>
    ///// <exception cref="ArgumentNullException"></exception>
    ///// <exception cref="Exception"></exception>
    //public static IOGraphOperationDescriptor UseResolver<T>(
    //    this IOGraphOperationDescriptor descriptor,
    //    Func<IOGraphOperationContext, Task<IQueryable<T>>> resolver)
    //{
    //    if (resolver is null)
    //    {
    //        throw new ArgumentNullException(nameof(resolver));
    //    }

    //    return descriptor
    //        .UseQueryProvider<QueryableQueryProvider<T>>()
    //        .UseResolver(async context =>
    //        {
    //            var queryable = await resolver.Invoke(context);

    //            // Get the binding to the operation
    //            var graphNode = context.GetNode();

    //            // Validate that the type matches the unerlying node type
    //            if (!typeof(T).IsAssignableTo(graphNode.Type.RuntimeType))
    //            {
    //                throw new Exception();
    //            }

    //            var graphQuery          = context.GetQuery();
    //            var graphQueryOptions   = context.GetQueryOptions();
    //            var graphQueryProvider  = context.GetQueryProvider();
    //            var graphQueryContext   = new QueryableQueryContext()
    //            {
    //                Query               = graphQuery,
    //                Node                = graphNode,
    //                ServiceProvider     = context.GetService<IServiceProvider>(),
    //                Queryable           = queryable 
    //            };

    //            var graphQueryResults = await graphQueryProvider.ExecuteAsync(
    //                graphQueryContext, 
    //                graphQueryOptions);

    //            return new OGraphQueryResult(graphQueryResults);
    //        });
    //}

    ///// <summary>
    ///// 
    ///// </summary>
    ///// <typeparam name="T"></typeparam>
    ///// <param name="descriptor"></param>
    ///// <param name="resolver"></param>
    ///// <returns></returns>
    ///// <exception cref="ArgumentNullException"></exception>
    ///// <exception cref="Exception"></exception>
    //public static IOGraphOperationDescriptor UseResolver<T>(
    //    this IOGraphOperationDescriptor descriptor,
    //    Func<IOGraphOperationContext, IEnumerable> resolver)
    //{
    //    if (resolver is null)
    //    {
    //        throw new ArgumentNullException(nameof(resolver));
    //    }

    //    return descriptor
    //        .UseQueryProvider<EnumerableQueryProvider<T>>()
    //        .UseResolver(async context =>
    //        {
    //            // Get the binding to the operation
    //            var graphNode = context.GetNode();

    //            // Validate that the type matches the unerlying node type
    //            if (!typeof(T).IsAssignableTo(graphNode.Type.RuntimeType))
    //            {
    //                throw new Exception();
    //            }

    //            var graphQuery = context.GetQuery();
    //            var graphQueryOptions = context.GetQueryOptions();
    //            var graphQueryProvider = context.GetQueryProvider();

    //            var graphQueryContext = new EnumerableQueryContext()
    //            {
    //                Query = graphQuery,
    //            };

    //            var graphQueryResults = await graphQueryProvider.ExecuteAsync(
    //                graphQueryContext,
    //                graphQueryOptions);


    //            return default;
    //        });
    //}

    ///// <summary>
    ///// 
    ///// </summary>
    ///// <typeparam name="T"></typeparam>
    ///// <param name="descriptor"></param>
    ///// <param name="resolver"></param>
    ///// <returns></returns>
    ///// <exception cref="ArgumentNullException"></exception>
    ///// <exception cref="Exception"></exception>
    //public static IOGraphOperationDescriptor UseResolver<T>(
    //    this IOGraphOperationDescriptor descriptor,
    //    Func<IOGraphOperationContext, Task<IEnumerable>> resolver)
    //{
    //    if (resolver is null)
    //    {
    //        throw new ArgumentNullException(nameof(resolver));
    //    }

    //    return descriptor
    //        .UseQueryProvider<EnumerableQueryProvider<T>>()
    //        .UseResolver(async context =>
    //        {
    //            // Get the binding to the operation
    //            var graphNode = context.GetNode();

    //            // Validate that the type matches the unerlying node type
    //            if (!typeof(T).IsAssignableTo(graphNode.Type.RuntimeType))
    //            {
    //                throw new Exception();
    //            }

    //            var graphQuery = context.GetQuery();
    //            var graphQueryOptions = context.GetQueryOptions();
    //            var graphQueryProvider = context.GetQueryProvider();

    //            var graphQueryContext = new EnumerableQueryContext()
    //            {
    //                Query = graphQuery,
    //            };

    //            var graphQueryResults = await graphQueryProvider.ExecuteAsync(
    //                graphQueryContext,
    //                graphQueryOptions);


    //            return default;
    //        });
    //}

    ///// <summary>
    ///// 
    ///// </summary>
    ///// <typeparam name="T"></typeparam>
    ///// <param name="descriptor"></param>
    ///// <param name="resolver"></param>
    ///// <returns></returns>
    ///// <exception cref="ArgumentNullException"></exception>
    ///// <exception cref="Exception"></exception>
    //public static IOGraphOperationDescriptor UseResolver<T>(
    //    this IOGraphOperationDescriptor descriptor,
    //    Func<IOGraphOperationContext, IEnumerable<T>> resolver)
    //{
    //    if (resolver is null)
    //    {
    //        throw new ArgumentNullException(nameof(resolver));
    //    }

    //    return descriptor
    //        .UseQueryProvider<EnumerableQueryProvider<T>>()
    //        .UseResolver(async context =>
    //        {
    //            var graphEnumerable = resolver.Invoke(context);

    //            // Get the binding to the operation
    //            var graphNode = context.GetNode();

    //            // Validate that the type matches the unerlying node type
    //            if (!typeof(T).IsAssignableTo(graphNode.Type.RuntimeType))
    //            {
    //                throw new Exception();
    //            }

    //            var graphQuery = context.GetQuery();
    //            var graphQueryOptions = context.GetQueryOptions();
    //            var graphQueryProvider = context.GetQueryProvider();

    //            var graphQueryContext = new EnumerableQueryContext()
    //            {
    //                Query = graphQuery,
    //            };

    //            var graphQueryResults = await graphQueryProvider.ExecuteAsync(
    //                graphQueryContext,
    //                graphQueryOptions);


    //            return default;
    //        });
    //}

    ///// <summary>
    ///// 
    ///// </summary>
    ///// <typeparam name="T"></typeparam>
    ///// <param name="descriptor"></param>
    ///// <param name="resolver"></param>
    ///// <returns></returns>
    ///// <exception cref="ArgumentNullException"></exception>
    ///// <exception cref="Exception"></exception>
    //public static IOGraphOperationDescriptor UseResolver<T>(
    //    this IOGraphOperationDescriptor descriptor,
    //    Func<IOGraphOperationContext, Task<IEnumerable<T>>> resolver)
    //{
    //    if (resolver is null)
    //    {
    //        throw new ArgumentNullException(nameof(resolver));
    //    }

    //    return descriptor
    //        .UseQueryProvider<EnumerableQueryProvider<T>>()
    //        .UseResolver(async context =>
    //        {
    //            // Get the binding to the operation
    //            var graphNode = context.GetNode();

    //            // Validate that the type matches the unerlying node type
    //            if (!typeof(T).IsAssignableTo(graphNode.Type.RuntimeType))
    //            {
    //                throw new Exception();
    //            }

    //            var graphQuery = context.GetQuery();
    //            var graphQueryOptions = context.GetQueryOptions();
    //            var graphQueryProvider = context.GetQueryProvider();

    //            var graphQueryContext = new EnumerableQueryContext()
    //            {
    //                Query = graphQuery,
    //            };

    //            var graphQueryResults = await graphQueryProvider.ExecuteAsync(
    //                graphQueryContext,
    //                graphQueryOptions);


    //            return default;
    //        });
    //}

    ///// <summary>
    ///// 
    ///// </summary>
    ///// <typeparam name="T"></typeparam>
    ///// <param name="descriptor"></param>
    ///// <param name="resolver"></param>
    ///// <returns></returns>
    ///// <exception cref="ArgumentNullException"></exception>
    ///// <exception cref="Exception"></exception>
    //public static IOGraphOperationDescriptor UseResolver<T>(
    //    this IOGraphOperationDescriptor descriptor,
    //    Func<IOGraphOperationContext, T> resolver) where T : class
    //{
    //    if (resolver is null)
    //    {
    //        throw new ArgumentNullException();
    //    }

    //    return descriptor
    //        .UseQueryProvider<ObjectQueryProvider<T>>()
    //        .UseResolver(async context =>
    //        {
    //            // Get the binding to the operation
    //            var graphNode           = context.GetNode();

    //            // Validate that the type matches the unerlying node type
    //            if (!typeof(T).IsAssignableTo(graphNode.Type.RuntimeType))
    //            {
    //                throw new Exception();
    //            }

    //            var graphQuery          = context.GetQuery();
    //            var graphQueryOptions   = context.GetQueryOptions();
    //            var graphQueryProvider  = context.GetQueryProvider();

    //            var graphQueryContext = new ObjectQueryContext()
    //            {
    //                Query = graphQuery,
    //            };

    //            var graphQueryResults = await graphQueryProvider.ExecuteAsync(
    //                graphQueryContext,
    //                graphQueryOptions);

    //            return default;

    //        });
    //}
}
