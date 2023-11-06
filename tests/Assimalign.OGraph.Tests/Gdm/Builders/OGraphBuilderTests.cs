using Assimalign.OGraph.Syntax;
using System.Linq.Expressions;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace Assimalign.OGraph.Gdm.Tests;

public partial class OGraphBuilderTests
{
    //public interface IOGraphVertexBindingDescriptor<T>
    //{
    //    IOGraphVertexBindingDescriptor<T> Property<TMember>(Expression<Func<T, TMember>> expression);
    //}
    //public abstract class VertexBinding<T>
    //{
    //    protected abstract void Configure(IOGraphVertexBindingDescriptor<T> descriptor);
    //}

    //public class EmployeeVertexBinding : VertexBinding<Employee>
    //{
    //    protected override void Configure(IOGraphVertexBindingDescriptor<Employee> descriptor)
    //    {
    //        throw new NotImplementedException();
    //    }
    //}


    public class EmployeeEntityType : GdmEntityType<Employee>
    {
        protected override void Configure(IOGraphGdmEntityTypeDescriptor<Employee> vertex)
        {
            vertex.HasKey(p => p.EmployeeId);

            vertex.HasProperty(p => p.EmployeeId).UsePropertyName("employeeId")
                .UseType<GdmStringType>();

            vertex.HasProperty(p => p.Details)
                .UsePropertyName("details")
                .UseType(details =>
                {
                    details.HasProperty("fullName");
                    details.HasProperty(p => p!.FirstName).UsePropertyName("firstName");
                    details.HasProperty(p => p!.LastName).UsePropertyName("lastName");
                    details.HasProperty(p => p!.MiddleName).UsePropertyName("middleName");
                    details.HasProperty(p => p!.Birthdate).UsePropertyName("birthdate");
                });

            vertex.HasProperty(p => p.CreatedBy)
                .UsePropertyName("createdBy")
                .UseType(audit =>
                {
                    audit.HasName("EmployeeCreatedByAuditField");
                    audit.HasProperty(p => p!.UserId).UsePropertyName("userId");
                    audit.HasProperty(p => p!.Timestamp).UsePropertyName("timestamp");
                });

            vertex.HasProperty(p => p.UpdatedBy)
                .UsePropertyName("updatedBy")
                .UseType(audit =>
                {
                    audit.HasName("EmployeeUpdatedByAuditField");
                    audit.HasProperty(p => p!.UserId).UsePropertyName("userId");
                    audit.HasProperty(p => p!.Timestamp).UsePropertyName("timestamp");
                });

            vertex.HasProperty(p => p.Roles)
                .UsePropertyName("roles");
        }
    }

    [Fact]
    public void Test1()
    {

        var type = new EmployeeEntityType();


        var builder = new OGraphBuilder("employees");

        var graph = builder
            .ConfigureModel(gdm =>
            {
                gdm.AddVertex<Employee>(vertex =>
                {
                    vertex.HasKey(p => p.EmployeeId);

                    vertex.HasProperty(p => p.EmployeeId).UsePropertyName("employeeId")
                        .UseType<GdmStringType>();

                    vertex.HasProperty(p => p.Details)
                        .UsePropertyName("details")
                        .UseType(details =>
                        {
                            details.HasProperty("fullName");
                            details.HasProperty(p => p!.FirstName).UsePropertyName("firstName");
                            details.HasProperty(p => p!.LastName).UsePropertyName("lastName");
                            details.HasProperty(p => p!.MiddleName).UsePropertyName("middleName");
                            details.HasProperty(p => p!.Birthdate).UsePropertyName("birthdate");
                        });

                    vertex.HasProperty(p => p.CreatedBy)
                        .UsePropertyName("createdBy")
                        .UseType(audit =>
                        {
                            audit.HasName("EmployeeCreatedByAuditField");
                            audit.HasProperty(p => p!.UserId).UsePropertyName("userId");
                            audit.HasProperty(p => p!.Timestamp).UsePropertyName("timestamp");
                        });

                    vertex.HasProperty(p => p.UpdatedBy)
                        .UsePropertyName("updatedBy")
                        .UseType(audit =>
                        {
                            audit.HasName("EmployeeUpdatedByAuditField");
                            audit.HasProperty(p => p!.UserId).UsePropertyName("userId");
                            audit.HasProperty(p => p!.Timestamp).UsePropertyName("timestamp");
                        });

                    vertex.HasProperty(p => p.Roles)
                        .UsePropertyName("roles");

                    //vertex.HasEdge<EmployeeAddress>("addresses")    // Route = /employees/{employeeId}/addresses
                    //    .WithMany()
                    //    .HasReferenceKey(p => p.EmployeeId);

                    //vertex.HasEdge<EmployeeAddress>("primaryAddress")    // Route = /employees/{employeeId}/addresses
                    //    .WithOne()
                    //    .HasReferenceKey(p => p.EmployeeId);

                    //vertex.HasEdge<EmployeeTaxInfo>("taxInfo")
                    //    .WithOne()
                    //    .HasReferenceKey(p => p.EmployeeId);
                });
                gdm.AddVertex<EmployeeAddress>(vertex =>
                {
                    //vertex.HasLabel("address");
                    //vertex.HasKey(p => p.AddressId);



                    //vertex.HasEdge<Employee>("employee") 
                    //    .WithOne()
                    //    .HasReferenceKey(p => p.EmployeeId);
                });
                gdm.AddVertex<EmployeeAddressType>(vertex =>
                {
                    //vertex.HasLabel("addressType");

                });
                gdm.AddVertex<EmployeeTaxInfo>(vertex =>
                {
                    //vertex.HasLabel("taxInfo");
                });

                // Added custom types
                //gdm.AddType<Employee>("employeeCreateInput", employee =>
                //{
                //    employee.Ignore(p => p.EmployeeId);
                //});
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

    }
}