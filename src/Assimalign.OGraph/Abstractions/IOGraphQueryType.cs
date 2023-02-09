using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Assimalign.OGraph;

public interface IOGraphQueryType
{
    /// <summary>
    /// 
    /// </summary>
    Type Type { get; }
    /// <summary>
    /// 
    /// </summary>
    Name TypeName { get; }
}
public interface IOGraphQueryType<T> : IOGraphQueryType
{

}
