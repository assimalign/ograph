using System;

namespace Assimalign.OGraph.Internal;


internal class OGraphPropertyDescriptor : IOGraphPropertyDescriptor
{
    private readonly OGraphProperty property;

    public OGraphPropertyDescriptor(OGraphProperty property)
    {
        if (property is null)
        {
            throw new ArgumentNullException(nameof(property));
        }

        this.property = property;
    }

    public IOGraphPropertyDescriptor UseName(Name name)
    {
        property.Name = name;
        return this;
    }
    public IOGraphPropertyDescriptor UseMetadata(string key, object value)
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
    public IOGraphPropertyDescriptor UseMiddleware(IOGraphPropertyMiddleware middleware)
    {
        if (middleware is null)
        {
            throw new ArgumentNullException(nameof(middleware));
        }
        property.Middleware.Enqueue(middleware);
        return this;
    }
    public IOGraphPropertyDescriptor UseMiddleware(OGraphPropertyMiddleware middleware)
    {
        if (middleware is null)
        {
            throw new ArgumentNullException(nameof(middleware));
        }
        property.Middleware.Enqueue(new OGraphPropertyMiddlewareDefault(middleware));
        return this;
    }
    public IOGraphPropertyDescriptor UseMiddleware<TMiddleware>() where TMiddleware : IOGraphPropertyMiddleware, new()
    {
        property.Middleware.Enqueue(new TMiddleware());
        return this;
    }
    public IOGraphPropertyDescriptor UseResolver(IOGraphPropertyResolver resolver)
    {
        if (resolver is null)
        {
            throw new ArgumentNullException(nameof(resolver));
        }
        property.Resolver = resolver;
        return this;
    }
    public IOGraphPropertyDescriptor UseResolver(OGraphPropertyResolver resolver)
    {
        if (resolver is null)
        {
            throw new ArgumentNullException(nameof(resolver));
        }
        property.Resolver = new OGraphPropertyResolverDefault(resolver);
        return this;
    }
    public IOGraphPropertyDescriptor UseType<TType>() where TType : IOGraphType, new()
    {
        property.Type = new TType();
        return this;
    }

    public IOGraphPropertyDescriptor UseType(Action<IOGraphComplexTypeDescriptor> configure)
    {
        if (configure is null)
        {
            throw new ArgumentNullException(nameof(configure));
        }

        var complexType = new ComplexType();
        var descriptor = new OGraphComplexTypeDescriptor(complexType);

        configure.Invoke(descriptor);

        property.Type = complexType;

        return this;
    }
}


