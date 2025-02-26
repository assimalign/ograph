using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Assimalign.OGraph.Gdm.Internal;

using Assimalign.OGraph.Gdm.Elements;

internal class GdmEntityTypeDescriptor<[DynamicallyAccessedMembers(
    DynamicallyAccessedMemberTypes.PublicParameterlessConstructor |
    DynamicallyAccessedMemberTypes.PublicProperties)] T> : IOGraphGdmEntityTypeDescriptor<T>
    where T : class, new()
{
    private readonly GdmEntityType<T> entityType;
    private readonly GdmGraph graph;

    public GdmEntityTypeDescriptor(GdmEntityType<T> entityType, GdmGraph graph)
    {
        this.entityType = entityType;
        this.graph = graph;
    }

    public IOGraphGdmFunctionDescriptor HasFunction(GdmLabel label)
    {
        throw new NotImplementedException();
    }

    public IOGraphGdmFunctionDescriptor<TFunction?> HasFunction<TFunction>(Expression<Func<T, TFunction?>> expression)
    {
        throw new NotImplementedException();
    }

    public IOGraphGdmEntityTypeDescriptor<T> HasKey(GdmLabel label)
    {
        throw new NotImplementedException();
    }

    public IOGraphGdmEntityTypeDescriptor<T> HasKey<TKey>(Expression<Func<T, TKey>> expression) where TKey : struct
    {
        throw new NotImplementedException();
    }

    public IOGraphGdmEntityTypeDescriptor<T> HasKey<TKey>(Expression<Func<T, TKey?>> expression) where TKey : struct
    {
        throw new NotImplementedException();
    }

    public IOGraphGdmEntityTypeDescriptor<T> HasName(GdmLabel label)
    {
        throw new NotImplementedException();
    }

    public IOGraphGdmPropertyDescriptor HasProperty(GdmLabel label)
    {
        throw new NotImplementedException();
    }

    public IOGraphGdmPropertyDescriptor<TProperty?> HasProperty<TProperty>(
        Expression<Func<T, TProperty?>> expression)
    {
        GdmProperty property = default!;

        return new GdmPropertyDescriptor<TProperty?>(property, graph);
    }

    public IOGraphGdmPropertyDescriptor<T> WithMeta(string key, string value)
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

    //private PropertyInfo AssertExpression<TMember>(Expression<Func<T, TMember>> expression)
    //{
    //    if (expression is null)
    //    {
    //        throw new ArgumentNullException(nameof(expression));
    //    }
    //    if (expression.Body is not MemberExpression memberExpression)
    //    {
    //        throw new ArgumentException("");
    //    }
    //    //if (!memberExpression.Member.DeclaringType.IsAssignableTo(typeof(T)))
    //    //{
    //    //    throw new Exception();
    //    //}
    //    if (memberExpression.Member is not PropertyInfo propertyInfo)
    //    {
    //        throw new Exception();
    //    }

    //    return propertyInfo;
    //}
}