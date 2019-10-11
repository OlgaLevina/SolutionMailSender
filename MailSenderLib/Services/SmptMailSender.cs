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

        public async Task SendAsync(Email Message, Sender From, Recipient To)
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
                    await client.SendMailAsync(message).ConfigureAwait(false);// конфигуратор - лучше делать везде, где нет связи с пользовательским интерфейсом 
                    //await Task.Run(() => client.Send(message)); //если нет асинхронных методов
                }
            }
        }

        public async Task SendParallelAsync(Email Message, Sender From, IEnumerable<Recipient> To)
        {
            await Task.WhenAll(To.Select(to => SendAsync(Message, From, To))).ConfigureAwait(false);
            //var send_task = new List<Task>();//это и все, что ниже - аналог верхней строчки
            //foreach (var recipient in To) send_task.Add(SendAsync(Message, From, recipient));
            //await Task.WhenAll(send_task).ConfigureAwait(false);
        }

        public async Task SendAsync(Email Message, Sender From, IEnumerable<Recipient> To)
        {
            foreach (var recipient in To) await SendAsync(Message, From, recipient).ConfigureAwait(false); //рассылаем последовательно, но асинхроно
        }

        public void SendParallel (Email Message, Sender From, IEnumerable<Recipient> To)
        {
            foreach (var recipient in To) ThreadPool.QueueUserWorkItem(_=> Send(Message, From, recipient));
        }
    }

}
