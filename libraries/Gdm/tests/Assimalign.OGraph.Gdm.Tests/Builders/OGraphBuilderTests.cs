using System.Linq.Expressions;
using System.Text.Json;
using System.Text.Json.Nodes;
using Xunit;

namespace Assimalign.OGraph.Gdm.Tests;


public class GdmEmployeeRole : GdmComplexType<EmployeeRole>
{
    protected override void Configure(IOGraphGdmComplexTypeDescriptor<EmployeeRole> descriptor)
    {
        base.Configure(descriptor);
    }
}

public class GdmEmployeeRoleCollection : GdmListType<EmployeeRole>
{
    public GdmEmployeeRoleCollection() : base(new GdmEmployeeRole())
    {
        
    }
}

public class EmployeeVertex : GdmVertex<Employee>
{
    protected override void Configure(IOGraphGdmVertexDescriptor<Employee> descriptor)
    {
        descriptor.HasLabel("employee");
        descriptor.HasType<EmployeeEntityType>();

        //descriptor.BindGet("GetEmployees")
        //    .UseRoute("/api/employees")
        //    .UseMiddleware((context, cancellationToken, next) =>
        //    {
        //        return next.Invoke(context, cancellationToken);
        //    })
        //    .UseResolver((context, cancellationToken) =>
        //    {

        //        return default!;
        //    });

        //descriptor.BindGet("GetEmployeeById")
        //    .UseRoute("/api/employees/{employeeId}")
        //    .UseMiddleware((context, cancellationToken, next) =>
        //    {
        //        return next.Invoke(context, cancellationToken);
        //    })
        //    .UseResolver((context, cancellationToken) =>
        //    {

        //        return default!;
        //    });

        //descriptor.BindPost("CreateEmployee")
        //    .UseRoute("/api/employees")
        //    .UseMiddleware((context, cancellationToken, next) =>
        //    {
        //        return next.Invoke(context, cancellationToken);
        //    })
        //    .UseResolver((context, cancellationToken) =>
        //    {

        //        return default!;
        //    });
    }
}

public partial class OGraphBuilderTests
{
    [Fact]
    public void Test1()
    {
        var builder = OGraphGdmBuilder.Create("employees");

        builder.AddType<AuditField>(type =>
        {
            type.HasLabel("AuditField");
            type.HasProperty(p => p!.UserId).UsePropertyName("userId");
            type.HasProperty(p => p!.Timestamp).UsePropertyName("timestamp");
        });
        builder.AddVertex<Employee>(vertex =>
        {
            vertex.HasLabel("employee")
                .HasType(entity =>
                {
                    entity.HasKey(p => p.EmployeeId);

                    entity.HasProperty(p => p.EmployeeId)
                        .UsePropertyName("employeeId")
                        .UseType<GdmStringType>()
                        .UseMetadata("description", "The unique identifier of the employee")
                        .UseSetter<Employee, EmployeeId?>((obj, id) => obj.EmployeeId = id);


                    entity.HasProperty(p => p.Temp)
                        .UseType<GdmInt32Type>();

                    entity.HasProperty(p => p.Details)
                        .UsePropertyName("details")
                        .UseType(details =>
                        {
                            details.HasProperty(p => p!.FirstName)
                                .UsePropertyName("firstName")
                                .UseType<GdmStringType>();

                            details.HasProperty(p => p!.LastName)
                                .UsePropertyName("lastName")
                                .UseType<GdmStringType>();

                            details.HasProperty(p => p!.MiddleName)
                                .UsePropertyName("middleName")
                                .UseType<GdmStringType>();

                            details.HasProperty(p => p!.Birthdate)
                                .UsePropertyName("birthdate")
                                .UseType<GdmDateType>();
                        });

                    entity.HasProperty(p => p.CreatedBy)
                        .UsePropertyName("createdBy");
                    //.UseType(audit =>
                    //{
                    //    audit.HasName("EmployeeCreatedByAuditField");
                    //    audit.HasProperty(p => p!.UserId).UsePropertyName("userId");
                    //    audit.HasProperty(p => p!.Timestamp).UsePropertyName("timestamp");
                    //});

                    entity.HasProperty(p => p.UpdatedBy)
                        .UsePropertyName("updatedBy");
                    //.UseType(audit =>
                    //{
                    //    audit.HasName("EmployeeUpdatedByAuditField");
                    //    audit.HasProperty(p => p!.UserId).UsePropertyName("userId");
                    //    audit.HasProperty(p => p!.Timestamp).UsePropertyName("timestamp");
                    //});

                    entity.HasProperty(p => p.Roles)
                        .UsePropertyName("roles")
                        .UseType(role =>
                        {
                            role.HasProperty(p => p.Id).UsePropertyName("id");
                            role.HasProperty(p => p.Name).UsePropertyName("name");
                        });
                });
        });
        builder.AddVertex<EmployeeAddress>(vertex =>
        {
            vertex.HasKey(p => p.AddressId);

            vertex.HasProperty(p => p.Address);

            //vertex.HasEdge<Employee>("employee") 
            //    .WithOne()
            //    .HasReferenceKey(p => p.EmployeeId);
        });
        builder.AddVertex<EmployeeAddressType>(vertex =>
        {
            vertex.HasKey(p => p.TypeId);
            //vertex.HasLabel("addressType");

        });
        builder.AddVertex<EmployeeTaxInfo>(vertex =>
        {
            vertex.HasKey(p => p.TaxInfoId);
        });


        var model = builder.Build();

        var vertices = model.GetGdmVertices();
        var types = model.GetGdmTypes();
        var primitives = model.GetGdmPrimitiveTypes();
        var entities = model.GetGdmEntityTypes();
        var collection = model.GetGdmCollectionTypes();

    }
}