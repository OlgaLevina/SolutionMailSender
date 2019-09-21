using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security;

namespace MailSenderWPFTest
{
    class PersonMail
    {
        public  string Login;
        public  SecureString Password;//для хранения паролей, сторонние не смогут прочитать, но для передачи в бд не подойдет, нужно будет преобразовывать в строку
        public string Email;
        public string Name;
    }
}
