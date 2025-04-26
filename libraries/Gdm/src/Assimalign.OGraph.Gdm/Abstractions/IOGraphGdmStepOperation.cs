using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Gdm;

/// <summary>
/// 
/// </summary>
public interface IOGraphGdmStepOperation : IOGraphGdmOperation
{
    /// <summary>
    /// 
    /// </summary>
    IOGraphGdmStep Step { get; }
}
