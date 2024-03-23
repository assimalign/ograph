namespace Assimalign.OGraph.Gdm;

public interface IOGraphGdmVertexDescriptor
{
    IOGraphGdmVertexDescriptor HasLabel(Label label);
    IOGraphGdmVertexDescriptor HasType(IOGraphGdmEntityType type);
    IOGraphGdmVertexDescriptor HasType<TGdmType>() where TGdmType : IOGraphGdmEntityType, new();
}
