using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph;

public sealed class OGraphQueryOptions
{
    public bool DiableFiltering { get; set; }
    public bool DisableSorting { get; set; }
    public bool DisablePagination { get; set; }
}
