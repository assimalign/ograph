using System;
using System.Collections.Generic;

namespace Assimalign.OGraph.Gdm.Internal;

internal class GdmValidator
{
    private readonly IList<GdmValidatorRule> rules = new List<GdmValidatorRule>();

    public GdmValidator()
    {
        AddRule<GdmComplexTypeCheckRule>();
        AddRule<GdmEntityTypeMissingKeyValidatorRule>();
    }

    private void AddRule<TRule>() where TRule : GdmValidatorRule, new()
    {
        rules.Add(new TRule());
    }

    public GdmValidatorResult Validate(IOGraphGdm model)
    {
        var result = new GdmValidatorResult();
        var context = new GdmValidatorContext() 
        { 
            Model = model,
            Errors = result.Errors
        };

        foreach (var rule in rules)
        {
            rule.OnValidate(context);
        }

        return result;
    }
}