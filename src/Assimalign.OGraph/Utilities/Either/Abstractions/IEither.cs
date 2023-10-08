using System;

namespace Assimalign.OGraph;

public interface IEither
{
    int TypeIndex { get; }
    Type Type { get; }
    object Value { get; }
}