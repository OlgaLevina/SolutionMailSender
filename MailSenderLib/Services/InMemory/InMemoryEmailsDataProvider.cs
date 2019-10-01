//using MailSenderLib.Data.LinqToSQL;
using MailSenderLib.Entities;
using MailSenderLib.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailSenderLib.Services
{
    public class InMemoryEmailsDataProvider : InMemoryDataProvider<Email>, IEmailsDataProvider
    {
        public InMemoryEmailsDataProvider()
        {
            _Items.AddRange(Enumerable.Range(1,20).Select(i => new Email { Id=i, Subject=$"Message {i}",Body=$"Text {i}"}))
        }

        public override void Edit(int id, Email item)
        {
            var db_item=GetById(id);
            if (db_item is null) return;
            db_item.Subject = item.Subject;
            db_item.Body = item.Body;

        }

    }
}
