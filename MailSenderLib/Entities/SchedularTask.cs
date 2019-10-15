using MailSenderLib.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailSenderLib.Entities
{
    public class SchedularTask: BaseEntity
    {
        public DateTime Time  { get; set; }
        [Required]
        public Server Server { get; set; }
        [Required]
        public Sender Sender { get; set; }
        [Required]
        public RecipientsList Recipients { get; set; }
        [Required]
        public Email Email { get; set; }
    }
}
