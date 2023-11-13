

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.ErpCore.Types;

using Assimalign.OGraph;
using Assimalign.ErpCore.Entities;

public class EmployeeAddressType : ComplexType<EmployeeAddress>
{
    protected override void Configure(IOGraphComplexTypeDescriptor<EmployeeAddress> descriptor)
    {
        descriptor.HasProperty(p => p.Address)
            .UseType(address =>
            {
                // Extending Address Type by adding a computed property
                address.HasProperty("fullAddress")
                    .UseResolver(context =>
                    {
                        var parent = context.GetParent<Address>();
                        var builder = new StringBuilder();

                        builder.Append(parent.StreetOne);
                        builder.Append(parent.StreetTwo);
                        builder.Append(parent.City);
                        builder.Append(parent.ZipCode);

                        return builder.ToString();
                    });
            });

    }
}
