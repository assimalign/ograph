using System;
using System.Reflection;
using System.Linq.Expressions;
using System.Collections.Generic;

namespace Assimalign.OGraph.Gdm.Elements;

using Internal;

public sealed class GdmProperty : GdmMember, IOGraphGdmProperty
{
    private Lazy<GdmType> _type = default!;
    private GdmPropertyGetter _getter = default!;
    private GdmPropertySetter _setter = default!;
    private bool _isReadOnly;
    private bool _isNullable;

    #region Constructors

    internal GdmProperty() { }

    internal GdmProperty(GdmName name, GdmType type, GdmEntityType declaringType, bool isReadOnly = false, bool isNullable = false)
        : base(name, declaringType)
    {
        _type = new Lazy<GdmType>(ThrowHelper.ThrowIfNull(type));
        _isReadOnly = isReadOnly;
        _isNullable = isNullable;

        SetGetterAndSetter();
    }

    internal GdmProperty(
        GdmName name,
        GdmType type,
        GdmComplexType declaringType,
        bool isReadOnly = false,
        bool isNullable = false)
        : base(name, declaringType)
    {
        _type = new Lazy<GdmType>(ThrowHelper.ThrowIfNull(type));
        _isReadOnly = isReadOnly;
        _isNullable = isNullable;

        SetGetterAndSetter();
    }

    #endregion

    public GdmType Type => _type.Value;
    public GdmPropertyGetter Getter => _getter;
    public GdmPropertySetter Setter => _setter;
    public bool IsReadOnly => _isReadOnly;
    public bool IsNullable => _isNullable;
    IOGraphGdmType IOGraphGdmProperty.Type => Type;
    private void SetGetterAndSetter()
    {
        if (typeof(Dictionary<string, object>) == DeclaringType.RuntimeType)
        {
            _getter = (obj) =>
            {
                if (obj is Dictionary<string, object> dict)
                {
                    return dict[Name];
                }
                throw new InvalidCastException();
            };
            _setter = (obj, value) =>
            {
                if (obj is Dictionary<string, object> dict)
                {
                    dict[Name] = value;
                }
                throw new InvalidCastException();
            };
        }
        else
        {
            var propertyInfo = DeclaringType.RuntimeType.GetProperty(
                Name, 
                BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);

            if (propertyInfo is null)
            {
                throw new Exception();
            }

            _setter = propertyInfo.SetValue;
            _getter = propertyInfo.GetValue;

        }
    }

    internal void SetNullable(bool isNullable = true) => _isNullable = isNullable;
    internal void SetReadOnly(bool isReadOnly = true) => _isReadOnly = isReadOnly;
    internal void SetType(GdmType type) => _type = new Lazy<GdmType>(ThrowHelper.ThrowIfNull(type));
    internal void SetGetter(GdmPropertyGetter getter) => _getter = ThrowHelper.ThrowIfNull(getter);
    internal void SetSetter(GdmPropertySetter setter) => _setter = ThrowHelper.ThrowIfNull(setter);


    //    private void SetGetterAndSetter()
    //    {
    //#pragma warning disable IL2075 // 'this' argument does not satisfy 'DynamicallyAccessedMembersAttribute' in call to target method. The return value of the source method does not have matching annotations.
    //        var propertyInfo = Type.RuntimeType.GetProperty(Name, BindingFlags.Instance | BindingFlags.Public | BindingFlags.IgnoreCase);

    //        if (propertyInfo is null)
    //        {
    //            ThrowHelper.ThrowArgumentException("");
    //        }

    //        _setter = propertyInfo.SetValue;
    //        _getter = propertyInfo.GetValue;

    //        SetMemberInfo(propertyInfo);
    //#pragma warning restore IL2075 // 'this' argument does not satisfy 'DynamicallyAccessedMembersAttribute' in call to target method. The return value of the source method does not have matching annotations.
    //    }
}