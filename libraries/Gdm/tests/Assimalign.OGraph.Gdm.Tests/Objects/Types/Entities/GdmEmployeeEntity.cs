using System;

namespace Assimalign.OGraph.Gdm.Tests;

using Elements;
using Objects;

public class GdmEmployeeEntity : GdmEntityType<Employee>
{
    public GdmEmployeeEntity(GdmGraph graph) : base(graph)
    {
    }

    protected override void Configure(GdmEntityTypeDescriptor<Employee> descriptor)
    {
        descriptor.HasName("Employee");
        descriptor.HasKey(p => p.EmployeeId);

        descriptor.HasProperty(p => p.EmployeeId)
            .UsePropertyName("employeeId")
            .UseType<GdmUuidType>()
            .UseSetter((instance, value) => (instance as Employee)!.EmployeeId = value switch // Value Object setter override due to implicit conversions
            {
                EmployeeId id => id,
                _ => throw new Exception()
            });

        descriptor.HasProperty(p => p.Info)
            .UsePropertyName("details")
            .UseType(details =>
            {
                details.HasProperty(p => p.FirstName).UsePropertyName("firstName").UseType<GdmStringType>();
                details.HasProperty(p => p.LastName).UsePropertyName("lastName").UseType<GdmStringType>();
                details.HasProperty(p => p.MiddleName).UsePropertyName("middleName").UseType<GdmStringType>();
                details.HasProperty(p => p.Birthdate).UsePropertyName("birthdate").UseType<GdmDateType>();
            });

        descriptor.HasProperty(p => p.Roles).UsePropertyName("roles")
            .UseType(role =>
            {
                role.HasName("EmployeeRole");
                role.HasProperty(p => p.Id).UsePropertyName("id").UseType<GdmInt32Type>();
                role.HasProperty(p => p.Name).UsePropertyName("name").UseType<GdmStringType>();
            });

        descriptor.HasProperty(p => p.CreatedBy).UsePropertyName("createdBy").UseType<GdmAuditField>();
        descriptor.HasProperty(p => p.UpdatedBy).UsePropertyName("updatedBy").UseType<GdmAuditField>();
    }
}