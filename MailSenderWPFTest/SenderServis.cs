using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailSenderWPFTest
{
    public static class SenderServis
    {

        public static string Host = "smtp.mail.ru";//гугл не рекомендуется - расценит как врага, можно smtp.mail.ru smtp.yandex.ru
        public static int Port = 25;
    }
}
