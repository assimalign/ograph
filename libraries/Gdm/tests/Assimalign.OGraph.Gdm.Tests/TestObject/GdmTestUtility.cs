using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Gdm.Tests;

public static class GdmTestUtility : IEqualityOperators
{
    private readonly static IOGraphGdmFactory factory;

    
    static GdmTestUtility()
    {
        // Reserved routes

        // /api/employees/$transactions
        // /api/employees/$model

        var gdm = OGraphGdmBuilder.Create("ErpCore")
            .AddGraph("Users", descriptor =>
            {
                descriptor.AddVertex<User>("User", descriptor =>
                {
                    descriptor.HasLabel("User");
                    descriptor.HasKey(p => p.Id);

                    descriptor.HasProperty(p => p.Id).UsePropertyName("id");
                });
            })
            .AddGraph("Employees", descriptor =>
            {
                descriptor.AddVertex<Employee>(vertex =>
                {
                    vertex.HasLabel("employees");
                    vertex.HasEntityType(entity =>
                    {
                        entity.HasLabel("Employee");
                        entity.HasKey(p => p.EmployeeId);
                        entity.HasProperty(p => p.EmployeeId).UsePropertyName("id");
                    });
                });
                descriptor.AddVertex<EmployeeAddress>("addresses", entity =>
                {

                });
            })
            .Build();



        //factory = new OGraphGdmFactoryBuilder()
        //    .Configure("employees", builder =>
        //    {
        //        builder.AddVertex<EmployeeVertex>();
        //        builder.AddVertex<EmployeeAddressVertex>();
        //        builder.AddVertex<EmployeeAddressTypeVertex>();
        //    })
        //    .Configure("employees", builder =>
        //    {
        //        builder.SetAllPropertiesToCamalCase();

        //        // Configure Types
        //        // Value Objects/DTO (Data Transfer Objects) 
        //        builder.AddComplexType<Employee>(complexType =>
        //        {
        //            complexType.HasLabel("employeeUpdateOrCreate");
        //            complexType.HasProperty(p => p.Info).UsePropertyName("info");
        //        });

        //        // Three Ways for handling Value Objects like Employee ID
        //        // 1. Custom Type
        //        // 2. Custom Setter

        //        // Configure Vertices
        //        builder.AddVertex<Employee>("employees", entity =>
        //        {
        //            entity.HasLabel("employee");
        //            entity.HasKey(p => p.EmployeeId);

        //            entity.HasProperty(p => p.EmployeeId)
        //                .UsePropertyName("employeeId")
        //                .UseType<GdmGuidType>()
        //                .UseSetter((instance, value) => (instance as Employee)!.EmployeeId = value switch // Value Object setter override due to implicit conversions
        //                {
        //                    EmployeeId id => id,
        //                    Guid guid => guid,
        //                    null => null,
        //                    _ => throw new Exception()
        //                });

        //            entity.HasProperty(p => p.Info)
        //                .UsePropertyName("info")
        //                .UseType(details =>
        //                {
        //                    details.HasProperty(p => p.FirstName).UsePropertyName("firstName").UseType<GdmStringType>();
        //                    details.HasProperty(p => p.LastName).UsePropertyName("lastName").UseType<GdmStringType>();
        //                    details.HasProperty(p => p.MiddleName).UsePropertyName("middleName").UseType<GdmStringType>();
        //                    details.HasProperty(p => p.Birthdate).UsePropertyName("birthdate").UseType<GdmDateType>();
        //                });

        //            entity.HasProperty(p => p.Roles)
        //                .UsePropertyName("roles")
        //                .UseType("employeeRoleCollection", role =>
        //                {
        //                    role.HasLabel("employeeRole");
        //                    role.HasProperty(p => p.Id).UsePropertyName("id").UseType<GdmInt32Type>();
        //                    role.HasProperty(p => p.Name).UsePropertyName("name").UseType<GdmStringType>();
        //                });

        //            entity.HasProperty(p => p.CreatedBy)
        //                .UsePropertyName("createdBy")
        //                .UseType(auditField =>
        //                {

        //                });
                        
        //        });
        //        builder.AddVertex<EmployeeTaxInfo>("employeeTaxInfo", entity =>
        //        {
        //            entity.HasLabel("employeeTaxInfo");
        //            entity.HasKey(p => p.TaxInfoId);
        //        });
        //        builder.AddVertex<EmployeeAddress>("employeeAddresses", entity =>
        //        {
        //            entity.HasLabel("employeeAddress");
        //            entity.HasKey(p => p.AddressId);

        //        });
        //        builder.AddVertex<EmployeeAddressType>("employeeAddressTypes", entity =>
        //        {
        //            entity.HasLabel("employeeAddressType");
        //            entity.HasKey(p => p.TypeId);

        //        });
        //        builder.AddVertex<EmployeeJob>("employeeJobs", entity =>
        //        {
        //            entity.HasLabel("employeeJob");
        //        });
        //        builder.AddVertex<EmployeeTask>("employeeTasks", entity =>
        //        {
        //            entity.HasLabel("employeeTask");
        //            entity.HasKey(p => p.TaskId);

        //        });
        //        builder.AddVertex<EmployeeWorkItem>("employeeWorkItems", entity =>
        //        {
        //            entity.HasLabel("employeeWorkItem");
        //            entity.HasKey(p => p.WorkItemId);
        //        });

        //        // Configure Edges
        //        builder.AddEdge<Employee, EmployeeAddress>("addresses");
        //        builder.AddEdge<Employee, EmployeeAddress>("primaryAddress");
        //        builder.AddEdge<EmployeeAddress, EmployeeAddressType>("types");

        //        builder.AddEdge<Employee, EmployeeJob>("jobs");
        //        builder.AddEdge<EmployeeJob, EmployeeTask>("tasks");
        //        builder.AddEdge<EmployeeTask, EmployeeWorkItem>("workItems");
        //    })
        //    .Build();
    }



    public static IOGraphGdm CreateFluentOnlyModel()
    {
        return factory.Create("employees");
    }
}
