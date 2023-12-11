using System;
using System.Diagnostics.CodeAnalysis;

namespace Assimalign.OGraph.Internal;

internal static class ThrowHelper
{
    [DoesNotReturn]
    internal static void ThrowArgumentException(string message) =>
        throw new ArgumentException(message);

    [DoesNotReturn]
    internal static void ThrowInvalidOperationException(string message) => 
        throw new InvalidOperationException(message);

    [DoesNotReturn]
    internal static void ThrowArgumentNullException(string paramName) =>
        throw new ArgumentNullException(paramName);
}
