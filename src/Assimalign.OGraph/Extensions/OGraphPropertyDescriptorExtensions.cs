using Assimalign.OGraph.Internal;
using System;
using System.Threading.Tasks;

namespace Assimalign.OGraph;

public static class OGraphPropertyDescriptorExtensions
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TProperty"></typeparam>
    /// <param name="descriptor"></param>
    /// <param name="resolver"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static IOGraphPropertyDescriptor<TProperty> UseResolver<TProperty>(
        this IOGraphPropertyDescriptor<TProperty> descriptor,
        Func<IOGraphPropertyContext, TProperty> resolver)
    {
        if (resolver is null)
        {
            throw new ArgumentNullException(nameof(resolver));
        }
        return descriptor.UseResolver(context =>
        {
            return ValueTask.FromResult<IOGraphPropertyResult>(new OGraphPropertyResult()
            {
                Data = resolver.Invoke(context)
            });
        });
    }



    public static IOGraphPropertyDescriptor UseResolver<TProperty>(
        this IOGraphPropertyDescriptor descriptor,
        Func<IOGraphPropertyContext, TProperty> resolver)
    {
        if (resolver is null)
        {
            throw new ArgumentNullException(nameof(resolver));
        }

        descriptor.UseOGraphType(typeof(TProperty));

        return descriptor.UseResolver(context =>
        {
            return ValueTask.FromResult<IOGraphPropertyResult>(new OGraphPropertyResult()
            {
                Data = resolver.Invoke(context)
            });
        });
    }



    private static void UseOGraphType(this IOGraphPropertyDescriptor descriptor, Type type)
    {
        if (type.IsStringType())
        {
            descriptor.UseType<StringType>();
        }


        throw new Exception();
    }
}
