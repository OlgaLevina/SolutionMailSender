using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Mail;
using System.Security;
using MailSenderLib.Entities;

namespace MailSender
{
    class EmailSendServiceClass
    {

        public void Send(Server author,List<string> addresses, Letter msg, IResult resultMsg)
        {

            using (SmtpClient client = new SmtpClient(author.Address, author.Port))
            {
                client.EnableSsl = true;
                client.Credentials = new NetworkCredential(author.UserName, author.Password);
                foreach(string address in addresses)
                {
                    using (MailMessage message = new MailMessage())
                    {
                        message.From = new MailAddress(author.UserName, author.Name);
                        message.To.Add(address);
                        message.Subject = msg.Subject;
                        message.Body = msg.Body;
                        message.IsBodyHtml = msg.IsBodyHTML;
                        // message.Attachments.Add(new Attachment(file))
                        try { client.Send(message); resultMsg.ShowResult("Was sent!"); }
                        catch (Exception error) { resultMsg.ShowResult(error.Message); }
                    }
                }
            }
        }
    }
}
