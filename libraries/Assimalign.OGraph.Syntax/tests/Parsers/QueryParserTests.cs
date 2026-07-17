using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Assimalign.OGraph.Syntax;

public partial class QueryParserTests
{
    private const string query = """
	filtER({ # Some random capital letters to test case insensitivity
		firstName eq 'Chase' and (
			startsWith(
				firstName, 'c', true
			) and 
			endsWith(
				lastName, 'e', true
			)
		) or 
		any(
			addresses,
			city eq 'Charlotte'
		)
	})
	.projeCt({
		firstName
		lastName
		middleName
		addresses as userAddresses {
			streetOne
			streetTwo 
			city
			state 
		}
		auditEntry {
			createdBy
			createdDateTime
			updatedBy
			updatedDateTime
		}
	})
	.page({
		skip 0
		take 25
		token ''
	})
	.sort({
		firstName desc
		lastName asc
	})
	""";


    [Fact(DisplayName = "Parser Test: Complete Successful Parse")]
    public void CompleteParseSuccessful()
    {
		var parser = new QueryParser();
		var document = parser.Parse(query);

		Assert.Empty(document.Errors);
	}
}
