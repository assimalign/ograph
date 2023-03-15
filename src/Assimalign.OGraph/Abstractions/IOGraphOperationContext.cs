using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph;

using Assimalign.OGraph.Syntax;
using System.IO;
using System.Security.Claims;
using System.Text.Json;
using System.Xml;

public interface IOGraphOperationContext
{
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    IOGraph GetOGraph();
    /// <summary>
    /// Get's the binded node for the given operation.
    /// </summary>
    /// <returns></returns>
    IOGraphNode GetOGraphNode();
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    QueryDocument GetOGraphQuery();
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    RootNode GetOGraphQueryRoot();
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    PageNode GetOGraphPaging();
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    SortNode GetOGraphSorting();
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    FilterNode GetOGraphFiltering();
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    ProjectionNode GetOGraphProjections();
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    XmlWriter GetXmlWriter();
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    Utf8JsonWriter GetJsonWriter();
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




    Stream GetResponseBody();

    ClaimsPrincipal GetClaimsPrincipal();
}
