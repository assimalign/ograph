using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Gdm.Internal;

internal class GdmSerializationException : OGraphGdmException
{
    public GdmSerializationException(string? message, Exception? innerException) : base(message, innerException)
    {
    }

    public override string? Source { get; set; }
}
