using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Assimalign.OGraph;



public delegate ValueTask<IOGraphPropertyResult> OGraphPropertyResolver(IOGraphPropertyResolverContext context);

public delegate ValueTask<T> OGraphPropertyResolver<T>(IOGraphPropertyResolverContext context);

