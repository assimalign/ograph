using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Internal;

internal class OGraphNodePropertyDescriptor<T> : IOGraphNodePropertyDescriptor<T>
{

   


    public IOGraphNodePropertyDescriptor<T> HasName(Name name)
    {
        throw new NotImplementedException();
    }

    public IOGraphNodePropertyDescriptor<T> HasResolver(IOGraphTypeResolver resolver)
    {
        throw new NotImplementedException();
    }

    public IOGraphNodePropertyDescriptor<T> HasResolver(OGraphTypeResolver<T> resolver)
    {
        throw new NotImplementedException();
    }

    public IOGraphNodePropertyDescriptor<T> HasType<TType>() where TType : IOGraphType, new()
    {
        throw new NotImplementedException();
    }
}
