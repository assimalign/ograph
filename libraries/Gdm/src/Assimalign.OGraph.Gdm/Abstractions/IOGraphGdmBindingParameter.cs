namespace Assimalign.OGraph.Gdm;

public interface IOGraphGdmBindingParameter
{
    Label Label { get; }
    GdmParameterKind Kind { get; }
    IOGraphGdmTypeReference Type { get; }
}