using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Execution
{
    public class OGraphOptions
    {
        /// <summary>
        /// Limit the data resolution of a query to a set number of Nodes.
        /// </summary>
        public int? MaxDegreeOfSeparation { get; set; }
    }
}
