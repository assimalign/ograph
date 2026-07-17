using System;

namespace Assimalign.OGraph.Gdm;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
public sealed class GdmComplexTypeAttribute : Attribute
{
    public GdmComplexTypeAttribute()
    {
    }
}