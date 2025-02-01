using Assimalign.ErpCore.Entities;
using Assimalign.OGraph;
using Assimalign.OGraph.Gdm;
using Assimalign.OGraph.AspNetCore;


var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddOGraphOptions(options =>
    {
        options.RoutePrefix = "api";
    })
    .AddOGraph("employees", builder =>
    {
        builder.AddVertex<Employee>(vertex =>
        {
            vertex.HasLabel("employee");
            vertex.HasType(entity =>
            {
                entity.HasKey(p => p.EmployeeId);
                entity.HasProperty(p => p.Details);
            });
        });
        builder.AddVertex<EmployeeAddress>(vertex =>
        {
            vertex.HasLabel("address");
            vertex.HasType(entity =>
            {
                entity.HasKey(p => p.AddressId);
                entity.HasProperty(p => p.Address);
            });
        });
    });
   

var app = builder.Build();


app.MapOGraphBinding<Employee>(descriptor =>
{
    descript
    descriptor.MapGet("GetEmployees")
        .UseRoute("employees")
        .UseResolver(async (context, cancellationToken) =>
        {
            return OGraphResult.Unauthorized();
        });

    descriptor.MapGet("GetEmployeeById")
       .UseRoute("employees/{employeeId}")
       .UseResolver(async (context, cancellationToken) =>
       {
           return default;
       });

    descriptor.MapPut("UpdateEmployee")
        .UseRoute("employees/{employeeId}")
        .UseResolver(async (context, cancellationToken) =>
        {
            return default;
        });
});

app.MapOGraphBinding<EmployeeAddress>(descriptor =>
{
    descriptor.MapGet("GetEmployeeAddresses")
       .UseRoute("employees/{employeeId}/addresses")
       .UseResolver(async (context, cancellationToken) =>
       {
           return default;
       });

    descriptor.MapGet("GetEmployeeAddressById")
       .UseRoute("employees/{employeeId}/addresses/{addressId}")
       .UseResolver(async (context, cancellationToken) =>
       {
           return default;
       });

    descriptor.MapPut("UpdateEmployeeAddresses")
        .UseRoute("employees/{employeeId}/addresses/{addressId}")
        .UseResolver(async (context, cancellationToken) =>
        {
            return default;
        });
});

app.RunOGraph();