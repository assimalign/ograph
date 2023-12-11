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




    internal partial class TypeTestEntity : GdmEntityType<Employee>
    {
        private readonly Action<IOGraphGdmEntityTypeDescriptor<Employee>> configure;

        public TypeTestEntity(Action<IOGraphGdmEntityTypeDescriptor<Employee>> configure)
        {
            this.configure = configure;
        }

        protected override void Configure(IOGraphGdmEntityTypeDescriptor<Employee> descriptor)
        {
            configure.Invoke(descriptor);
        }
    }
}
