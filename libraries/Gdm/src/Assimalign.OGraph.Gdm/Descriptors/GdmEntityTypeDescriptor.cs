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
    private readonly GdmGraph graph;
    private readonly List<Action<GdmEntityType<T>>> _actions;

    private GdmName _name;

    public GdmEntityTypeDescriptor(GdmGraph graph)
    {
        Graph = graph;
    }

    internal GdmGraph Graph { get; }

    public GdmEntityTypeDescriptor<T> HasName(GdmName name)
    {
        return this;
    }
    public GdmEntityTypeDescriptor<T> HasKey<TKey>(Expression<Func<T, TKey>> expression) where TKey : struct
    {
        return this;
    }
    public GdmEntityTypeDescriptor<T> HasKey<TKey>(Expression<Func<T, TKey?>> expression) where TKey : struct
    {
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
        var propertyInfo = AssertExpression(expression);

        // Build descriptor with default configuration
        var descriptor = new GdmPropertyDescriptor<T, TProperty?>(Graph)
            .UsePropertyName(propertyInfo.Name)
            .UseGetter(propertyInfo.GetValue)
            .UseSetter(propertyInfo.SetValue);

        _actions.Add(entity =>
        {
            var property = (GdmProperty)(descriptor as IOGraphGdmPropertyDescriptor).Describe();

            entity.Members.Add(property);
        });

        return descriptor;
    }
    //public GdmFunctionDescriptor<TFunction?> HasFunction<TFunction>(Expression<Func<T, TFunction?>> expression)
    //{

    //}



    IOGraphGdmEntityType IOGraphGdmDescriptor<IOGraphGdmEntityType>.Describe()
    {
        return default;
    }

    IOGraphGdmElement IOGraphGdmDescriptor.Describe()
    {
        return (this as IOGraphGdmDescriptor).Describe();
    }
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