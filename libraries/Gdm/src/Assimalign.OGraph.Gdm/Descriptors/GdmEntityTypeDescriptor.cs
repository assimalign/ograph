using System;
using System.Linq.Expressions;
using System.Diagnostics.CodeAnalysis;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Assimalign.OGraph.Gdm;

using Elements;
using Internal;

public class GdmEntityTypeDescriptor<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)] T> : IOGraphGdmEntityTypeDescriptor
    where T : class, new()
{
    private readonly GdmEntityType<T> _entityType;

    internal GdmEntityTypeDescriptor(GdmEntityType<T> entityType)
    {
        _entityType = entityType;
    }

    public GdmEntityTypeDescriptor<T> HasName(GdmName name)
    {
        _entityType.SetName(name);
        return this;
    }
    public GdmEntityTypeDescriptor<T> HasKey<TKey>(Expression<Func<T, TKey>> expression) where TKey : struct
    {
        // Get Property Info from reflected type
        var propertyInfo = AssertExpression(expression);



        return this;
    }
    public GdmEntityTypeDescriptor<T> HasKey<TKey>(Expression<Func<T, TKey?>> expression) where TKey : struct
    {
        // Get Property Info from reflected type
        var propertyInfo = AssertExpression(expression);

        return this;
    }
    public GdmEntityTypeDescriptor<T> HasProperty(GdmProperty property)
    {
        ThrowHelper.ThrowIfNull(property);


        return this;
    }
    public GdmPropertyDescriptor<T, TProperty?> HasProperty<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)] TProperty>(Expression<Func<T, TProperty?>> expression)
    {
        // Get Property Info from reflected type
        PropertyInfo propertyInfo = AssertExpression(expression);

        // Check for existing property
        GdmProperty? property = _entityType.Members
            .OfType<GdmProperty>()
            .FirstOrDefault(prop => prop.Name == "");

        if (property is null)
        {
            property = new GdmProperty();
            //property.SetMemberInfo(propertyInfo);
            property.SetName(propertyInfo.Name);

            _entityType.Members.Add(property);

            return new GdmPropertyDescriptor<T, TProperty?>(property)
                .UseGetter(expression.Compile())
                .UseSetter((instance, value) => propertyInfo.SetValue(instance, value));
        }

        // Build descriptor with default configuration
        return new GdmPropertyDescriptor<T, TProperty?>(property);
    }
    //public GdmFunctionDescriptor<TFunction?> HasFunction<TFunction>(Expression<Func<T, TFunction?>> expression)
    //{

    //}
    IOGraphGdmEntityTypeDescriptor IOGraphGdmEntityTypeDescriptor.AddMeta(string key, string value)
    {
        throw new NotImplementedException();
    }
    IOGraphGdmEntityTypeDescriptor IOGraphGdmEntityTypeDescriptor.HasFunction(IOGraphGdmFunction function)
    {
        throw new NotImplementedException();
    }
    IOGraphGdmEntityTypeDescriptor IOGraphGdmEntityTypeDescriptor.HasKey(IOGraphGdmEntityKey key)
    {
        throw new NotImplementedException();
    }
    IOGraphGdmEntityTypeDescriptor IOGraphGdmEntityTypeDescriptor.HasName(GdmName name)
    {
        throw new NotImplementedException();
    }
    IOGraphGdmEntityTypeDescriptor IOGraphGdmEntityTypeDescriptor.HasProperty(IOGraphGdmProperty property)
    {
        throw new NotImplementedException();
    }


    //public IOGraphGdmEntityTypeDescriptor<T> HasLabel(Label label)
    //{
    //    entityType.label = label;
    //    return this;
    //}
    //public IOGraphGdmEntityTypeDescriptor<T> HasKey(Label label)
    //{
    //    var propertyInfo = typeof(T).GetProperty(label);
    //    if (propertyInfo is null)
    //    {
    //        throw new InvalidOperationException($"The property '{label}' does not exist on type {typeof(T).Name}");
    //    }
    //    var property = entityType.GetProperty(propertyInfo);
    //    property.Getter ??= propertyInfo.GetValue;
    //    property.Setter ??= propertyInfo.SetValue;
    //    return this;
    //}
    //public IOGraphGdmEntityTypeDescriptor<T> HasKey<TMember>(Expression<Func<T, TMember>> expression) where TMember : struct
    //{
    //    var propertyInfo = AssertExpression(expression);
    //    var property = entityType.GetProperty(propertyInfo);
    //    var method = expression.Compile();

    //    property.Getter ??= (instance) => method.Invoke((T)instance);
    //    property.Setter ??= propertyInfo.SetValue;

    //    return this;
    //}
    //public IOGraphGdmEntityTypeDescriptor<T> HasKey<TMember>(Expression<Func<T, TMember?>> expression) where TMember : struct
    //{
    //    var propertyInfo = AssertExpression(expression);
    //    var property = entityType.GetProperty(propertyInfo);
    //    var method = expression.Compile();

    //    property.Getter ??= (instance) => method.Invoke((T)instance);
    //    property.Setter ??= propertyInfo.SetValue;

    //    return this;
    //}
    //public IOGraphGdmPropertyDescriptor HasProperty(Label label)
    //{
    //    var propertyInfo = typeof(T).GetProperty(label);
    //    if (propertyInfo is null)
    //    {
    //        throw new Exception();
    //    }
    //    var property = entityType.GetProperty(propertyInfo);
    //    property.Getter ??= propertyInfo.GetValue;
    //    property.Setter ??= propertyInfo.SetValue;
    //    property.DeclaringType = new GdmTypeReference()
    //    {
    //        Definition = entityType
    //    };
    //    return new GdmPropertyDescriptor(property);
    //}
    //public IOGraphGdmPropertyDescriptor<TMember?> HasProperty<TMember>(Expression<Func<T, TMember?>> expression)
    //{
    //    var propertyInfo = AssertExpression(expression)!;
    //    var property = entityType.GetProperty(propertyInfo);
    //    var method = expression.Compile();

    //    property.Getter ??= (instance) => method.Invoke((T)instance);
    //    property.Setter ??= propertyInfo.SetValue;
    //    property.DeclaringType = new GdmTypeReference()
    //    {
    //        Definition = entityType
    //    };

    //    return new GdmPropertyDescriptor<TMember?>(property);
    //}

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