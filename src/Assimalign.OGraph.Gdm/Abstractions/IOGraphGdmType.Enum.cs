using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Gdm;

public interface IOGraphGdmEnumType : IOGraphGdmType
{
    /// <summary>
    /// 
    /// </summary>
    public EnumValue[] Values { get; }
}

