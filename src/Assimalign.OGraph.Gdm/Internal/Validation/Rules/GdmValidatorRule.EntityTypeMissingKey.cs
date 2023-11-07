using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Gdm.Internal;

internal class GdmEntityTypeMissingKeyValidatorRule : GdmValidatorRule
{
    public override void OnValidate(GdmValidatorContext context)
    {
        var entityTypes = context.Model.Elements.OfType<IOGraphGdmEntityType>();

        foreach (var entityType in entityTypes)
        {
            // 1. Check for no key
            if (!entityType.Properties.Any(p => p.IsKey))
            {
                context.AddFailure(error =>
                {
                    error.Message = $"No Key was specified in entity: {entityType.Label}.";
                });
            }
            if (entityType.Properties.Where(p=>p.IsKey).Count() > 2)
            {

            }
        }
    }
}
