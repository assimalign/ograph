using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection.Emit;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics.CodeAnalysis;

namespace Assimalign.OGraph.Gdm.Internal;

internal class GdmComplexTypeDescriptor<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties)] T> : IOGraphGdmComplexTypeDescriptor<T> 
    where T : class, new()
{
    private readonly GdmComplexType<T> complexType;

    public GdmComplexTypeDescriptor(GdmComplexType<T> complexType)
    {
        this.complexType = complexType;
    }

    public IOGraphGdmComplexTypeDescriptor<T> HasLabel(Label label)
    {
        complexType.Label = label;
        return this;
    }

    public IOGraphGdmPropertyDescriptor HasProperty(Label name)
    {
        throw new NotImplementedException();
    }

    public IOGraphGdmPropertyDescriptor<TMember> HasProperty<TMember>(Expression<Func<T, TMember>> expression)
    {
        var propertyInfo = AssertExpression(expression);
        var property = complexType.Properties.First(p =>
        {
            if (p is GdmProperty ip) // ip = internal property
            {
                return ip.PropertyInfo.Name == propertyInfo.Name;
            }
            return p.Label == propertyInfo.Name;
        });
        if (property is not null && property is GdmProperty internalProp)
        {
            return new GdmPropertyDescriptor<TMember>(internalProp);
        }

        throw new NotImplementedException();
    }

    public IOGraphGdmComplexTypeDescriptor<T> Ignore(Label label)
    {
        var propertyInfo = typeof(T).GetProperty(label);
        if (propertyInfo is null)
        {
            throw new InvalidOperationException($"The property '{label}' does not exist on type {typeof(T).Name}");
        }

        var propertyName = propertyInfo.Name;
        var properties = complexType.Properties;
        var property = properties.FirstOrDefault(p => p.Label == propertyName);

        if (property is not null)
        {
            properties.Remove(property);
        }

        return this;
    }

    public IOGraphGdmComplexTypeDescriptor<T> Ignore<TProperty>(Expression<Func<T, TProperty>> expression)
    {
        var propertyInfo = AssertExpression(expression);
        var propertyName = propertyInfo.Name;
        var properties = complexType.Properties;
        var property = properties.First(p =>
        {
            if (p is GdmProperty ip) // ip = internal property
            {
                return ip.PropertyInfo.Name == propertyInfo.Name;
            }
            return p.Label == propertyInfo.Name;
        });

        if (property is not null)
        {
            properties.Remove(property);
        }

        return this;
    }

    private GdmProperty WrapProperty(IOGraphGdmProperty property, PropertyInfo propertyInfo)
    {
        complexType.Properties.Remove(property);

        var wrapped = new GdmProperty()
        {
            IsComputed = property.IsComputed,
            Getter = property.Getter,
            Setter = property.Setter,
            PropertyInfo = propertyInfo,
            IsNullable = property.IsNullable,
            Metadata = property.Metadata,
            Label = property.Label,
            Type = property.Type
        };

        complexType.Properties.Add(wrapped);

        return wrapped;
    }
    private PropertyInfo AssertExpression<TMember>(Expression<Func<T, TMember>> expression)
    {
        // 1. Null reference check
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
