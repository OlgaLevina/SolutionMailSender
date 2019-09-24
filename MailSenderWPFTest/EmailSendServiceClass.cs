using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Mail;
using System.Security;

namespace MailSenderWPFTest
{
    static class EmailSendServiceClass
    {
        public static void Send(KeyValuePair<string,int> host, PersonMail author,List<string> adresses, MessageContet msg,IResult resultMsg)
        {

            using (SmtpClient client = new SmtpClient(host.Key, host.Value))
            {
                client.EnableSsl = true;//подтверждаем шифрование
                client.Credentials = new NetworkCredential(author.Login, author.Password);//учетные данные
                //другие параметры из методички можно не включать
                using (MailMessage message = new MailMessage())
                {
                    message.From = new MailAddress(author.Email, author.Name);
                    message.To.Add(new MailAddress(author.Email, author.Name));
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
