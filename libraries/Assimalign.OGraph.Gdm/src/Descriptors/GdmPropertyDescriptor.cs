using System;
using System.Linq;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;


namespace Assimalign.OGraph.Gdm;

using Elements;
using Internal;

public class GdmPropertyDescriptor<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)] T, TProperty> : IOGraphGdmPropertyDescriptor
{
    private readonly GdmProperty _property;

    internal GdmPropertyDescriptor(GdmProperty property)
    {
        _property = property;
    }

    public GdmPropertyDescriptor<T, TProperty> UsePropertyName(GdmName name)
    {
        _property.SetName(ThrowHelper.ThrowIfNameEmpty(name));
        return this;
    }
    public GdmPropertyDescriptor<T, TProperty> UseGetter(Func<T, TProperty> func)
    {
        ThrowHelper.ThrowIfNull(func);
        _property.SetGetter((obj) =>
        {
            return func.Invoke(ThrowHelper.ThrowIfNotType<T>(obj));
        });

        return this;
    }
    public GdmPropertyDescriptor<T, TProperty> UseSetter(Action<T, TProperty> action)
    {
        ThrowHelper.ThrowIfNull(action);

        _property.SetSetter((obj, prop) =>
        {
            action.Invoke(
                ThrowHelper.ThrowIfNotType<T>(obj),
                ThrowHelper.ThrowIfNotType<TProperty>(prop));
        });

        return this;
    }

    /// <summary>
    /// The name of the type to locate.
    /// </summary>
    /// <param name="typeName"></param>
    /// <returns></returns>
    public GdmPropertyDescriptor<T, TProperty> UseType(GdmName typeName)
    {
        ThrowHelper.ThrowIfNameEmpty(typeName);

        var graph = _property.DeclaringType.Graph;
        var types = graph.Types as IEnumerable<GdmType>;
        var type = types.Find(typeName);

        if (type is null)
        {
            ThrowHelper.ThrowArgumentException("The type could not be located");
        }

        _property.SetType(type);

        return this;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    public GdmPropertyDescriptor<T, TProperty> UseType(GdmType type)
    {
        ThrowHelper.ThrowIfNull(type);
        
        var graph = _property.DeclaringType.Graph;

        type.SetGraph(graph);
        type.Configure();

        return this;
    }

    public GdmPropertyDescriptor<T, TProperty> UseType<TType>(Func<GdmGraph, TType> func) where TType : GdmType
    {
        ThrowHelper.ThrowIfNull(func);

        _property.SetType(func(_property.DeclaringType.Graph));

        return this;
    }
    public GdmPropertyDescriptor<T, TProperty> UseType<TType>() where TType : GdmType
    {
        return this;
    }

    public GdmPropertyDescriptor<T, TProperty> UseBinding(GdmPropertyBinding binding)
    {
        
        return this;
    }

    public GdmPropertyDescriptor<T, TProperty> AddMeta(string key, object value)
    {
        _property.Meta.Add(key, value);
        return this;
    }

    IOGraphGdmPropertyDescriptor IOGraphGdmPropertyDescriptor.UsePropertyName(GdmName name)
    {
        return UsePropertyName(name);
    }
    IOGraphGdmPropertyDescriptor IOGraphGdmPropertyDescriptor.UseType(IOGraphGdmType type)
    {
        return UseType(ThrowHelper.ThrowIfNotType<GdmType>(type));
    }
    IOGraphGdmPropertyDescriptor IOGraphGdmPropertyDescriptor.UseGetter(GdmPropertyGetter getter)
    {
        return UseGetter((instance => (TProperty)getter(instance!)!));
    }
    IOGraphGdmPropertyDescriptor IOGraphGdmPropertyDescriptor.UseSetter(GdmPropertySetter setter)
    {
        return UseSetter((instance, value) => setter(instance!, value!));
    }
    IOGraphGdmPropertyDescriptor IOGraphGdmPropertyDescriptor.AddMeta(string key, object value)
    {
        return AddMeta(key, value);
    }
    IOGraphGdmPropertyDescriptor IOGraphGdmPropertyDescriptor.IsRequired()
    {
        throw new NotImplementedException();
    }
    IOGraphGdmPropertyDescriptor IOGraphGdmPropertyDescriptor.UseDirective(IOGraphGdmDirective directive)
    {
        // TODO: [O01.01.02.02] type-system runtime
        throw new NotImplementedException();
    }
}