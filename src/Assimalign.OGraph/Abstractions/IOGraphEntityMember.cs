using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph;

<<<<<<< Updated upstream:src/Assimalign.OGraph/Abstractions/IOGraphEntityMember.cs
public interface IOGraphEntityMember
{
    /// <summary>
    /// The member name.
    /// </summary>
    string Name { get;  }
=======
public interface IOGraphTypeCollection : IList<IOGraphType>
{
    IOGraphType this[Name name] { get; set; }
>>>>>>> Stashed changes:src/Assimalign.OGraph/Abstractions/IOGraphTypeCollection.cs
}
