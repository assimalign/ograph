using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph;

public readonly struct Method
{
	public Method(string value)
	{
		this.Value = value;
	}

	public string Value { get; }

    public static implicit operator Method(string value) => new Method(value);
	public static implicit operator string(Method method) => method.Value;
}
