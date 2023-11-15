using System.Diagnostics;

namespace Assimalign.OGraph.Gdm.Internal;

[DebuggerDisplay("Gdm: {Label}")]
internal class Gdm : IOGraphGdm
{
    public Gdm() { }
    public Label Label { get; set; }
    public IOGraphGdmElementCollection Elements { get; } = new GdmElementCollection();
}