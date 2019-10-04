using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Mail;
using System.Security;
using MailSenderLib.Entities;
using MailSender.Controls;
//using MailSenderLib.Data.LinqToSQL;

namespace MailSender
{
    class EmailSendServiceClass
    {

        public Server Author { get; set; }
        public Email Msg { get; set; }
        public IResult ResultMsg { get; set; }

        public EmailSendServiceClass(Server author, MailSenderLib.Entities.Email msg, IResult resultMsg)
        { Author = author;Msg = msg;ResultMsg = resultMsg; }

        public void Send<T>(IEnumerable<T> addresses)
        {

            using (SmtpClient client = new SmtpClient(Author.Host, Author.Port))
            {
                client.EnableSsl = true;
                client.Credentials = new NetworkCredential(Author.UserName, Author.Password);
                foreach(T address in addresses)
                {
                    using (System.Net.Mail.MailMessage message = new System.Net.Mail.MailMessage())
                    {
                        message.From = new MailAddress(Author.UserName, Author.Name);
                        message.To.Add(address as string);
                        message.Subject = Msg.Subject;
                        message.Body = Msg.Body;
                        message.IsBodyHtml = Msg.IsBodyHTML;
                        // message.Attachments.Add(new Attachment(file))
                        try { client.Send(message); ResultMsg.Show("Was sent!"); }
                        catch (Exception error) { ResultMsg.Show(error.Message); }
                    }
                }
            }
        }

        public void Send(IQueryable<Recipient> emails)
        {
            var addresses = emails.Select(s => new { s.Address }).ToList();
            Send(addresses);
        }
    }
}
