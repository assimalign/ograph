using System;
using System.Collections.Generic;

namespace Assimalign.OGraph.Gdm.Internal;

internal class GdmProperty : IOGraphGdmProperty
{
    private readonly IList<IOGraphGdmBinding> bindings = new List<IOGraphGdmBinding>();
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
    public GdmPropertyGetter Getter { get; set; } = default!;
    public GdmPropertySetter Setter { get; set; } = default!;

    public void AddBinding(IOGraphGdmBinding binding)
    {
        if (binding is null)
        {
            throw new ArgumentNullException(nameof(binding));
        }
        bindings.Add(binding);
    }
    public IEnumerable<IOGraphGdmBinding> GetBindings()
    {
        return bindings;
    }
}