using System;
using System.Xml;
using System.Text.Json;
using System.Diagnostics;
using System.Linq;
using System.Reflection;

namespace Assimalign.OGraph.Gdm;

using Assimalign.OGraph.Gdm.Internal;

[DebuggerDisplay("Gdm Type ({Kind}): {Label}")]
public class GdmComplexType<T> : IOGraphGdmComplexType
    where T : class, new()
{
    private readonly Action<IOGraphGdmComplexTypeDescriptor<T>> configure;

    internal Label label = typeof(T).Name;

    private GdmComplexType(Action<IOGraphGdmComplexTypeDescriptor<T>> configure)
    {
        this.configure = configure;
        Initialize();
        Configure(new GdmComplexTypeDescriptor<T>(this));
    }

    public GdmComplexType() 
        : this(descriptor => { })
    {
    }

    private void Initialize()
    {
        foreach (var property in typeof(T).GetGdmComplexTypeProperties())
        {
            property.DeclaringType = new GdmTypeReference()
            {
                Definition = this
            };
            Properties.Add(property);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="descriptor"></param>
    protected virtual void Configure(IOGraphGdmComplexTypeDescriptor<T> descriptor)
    {
        configure?.Invoke(descriptor);
    }

    public Label Label => label;
    public GdmTypeKind Kind => GdmTypeKind.Complex;
    public IOGraphGdmPropertyCollection Properties { get; } = new GdmPropertyCollection();
    public Type RuntimeType => typeof(T);
    public GdmElementType ElementType => GdmElementType.Type;

    public virtual T Read(ref Utf8JsonReader reader)
    {
        throw new NotImplementedException();
    }
    public virtual T Read(XmlReader reader)
    {
        throw new NotImplementedException();
    }
    public virtual void Write(Utf8JsonWriter writer, T value)
    {
        throw new NotImplementedException();
    }
    public virtual void Write(XmlWriter writer, T value)
    {
        throw new NotImplementedException();
    }

    object IOGraphGdmType.Read(ref Utf8JsonReader reader) => Read(ref reader)!;
    object IOGraphGdmType.Read(XmlReader reader) => Read(reader)!;
    void IOGraphGdmType.Write(Utf8JsonWriter writer, object value) => Write(writer, AssertType(value));
    void IOGraphGdmType.Write(XmlWriter writer, object value) => Write(writer, AssertType(value));

    private T AssertType(object value)
    {
        if (value is not T type)
        {
            throw new InvalidOperationException($"Could not write type {value.GetType().Name}. Expected {typeof(T).Name}");
        }
        return type;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Label, typeof(IOGraphGdmComplexType));
    }
    public override bool Equals(object? instance)
    {
        if (instance is not null)
        {
            return GetHashCode() == instance.GetHashCode();
        }

        return false;
    }

    public static GdmComplexType<T> Create<T>(Action<IOGraphGdmComplexTypeDescriptor<T>> configure) where T: class, new()
    {
        if (configure is null)
        {
            throw new ArgumentNullException(nameof(configure));
        }
        return new GdmComplexType<T>(configure);
    }
}