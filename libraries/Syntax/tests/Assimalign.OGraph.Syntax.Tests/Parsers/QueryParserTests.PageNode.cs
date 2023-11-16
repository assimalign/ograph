using System;
using System.Linq;
using Xunit;


namespace Assimalign.OGraph.Syntax;

public partial class QueryParserTests
{

    [Fact]
    public void TestSkipTakeParsedSuccess()
    {
        var query = ".page({ take 25 skip 50}).page(employees, { take 25 skip 25 })";
        var parser = new QueryParser();
        var document = parser.Parse(query);
        var rootNode = Assert.IsType<VertexNode>(document.Root);

        Assert.Single(rootNode.Nodes);

        var pageNode = Assert.IsType<PageNode>(rootNode.Nodes.First());

        Assert.NotNull(pageNode.Take);
        Assert.NotNull(pageNode.Skip);

        var skipConstant = Assert.IsType<ConstantNode>(pageNode.Skip);
        var takeConstant = Assert.IsType<ConstantNode>(pageNode.Take);

        //Assert.Equal(skipConstant.V)
    }


    [Fact]
    public void TestSkipTakeParsedFailure()
    {
        var query = "query().page({ take 25 skip 50})";
        var parser = new QueryParser();
        var document = parser.Parse(query);
        var rootNode = Assert.IsType<VertexNode>(document.Root);

        Assert.Single(rootNode.Nodes);

        var pageNode = Assert.IsType<PageNode>(rootNode.Nodes.First());

        Assert.NotNull(pageNode.Take);
        Assert.NotNull(pageNode.Skip);
    }
}
