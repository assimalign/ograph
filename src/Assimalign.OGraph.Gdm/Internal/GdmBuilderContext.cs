using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Gdm.Internal;

internal class GdmBuilderContext
{
    private static readonly GdmBuilderContext? context;
    
    public GdmBuilderContext()
    {

    }


    public IOGraphGdmTypeCollection Types { get; }



    public IOGraphGdmType GetTypeByName(Label name)
    {
        throw new NotImplementedException();
    }

    public IOGraphGdmType GetTypeByRuntimeType(Type type)
    {
        throw new NotImplementedException();
    }

}
