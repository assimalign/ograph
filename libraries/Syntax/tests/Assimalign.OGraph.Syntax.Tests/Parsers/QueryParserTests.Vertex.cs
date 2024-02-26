using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Assimalign.OGraph.Syntax;

public partial class QueryParserTests
{
    [Fact]
    public void TestVertexParseSuccess()
    {
        var query = "vertex(employees)";
        var parser = new QueryParser();
        var document = parser.Parse(query);


        Assert.Empty(document.Errors);
    }

    [Fact]
    public void TestVertexWithArgumentParseSuccess()
    {
        var query = """
        vertex(employees, 1234532)
            .project({
                id as myId
                companyId
                companyInfo as info {
                    companyName
                }
            })
            .page({ 
                skip 0
                take 20
             })
            .edge(department as employeeDepartments)
                .page({ 
                    skip 0 
                    take 20
                })
            .edge(addresses)
                .project({
                    
                })
                .page({ 
                    skip 0 
                    take 20
                })
                .edge(addresses/types)
                    .page({ 
                        skip 0 
                        take 20
                    })
            .edge(primaryAddress)
                .page({ 
                    skip 0 
                    take 20
                })
            .edge(jobs)
                .page({ 
                    skip 0 
                    take 20
                })
                .edge(jobs/tasks)
                    .page({ 
                        skip 0 
                        take 20
                    })
                    .edge(jobs/tasks/workItems)
                        .page({ 
                            skip 0 
                            take 20
                        })
""";
        var parser = new QueryParser();
        var document = parser.Parse(query);


        Assert.Empty(document.Errors);
    }


    [Fact]
    public void TestVertexSpaceBetweenParenthesisSuccess()
    {
        var query = "vertex ('employees')";
        var parser = new QueryParser();
        var document = parser.Parse(query);


        Assert.Empty(document.Errors);
    }
}
