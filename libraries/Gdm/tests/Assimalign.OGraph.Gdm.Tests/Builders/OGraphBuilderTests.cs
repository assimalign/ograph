using System.Linq.Expressions;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace Assimalign.OGraph.Gdm.Tests;

public class EmployeeEntityType : GdmEntityType<Employee>
{
    protected override void Configure(IOGraphGdmEntityTypeDescriptor<Employee> descriptor)
    {
        descriptor.HasProperty(p => p.Details)
            .UsePropertyName("details");
            
    }
}

public class EmployeeVertex : GdmVertex<Employee>
{
    protected override void Configure(IOGraphGdmVertexDescriptor<Employee> descriptor)
    {
        descriptor.HasLabel("employee");
        descriptor.HasType<EmployeeEntityType>();

        descriptor.BindGet("GetEmployees")
            .UseRoute("/api/employees")
            .UseMiddleware((context, cancellationToken, next) =>
            {
                return next.Invoke(context, cancellationToken);
            })
            .UseResolver((context, cancellationToken) =>
            {

                return default!;
            });

        descriptor.BindGet("GetEmployeeById")
            .UseRoute("/api/employees/{employeeId}")
            .UseMiddleware((context, cancellationToken, next) =>
            {
                return next.Invoke(context, cancellationToken);
            })
            .UseResolver((context, cancellationToken) =>
            {

                return default!;
            });

        descriptor.BindPost("CreateEmployee")
            .UseRoute("/api/employees")
            .UseMiddleware((context, cancellationToken, next) =>
            {
                return next.Invoke(context, cancellationToken);
            })
            .UseResolver((context, cancellationToken) =>
            {

                return default!;
            });
    }
}

public partial class OGraphBuilderTests
{
    [Fact]
    public void Test1()
    {
        var builder = new OGraphBuilder("employees");

        var graph = builder
            .ConfigureModel(gdm =>
            {
                gdm.AddVertex<EmployeeVertex>();

                gdm.AddType<AuditField>(type =>
                {
                    type.HasProperty(p => p!.UserId).UsePropertyName("userId");
                    type.HasProperty(p => p!.Timestamp).UsePropertyName("timestamp");
                });
                gdm.AddVertex<Employee>(vertex =>
                {
                    vertex.HasType<EmployeeEntity>();
                    vertex.HasLabel("employee")
                        .HasType(entity =>
                        {
                            entity.HasKey(p => p.EmployeeId);

                            entity.HasProperty(p => p.EmployeeId)
                                .UsePropertyName("employeeId")
                                .UseType<GdmStringType>()
                                .UseMetadata("description", "The unique identifier of the employee");

                            entity.HasProperty(p => p.Details)
                                .UsePropertyName("details")
                                .UseType(details =>
                                {
                                    details.HasProperty(p => p!.FirstName).UsePropertyName("firstName");
                                    details.HasProperty(p => p!.LastName).UsePropertyName("lastName");
                                    details.HasProperty(p => p!.MiddleName).UsePropertyName("middleName");
                                    details.HasProperty(p => p!.Birthdate).UsePropertyName("birthdate");
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
                gdm.AddVertex<EmployeeAddress>(vertex =>
                {
                    vertex.HasKey(p => p.AddressId);
                    //vertex.HasLabel("address");
                    //vertex.HasKey(p => p.AddressId);

                    vertex.HasProperty(p => p.Address);

                    //vertex.HasEdge<Employee>("employee") 
                    //    .WithOne()
                    //    .HasReferenceKey(p => p.EmployeeId);
                });
                gdm.AddVertex<EmployeeAddressType>(vertex =>
                {
                    vertex.HasKey(p => p.TypeId);
                    //vertex.HasLabel("addressType");

                });
                gdm.AddVertex<EmployeeTaxInfo>(vertex =>
                {
                    vertex.HasKey(p => p.TaxInfoId);
                });
            })
            //.ConfigureApplication(app =>
            //{
            //    // TODO: Need to add a vertex resolver binding
            //    app.Bind<Employee>(descriptor =>
            //    {
            //        descriptor.MapGet("getEmployees")
            //            .UseRoute("/employees")
            //            .UseResolver(async (context, cancellationToken) =>
            //            {
            //                return default!;
            //            });

            //        descriptor.MapGet("get")
            //            .UseRoute("/employees/{employeeId}")
            //            .UseResolver(async (context, cancellationToken) =>
            //            {
            //                return default!;
            //            });
            //    });
            //    app.Bind<EmployeeAddress>(descriptor =>
            //    {
            //        descriptor.MapGet("getAllEmployeeAddresses")
            //            .UseRoute("/addresses")
            //            .UseResolver(async (context, cancellationToken) =>
            //            {
            //                return default!;
            //            });

            //        descriptor.MapGet("employeeAddresses")
            //            .UseRoute("/employees/{employeeId}/addresses")
            //            .UseResolver(async (context, cancellationToken) =>
            //            {
            //                return default!;
            //            });

            //        descriptor.MapGet("QueryUserPrimaryAddress")
            //            .UseRoute("/employees/{employeeId}/primaryAddress")
            //            .UseResolver(async (context, cancellationToken) =>
            //            {
            //                return default!;
            //            });

            //        descriptor.MapGet("QueryUserAddressById")
            //            .UseRoute("/employees/{employeeId}/addresses/{addressId}")
            //            .UseResolver(async (context, cancellationToken) =>
            //            {
            //                return default!;
            //            });

            //        descriptor.MapPost("createEmployeeAddress");
            //    });
            //    app.Bind<EmployeeTaxInfo>(descriptor =>
            //    {

            //    });
            //})
            .Build();

        var model = graph.Model;

        var vertices = model.GetGdmVertices();
        var types = model.GetGdmTypes();
        var primitives = model.GetGdmPrimitiveTypes();
        var entities = model.GetGdmEntityTypes();
        var collection = model.GetGdmCollectionTypes();

    }
}