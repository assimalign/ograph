using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Server.Tests;

using Gdm;

internal static class TestHelpers
{
    private static IOGraphExecutorBuilder builder = new OGraphExecutorBuilder();


    private static void ConfigureOptions()
    {
        builder.ConfigureOptions(options =>
        {
            options.RoutePrefix = "api";
            options.Timeout = TimeSpan.FromSeconds(30);
        });
    }

    private static void ConfigureModel()
    {
        builder.ConfigureModel("employees", model =>
        {
            model.AddVertex<Employee>(vertex =>
            {
                vertex.HasLabel("employee");
                vertex.HasEntityType(entity =>
                {
                    entity.HasKey(p => p.EmployeeId);

                    entity.HasProperty(p => p.EmployeeId).UsePropertyName("employeeId");
                    entity.HasProperty(p => p.Details)
                        .UsePropertyName("details")
                        .UseType(details =>
                        {
                            details.HasProperty(p => p.FirstName).UsePropertyName("firstName");
                            details.HasProperty(p => p.LastName).UsePropertyName("lastName");
                            details.HasProperty(p => p.MiddleName).UsePropertyName("middleName");
                            details.HasProperty(p => p.Birthdate).UsePropertyName("birthdate");
                        });

                    entity.HasProperty(p => p.Roles)
                        .UsePropertyName("employeeId")
                        .UseType(role =>
                        {
                            role.HasProperty(p => p.Id).UsePropertyName("id");
                            role.HasProperty(p =>p.Name).UsePropertyName("name");
                        });

                    entity.HasProperty(p => p.CreatedBy)
                        .UsePropertyName("createdBy")
                        .UseType(created =>
                        {
                            created.HasProperty(p => p.UserId).UsePropertyName("userId");
                            created.HasProperty(p => p.Timestamp).UsePropertyName("timestamp");
                        });

                    entity.HasProperty(p => p.UpdatedBy)
                        .UsePropertyName("updatedBy")
                        .UseType(updated =>
                        {
                            updated.HasProperty(p => p.UserId).UsePropertyName("userId");
                            updated.HasProperty(p => p.Timestamp).UsePropertyName("timestamp");
                        });
                });

                vertex.HasEdge<EmployeeAddress>("");
            });
            model.AddVertex<EmployeeAddress>(vertex =>
            {
                vertex.HasLabel("address");
            });
        });
    }



    public static IOGraphExecutor GetExecutor()
    {
        ConfigureModel();

        return builder.Build();
    }
}
