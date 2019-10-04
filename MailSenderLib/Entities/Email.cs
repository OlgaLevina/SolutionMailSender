using MailSenderLib.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailSenderLib.Entities
{
    public class Email:NamedEntity
    {
        //public int Id { get; set; }
        //public string Name { get; set; }
        public string Body { get; set; } = "Вы получили это письмо, потому что сами подписались на это!";
        public string Subject { get; set; } = "Geekbrains - рассылка";
        public string HTML { get; set; } = string.Empty;
        public bool IsBodyHTML { get; set; } = false;
        public string Attachment { get; set; } = string.Empty;
}
}
