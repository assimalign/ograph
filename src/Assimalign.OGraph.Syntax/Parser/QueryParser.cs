using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Syntax;

public sealed class QueryParser
{
	public QueryParser()
	{

	}

	public QueryTree Parse(string query) => Parse(Encoding.Default.GetBytes(query));
	public QueryTree Parse(byte[] query)
	{
		var lexer = new TokenLexer(query);

		while (lexer.HasNext)
		{
			var token = lexer.Next();
		}



		return default;
	}
}
