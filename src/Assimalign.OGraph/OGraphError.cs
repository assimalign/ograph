using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph;

public sealed class OGraphError : IOGraphError
{
    /// <inheritdoc />
    public string? Code { get; set; }

    /// <inheritdoc />
    public string? Message { get; set; }

    /// <inheritdoc />
    public IOGraphErrorDetailsCollection? Details { get; set; }
}
