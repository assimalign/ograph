using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph;

public interface IOGraphTypeResolverContext
{
    Stream GetRequestBody();
}
