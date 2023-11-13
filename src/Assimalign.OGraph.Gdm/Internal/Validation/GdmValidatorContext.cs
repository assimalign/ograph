using System;
using System.Collections.Generic;

namespace Assimalign.OGraph.Gdm.Internal;

internal class GdmValidatorContext
{
    public IOGraphGdm Model { get; init; } = default!;
    public IList<GdmValidatorError> Errors { get; init; } = default!;

    public void AddFailure(GdmValidatorError error)
    {
        if (error is null)
        {
            throw new ArgumentNullException(nameof(error));
        }
        Errors.Add(error);
    }
    public void AddFailure(Action<GdmValidatorError> configure)
    {
        var error = new GdmValidatorError();

        configure.Invoke(error);

        AddFailure(error);
    }
}
