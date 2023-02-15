using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph;

public interface IOGraphNodeProperty
{
    bool IsKey { get; }
    bool IsSortable { get; }
    bool IsPagable { get; }
    bool IsFilterable { get; }
    Name PropertyName { get; }
    IOGraphType PropertyType { get; }
}
