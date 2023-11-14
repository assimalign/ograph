using System;
using System.Linq;
using System.Collections.Generic;

namespace Assimalign.OGraph.Gdm.Internal;

internal class GdmValidatorResult
{
    public GdmValidatorResult()
    {
        Errors = new List<GdmValidatorError>();
    }
    public bool IsValid => !Errors.Any();
    public IList<GdmValidatorError> Errors { get; }
    public AggregateException ToException()
    {
        return new AggregateException(Errors.Select(p => new GdmModelException(p)));
    }
}
