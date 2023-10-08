using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml;

namespace Assimalign.OGraph.Types;

public abstract class TypeBase : IOGraphType
{
    public abstract Name TypeName { get; }
    public abstract OGraphTypeIdentifier TypeIdentifier { get; }
    public abstract Type? RuntimeType { get; }
    public abstract bool IsNullable { get; }
}
