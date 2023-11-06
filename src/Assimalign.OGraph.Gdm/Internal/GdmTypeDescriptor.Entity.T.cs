using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Assimalign.OGraph.Gdm.Internal;

internal class GdmEntityTypeDescriptor<T> : IOGraphGdmEntityTypeDescriptor<T> where T : class, new()
{
    private readonly GdmEntityType<T> entityType;
    public GdmEntityTypeDescriptor(GdmEntityType<T> entityType)
    {
        this.entityType = entityType;
    }
    public GdmBuilderContext Context { get; init; } = default!;

    public IOGraphGdmEntityTypeDescriptor<T> HasKey(Label label)
    {
        var propertyInfo = typeof(T).GetProperty(label, BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);
        
        if (propertyInfo is null)
        {
            throw new InvalidOperationException($"The property '{label}' does not exist on type {typeof(T).Name}");
        }
        
        var propertyName = propertyInfo.Name;
        var property = entityType.Properties.First(p =>
        {
            if (p is GdmProperty ip) // ip = internal property
            {
                return ip.PropertyInfo.Name == propertyInfo.Name;
            }
            return p.Name == label;
        });
        if (property is not GdmProperty ip)
        {
            ip = WrapProperty(property, propertyInfo);
        }

        entityType.keyResolver = instance =>
        {
            return propertyInfo.GetValue(instance)!;
        };

        ip.IsKey = true;

        return this;
    }
    public IOGraphGdmEntityTypeDescriptor<T> HasKey<TMember>(Expression<Func<T, TMember>> expression) where TMember : struct
    {
        var propertyInfo = AssertExpression(expression);
        var properties = entityType.Properties;
        var property = entityType.Properties.First(p =>
        {
            if (p is GdmProperty ip) // ip = internal property
            {
                return ip.PropertyInfo.Name == propertyInfo.Name;
            }
            return p.Name == propertyInfo.Name;
        });
        //TODO: revisit logic
        if (property is not GdmProperty ip)
        {
            ip = WrapProperty(property, propertyInfo);
        }
        var method = expression.Compile();
        entityType.keyResolver = value =>
        {
            if (value is not T t)
            {
                throw new InvalidOperationException("");
            }
            return method.Invoke(t);
        };
        ip.IsKey = true;

        return this;
    }
    public IOGraphGdmEntityTypeDescriptor<T> HasKey<TMember>(Expression<Func<T, TMember?>> expression) where TMember : struct
    {
        var propertyInfo = AssertExpression(expression);
        var propertyName = propertyInfo.Name;
        var property = entityType.Properties.First(p =>
        {
            if (p is GdmProperty ip) // ip = internal property
            {
                return ip.PropertyInfo.Name == propertyInfo.Name;
            }
            return p.Name == propertyName;
        });

        entityType.keyResolver = instance =>
        {
            return propertyInfo.GetValue(instance)!;
        };

        return this;
    }
    public IOGraphGdmEntityTypeDescriptor<T> Ignore(Label label)
    {
        var propertyInfo = typeof(T).GetProperty(label, BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);
        if (propertyInfo is null)
        {
            throw new InvalidOperationException($"The property '{label}' does not exist on type {typeof(T).Name}");
        }
        
        var propertyName = propertyInfo.Name;
        var properties = entityType.Properties;
        var property = properties.FirstOrDefault(p => p.Name == propertyName);

        if (property is not null)
        {
            properties.Remove(property);
        }

        return this;
    }
    public IOGraphGdmEntityTypeDescriptor<T> Ignore<TMember>(Expression<Func<T, TMember>> expression)
    {
        var propertyInfo = AssertExpression(expression);
        var propertyName = propertyInfo.Name;
        var properties = entityType.Properties;
        var property = properties.First(p =>
        {
            if (p is GdmProperty ip) // ip = internal property
            {
                return ip.PropertyInfo.Name == propertyInfo.Name;
            }
            return p.Name == propertyInfo.Name;
        });

        if (property is not null)
        {
            properties.Remove(property);
        }

        return this;
    }
    public IOGraphGdmPropertyDescriptor<TMember?> HasProperty<TMember>(Expression<Func<T, TMember?>> expression)
    {
        var propertyInfo = AssertExpression(expression);
        var property = entityType.Properties.First(p =>
        {
            if (p is GdmProperty ip) // ip = internal property
            {
                return ip.PropertyInfo.Name == propertyInfo.Name;
            }
            return p.Name == propertyInfo.Name;
        });
        if (property is not null && property is GdmProperty internalProp)
        {
            return new GdmPropertyDescriptor<TMember?>(internalProp);
        }

        throw new NotImplementedException();
    }


    // This might be overkill., but a user may pass their own implementation of IOGraphGdmProperty and add it manually. 
    // If they we need to wrap it and pull it out of the current collection in the entity
    private GdmProperty WrapProperty(IOGraphGdmProperty property, PropertyInfo propertyInfo)
    {
        entityType.Properties.Remove(property);

        var wrapped = new GdmProperty()
        {
            IsComputed = property.IsComputed,
            Getter = property.Getter,
            Setter = property.Setter,
            PropertyInfo = propertyInfo,
            IsNullable = property.IsNullable,
            Metadata = property.Metadata,
            Name = property.Name,
            Type = property.Type
        };

        entityType.Properties.Add(wrapped);

        return wrapped;
    }
    private PropertyInfo AssertExpression<TMember>(Expression<Func<T, TMember>> expression)
    {
        if (expression is null)
        {
            throw new ArgumentNullException(nameof(expression));
        }
        if (expression.Body is not MemberExpression memberExpression)
        {
            throw new ArgumentException("");
        }
        //if (!memberExpression.Member.DeclaringType.IsAssignableTo(typeof(T)))
        //{
        //    throw new Exception();
        //}
        if (memberExpression.Member is not PropertyInfo propertyInfo)
        {
            throw new Exception();
        }
        return propertyInfo;
    }
}
