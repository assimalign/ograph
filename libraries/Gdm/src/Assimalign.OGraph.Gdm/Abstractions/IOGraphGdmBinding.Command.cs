using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Gdm;

public interface IOGraphGdmCommandBinding : IOGraphGdmBinding
{
    /// <summary>
    /// Gets the input type, if any.
    /// </summary>
    IOGraphGdmTypeReference ReturnType { get; }
}
