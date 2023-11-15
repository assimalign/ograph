using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Gdm;

public static class OGraphGdmVertexBindingExtensions
{

    public static IOGraphGdmVertexOperationDescriptor BindGet<T>(this IOGraphGdmVertexDescriptor<T> descriptor, Label label) 
        where T : class, new()
    {


        return default;
    }

    public static IOGraphGdmVertexOperationDescriptor BindPatch<T>(this IOGraphGdmVertexDescriptor<T> descriptor, Label label)
        where T : class, new()
    {


        return default;
    }

    public static IOGraphGdmVertexOperationDescriptor BindPut<T>(this IOGraphGdmVertexDescriptor<T> descriptor, Label label)
        where T : class, new()
    {


        return default;
    }

    public static IOGraphGdmVertexOperationDescriptor BindPost<T>(this IOGraphGdmVertexDescriptor<T> descriptor, Label label)
        where T : class, new()
    {


        return default;
    }

    public static IOGraphGdmVertexOperationDescriptor BindDelete<T>(this IOGraphGdmVertexDescriptor<T> descriptor, Label label)
        where T : class, new()
    {


        return default;
    }
}
