using System;

namespace Assimalign.OGraph;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
public class OGraphVertexAttribute : Attribute
{
	public OGraphVertexAttribute(string nodeName)
	{
		
	}

	
}