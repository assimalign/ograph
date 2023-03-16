using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Text.Json;
using System.Threading.Tasks;

namespace Assimalign.OGraph;

public interface IOGraphComplexType : IOGraphType
{
    /// <summary>
    /// 
    /// </summary>
    IOGraphPropertyCollection Properties { get; }
}
