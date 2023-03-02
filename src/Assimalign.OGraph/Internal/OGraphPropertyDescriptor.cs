using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Internal;


internal class OGraphPropertyDescriptor : IOGraphPropertyDescriptor
{
    public IOGraphPropertyDescriptor UseMetadata(string key, object value)
    {
        throw new NotImplementedException();
    }

    public IOGraphPropertyDescriptor UseMiddleware(IOGraphPropertyMiddleware middleware)
    {
        throw new NotImplementedException();
    }

    public IOGraphPropertyDescriptor UseResolver(IOGraphPropertyResolver resolver)
    {
        throw new NotImplementedException();
    }

    public IOGraphPropertyDescriptor UseResolver(OGraphPropertyResolver resolver)
    {
        throw new NotImplementedException();
    }

    public IOGraphPropertyDescriptor UseResolver<T>(OGraphPropertyResolver<T> resolver)
    {
        throw new NotImplementedException();
    }

    public IOGraphPropertyDescriptor UseType<TType>() where TType : IOGraphType, new()
    {
        throw new NotImplementedException();
    }
}


