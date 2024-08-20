namespace Assimalign.OGraph.Gdm;

public interface IOGraphGdmBindingParameter
{
    string Name { get; }    
    IOGraphGdmTypeReference Type { get; }
}