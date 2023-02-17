using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Internal;


internal class OGraphNodePropertyDescriptor : IOGraphNodePropertyDescriptor
{
    public IOGraphNodePropertyDescriptor HasResolver(IOGraphTypeResolver resolver)
    {
        throw new NotImplementedException();
    }

    public IOGraphNodePropertyDescriptor HasResolver(OGraphTypeResolver resolver)
    {
        throw new NotImplementedException();
    }

    public IOGraphNodePropertyDescriptor HasType<TType>() where TType : IOGraphType, new()
    {
        throw new NotImplementedException();
    }
}

internal class OGraphNodePropertyDescriptor<T> : IOGraphNodePropertyDescriptor<T>
{

   
    public OGraphNodeProperty Property { get; init; }

    public IOGraphNodePropertyDescriptor<T> HasName(Name name)
    {
        return this;
    }

    public IOGraphNodePropertyDescriptor<T> HasResolver(IOGraphTypeResolver resolver)
    {



        return this;
    }

    public IOGraphNodePropertyDescriptor<T> HasResolver(OGraphTypeResolver<T> resolver)
    {
        return this;
    }

    public IOGraphNodePropertyDescriptor<T> HasType<TType>() where TType : IOGraphType, new()
    {
        return this;
    }
}
