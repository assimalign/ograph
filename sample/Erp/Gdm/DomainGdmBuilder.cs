using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assimalign.OGraph.Gdm;

namespace Erp.Gdm;

using Erp.Entities;

internal class DomainGdmBuilder
{
    public static IOGraphGdmFactory BuildFluentFactory()
    {
        var builder = new OGraphGdmFactoryBuilder();

        builder.UseStrategy(GdmBuilderStrategy.Implicit);



        builder.Configure("employees", graph =>
        {
            // Define Reusable Types
            graph.AddComplexType<Audit>(complexType =>
            {
                complexType.HasProperty(p => p.UserId);
                complexType.HasProperty(p => p.Timestamp);
            });

            graph.AddVertex<Employee>(vertex =>
            {
                vertex.HasLabel("Employee");
                vertex.HasType(employee =>
                {
                    employee.HasKey(p => p.Id);
                    employee.HasProperty(p => p.Info)
                        .UseType(info =>
                        {
                            info.HasProperty(p => p.FirstName);
                            info.HasProperty(p => p.LastName);
                            info.HasProperty(p => p.MiddleName);
                        });

                    employee.HasProperty(p => p.Created);
                    employee.HasProperty(p => p.Domain).UsePropertyName("__domain");
                });
            });
        });
        builder.Configure("users", graph =>
        {
            graph.AddVertex<User>(vertex =>
            {
                vertex.HasLabel("users");
                vertex.HasType(user =>
                {
                    user.HasKey(p => p.Id);

                    user.HasProperty(p => p.Id);
                    user.HasProperty(p => p.Username);
                    user.HasProperty(p => p.Email);

                });
            });
        });
        builder.Configure("companies", graph =>
        {
            graph.AddVertex<Company>(vertex =>
            {
                vertex.HasLabel("Company");
                vertex.HasType(company =>
                {

                });
            });
        });
        builder.Configure("contacts", graph =>
        {
            graph.AddVertex<Contact>(vertex =>
            {
                vertex.HasLabel("Contact");
                vertex.HasType(contact =>
                {

                });
            });
        });
        builder.Configure("organizations", graph =>
        {
            graph.AddVertex<Organization>(vertex =>
            {
                vertex.HasLabel("Organization");
                vertex.HasType(organization =>
                {

                });
            });
        });

        var factory =  builder.Build();
        var employeeGraph = factory.Create("employees");

        var vertex = employeeGraph.GetGdmVertices().First(p => p.IsRuntimeTypeMatch(typeof(Employee)));

        return factory;
    }
}
