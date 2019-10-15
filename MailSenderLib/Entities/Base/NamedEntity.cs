using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailSenderLib.Entities.Base
{
    public abstract class NamedEntity : BaseEntity
    {
        [Required]
        public virtual string Name { get; set; }
    }
}
