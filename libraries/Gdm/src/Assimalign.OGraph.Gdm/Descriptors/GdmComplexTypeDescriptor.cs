using System;
using System.Linq.Expressions;
using System.Reflection;
using System.Diagnostics.CodeAnalysis;

namespace Assimalign.OGraph.Gdm;

using Elements;
using Internal;

public class GdmComplexTypeDescriptor<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)] T> : IOGraphGdmComplexTypeDescriptor
    where T : class, new()
{
    private readonly GdmComplexType<T> _complexType;

    internal GdmComplexTypeDescriptor(GdmComplexType<T> complexType)
    {
        _complexType = complexType;
    }

    public GdmComplexTypeDescriptor<T> HasName(GdmName name)
    {
        _complexType.SetName(name);
        return this;
    }
    public GdmComplexTypeDescriptor<T> HasProperty(GdmName name)
    {
        var propertyInfo = typeof(T).GetProperty(name, BindingFlags.Instance | BindingFlags.Public);
        
        if (propertyInfo is null)
        {
            throw new Exception("The property does not exits.");
        }

        if (!propertyInfo.CanWrite)
        {
            throw new Exception("The property must be writable");
        }

        if (!propertyInfo.CanRead)
        {
            throw new Exception("The property must be readable.");
        }

        GdmProperty? property = default;

   //     var isExisting = _complexType.Members.TryGetProperty(propertyInfo.Name, out property);

        //property.Getter ??= propertyInfo.GetValue;
        //property.Setter ??= propertyInfo.SetValue;

        return this;
    }
    public GdmComplexTypeDescriptor<T> HasProperty(GdmProperty property)
    {
        property.SetDeclaringType(_complexType);
        return this;
    }
    public GdmPropertyDescriptor<T, TProperty> HasProperty<TProperty>(Expression<Func<T, TProperty>> expression)
    {
        var propertyInfo = AssertExpression(expression)!;


        //var property = _complexType.GetProperty(propertyInfo);
        //var method = expression.Compile();
        //property.Getter ??= (instance) => method.Invoke((T)instance);
        //property.Setter ??= propertyInfo.SetValue;
        //property.DeclaringType = new GdmTypeReference()
        //{
        //    Definition = _complexType
        //};
        //return new GdmPropertyDescriptor<TProperty>(property);

        return default;
    }
    public GdmComplexTypeDescriptor<T> AddMeta(string key, string value)
    {
        return this;
    }
    IOGraphGdmComplexTypeDescriptor IOGraphGdmComplexTypeDescriptor.AddMeta(string key, string value)
    {
        return AddMeta(key, value);
    }
    IOGraphGdmComplexTypeDescriptor IOGraphGdmComplexTypeDescriptor.HasFunction(IOGraphGdmFunction function)
    {
        throw new NotImplementedException();
    }
    IOGraphGdmComplexTypeDescriptor IOGraphGdmComplexTypeDescriptor.HasName(GdmName name)
    {
        return HasName(name);
    }
    IOGraphGdmComplexTypeDescriptor IOGraphGdmComplexTypeDescriptor.HasProperty(IOGraphGdmProperty property)
    {
        return HasProperty(ThrowHelper.ThrowIfNotType<GdmProperty>(property));
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
