namespace Assimalign.OGraph.Gdm;

public sealed class OGraphGdmBuilderOptions
{
    /// <summary>
    /// Will implicitly create Entity Collection Types.
    /// </summary>
    public bool GenerateEntityCollectionTypes { get; set; } = true;
    /// <summary>
    /// 
    /// </summary>
    public bool ConvertAllLabelsToCamalCase { get; set; }
}
