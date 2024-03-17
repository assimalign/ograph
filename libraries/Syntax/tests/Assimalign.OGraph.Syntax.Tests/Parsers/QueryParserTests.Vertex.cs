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
                employeeId
                employeeInfo as info {
                    firstName
                    lastName
                    middleName
                }
                created {
                    timestamp
                    userId
                }
                updates {
                    timestamp
                    userId
                }
            })

            .page({ 
                skip 0
                take 20
             })
            .edge(department as employeeDepartment)
                .project({
                    id
                    departmentId
                    info {
                        name
                    }
                })
                .page({ 
                    skip 0 
                    take 20
                })
            .edge(addresses)
                .project({
                    id
                    addressId
                    info {
                        streetOne
                        streetTwo
                        streetThree
                        city
                        state
                        zipCode
                    }
                })
                # .sort({
                #     info.streetOne
                # })
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
                .project({
                    id
                    name
                })
                .page({ 
                    skip 0 
                    take 20
                })
                .edge(jobs/tasks)
                    .project({
                        id
                        displayName
                    })
                    .page({ 
                        skip 0 
                        take 20
                    })
                    .edge(jobs/tasks/workItems)
                        .project({
                            id
                            info as workItemInfo{
                                name
                            }
                        })
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
