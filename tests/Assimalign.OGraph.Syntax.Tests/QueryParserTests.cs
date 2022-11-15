using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Assimalign.OGraph.Syntax.Tests;

public class QueryParserTests
{
    private const string query = @"
	.filtER({
		firstName eq 'Chase' and
		(
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
	.select({
		toLower(firstName) as firstName 
		toLower(lastName) as lastName
		concat(toUpper(firstName), ' ', toUpper(lastName)) as fullName
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
	})";
    [Fact]
    public void Test1()
    {
		var parser = new QueryParser();

		parser.Parse(query);
        



    }
}
