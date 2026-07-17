using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Gdm.Internal;

internal class GdmFunction : GdmMember, IOGraphGdmFunction
{
    public IOGraphGdmTypeReference ReturnType { get; set; } = default!;
    public IOGraphGdmParameterCollection Parameters => throw new NotImplementedException();
    public override GdmElementKind ElementKind { get; } = GdmElementKind.Function;
}
