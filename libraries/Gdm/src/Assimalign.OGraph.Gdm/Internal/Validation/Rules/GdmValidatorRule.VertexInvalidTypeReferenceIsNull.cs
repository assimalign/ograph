namespace Assimalign.OGraph.Gdm.Internal;

using Properties;

internal class GdmVertexInvalidTypeReferenceIsNullValidatorRule : GdmValidatorRule
{
    public override void OnValidate(GdmValidatorContext context)
    {
        var vertices = context.Model.GetGdmVertices();

        foreach (var vertex in vertices)
        {
            if (vertex.Type is null || vertex.Type.Definition is null)
            {
                context.AddFailure(error =>
                {
                    error.Code = GdmErrorCode.GDM1002;
                    error.Message = Resources.GDM1002;
                    error.Source = vertex.Label;
                });
            }
        }
    }
}
