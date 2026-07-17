using System.Diagnostics;

namespace Assimalign.OGraph.Gdm.Internal;

[DebuggerDisplay("Gdm = {Label} [{ElementKind}]")]
internal abstract class GdmMember : IOGraphGdmMember
{
    public virtual bool IsBound { get; set; }
    public virtual Label Label { get; set; }
    public virtual IOGraphGdmTypeReference DeclaringType { get; set; } = default!;  
    public virtual IOGraphGdmMetadata Meta { get; } = new GdmMetadata();
    public abstract GdmElementKind ElementKind { get; }
}
