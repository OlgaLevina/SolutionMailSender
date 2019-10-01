using MailSenderLib.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailSenderLib.Entities
{
    public class RecipientsList: NamedEntity
    {
        public ICollection<Recipient> Recipients { get; set; }
    }
}
