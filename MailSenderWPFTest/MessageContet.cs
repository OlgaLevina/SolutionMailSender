using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailSenderWPFTest
{
    public class MessageContet
    {
        public string Body="Вы получили это письмо, потому что сами подписались на это!";
        public string Subject = "Geekbrains - рассылка";
        public string HTML = string.Empty;
        public bool IsBodyHTML=false;
        public string Attachment = string.Empty;
    }
}
