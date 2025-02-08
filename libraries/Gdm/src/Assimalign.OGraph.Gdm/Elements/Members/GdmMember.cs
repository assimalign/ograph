namespace Assimalign.OGraph.Gdm.Elements;

using Internal;

public abstract class GdmMember : IOGraphGdmMember
{
    protected GdmMember(Label label, GdmType declaringType)
    {
        Label = label;
        DeclaringType = ThrowHelper.ThrowIfNull(declaringType, nameof(declaringType));
    }


    public virtual bool IsBound { get; internal set; }
    public virtual Label Label { get; internal set; }
    public virtual GdmType DeclaringType { get; internal set; }
    public abstract GdmElementKind ElementKind { get; }
    public GdmMetadata Meta { get; } = new GdmMetadata();
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


    IOGraphGdmMetadata IOGraphGdmElement.Meta => Meta;
    IOGraphGdmType IOGraphGdmMember.DeclaringType => DeclaringType;
}
