using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph;

public interface IOGraphOperationResolverContext
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="headerName"></param>
    /// <returns></returns>
    T GetHeader<T>(string headerName);
    /// <summary>
    /// Returns the route parameter
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="parameterName"></param>
    /// <returns></returns>
    T GetRouteValue<T>(string parameterName);
}
