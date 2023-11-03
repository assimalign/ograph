namespace Assimalign.OGraph.Gdm.Internal;

internal abstract class GdmTypeReference<T> : IOGraphGdmTypeReference
    where T : IOGraphGdmType
{
    public T Definition { get; init; } = default!;
    IOGraphGdmType IOGraphGdmTypeReference.Definition => Definition;
}
