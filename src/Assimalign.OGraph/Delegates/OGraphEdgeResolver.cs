using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph;

/// <summary>
/// 
/// </summary>
/// <typeparam name="T"></typeparam>
/// <param name="context"></param>
/// <returns></returns>
public delegate Task<T> OGraphEdgeResolver<T>(IOGraphEdgeResolverContext context);

/// <summary>
/// 
/// </summary>
/// <param name="context"></param>
/// <returns></returns>
public delegate Task<IOGraphEdgeResult> OGraphEdgeResolver(IOGraphEdgeResolverContext context);




