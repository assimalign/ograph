using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph;

public interface IOGraphComplexType : IOGraphType
{
    IEnumerable<IOGraphComplexTypeProperty> Properties { get; }
}


public interface IOGraphComplexTypeProperty
{
    Label PropertyName { get; }
    IOGraphType PropertyType { get; }
}