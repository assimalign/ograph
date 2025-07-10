using System;
using System.Collections.Generic;
using System.Text;

namespace System;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
public sealed class EntityTypeAttribute : Attribute
{
    public EntityTypeAttribute()
    {
        
    }
}
