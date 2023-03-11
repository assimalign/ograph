using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Internal;

internal class OGraphPropertyDescriptor<T> : IOGraphPropertyDescriptor<T>
{
    private readonly OGraphProperty property;

    public OGraphPropertyDescriptor(OGraphProperty? property)
    {
        if (property is null)
        {
            throw new ArgumentNullException(nameof(property));
        }

        this.property = property;   
    }

    public IOGraphPropertyDescriptor<T> UseMetadata(string key, object value)
    {
        if (key is null)
        {
            throw new ArgumentNullException(nameof(key));
        }
        if (value is null)
        {
            throw new ArgumentNullException(nameof(value));
        }

        property.Metadata[key] = value;

        return this;
    }

    public IOGraphPropertyDescriptor<T> UseMiddleware(IOGraphPropertyMiddleware middleware)
    {
        if (middleware is null)
        {
            throw new ArgumentNullException(nameof(middleware));
        }
        
        property.Middleware.Enqueue(middleware);

        return this;
    }

    public IOGraphPropertyDescriptor<T> UseMiddleware(OGraphPropertyMiddleware middleware)
    {
        throw new NotImplementedException();
    }

    public IOGraphPropertyDescriptor<T> UseMiddleware<TMiddleware>() where TMiddleware : IOGraphPropertyMiddleware, new()
    {
        throw new NotImplementedException();
    }

    public IOGraphPropertyDescriptor<T> UseName(Name name)
    {
        property.Name = name;

        return this;
    }

    public IOGraphPropertyDescriptor<T> UseResolver(IOGraphPropertyResolver resolver)
    {



        return this;
    }

    public IOGraphPropertyDescriptor<T> UseResolver(OGraphPropertyResolver<T> resolver)
    {
        if (resolver is null)
        {
            throw new ArgumentNullException(nameof(resolver));
        }

        

        return this;
    }

    public IOGraphPropertyDescriptor<T> UseResolver(OGraphPropertyResolver resolver)
    {
        if (resolver is null)
        {
            throw new ArgumentNullException(nameof(resolver));
        }

        property.Resolver = new OGraphPropertyResolverDefault(resolver);

        return this;
    }

    public IOGraphPropertyDescriptor<T> UseType<TType>() where TType : IOGraphType, new()
    {
        return this;
    }
}
