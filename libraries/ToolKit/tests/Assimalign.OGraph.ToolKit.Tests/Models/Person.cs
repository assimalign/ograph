using System;
using System.Collections.Generic;
using System.Text;

namespace Assimalign.OGraph.EntityModel.Tests;

[PickType("PersonCreateInput", Properties = [""])]
public class Person //: Entity<Person>
{
    public Guid? Id { get; set; }
    public Name? Name { get; set; }
    public IEnumerable<PersonInterest>? Interests { get; set; }
}
