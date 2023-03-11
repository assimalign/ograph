using System;
using System.Threading.Tasks;

namespace Assimalign.OGraph;

/// <summary>
/// 
/// </summary>
/// <typeparam name="T"></typeparam>
/// <param name="context"></param>
/// <returns></returns>

public delegate ValueTask<T> OGraphPropertyResolver<T>(IOGraphPropertyResolverContext context);

/// <summary>
/// 
/// </summary>
/// <param name="context"></param>
/// <returns></returns>
public delegate ValueTask<IOGraphPropertyResult> OGraphPropertyResolver(IOGraphPropertyResolverContext context);