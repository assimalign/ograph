using System;
using System.Collections.Generic;

namespace Assimalign.OGraph.Gdm.Internal;

internal class GdmValidator
{
    private static readonly IList<GdmValidatorRule> rules = new List<GdmValidatorRule>();

    static GdmValidator()
    {
        AddRule<GdmComplexTypeCheckRule>();
        AddRule<GdmComplexTypeKeyDisallowedValidatorRule>();

        AddRule<GdmEntityTypeMissingKeyValidatorRule>();

        AddRule<GdmVertexInvalidTypeReferenceIsNullValidatorRule>();
        AddRule<GdmVertexInvalidTypeReferenceNotEntityTypeValidatorRule>();
    }

    private static void AddRule<TRule>() where TRule : GdmValidatorRule, new()
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