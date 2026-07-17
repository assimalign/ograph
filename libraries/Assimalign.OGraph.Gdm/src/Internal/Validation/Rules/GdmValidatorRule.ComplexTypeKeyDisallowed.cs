using System.Linq;

namespace Assimalign.OGraph.Gdm.Internal;

using Properties;

internal class GdmComplexTypeKeyDisallowedValidatorRule : GdmValidatorRule
{
    public override void OnValidate(GdmValidatorContext context)
    {
        //var complexTypes = context.Model.GetGdmComplexTypes()
        //    .Where(p => p is not IOGraphGdmEntityType);

        //foreach (var complexType in complexTypes)
        //{
        //    var property = complexType.Properties.FirstOrDefault(p => p.IsKey);

        //    if (property is not null)
        //    {
        //        context.AddFailure(error =>
        //        {
        //            error.Code = GdmErrorCode.GDM0301;
        //            error.Message = Resources.GDM0301;
        //            error.Source = $"{complexType.Label}.{property.Label}";
        //        });
        //    }
        //}
    }
}
