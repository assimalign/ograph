using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph;

public abstract class OGraphQueryOptions
{
    public bool CanSort { get; set; }
    public bool CanFilter { get; set; }
    public bool CanPage { get; set;  }
    public bool CanProject { get; set; }
    
}
