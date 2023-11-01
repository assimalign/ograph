using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Gdm;

/// <summary>
/// 
/// </summary>
public interface IOGraphGdmEntityType : IOGraphGdmComplexType
{
    /// <summary>
    /// 
    /// </summary>
    OGraphEntityKeyResolver KeyResolver { get; }
}


public delegate object OGraphEntityKeyResolver(object entity);