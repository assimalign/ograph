using System;

namespace Assimalign.OGraph;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
public sealed class GdmComplexTypeAttribute : Attribute
{
    public GdmComplexTypeAttribute()
    {
    }
}