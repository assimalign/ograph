using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Assimalign.OGraph.AspNetCore.Controllers;

internal abstract class OGraphController : ControllerBase
{

    public abstract object CreateNode();


    public override AcceptedResult Accepted()
    {
        return base.Accepted();
    }
}
