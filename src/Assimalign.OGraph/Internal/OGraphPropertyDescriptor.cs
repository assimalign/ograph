using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Assimalign.OGraph.Internal;


internal class OGraphPropertyDescriptor : IOGraphPropertyDescriptor
{
    private readonly IOGraphProperty property;

    public OGraphPropertyDescriptor(IOGraphProperty property)
    {
        if (property is null)
        {
            throw new ArgumentNullException(nameof(property));
        }

        this.property = property;
    }

    public IOGraphPropertyDescriptor UseMetadata(string key, object value)
    {
        throw new NotImplementedException();
    }

    public IOGraphPropertyDescriptor UseMiddleware(IOGraphPropertyMiddleware middleware)
    {
        throw new NotImplementedException();
    }

    public IOGraphPropertyDescriptor UseMiddleware(OGraphPropertyMiddleware middleware)
    {
        throw new NotImplementedException();
    }

    public IOGraphPropertyDescriptor UseMiddleware<TMiddleware>() where TMiddleware : IOGraphPropertyMiddleware, new()
    {
        throw new NotImplementedException();
    }

    public IOGraphPropertyDescriptor UseResolver(IOGraphPropertyResolver resolver)
    {
        if (resolver is null)
        {
            throw new ArgumentNullException(nameof(resolver));
        }

        throw new NotImplementedException();
    }

    public IOGraphPropertyDescriptor UseResolver(OGraphPropertyResolver resolver)
    {
        if (resolver is null)
        {
            throw new ArgumentNullException(nameof(resolver));
        }

        var prop = property.GetType().GetProperty(nameof(IOGraphProperty.Resolver));

        if (prop is null)
        {
            throw new Exception();
        }
        if (!prop.CanWrite)
        {
            throw new Exception();
        }

        prop.SetValue(property, new OGraphPropertyResolverDefault(resolver), null);

        return this;
    }

    public IOGraphPropertyDescriptor UseResolver<T>(OGraphPropertyResolver<T> resolver)
    {
        if (resolver is null)
        {
            throw new ArgumentNullException(nameof(resolver));
        }

        var prop = property.GetType().GetProperty(nameof(IOGraphProperty.Resolver));

        if (prop is null)
        {
            throw new Exception();
        }
        if (!prop.CanWrite)
        {
            throw new Exception();
        }

        prop.SetValue(property, new OGraphPropertyResolverDefault<T>(resolver), null);

        return this;
    }

    public IOGraphPropertyDescriptor UseType<TType>() where TType : IOGraphType, new()
    {
        throw new NotImplementedException();
    }
}


