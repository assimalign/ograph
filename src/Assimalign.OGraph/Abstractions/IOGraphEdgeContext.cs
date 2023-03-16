
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph;

using Assimalign.OGraph.Syntax;

public interface IOGraphEdgeContext
{
    /// <summary>
    /// Get's the OGrapp Model.
    /// </summary>
    /// <returns></returns>
    IOGraph GetGraph();
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    IOGraphEdge GetEdge();
    /// <summary>
    /// Get's the HTTP request query.
    /// </summary>
    /// <returns></returns>
    QueryDocument GetQuery();
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    IOGraphQueryProvider GetQueryProvider();
    /// <summary>
    /// Get's the Parent object in which the edge is being executed for.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    T GetParent<T>();
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    T GetService<T>();
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    ClaimsPrincipal GetClaimsPrincipal();
}
