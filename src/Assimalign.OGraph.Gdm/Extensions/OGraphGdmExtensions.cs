using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Gdm;

public static class OGraphGdmExtensions
{
    public static IOGraphGdmPropertyDescriptor<T?> UseType<T>(
        this IOGraphGdmPropertyDescriptor<T?> descriptor, 
        Action<IOGraphGdmComplexTypeDescriptor<T?>> configure) where T : class, new()
    {
        if (configure is null)
        {
            throw new ArgumentNullException(nameof(configure));
        }


        return descriptor;
    }
}
