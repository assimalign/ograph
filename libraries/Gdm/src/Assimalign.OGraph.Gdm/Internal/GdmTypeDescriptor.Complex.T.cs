using System;
using System.Linq.Expressions;
using System.Reflection;
using System.Diagnostics.CodeAnalysis;

namespace Assimalign.OGraph.Gdm.Internal;

using Assimalign.OGraph.Gdm.Elements;

internal class GdmComplexTypeDescriptor<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties)] T> : IOGraphGdmComplexTypeDescriptor<T>
    where T : class, new()
{
    private readonly GdmComplexType<T> complexType;

    public GdmComplexTypeDescriptor(GdmComplexType<T> complexType)
    {
        this.complexType = complexType;
    }

    public IOGraphGdmFunctionDescriptor<TFunction> HasFunction<TFunction>(Expression<Func<T, TFunction>> expression)
    {
        throw new NotImplementedException();
    }

    public IOGraphGdmComplexTypeDescriptor<T> HasLabel(Label label)
    {
        complexType.label = label;
        return this;
    }
    public IOGraphGdmPropertyDescriptor HasProperty(Label label)
    {
        var propertyInfo = typeof(T).GetProperty(label);
        if (propertyInfo is null)
        {
            throw new Exception();
        }
        var property = complexType.GetProperty(propertyInfo);
        property.Getter ??= propertyInfo.GetValue;
        property.Setter ??= propertyInfo.SetValue;
        property.DeclaringType = new GdmTypeReference()
        {
            Definition = complexType
        };
        return new GdmPropertyDescriptor(property);
    }
    public IOGraphGdmPropertyDescriptor<TMember> HasProperty<TMember>(Expression<Func<T, TMember>> expression)
    {
        var propertyInfo = AssertExpression(expression)!;
        var property = complexType.GetProperty(propertyInfo);
        var method = expression.Compile();
        property.Getter ??= (instance) => method.Invoke((T)instance);
        property.Setter ??= propertyInfo.SetValue;
        property.DeclaringType = new GdmTypeReference()
        {
            Definition = complexType
        };
        return new GdmPropertyDescriptor<TMember>(property);
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
