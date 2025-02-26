using System;
using Xunit;

namespace Assimalign.OGraph.Gdm.Tests;

using Internal;


public partial class ValueObjectTests
{
    [Fact(DisplayName = "Value Object (Label): Invalid Character")]
    public void TestInvalidNameCharacter()
    {
        Assert.Throws<GdmModelException>((Action)(() =>
        {
            GdmName name = "test property";
        }));
        Assert.Throws<ArgumentNullException>((Action)(() =>
        {
            GdmName name = "";
        }));
    }

    [Fact(DisplayName = "Value Object (Label): To Camal Case")]
    public void TestToCamalCase()
    {
        GdmName name = "MyName";

        Assert.Equal("myName", name.ToCamalCase());
    }

    [Fact(DisplayName = "Value Object (Label): To Pascal Case")]
    public void TestToPascalCase()
    {
        GdmName name = "myName";

        Assert.Equal("MyName", name.ToPascalCase());
    }

    [Fact(DisplayName = "Value Object (Label): Comparison")]
    public void TestComparison()
    {
        GdmName name1 = "abc";
        GdmName name2 = "abd";

        Assert.Equal(name1.Value.CompareTo(name2.Value), name1.CompareTo(name2));
    }

    [Fact(DisplayName = "Value Object (Name): Equality")]
    public void TestComparisonEquality()
    {
        GdmName name1 = "abc";
        GdmName name2 = "abc";
        GdmName name3 = "abd";

        Assert.True(name1 == name2);
        Assert.True(name2 != name3);
        Assert.True(name1 != name3);
        Assert.False(name1 == name3);
    }
}
