using Assimalign.OGraph.Gdm.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Gdm.Elements;

public abstract class GdmMember : IOGraphGdmMember
{
    protected GdmMember(Label label)
    {
        
    }


    public virtual bool IsBound { get; set; }
    public virtual Label Label { get; set; }
    public virtual IOGraphGdmType DeclaringType { get; set; } = default!;
    public IOGraphGdmMetadata Meta { get; } = new GdmMetadata();
    public abstract GdmElementKind ElementKind { get; }
}
