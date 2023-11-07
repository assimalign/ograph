using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Gdm;

public static class OGraphGdmPropertyDescriptorExtensions
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="descriptor"></param>
    /// <param name="configure"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static IOGraphGdmPropertyDescriptor<T?> UseType<T>(
        this IOGraphGdmPropertyDescriptor<T?> descriptor,
        Action<IOGraphGdmComplexTypeDescriptor<T>> configure) where T : class, new()
    {
        if (configure is null)
        {
            throw new ArgumentNullException(nameof(configure));
        }

        return descriptor.UseType(GdmComplexType<T>.Create(configure));
    }
}
