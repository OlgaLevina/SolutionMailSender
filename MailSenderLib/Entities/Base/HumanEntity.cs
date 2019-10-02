using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailSenderLib.Entities.Base
{
    public abstract class HumanEntity : NamedEntity
    {
        public virtual string Address { get; set; }
    }
}
