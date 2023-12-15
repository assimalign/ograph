using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Assimalign.OGraph.Gdm.Tests;

public partial class TypeTests
{
    [Fact(DisplayName = "Test Test (Entity): Configure Argument Null Exception")]
    public void ThrowNullArgumentExceptionTest()
    {
        Assert.Throws<ArgumentNullException>(() =>
        {
            var entityType = GdmEntityType<Employee>.Create(default!);
        });
    }


    [Fact]
    public void TestPropertyNameCasingSuccess()
    {
        var entityType = GdmEntityType<Employee>.Create(descriptor =>
        {
            descriptor.HasLabel("Employee");

            descriptor.HasProperty(p => p.EmployeeId).UsePropertyName("employeeId");
            descriptor.HasProperty(p => p.Roles).UsePropertyName("roles")
                .UseType(role =>
                {
                    role.HasLabel("EmployeeRole");
                    role.HasProperty(p => p.Id).UsePropertyName("id");
                });
        });


        var property = entityType.Properties.First(p => p.Label == "employeeId");

        Assert.NotNull(property);
    }




    public partial class TestEntityType
    {
        public partial class NonGeneric : GdmEntityType
        {
            protected override void Configure(IOGraphGdmEntityTypeDescriptor descriptor)
            {
                descriptor.HasLabel("Employee")
                    .HasRuntimeType(typeof(Employee));
            }
        }

        public partial class Generic : GdmEntityType<Employee>
        {
            protected override void Configure(IOGraphGdmEntityTypeDescriptor<Employee> descriptor)
            {
                descriptor.HasLabel("employeeEntity");

                descriptor.HasKey(p => p.EmployeeId);


                descriptor.HasProperty("");

                descriptor.HasProperty(p => p.CreatedBy)
                    .UsePropertyName("createdBy")
                    .UseTypeReference("");
            }
        }
    }
}
