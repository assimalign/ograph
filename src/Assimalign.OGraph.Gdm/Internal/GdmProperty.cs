using System;
using System.Collections.Generic;

namespace Assimalign.OGraph.Gdm.Internal;

internal class GdmProperty : IOGraphGdmProperty
{
    private readonly IList<IOGraphGdmPropertyBinding> bindings = new List<IOGraphGdmPropertyBinding>();
    public GdmProperty()
    {
        Metadata = new GdmMetadata();
        Getter = () => this.value;
        Setter = (value) => this.value = value;
    }

    private volatile object? value;

    public Label Name { get; set; }
    public IOGraphGdmTypeReference Type { get; set; } = default!;
    public IOGraphGdmMetadata Metadata { get; }
    public bool IsKey { get; set; }
    public bool IsComputed { get; set; }
    public bool IsNullable { get; set; }
    public GdmPropertyGetter Getter { get; }
    public GdmPropertySetter Setter { get; }
    public void AddBinding(IOGraphGdmPropertyBinding binding)
    {
        if (binding is null)
        {
            throw new ArgumentNullException(nameof(binding));
        }
        bindings.Add(binding);
    }
    public IEnumerable<IOGraphGdmPropertyBinding> GetBindings()
    {
        return bindings;
    }
}