using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph;

/// <summary>
/// 
/// </summary>
public interface IOGraphEntityType : IOGraphComplexType
{
    /// <summary>
    /// 
    /// </summary>
    OGraphEntityKeyResolver KeyResolver { get; }
}


public delegate object OGraphEntityKeyResolver(object entity);