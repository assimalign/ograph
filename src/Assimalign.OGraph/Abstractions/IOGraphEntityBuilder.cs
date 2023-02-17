using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph;

public interface IOGraphEntityBuilder
{
<<<<<<< Updated upstream:src/Assimalign.OGraph/Abstractions/IOGraphEntityBuilder.cs


    IOGraphEntity Build();
=======
    IOGraphEdge this[Name name] { get; set; }
    
>>>>>>> Stashed changes:src/Assimalign.OGraph/Abstractions/IOGraphEdgeCollection.cs
}
