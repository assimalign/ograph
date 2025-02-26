using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Gdm.Internal;

internal class GdmFunctionDescriptor<T> : IOGraphGdmFunctionDescriptor<T>
{
    public IOGraphGdmFunctionDescriptor<T> UseFunctionName(GdmLabel label)
    {
        throw new NotImplementedException();
    }

    public IOGraphGdmFunctionDescriptor<T> UseParameter<TType>(GdmLabel label) where TType : IOGraphGdmType, new()
    {
        throw new NotImplementedException();
    }

    public IOGraphGdmFunctionDescriptor<T> UseParameter(GdmLabel label, IOGraphGdmType type)
    {
        throw new NotImplementedException();
    }

    public IOGraphGdmFunctionDescriptor<T> UseType(IOGraphGdmType type)
    {
        throw new NotImplementedException();
    }

    public IOGraphGdmFunctionDescriptor<T> UseType(Func<IOGraphGdmType, IOGraphGdmGraph> type)
    {
        throw new NotImplementedException();
    }
}
