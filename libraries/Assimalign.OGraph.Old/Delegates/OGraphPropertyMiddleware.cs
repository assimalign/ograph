using System;
using System.Threading.Tasks;

namespace Assimalign.OGraph;

/// <summary>
/// 
/// </summary>
/// <param name="context"></param>
/// <param name="next"></param>
/// <returns></returns>
public delegate ValueTask<IOGraphResult> OGraphPropertyMiddleware(IOGraphPropertyContext context, OGraphPropertyHandler next);
