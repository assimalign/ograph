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
        return OGraphGdmBuilder.Create("ErpCore")
            .AddGraph("organizations", descriptor =>
            {
                descriptor.AddMeta("description", "");

                descriptor.AddVertex<Organization>("organizations", descriptor =>
                {
                    descriptor.HasLabel("Organization");
                    descriptor.HasKey(p => p.Id);
                    descriptor.HasProperty(p => p.Id)
                        .AddMeta("description", "")
                        .IsRequired();

                    descriptor.HasKey(entity =>
                    {
                        var property = entity.Members
                            .OfType<GdmProperty>()
                            .First();

                        return new GdmEntityKey(property);
                    });


                    descriptor.AddMeta("", "");
                });
            })
            .AddGraph("users", descriptor =>
            {

                descriptor.AddVertex<User>("Users", descriptor =>
                {
                    descriptor.HasLabel("User");
                    descriptor.HasKey(p => p.Id);


                    descriptor.AddMeta("", "");

                    descriptor.HasProperty(p => p.Id).UsePropertyName("id")
                        .UseType(graph =>
                        {
                           
                        });
                });
                descriptor.AddVertex<UserProfile>("Profile", entity =>
                {
                    entity.HasLabel("UserProfile");
                });
            })
            .AddGraph("employees", graph =>
            {
                graph.AddVertex<Employee>(vertex =>
                {
                    vertex.HasLabel("employees");
                    vertex.HasEntityType(entity =>
                    {
                        entity.HasLabel("Employee");
                        entity.HasKey(p => p.EmployeeId);
                        entity.HasProperty(p => p.EmployeeId)
                            .UsePropertyName("id")
                            .WithMeta("", "");
                    });
                    

                    vertex.AddQuery("GetEmployeeById", operation =>
                    {
                        operation.AddParameter("id");
                    });

                    vertex.AddQuery("GetEmployees", operation =>
                    {
                        operation.AddParameter("id", p => p.IsRequired());
                    });
                });
                graph.AddVertex<EmployeeAddress>("addresses", entity =>
                {
                    entity.HasLabel("EmployeeAddress");
                });
            })
            .Build();
    }
}
