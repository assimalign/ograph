using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Assimalign.OGraph.Gdm.Tests;

using Elements;

public class AccessModifierTests
{
    [Fact]
    public void NonPublicConstructor()
    {
        AssertNoPublicConstructors<GdmElement>();
        AssertNoPublicConstructors<GdmBindableElement>();
        AssertNoPublicConstructors<GdmLabeledElement>();
        AssertNoPublicConstructors<GdmNamedElement>();
    }

    private void AssertNoPublicConstructors<T>()
    {
        Assert.DoesNotContain(typeof(T).GetConstructors(), ctor => ctor.IsPublic);
    }
}
