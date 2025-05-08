using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Gdm.Tests;

using Objects;
using Elements;

public static partial class GdmBuilderUtility
{
    private static IOGraphGdm CreateFluentModel()
    {
        // Builder process 
        var builder = GdmBuilder.Create("ErpCore")
            .AddGraph("Hrm", graph =>
            {
                graph.AddScalarType<GdmStringType>();

                graph.AddVertex<Employee>(vertex =>
                {
                    vertex.HasLabel("employees");
                    vertex.HasEntityType(entity =>
                    {
                        entity.HasName("employee");
                        entity.HasKey(p => p.EmployeeId);

                        entity.HasProperty(p => p.EmployeeId).UsePropertyName("id");
                        entity.HasProperty(p => p.Info)
                            .UseType(complex =>
                            {
                                complex.HasName("employeeInfo");
                                complex.HasProperty(p => p.FirstName);
                                complex.HasProperty(p => p.LastName);
                            });
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
            });
            //.AddGraph("Organizations", descriptor =>
            //{
            //    //descriptor.ConvertAllNamesToCamalCase(GdmElementKind.Member, GdmElementKind.Type);

            //    // Types
            //    descriptor.AddComplexType<EmployeeInfo>(complex =>
            //    {
            //        complex.HasName("EmployeeInfo");
            //    });
            //    descriptor.AddComplexType<Employee>(complex =>
            //    {
            //        complex.HasName("EmployeeCreateInput");
            //        complex.HasProperty(p => p.Info)
            //            .UseType(info =>
            //            {
            //                info.HasName("");
            //                info.HasProperty(p => p.FirstName);
            //                info.HasProperty(p => p.LastName);
            //            });
            //        complex.HasProperty(p => p.Roles)
            //            .UseType(role =>
            //            {

            //            });
            //    });
            //    descriptor.AddComplexType<Employee>(complex =>
            //    {
            //        complex.HasName("EmployeeUpdateInput");
            //        complex.HasProperty(p => p.Info);
            //    });
            //    descriptor.AddEntityType<Employee>(entity =>
            //    {
            //        entity.HasName("Employee");
            //    });

            //    // Vertices
            //    descriptor.AddVertex<Employee>(vertex =>
            //    {
            //        vertex.HasLabel("EmployeeVertex");
            //        vertex.HasEntityType("Employee");
            //    });
            //    descriptor.AddVertex<EmployeeAddress>(vertex =>
            //    {
            //        vertex.HasLabel("EmployeeAddress");
            //        vertex.HasEntityType("");
            //        vertex.HasEntityType(entity =>
            //        {
            //            entity.HasName("");
            //            entity.HasKey(p => p.EmployeeId);

            //            entity.HasProperty(p => p.AddressId).UsePropertyName("id");
            //            entity.HasProperty(p => p.EmployeeId).UsePropertyName("EmployeeId");
            //            entity.HasProperty(p => p.Address);
            //        });
            //    });


            //    #region Types

            //    //graph.AddType()
            //    //graph.AddComplexType<Organization>(complex =>
            //    //{
            //    //    complex.HasName("CreateOrUpdateOrganization");
            //    //});

            //    #endregion

            //    descriptor.AddVertex<Organization>(vertex =>
            //    {
            //        vertex.HasLabel("organizations");
            //        vertex.HasEntityType(entity =>
            //        {
            //            entity.HasName("Organization");
            //            entity.HasKey(p => p.Id);

            //            entity.HasProperty(p => p.Id);
            //            entity.HasProperty(p => p.Created)
            //                .UseType(complex =>
            //                {
            //                    complex.HasProperty(p => p.UserId);
            //                    complex.HasProperty(p => p.Timestamp).UseType<GdmDateTimeOffsetType>();
            //                    //complex.HasProperty(p=>p.)
            //                });
            //        });
            //    });
            //    descriptor.AddVertex<Organization>("organizations", descriptor =>
            //    {
            //        descriptor.HasName("Organization");
            //        descriptor.HasKey(p => p.Id);
            //        descriptor.HasProperty(p => p.Id)
            //            .UsePropertyName("id")
            //            .AddMeta("description", "")
            //            .IsRequired();

            //        descriptor.AddMeta("", "");

            //    });
            //})
            //.AddGraph("Administration", graph =>
            //{
            //    graph.AddEntityType<User>(entity =>
            //    {
            //        entity.HasName("User");
            //        entity.HasKey(p => p.Id);

            //        entity.HasProperty(p => p.Id).UsePropertyName("id");
            //        entity.HasProperty(p => p.Info).UsePropertyName("info")
            //            .IsRequired()
            //            .UseType(complex =>
            //            {
            //                complex.HasName("UserInfo");
            //                complex.HasProperty(p => p.FirstName).UsePropertyName("firstName");
            //            });
            //        entity.AddMeta("", "");
            //    });
            //    graph.AddComplexType<User>(complex =>
            //    {
            //        complex.HasName("CreateOrUpdateUser");
            //        complex.HasProperty(p => p.Info);
            //    });

            //    graph.AddVertex<User>(vertex =>
            //    {
            //        vertex.HasLabel("users");
            //        vertex.HasEntityType(entity =>
            //        {
            //            entity.HasName("User");
            //            entity.HasKey(p => p.Id);

            //            entity.HasProperty(p => p.Id).UsePropertyName("id");
            //            entity.HasProperty(p => p.Info).UsePropertyName("info")
            //                .IsRequired()
            //                .UseType(complex =>
            //                {
            //                    complex.HasName("UserInfo");
            //                    complex.HasProperty(p => p.FirstName).UsePropertyName("firstName");
            //                });

            //            entity.AddMeta("", "");
            //        });

            //        //vertex.HasOperation()
            //    });
            //    graph.AddVertex<UserProfile>("Profile", entity =>
            //    {
            //        entity.HasName("UserProfile");
            //    });

            //    graph.AddEdge<User, UserProfile>(edge =>
            //    {

            //    });

            //})
            return builder.Build();
    }
}
