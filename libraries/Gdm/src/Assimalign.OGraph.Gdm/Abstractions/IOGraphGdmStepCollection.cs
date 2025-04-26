using System;
using System.Collections.Generic;

namespace Assimalign.OGraph.Gdm;

public interface IOGraphGdmStepCollection : ICollection<IOGraphGdmStep>
{

    IOGraphGdmStep this[GdmStepValue step] { get; }
}