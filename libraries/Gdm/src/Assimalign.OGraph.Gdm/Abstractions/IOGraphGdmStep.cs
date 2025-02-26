using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Gdm;

/// <summary>
/// 
/// </summary>
public interface IOGraphGdmStep
{

    /// <summary>
    /// 
    /// </summary>
    IOGraphGdmOperationCollection Operations { get; }
}
