using System;

namespace Assimalign.OGraph.Gdm.Tests;

public class GdmEmployeeEntity : GdmEntityType<Employee>
{
    protected override void Configure(IOGraphGdmEntityTypeDescriptor<Employee> descriptor)
    {
        descriptor.HasName("Employee");
        descriptor.HasKey(p => p.EmployeeId);

        descriptor.HasProperty(p => p.EmployeeId)
            .UsePropertyName("employeeId")
            .UseType<GdmGuidType>()
            .UseSetter((instance, value) => (instance as Employee)!.EmployeeId = value switch // Value Object setter override due to implicit conversions
            {
                EmployeeId id => id,
                Guid guid => guid,
                null => null,
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
            .UseType("EmployeeRoleCollection", role =>
            {
                role.HasLabel("EmployeeRole");
                role.HasProperty(p => p.Id).UsePropertyName("id").UseType<GdmInt32Type>();
                role.HasProperty(p => p.Name).UsePropertyName("name").UseType<GdmStringType>();
            });

        descriptor.HasProperty(p => p.CreatedBy).UsePropertyName("createdBy").UseType<GdmAuditField>();
        descriptor.HasProperty(p => p.UpdatedBy).UsePropertyName("updatedBy").UseType<GdmAuditField>();
    }
}