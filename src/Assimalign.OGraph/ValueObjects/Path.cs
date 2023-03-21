using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph;

public readonly struct Path
{
    public Path(string path)
    {
        if (string.IsNullOrEmpty(path))
        {
            throw new ArgumentNullException(nameof(path));
        }
        Segments = path.Trim('/').Split('/').Select(segment =>
        {
            return new PathSegment(segment);

        }).ToArray();
    }
    public PathSegment[] Segments { get; }
}
