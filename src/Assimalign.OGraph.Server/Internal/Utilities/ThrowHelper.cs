using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Internal;

internal static class ThrowHelper
{
    [DoesNotReturn]
    internal static void ThrowInvalidOperationException(string message) => 
        throw new InvalidOperationException(message);
}
