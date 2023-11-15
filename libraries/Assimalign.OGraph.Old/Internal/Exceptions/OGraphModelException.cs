using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Internal;

internal class OGraphModelException : OGraphException
{
    public OGraphModelException(string message) : base(message)
    {
    }

    public OGraphModelException(string message, Exception innerException) : base(message, innerException)
    {
    }

    public override OGraphErrorType ErrorType => OGraphErrorType.Build;

    public override OGraphErrorCode ErrorCode { get; }
}
