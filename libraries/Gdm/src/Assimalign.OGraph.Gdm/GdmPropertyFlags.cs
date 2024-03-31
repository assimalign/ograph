using System;

namespace Assimalign.OGraph.Gdm;

[Flags]
public enum GdmPropertyFlags
{
    None = 0,
    ReadOnly,
    Nullable,
    Computed
}
