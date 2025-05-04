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
    private GdmPropertyGetter? _getter;
    private GdmPropertySetter? _setter;
    private GdmType? _type;
    private GdmName _name;

    internal GdmPropertyDescriptor(GdmGraph graph)
    {
        Graph = graph;

        //_type = (graph.Types as IEnumerable<GdmType>).FirstOrDefault(p => p.IsAssignableTo(typeof(T), checkNullable: true)
    }

    internal GdmGraph Graph { get; }

    public GdmPropertyDescriptor<T, TProperty> UsePropertyName(GdmName name)
    {
        _name = name;
        return this;
    }
    public GdmPropertyDescriptor<T, TProperty> UseSetter(GdmPropertySetter setter)
    {
        _setter = ThrowHelper.ThrowIfNull(setter);
        return this;
    }
    public GdmPropertyDescriptor<T, TProperty> UseGetter(GdmPropertyGetter getter)
    {
        _setter = ThrowHelper.ThrowIfNull(setter);
        return this;
    }
    public GdmPropertyDescriptor<T, TProperty> UseType(GdmName typeName)
    {
        return this;
    }
    public GdmPropertyDescriptor<T, TProperty> UseType(GdmType type)
    {
        _type = Graph.Types.GetOrAdd(type.Name, type.RuntimeType, (_, _) =>
        {
            type.Initialize(Graph);

            return type;
        });

        return this;
    }
    
    public GdmPropertyDescriptor<T, TProperty> UseType<TType>() where TType : GdmType, new()
    {
        return UseType(new TType());
    }

    internal GdmProperty GetProperty(GdmType declaringType)
    {
        if (_type is null)
        {
            var runtimeType = typeof(T);

            _type = Graph.Types.GetOrAdd(runtimeType.Name, runtimeType, (name, runtimeType) =>
            {

            });
        }

        if(_getter is null)
        {
            throw new Exception();
        }

        if (_setter is null)
        {
            throw new Exception();
        }


        var gdmProperty = new GdmProperty(
            _name,
            _type,
            declaringType,
            _getter,
            _setter);
    }

    internal GdmProperty Describe()
    {
        return default;
    }

    IOGraphGdmPropertyDescriptor IOGraphGdmPropertyDescriptor.UsePropertyName(GdmName name)
    {
        return UsePropertyName(name);
    }
    IOGraphGdmPropertyDescriptor IOGraphGdmPropertyDescriptor.UseType(IOGraphGdmType type)
    {
        throw new NotImplementedException();
    }
    IOGraphGdmPropertyDescriptor IOGraphGdmPropertyDescriptor.UseGetter(GdmPropertyGetter getter)
    {
        return UseGetter(getter);
    }
    IOGraphGdmPropertyDescriptor IOGraphGdmPropertyDescriptor.UseSetter(GdmPropertySetter setter)
    {
        return UseSetter(setter);
    }
    IOGraphGdmPropertyDescriptor IOGraphGdmPropertyDescriptor.AddMeta(string key, string value)
    {
        throw new NotImplementedException();
    }
    IOGraphGdmPropertyDescriptor IOGraphGdmPropertyDescriptor.IsRequired()
    {
        throw new NotImplementedException();
    }
    IOGraphGdmProperty IOGraphGdmDescriptor<IOGraphGdmProperty>.Describe()
    {
        return Describe();
    }
    IOGraphGdmElement IOGraphGdmDescriptor.Describe()
    {
        return Describe();
    }
}