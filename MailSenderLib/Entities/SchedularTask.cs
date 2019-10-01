using MailSenderLib.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailSenderLib.Entities
{
    public class SchedularTask: BaseEntity
    {
        public DateTime Time  { get; set; } 
        public Server Server { get; set; }
        public Sender Sender { get; set; }
        public RecipientsList Recipients { get; set; }
        public Email Email { get; set; }
    }
}
