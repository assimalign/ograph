namespace Assimalign.OGraph.Gdm.Internal;

internal class GdmProperty : IOGraphGdmProperty
{
    public GdmProperty()
    {
        Metadata = new GdmMetadata();
    }

    public Label Name { get; set; }
    public IOGraphGdmTypeReference Type { get; set; } = default!;
    public IOGraphGdmMetadata Metadata { get; }
    public bool IsKey { get; set; }
    public bool IsComputed { get; set; }
    public bool IsNullable { get; set; }
    public IOGraphGdmPropertyGetter Getter { get; set; } = default!;
    public IOGraphGdmPropertySetter Setter { get; set; } = default!;
}