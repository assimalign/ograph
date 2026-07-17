using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Assimalign.OGraph.Syntax.Internal;

internal static class ThrowHelper
{
    [DoesNotReturn]
    internal static void ThrowInvalidOperationException(string message) =>
        throw new InvalidOperationException(message);

    [DoesNotReturn]
    internal static void ThrowArgumentNullException(string paramName) => 
        throw new ArgumentNullException(paramName);

    [DoesNotReturn]
    internal static void ThrowArgumentNullException(string paramName, string message) =>
        throw new ArgumentNullException(paramName, message);

    [DoesNotReturn]
    internal static void ThrowArgumentException(string message) =>
        throw new ArgumentException(message);
}
