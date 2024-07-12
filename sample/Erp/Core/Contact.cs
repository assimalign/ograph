using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp;

public class Contact : DomainEntity<Contact, ContactId>
{
    public override Domain Domain => Domain.Contacts;
    public override DomainEntityType EntityType => DomainEntityType.Contact;
}
