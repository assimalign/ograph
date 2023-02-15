using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph;

public readonly struct QueryParam
{

	public QueryParam(string value)
	{
		this.Value = value;    
	}

	public string Value { get; }


    public static implicit operator QueryParam(string value) => new QueryParam(value);

}
