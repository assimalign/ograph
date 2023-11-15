using System.Diagnostics;

namespace Assimalign.OGraph.Gdm.Internal;

[DebuggerDisplay("Gdm Type Reference: {Definition?.Label}")]
internal class GdmTypeReference<T> : IOGraphGdmTypeReference
    where T : IOGraphGdmType
{
    public GdmTypeReference() { }
    public GdmTypeReference(T definition)
    {
        Definition = definition;
    }
    public T Definition { get; init; } = default!;
    IOGraphGdmType IOGraphGdmTypeReference.Definition => Definition;

    public static implicit operator GdmTypeReference<T>(T definition) => new GdmTypeReference<T>(definition);
}
