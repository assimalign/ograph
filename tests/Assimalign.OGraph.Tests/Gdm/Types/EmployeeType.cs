using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Gdm;

public class EmployeeType : ComplexType<Employee>
{

    protected override void Configure(IOGraphComplexTypeDescriptor<Employee> descriptor)
    {
        descriptor.HasProperty(p => p.Details)
            .UseName("EmployeeDetails")
            .UseResolver(async (context, cancellationToken) =>
            {
                return new PropertyResult()
                {
                    Value = new EmployeeDetails()
                };
            });
    }


    [Fact]
    private void Test()
    {
        var properties = this.Properties;

        var names = properties.Select(p => p.Name.ToCamalCase());
    }
}
