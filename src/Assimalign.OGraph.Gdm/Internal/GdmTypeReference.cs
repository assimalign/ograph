using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Gdm.Internal;

[DebuggerDisplay("Gdm Type Reference: {Definition?.Label}")]
internal class GdmTypeReference : IOGraphGdmTypeReference
{
    public IOGraphGdmType Definition { get; init; } = default!;
}
