using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Gdm.Tests;

using Objects;
using Elements;

public static partial class GdmBuilderUtility
{
    private static IOGraphGdm CreateFluentModel()
    {

        // Builder process 
        return OGraphGdmBuilder.Create("ErpCore")
            .AddGraph("organizations", graph =>
            {
                graph.AddMeta("description", "");

                #region Types

                graph.AddComplexType<Organization>(complex =>
                {
                    complex.HasName("CreateOrUpdateOrganization");
                });

                #endregion

                graph.AddVertex<Organization>(vertex =>
                {
                    vertex.HasLabel("organizations");
                    vertex.HasEntityType(entity =>
                    {
                        entity.HasName("Organization");
                        entity.HasKey(p => p.Id);

                        entity.HasProperty(p => p.Id);
                        entity.HasProperty(p => p.Created)
                            .UseType(complex =>
                            {
                                complex.HasProperty(p => p.UserId);
                                complex.HasProperty(p => p.Timestamp).UseType<GdmDateTimeOffsetType>();
                                //complex.HasProperty(p=>p.)
                            });
                    });


                    //vertex.HasOperation();
                });
                graph.AddVertex<Organization>("organizations", descriptor =>
                {
                    descriptor.HasName("Organization");
                    descriptor.HasKey(p => p.Id);
                    descriptor.HasProperty(p => p.Id)
                        .AddMeta("description", "")
                        .IsRequired();

                    descriptor.AddMeta("", "");

                });
            })
            .AddGraph("users", graph =>
            {
                graph.AddEntityType<User>(entity =>
                {
                    entity.HasName("User");
                    entity.HasKey(p => p.Id);

                    entity.HasProperty(p => p.Id).UsePropertyName("id");
                    entity.HasProperty(p => p.Info).UsePropertyName("info")
                        .IsRequired()
                        .UseType(complex =>
                        {
                            complex.HasName("UserInfo");
                            complex.HasProperty(p => p.FirstName).UsePropertyName("firstName");
                        });
                    entity.AddMeta("", "");
                });
                graph.AddComplexType<User>(complex =>
                {
                    complex.HasName("CreateOrUpdateUser");
                    complex.HasProperty(p => p.Info);
                });

                graph.AddVertex<User>(vertex =>
                {
                    vertex.HasLabel("users");
                    vertex.HasEntityType(entity =>
                    {
                        entity.HasName("User");
                        entity.HasKey(p => p.Id);

                        entity.HasProperty(p => p.Id).UsePropertyName("id");
                        entity.HasProperty(p => p.Info).UsePropertyName("info")
                            .IsRequired()
                            .UseType(complex =>
                            {
                                complex.HasName("UserInfo");
                                complex.HasProperty(p => p.FirstName).UsePropertyName("firstName");
                            });

                        entity.AddMeta("", "");
                    });

                    //vertex.HasOperation()
                });
                graph.AddVertex<UserProfile>("Profile", entity =>
                {
                    entity.HasName("UserProfile");
                });
            })
            .AddGraph("employees", graph =>
            {
                graph.AddVertex<Employee>(vertex =>
                {
                    vertex.HasLabel("employees");
                    vertex.HasEntityType(entity =>
                    {
                        entity.HasName("Employee");
                        entity.HasKey(p => p.EmployeeId);
                        entity.HasProperty(p => p.EmployeeId)
                            .UsePropertyName("id")
                            .AddMeta("", "");
                    });
                });
                graph.AddVertex<EmployeeAddress>(vertex =>
                {
                    vertex.HasLabel("employeeAddresses");
                });
                graph.AddVertex<EmployeeJob>(vertex =>
                {
                    vertex.HasLabel("employeeJobs");
                    vertex.HasEntityType(entity =>
                    {
                        entity.HasName("Job");
                    });
                });
            })
            .Build();
    }
}
