using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml;

namespace Assimalign.OGraph.Gdm.Tests.TestObject.Types
{
    internal class EmployeeIdType : GdmPrimitiveType<EmployeeId>
    {
        public override Label Label => "EmployeeId";
        public override EmployeeId Read(ref Utf8JsonReader reader)
        {
            throw new NotImplementedException();
        }

        public override EmployeeId Read(XmlReader reader)
        {
            throw new NotImplementedException();
        }

        public override void Write(Utf8JsonWriter writer, EmployeeId value)
        {
            throw new NotImplementedException();
        }

        public override void Write(XmlWriter writer, EmployeeId value)
        {
            throw new NotImplementedException();
        }
    }
}
