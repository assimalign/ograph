namespace Assimalign.OGraph.Gdm.Tests;

public enum GdmBuilderUtilityStrategy
{
    Fluent,     // Building a GDM using a fluent interface only
    Composable, // Building a GDM using a composable interface only
    Mixed       // Building a GDM using both fluent and composable interfaces
}
