using System;

namespace Assimalign.OGraph.Internal;

internal class PropertyDescriptor<T> : IOGraphPropertyDescriptor<T>
{
    private readonly Property property;

    public PropertyDescriptor(Property? property)
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
        if (middleware is null)
        {
            throw new ArgumentNullException(nameof(middleware));
        }
        property.Middleware.Enqueue(new PropertyMiddlewareDefault(middleware));
        return this;
    }
    public IOGraphPropertyDescriptor<T> UseMiddleware<TMiddleware>() 
        where TMiddleware : IOGraphPropertyMiddleware, new()
    {
        property.Middleware.Enqueue(new TMiddleware());
        return this;
    }
    public IOGraphPropertyDescriptor<T> UseName(Label name)
    {
        property.Name = name;
        return this;
    }
    public IOGraphPropertyDescriptor<T> UseResolver(IOGraphPropertyResolver resolver)
    {
        if (resolver is null)
        {
            throw new ArgumentNullException(nameof(resolver));
        }
        property.Resolver = resolver;
        return this;
    }
    public IOGraphPropertyDescriptor<T> UseResolver(OGraphPropertyResolver resolver)
    {
        if (resolver is null)
        {
            throw new ArgumentNullException(nameof(resolver));
        }
        property.Resolver = new PropertyResolverDefault(resolver);
        return this;
    }
    public IOGraphPropertyDescriptor<T> UseResolver<TResolver>() where TResolver : IOGraphPropertyResolver, new()
    {
        property.Resolver = new TResolver();
        return this;
    }
    public IOGraphPropertyDescriptor<T> UseType<TType>() where TType : IOGraphType, new()
    {
        property.Type = new TType();
        return this;
    }
    public IOGraphPropertyDescriptor<T> UseType(IOGraphType type)
    {
        if (type is null)
        {
            throw new ArgumentNullException(nameof(type));
        }
        property.Type = type;
        return this;
    }
    public IOGraphPropertyDescriptor<T> UseType(Action<IOGraphComplexTypeDescriptor<T>> action)
    {
        throw new NotImplementedException();
    }
}
