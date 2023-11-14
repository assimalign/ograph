using Assimalign.OGraph.Gdm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Internal;

internal class OGraph : IOGraph
{
    public IOGraphGdm Model { get; init; }
}
