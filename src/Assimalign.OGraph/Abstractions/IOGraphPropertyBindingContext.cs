using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph;

using Assimalign.OGraph.Gdm;

public interface IOGraphPropertyBindingContext : IOGraphGdmBindingContext
{
    /// <summary>
    /// 
    /// </summary>
    new IOGraphGdmProperty Element { get; }
    /// <summary>
    /// 
    /// </summary>
    IOGraphExecutorRequest Request { get; }
    /// <summary>
    /// 
    /// </summary>
    IOGraphExecutorResponse Response { get; }
}
