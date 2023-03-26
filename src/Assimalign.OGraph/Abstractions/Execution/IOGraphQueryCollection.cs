using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph;

/// <summary>
/// A collection of query parameters.
/// </summary>
public interface IOGraphQueryCollection : IDictionary<string, QueryValue>
{
}
