using System.Collections.Generic;
using System.Diagnostics;

namespace Assimalign.OGraph.Gdm.Internal;

[DebuggerDisplay("Gdm = {Label}")]
internal class Gdm : IOGraphGdm
{
    public Gdm()
    {
        Elements.Add(this);
    }
    public Label Label { get; internal set; }
    public IOGraphGdmElementCollection Elements { get; } = new GdmElementCollection();
    public IEnumerable<IOGraphGdmBinding> Bindings { get; } = new List<IOGraphGdmBinding>();
    public GdmElementKind ElementKind => GdmElementKind.Graph;
    public void Bind(IOGraphGdmBinding binding)
    {
        if (binding is null)
        {
            ThrowHelper.ThrowArgumentNullException(nameof(binding));
        }
        (Bindings as List<IOGraphGdmBinding>)!.Add(binding);
    }
    void IOGraphGdmBindableElement.Unbind(IOGraphGdmBinding binding)
    {
        if (binding is null)
        {
            ThrowHelper.ThrowArgumentNullException(nameof(binding));
        }
        if (!(Bindings as List<IOGraphGdmBinding>)!.Remove(binding))
        {
            // TODO: Throw error
        }
    }
}