using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph;


public delegate Task<T> OGraphResolver<T>(IOGraphResolverContext context);


