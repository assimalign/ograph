using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;


namespace Assimalign.OGraph.Syntax.Tests;

public partial class QueryParserTests
{

    [Fact]
    public void TestSkipTakeParsedSuccess()
    {
        var query = "query().page({ take 25 skip 50})";
        var parser = new QueryParser();
        var node = parser.Parse(query);
        var rootNode = Assert.IsType<RootQueryNode>(node);

        Assert.Single(rootNode.Nodes);

        var pageNode = Assert.IsType<PageQueryNode>(rootNode.Nodes.First());

        Assert.NotNull(pageNode.Take);
        Assert.NotNull(pageNode.Skip);
    }


    [Fact]
    public void TestSkipTakeParsedFailure()
    {
        var query = "query().page({ take 25 t skip 50})";
        var parser = new QueryParser();
        var node = parser.Parse(query);
        var rootNode = Assert.IsType<RootQueryNode>(node);

        Assert.Single(rootNode.Nodes);

        var pageNode = Assert.IsType<PageQueryNode>(rootNode.Nodes.First());

        Assert.NotNull(pageNode.Take);
        Assert.NotNull(pageNode.Skip);
    }
}
