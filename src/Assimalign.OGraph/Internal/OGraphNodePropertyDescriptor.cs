using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Internal;


internal class OGraphNodePropertyDescriptor : IOGraphNodePropertyDescriptor
{
    public IOGraphNodePropertyDescriptor UseResolver(IOGraphTypeResolver resolver)
    {
        throw new NotImplementedException();
    }

    public IOGraphNodePropertyDescriptor UseResolver(OGraphTypeResolver resolver)
    {
        throw new NotImplementedException();
    }

    public IOGraphNodePropertyDescriptor UseType<TType>() where TType : IOGraphType, new()
    {
        throw new NotImplementedException();
    }
}

internal class OGraphNodePropertyDescriptor<T> : IOGraphNodePropertyDescriptor<T>
{

   
    public OGraphNodeProperty Property { get; init; }

    public IOGraphNodePropertyDescriptor<T> UseName(Name name)
    {
        return this;
    }

    public IOGraphNodePropertyDescriptor<T> UseResolver(IOGraphTypeResolver resolver)
    {



        return this;
    }

    public IOGraphNodePropertyDescriptor<T> UseResolver(OGraphTypeResolver<T> resolver)
    {
        return this;
    }

    public IOGraphNodePropertyDescriptor<T> UseType<TType>() where TType : IOGraphType, new()
    {
        return this;
    }
}
