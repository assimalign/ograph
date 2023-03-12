using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph;

public readonly struct OGraphObject : IEnumerable<OGraphObjectItem>
{
    public OGraphObject(IEnumerable<OGraphObjectItem> items)
    {
        if (items is null)
        {
            throw new ArgumentNullException("items");
        }

        this.Items = items.ToArray();
    }

    public OGraphObjectItem[] Items { get; }

    public IEnumerator<OGraphObjectItem> GetEnumerator() => (IEnumerator<OGraphObjectItem>)this.Items.GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();
}
