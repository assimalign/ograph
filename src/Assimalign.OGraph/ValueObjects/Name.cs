using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph;

public readonly struct Name
{
	public Name(string value)
	{
		this.Value = value;
	}
    public string Value { get; }

	public static implicit operator Name(string value) => new Name(value);
}