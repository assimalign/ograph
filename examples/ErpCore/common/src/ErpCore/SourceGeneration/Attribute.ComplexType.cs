using System;

namespace Assimalign.OGraph;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
public sealed class ComplexTypeAttribute : Attribute
{
    public ComplexTypeAttribute()
    {
    }
}