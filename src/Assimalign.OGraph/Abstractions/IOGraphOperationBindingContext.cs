using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph;

using Assimalign.OGraph.Gdm;

public interface IOGraphOperationBindingContext : IOGraphGdmBindingContext
{
    /// <summary>
    /// 
    /// </summary>
    new IOGraphGdmVertex Element { get; }
    /// <summary>
    /// 
    /// </summary>
    IOGraphExecutorRequest Request { get; }
    /// <summary>
    /// 
    /// </summary>
    IOGraphExecutorResponse Response { get; }



    IServiceProvider ServiceProvider { get; }



    T GetService<T>();

    T GetRequestBody<T>();
}
