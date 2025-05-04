using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Gdm.Internal;

using Elements;

internal class GdmTypeResolver
{
    private readonly GdmTypeCollection _types;

    public GdmTypeResolver(GdmTypeCollection types)
    {
        _types = types;
    }


    public GdmType Resolve(Type runtimeType)
    {

    }
}
