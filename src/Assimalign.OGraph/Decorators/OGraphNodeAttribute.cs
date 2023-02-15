using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
public class OGraphNodeAttribute : Attribute
{
	public OGraphNodeAttribute(string nodeName)
	{
        
	}

    
}
