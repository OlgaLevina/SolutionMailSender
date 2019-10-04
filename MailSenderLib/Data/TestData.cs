using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MailSenderLib.Entities;
using System.Security;

namespace MailSenderLib.Data
{
    public static class TestData
    {
        public static List<Server> Servers { get; } = new List<Server>
        {
            new Server{Id=1, Name="Yandex", Host="smtp.yandex.ru",Password=new SecureString(),UserName="UserName" },
            new Server{Id=2, Name="Mail", Host="smtp.mail.ru",Password=new SecureString(),UserName="UserName" },
            new Server{Id=3, Name="Gmail", Host="smtp.gmail.com",Password=new SecureString(),UserName="UserName", Port=433 },
        };
        public static List<Sender> Senders { get; } = new List<Sender> {
            new Sender{ Id=1, Address="ivanov@yandex.ru",Name="Иванов"},
            new Sender{ Id=2, Address="petrov@mail.ru",Name="Петров"},
            new Sender{ Id=3, Address="sidorov@gmail.com",Name="Сидоров"},
        };

        public static List<Email> Letters { get; } = new List<Email> {
            new Email{ Id=1, Name="Рассылка1"},
            new Email{ Id=2, Name="Рассылка2"},
            new Email{ Id=3, Name="Рассылка3"},
        };

    }
}
