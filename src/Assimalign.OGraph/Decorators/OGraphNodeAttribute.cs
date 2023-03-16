using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
public class OGraphNodeAttribute : Attribute
{
	public OGraphNodeAttribute(string nodeName)
	{
		
	}

	
}


[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false)]
public sealed class OGraphNodeAttribute<TNode> : Attribute 
	where TNode : IOGraphNode, new()
{
    public OGraphNodeAttribute()
    {
        
    }
}
