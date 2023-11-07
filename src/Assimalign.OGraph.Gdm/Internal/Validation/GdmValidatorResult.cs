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

    public OGraphGdmException ToException()
    {
        return new GdmModelException(Errors.First().Message)
        {
            Source = Errors.First().Source,
        };
    }
}
