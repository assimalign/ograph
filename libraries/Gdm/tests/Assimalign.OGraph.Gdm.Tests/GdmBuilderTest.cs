using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Assimalign.OGraph.Gdm.Tests;

using Objects;

public class GdmBuilderTest
{
    [Fact]
    public void Test()
    {
        var builder = GdmBuilder.Create()
            .AddGraph("Employees", descriptor =>
            {

                descriptor.AddNode<Employee>(vertex =>
                {
                    vertex.HasLabel("Employee");
                    vertex.HasEntityType(entity =>
                    {
                        
                    });
                });

            }).Build();


    }
}
