using System;

namespace Assimalign.OGraph.ValueObjects;

public partial class ValueObjectTests
{
    [Fact(DisplayName = "Value Object (Name): Invalid Character")]
    public void TestInvalidNameCharacter()
    {
        Assert.Throws<ArgumentException>(() =>
        {
            Name name = "test property";
        });
        Assert.Throws<ArgumentNullException>(() =>
        {
            Name name = "";
        });
    }

    [Fact(DisplayName = "Value Object (Name): To Camal Case")]
    public void TestToCamalCase()
    {
        Name name = "MyName";

        Assert.Equal("myName", name.ToCamalCase());
    }


    [Fact(DisplayName = "Value Object (Name): To Pascal Case")]
    public void TestToPascalCase()
    {
        Name name = "myName";

        Assert.Equal("MyName", name.ToPascalCase());
    }


    [Fact(DisplayName = "Value Object (Name): Comparison")]
    public void TestComparison()
    {
        Name name1 = "abc";
        Name name2 = "abd";

        Assert.Equal(name1.Value.CompareTo(name2.Value), name1.CompareTo(name2));
    }

    [Fact(DisplayName = "Value Object (Name): Equality")]
    public void TestComparisonEquality()
    {
        Name name1 = "abc";
        Name name2 = "Abc";
        Name name3 = "abd";

        Assert.True(name1 == name2);
        Assert.False(name1 != name2);
        Assert.True(name1 != name3);
        Assert.False(name1 == name3);
    }
}