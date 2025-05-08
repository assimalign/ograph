using System.Reflection;

namespace Assimalign.OGraph.Gdm.Elements;

using Internal;

/// <summary>
/// 
/// </summary>
public abstract class GdmMember : GdmBindableElement, IOGraphGdmMember
{
    private GdmType _declaringType = default!;

    #region Constructors

    internal GdmMember()
    {

    }
    internal GdmMember(GdmName name, GdmEntityType declaringType) : base(name)
    {
        _declaringType = ThrowHelper.ThrowIfNull(declaringType);
    }
    internal GdmMember(GdmName name, GdmComplexType declaringType) : base(name)
    {
        _declaringType = ThrowHelper.ThrowIfNull(declaringType);
    }

    #endregion

    #region Properties

    public GdmType DeclaringType => _declaringType;
    public sealed override GdmElementKind ElementKind { get; } = GdmElementKind.Member;
    IOGraphGdmMetaCollection IOGraphGdmElement.Meta => Meta;
    IOGraphGdmType IOGraphGdmMember.DeclaringType => DeclaringType;

    #endregion

    #region Methods - Public

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

    #endregion

    #region Methods - Internal
    internal void SetDeclaringType(GdmType declaringType) => _declaringType = declaringType;

    #endregion
}
