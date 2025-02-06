namespace Assimalign.OGraph.Gdm;

public interface IOGraphGdmVertexDescriptor
{
    IOGraphGdmVertexDescriptor HasLabel(Label label);
    IOGraphGdmVertexDescriptor HasEntityType(IOGraphGdmEntityType type);
    IOGraphGdmVertexDescriptor HasEntityType<TGdmType>() where TGdmType : IOGraphGdmEntityType, new();
}
