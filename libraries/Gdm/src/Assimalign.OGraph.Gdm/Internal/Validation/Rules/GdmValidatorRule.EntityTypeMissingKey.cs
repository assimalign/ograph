using System.Linq;

namespace Assimalign.OGraph.Gdm.Internal;

using Properties;

internal class GdmEntityTypeMissingKeyValidatorRule : GdmValidatorRule
{
    public override void OnValidate(GdmValidatorContext context)
    {
        var entityTypes = context.Model.GetGdmEntityTypes();

        foreach (var entityType in entityTypes)
        {
            var property = entityType.Properties.FirstOrDefault(p => p.IsKey);

            if (property is null)
            {
                context.AddFailure(error =>
                {
                    error.Code = OGraphGdmErrorCode.GDM0401;
                    error.Message = Resources.GDM0401;
                    error.Source = $"{entityType.Label}";
                });
            }
        }
    }
}
