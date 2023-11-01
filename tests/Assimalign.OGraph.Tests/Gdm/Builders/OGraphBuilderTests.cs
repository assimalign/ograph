using Assimalign.OGraph.Syntax;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace Assimalign.OGraph.Gdm.Tests;


interface IOGraphVertexResolutionDescriptor
{

}




public partial class OGraphBuilderTests
{
    [Fact]
    public void Test1()
    {
        var builder = default(IOGraphBuilder)!;

        var graph = builder
            .ConfigureModel(gdm =>
            {
                gdm.AddVertex<Employee>(vertex =>
                {
                    vertex.HasLabel("employee");
                    vertex.HasKey(p => p.EmployeeId);

                    vertex.HasProperty(p => p.EmployeeId)
                        .UseName("employeeId");

                    vertex.HasProperty(p => p.Details, details =>
                    {
                        details.HasProperty("fullName")
                            .UseType<StringType>();
                    });


                    vertex.HasEdge<EmployeeAddress>("addresses")    // Route = /employees/{employeeId}/addresses
                        .WithMany()
                        .HasReferenceKey(p => p.EmployeeId);

                    vertex.HasEdge<EmployeeAddress>("primaryAddress")    // Route = /employees/{employeeId}/addresses
                        .WithOne()
                        .HasReferenceKey(p => p.EmployeeId);

                    vertex.HasEdge<EmployeeTaxInfo>("taxInfo")
                        .WithOne()
                        .HasReferenceKey(p => p.EmployeeId);
                });
                gdm.AddVertex<EmployeeAddress>(vertex =>
                {
                    vertex.HasLabel("employeeAddress");
                    vertex.HasKey(p => p.AddressId);



                    vertex.HasEdge("employee");
                });
                gdm.AddVertex<EmployeeTaxInfo>(vertex =>
                {
                    vertex.HasLabel("employeeTaxInfo");
                });
                gdm.AddType<Employee>("employeeCreateInput", employee =>
                {
                    employee.Ignore(p => p.EmployeeId);
                });
            })
            .ConfigureApplication(app =>
            {
                app.Bind<Employee>(descriptor =>
                {
                    descriptor.MapGet("getEmployees")
                        .UseRoute("/employees")
                        .UseResolver(async (context, cancellationToken) =>
                        {
                            return default!;
                        });

                    descriptor.MapGet("get")
                        .UseRoute("/employees/{employeeId}")
                        .UseResolver(async (context, cancellationToken) =>
                        {
                            return default!;
                        });
                });
                app.Bind<EmployeeAddress>(descriptor =>
                {
                    descriptor.MapGet("getAllEmployeeAddresses")
                        .UseRoute("/addresses")
                        .UseResolver(async (context, cancellationToken) =>
                        {
                            return default!;
                        });

                    descriptor.MapGet("employeeAddresses")
                        .UseRoute("/employees/{employeeId}/addresses")
                        .UseResolver(async (context, cancellationToken) =>
                        {
                            return default!;
                        });

                    descriptor.MapGet("QueryUserPrimaryAddress")
                        .UseRoute("/employees/{employeeId}/primaryAddress")
                        .UseResolver(async (context, cancellationToken) =>
                        {
                            return default!;
                        });

                    descriptor.MapGet("QueryUserAddressById")
                        .UseRoute("/employees/{employeeId}/addresses/{addressId}")
                        .UseResolver(async (context, cancellationToken) =>
                        {
                            return default!;
                        });

                    descriptor.MapPost("createEmployeeAddress");
                });
                app.Bind<EmployeeTaxInfo>(descriptor =>
                {

                });
            })
            .Build();


        var executor = graph.GetExecutor();
    }
}