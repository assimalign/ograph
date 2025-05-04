namespace Assimalign.OGraph.Gdm.Elements;

using Internal;

/// <summary>
/// 
/// </summary>
public abstract class GdmMember : GdmElement, IOGraphGdmMember
{
    private GdmName _name;
    private GdmType _declaringType;

    internal GdmMember(GdmName name, GdmType declaringType)
    {
        ThrowHelper.ThrowIfNull(declaringType);
        ThrowHelper.ThrowIfNotType<IOGraphGdmType, IOGraphGdmComplexType, IOGraphGdmEntityType>(declaringType);

        _name = name;
        _declaringType = declaringType;
    }

    public virtual GdmName Name => _name;
    public virtual GdmType DeclaringType => _declaringType;
    public virtual bool IsBound { get; }
    IOGraphGdmMetaCollection IOGraphGdmElement.Meta => Meta;
    IOGraphGdmType IOGraphGdmMember.DeclaringType => DeclaringType;

    public bool IsProperty(out GdmProperty property)
    {
        if (this is GdmProperty p)
        {
            property = p;
            return true;
        }
        property = null!;
        return false;
    }
    public bool IsFunction(out GdmFunction function)
    {
        if (this is GdmFunction f)
        {
            function = f;
            return true;
        }
        function = null!;
        return false;
    }
    bool IOGraphGdmMember.IsProperty(out IOGraphGdmProperty property)
    {
        property = default!;

        if (IsProperty(out var prop))
        {
            property = prop;
        }

        return property is not null;
    }
    bool IOGraphGdmMember.IsFunction(out IOGraphGdmFunction function)
    {
        function = default!;

        if (IsFunction(out var func))
        {
            function = func;
        }

        return function is not null;
    }



}
