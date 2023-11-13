using System;
using System.Xml;
using System.Text.Json;
using System.Diagnostics;

namespace Assimalign.OGraph.Gdm;

using Assimalign.OGraph.Gdm.Internal;

[DebuggerDisplay("Gdm Type ({Kind}): {Label}")]
public class GdmEntityType<T> : IOGraphGdmEntityType
    where T : class, new()
{
    private readonly Action<IOGraphGdmEntityTypeDescriptor<T>>? configure;

    internal Label label = typeof(T).Name;
    internal GdmEntityKeyResolver keyResolver = default!;

    private GdmEntityType(Action<IOGraphGdmEntityTypeDescriptor<T>> configure)
    {
        this.configure = configure;
        Initialize();
        Configure(new GdmEntityTypeDescriptor<T>(this));
    }


    public GdmEntityType()
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

    protected virtual void Configure(IOGraphGdmEntityTypeDescriptor<T> descriptor)
    {
        configure?.Invoke(descriptor);
    }

    public Label Label => label;
    public GdmTypeKind Kind => GdmTypeKind.Entity;
    public IOGraphGdmPropertyCollection Properties { get; } = new GdmPropertyCollection();
    public Type RuntimeType => typeof(T);
    public GdmEntityKeyResolver KeyResolver => keyResolver!;
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

    #region Static Members/Methods
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <param name="configure"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static GdmEntityType<TEntity> Create<TEntity>(Action<IOGraphGdmEntityTypeDescriptor<TEntity>> configure) 
        where TEntity : class, new()
    {
        if (configure is null)
        {
            throw new ArgumentNullException(nameof(configure));
        }

        return new GdmEntityType<TEntity>(configure);
    }
    #endregion

    #region Overloads
    /// <inheritdoc />
    public override string ToString()
    {
        return Label;
    }

    /// <inheritdoc />
    public override int GetHashCode()
    {
        return HashCode.Combine(Label, typeof(IOGraphGdmEntityType));
    }

    /// <inheritdoc />
    public override bool Equals(object? instance)
    {
        if (instance is not null)
        {
            return GetHashCode() == instance.GetHashCode();
        }
        return false;
    }
    #endregion
}