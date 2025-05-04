using System;

namespace Assimalign.OGraph.Gdm;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
public sealed class GdmEntityTypeAttribute : Attribute
{
    public GdmEntityTypeAttribute()
    {
    }
}
