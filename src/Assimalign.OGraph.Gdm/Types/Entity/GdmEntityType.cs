using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Gdm;

public abstract class GdmEntityType<T> : ComplexType<T>, IOGraphGdmEntityType
    where T : class, new()
{
    public GdmEntityKeyResolver KeyResolver { get; set; } = default!;
}
