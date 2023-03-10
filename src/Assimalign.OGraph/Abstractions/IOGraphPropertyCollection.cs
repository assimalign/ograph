using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph;

public interface IOGraphPropertyCollection : IEnumerable<IOGraphProperty>
{

    void Add(IOGraphProperty property);

    void Remove(IOGraphProperty property);

    IOGraphProperty Find(Name name);
}
