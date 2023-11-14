using System.Diagnostics;

namespace Assimalign.OGraph.Gdm.Internal;

[DebuggerDisplay("Gdm Type Reference: {Definition?.Label}")]
internal class GdmTypeReference : IOGraphGdmTypeReference
{
    public IOGraphGdmType Definition { get; init; } = default!;
}
