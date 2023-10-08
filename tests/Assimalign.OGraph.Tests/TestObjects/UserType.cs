using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph.TestObjects;

public class UserType : ComplexType<UserType>
{
    protected override void Configure(IOGraphComplexTypeDescriptor<UserType> descriptor)
    {
        descriptor.HasProperty("test")
            .UseResolver(context =>
            {
                return "";
            });
    }
}
