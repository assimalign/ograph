using System;
using System.Threading.Tasks;

namespace Assimalign.OGraph;

/// <summary>
/// 
/// </summary>
/// <param name="context"></param>
/// <returns></returns>
public delegate ValueTask<IOGraphPropertyResult> OGraphPropertyHandler(IOGraphPropertyResolverContext context);