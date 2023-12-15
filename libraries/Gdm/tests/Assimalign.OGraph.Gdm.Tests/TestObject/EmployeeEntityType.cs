using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Gdm.Tests;

public class EmployeeEntityType : GdmEntityType<Employee>
{
    protected override void Configure(IOGraphGdmEntityTypeDescriptor<Employee> descriptor)
    {
        ConfigureEmployee(descriptor);
    }

    public static void ConfigureEmployee(IOGraphGdmEntityTypeDescriptor<Employee> descriptor)
    {
        descriptor.HasLabel("Employee");
        descriptor.HasKey(p => p.EmployeeId);

        descriptor.HasProperty(p => p.EmployeeId).UsePropertyName("employeeId")
            .UseSetter((instance, value) => (instance as Employee)!.EmployeeId = value switch
            {
                Guid guid       => new EmployeeId(guid),
                EmployeeId id   => id,
                _               => throw new Exception()
            })
            .IsRequired();

        descriptor.HasProperty(p => p.Roles).UsePropertyName("roles")
            .UseType(role =>
            {
                role.HasLabel("EmployeeRole");

                role.HasProperty(p => p.Id).UsePropertyName("id");
                role.HasProperty(p => p.Name).UsePropertyName("name");
            });
    }
}
