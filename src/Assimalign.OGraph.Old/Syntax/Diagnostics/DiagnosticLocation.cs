using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Syntax;

public enum DiagnosticLocation
{
    // <summary>
    /// The diagnostic location is known with absolute start and length values.
    /// </summary>
    Absolute,

    /// <summary>
    /// The diagnostic location is unknown, but relative to the syntax item it is associated with.
    /// </summary>
    Relative,

    /// <summary>
    /// The diagnostic location is unknown, but after the end of the syntax item it is associated with.
    /// </summary>
    RelativeEnd
}
