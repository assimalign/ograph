using System;
using System.IO;
using System.Security.Claims;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph;

using Assimalign.OGraph.Syntax;

public interface IOGraphOperationContext
{
    /// <summary>
    /// Get's the OGrapp Model.
    /// </summary>
    /// <returns></returns>
    IOGraph GetGraph();
    /// <summary>
    /// Get's the binded node for the given operation.
    /// </summary>
    /// <returns></returns>
    IOGraphNode GetNode();
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



    #region HTTP Request Information

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="headerName"></param>
    /// <returns></returns>
    T GetRequestHeader<T>(string headerName);
    /// <summary>
    /// Returns the route parameter
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="parameterName"></param>
    /// <returns></returns>
    T GetRequestRouteParam<T>(string parameterName);
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="parameterName">The name of the query parameter.</param>
    /// <returns></returns>
    T GetRequestQueryValue<T>(string parameterName);
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    T GetRequestBody<T>();
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    Stream GetRequestBody();
    /// <summary>
    /// Get's the authenticated user or application i
    /// </summary>
    /// <returns></returns>
    ClaimsPrincipal GetClaimsPrincipal();
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    T GetService<T>();

    #endregion




    Stream GetResponseBody();

    
}
