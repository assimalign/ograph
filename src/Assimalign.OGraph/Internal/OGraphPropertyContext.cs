using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Internal;

internal class OGraphPropertyContext : IOGraphPropertyContext
{

    public IServiceProvider ServiceProvider { get; init; }


    internal volatile object Parent;

    public T GetParent<T>()
    {
        if (Parent is T parent)
            return parent;

        throw new Exception();
    }

    public IOGraphType GetPropertyType()
    {
        throw new NotImplementedException();
    }

    public T GetService<T>()
    {
        throw new NotImplementedException();
    }
}
