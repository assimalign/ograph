using System;

namespace Assimalign.OGraph;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
public sealed class GdmEntityTypeAttribute : Attribute
{
    public GdmEntityTypeAttribute()
    {
    }
}
