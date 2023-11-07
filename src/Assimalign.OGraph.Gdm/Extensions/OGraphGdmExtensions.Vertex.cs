using System;
using System.Linq;
using System.Collections.Generic;

namespace Assimalign.OGraph.Gdm;

public static class OGraphGdmVertexExtensions
{

    public static IOGraphGdmVertexDescriptor<T> HasType<T>(this IOGraphGdmVertexDescriptor<T> descriptor, GdmVertex<T> vertex) where T : class, new()
    {


        return descriptor;
    }
   
}
