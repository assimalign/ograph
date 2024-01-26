using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Gdm.Tests;

public static class TestUtility
{
    private readonly static IOGraphGdmFactory factory;
    
    static TestUtility()
    {
        factory = new OGraphGdmFactoryBuilder()
            .Configure("FluentOnlyModel", modelBuilder =>
            {
                modelBuilder.AddType<AuditField>(audit =>
                {
                    audit.HasLabel("EmployeeAuditField");
                    audit.HasProperty(p => p.UserId).UsePropertyName("userId");
                    audit.HasProperty(p => p.Timestamp).UsePropertyName("timestamp");
                });
                // Option 01: Build Vertex from Entity descriptor
                modelBuilder.AddVertex<Employee>(entity =>
                {
                    entity.HasLabel("Employee");
                    entity.HasKey(p => p.EmployeeId);

                    entity.HasProperty(p => p.EmployeeId)
                        .UsePropertyName("employeeId")
                        .UseType<GdmGuidType>()
                        .UseSetter((instance, value) => (instance as Employee)!.EmployeeId = value switch // Value Object setter override due to implicit conversions
                        {
                            Guid guid       => new EmployeeId(guid),
                            EmployeeId id   => id,
                            null            => null,
                            _               => throw new Exception()
                        });

                    entity.HasProperty(p => p.Details)
                        .UsePropertyName("details")
                        .UseType(details =>
                        {
                            details.HasProperty(p => p.FirstName).UsePropertyName("firstName").UseType<GdmStringType>();
                            details.HasProperty(p => p.LastName).UsePropertyName("lastName").UseType<GdmStringType>();
                            details.HasProperty(p => p.MiddleName).UsePropertyName("middleName").UseType<GdmStringType>();
                            details.HasProperty(p => p.Birthdate).UsePropertyName("birthdate").UseType<GdmDateType>();
                        });

                    entity.HasProperty(p => p.Roles).UsePropertyName("roles")
                        .UseType("EmployeeRoleCollection", role =>
                        {
                            role.HasLabel("EmployeeRole");
                            role.HasProperty(p => p.Id).UsePropertyName("id").UseType<GdmInt32Type>();
                            role.HasProperty(p => p.Name).UsePropertyName("name").UseType<GdmStringType>();
                        });
                    entity.HasProperty(p => p.CreatedBy).UsePropertyName("createdBy");
                    entity.HasProperty(p => p.UpdatedBy).UsePropertyName("updatedBy");
                });
                //// Option 02: Build vertex from vertex descriptor
                //modelBuilder.AddVertex<EmployeeAddress>(vertex =>
                //{
                //    vertex.HasLabel("EmployeeAddress");
                //    vertex.HasType(entity =>
                //    {
                //        entity.HasKey(p => p.EmployeeId);
                //    });

                //    vertex.HasEdge<>
                //});
            })
            .Build();
    }



    public static IOGraphGdm CreateFluentOnlyModel()
    {
        return factory.Create("FluentOnlyModel");
    }
}
