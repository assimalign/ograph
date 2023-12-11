using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Assimalign.OGraph.Server.Tests;

using Gdm;

public partial class BuilderTests
{
    [Fact]
    public void Test()
    {
        var builder = default(IOGraphExecutorBuilder)!;

        builder.ConfigureModel("employees", model =>
        {
            model.AddVertex<Employee>(vertex =>
            {
                vertex.HasLabel("employee");
                vertex.HasType(entity =>
                {
                    entity.HasKey(p => p.EmployeeId);

                    entity.HasProperty(p=> p.EmployeeId)
                        .UsePropertyName("employeeId")
                        .UseSetter((obj, value) =>
                        {
                            if (obj is Employee employee)
                            {
                                switch(value)
                                {
                                    case Guid guid:
                                        {
                                            employee.EmployeeId = guid;
                                            break;
                                        }
                                    case EmployeeId id:
                                        {
                                            employee.EmployeeId = id;
                                            break;
                                        }
                                }
                            }
                        })
                        .UseGetter(obj =>
                        {
                            if (obj is Employee employee)
                            {
                                return employee.EmployeeId;
                            }
                            return default;
                        });
                });
            });
            model.AddVertex<EmployeeAddress>(vertex =>
            {
                vertex.HasLabel("address");
                vertex.HasType(entity =>
                {
                    entity.HasKey(p => p.AddressId);
                    entity.HasProperty(p => p.AddressId);
                });
            });
            model.AddVertex<EmployeeAddressType>(vertex =>
            {
                vertex.HasLabel("addressType");
                vertex.HasType(entity =>
                {
                    entity.HasKey(p => p.TypeId);
                    entity.HasProperty(p => p.TypeId);
                });
            });
            model.AddVertex<EmployeeTaxInfo>(vertex =>
            {
                vertex.HasLabel("taxInfo");
                vertex.HasType(entity =>
                {
                    entity.HasKey(p => p.TaxInfoId);
                    entity.HasProperty(p => p.EmployeeId);
                });
            });
        });

        builder.ConfigureApplication(app =>
        {
            app.Bind<Employee>(descriptor =>
            {
                descriptor.MapQuery("GetEmployees")
                    .UseMiddleware((context, cancellationToken, next) =>
                    {
                        if (!context.GetClaimsPrincipal().Identity!.IsAuthenticated)
                        {
                            return new R
                        }
                    })
                    .UseResolver((context, cancellationToken) =>
                    {
                        return Task.FromResult(default(IOGraphResult));
                    });
            });

            app.Bind<EmployeeAddress>(descriptor =>
            {

            });

            app.Bind<EmployeeAddressType>(descriptor =>
            {

            });

            app.Bind<EmployeeTaxInfo>(descriptor =>
            {

            });
        });

        var executor = builder.Build();

    }
}
