using System;

namespace Assimalign.OGraph.Gdm.Elements;

using Internal;

public class GdmParameter : IOGraphGdmParameter
{
    public GdmParameter(GdmLabel label, GdmType type, bool isRequired = false)
    {
        Label = label;
        Type = ThrowHelper.ThrowIfNull(type, nameof(type));
        IsRequired = isRequired;
    }

    public GdmLabel Label { get; }
    public GdmType Type { get; }
    public GdmMetadata Meta { get; } = new GdmMetadata();
    public bool IsRequired { get; }
    public GdmElementKind ElementKind { get; } = GdmElementKind.Parameter;
    IOGraphGdmType IOGraphGdmParameter.Type => Type;
    IOGraphGdmMetaCollection IOGraphGdmElement.Meta => Meta;
}
