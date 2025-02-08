using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph;


[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = false)]
internal sealed class ScalarTypeAttribute : Attribute
{
    public ScalarTypeAttribute()
    {
        
    }

    
}

public enum ScalarRuntimeType
{
    String,
    Int,
    



    // Custom,
}
