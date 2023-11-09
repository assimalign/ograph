using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Gdm;

public static class OGraphGdmTypeDescriptorExtensions
{


    public static IOGraphGdmEntityTypeDescriptor<T> HasEdge<T>(this IOGraphGdmEntityTypeDescriptor<T> descriptor) where T : class, new()
    {



        return descriptor;
    }
}
