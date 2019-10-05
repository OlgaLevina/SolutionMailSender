using MailSenderLib.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MailSenderLib.Services
{
    public class SmptMailSender
    {
        private readonly string host;
        private readonly int port;
        private readonly bool userSSL;
        private readonly string login;
        private readonly string password;

        public SmptMailSender(string Host, int Port, bool UserSSL,string Login,string Password)
        {
            host = Host;port = Port;userSSL = UserSSL;login = Login;password = Password;
        }

        public void Send(Email Message, Sender From, Recipient To)
        {
            using (var client = new SmtpClient(host, port) { EnableSsl = userSSL })
            {
                client.Credentials = new NetworkCredential
                {
                    UserName = login,
                    Password = password
                };

                using (var message = new MailMessage())
                {
                    message.From = new MailAddress(From.Address, From.Name);
                    message.To.Add(new MailAddress(To.Address, To.Name));
                    message.Subject = Message.Subject;
                    message.Body = Message.Body;
                    client.Send(message);
                }
            }
        }

        public void Send(Email Message, Sender From, IEnumerable<Recipient> To)
        {
            foreach (var recipient in To) Send(Message, From, recipient);
        }
        public void SendParallel (Email Message, Sender From, IEnumerable<Recipient> To)
        {
            foreach (var recipient in To) ThreadPool.QueueUserWorkItem(_=> Send(Message, From, recipient));
        }
    }

}
