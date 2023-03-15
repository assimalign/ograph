using Assimalign.OGraph.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph;

public interface IOGraphEdgeContext
{

    EdgeSortNode GetOGraphEdgeSorting();
    EdgeFilterNode GetOGraphEdgeFiltering();
    EdgeProjectionNode GetOGraphEdgeProjections();

    T GetParent<T>();

    ClaimsPrincipal GetClaimsPrincipal();
}
