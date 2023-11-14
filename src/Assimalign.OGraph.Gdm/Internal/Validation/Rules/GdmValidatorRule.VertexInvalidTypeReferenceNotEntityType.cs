namespace Assimalign.OGraph.Gdm.Internal;

using Properties;

internal class GdmVertexInvalidTypeReferenceNotEntityTypeValidatorRule : GdmValidatorRule
{
    public override void OnValidate(GdmValidatorContext context)
    {
        var vertices = context.Model.GetGdmVertices();

        foreach (var vertex in vertices)
        {
            if (vertex.Type is not null && vertex.Type.Definition is not null && vertex.Type.Definition is not IOGraphGdmEntityType)
            {
                context.AddFailure(error =>
                {
                    error.Code = OGraphGdmErrorCode.GDM1001;
                    error.Message = Resources.GDM1001;
                    error.Source = vertex.Label;
                });
            }
        }
    }
}
