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
    public class InMemoryRecipientsListsDataProvider : InMemoryDataProvider<RecipientsList>, IRecipientsListsDataProvider
    {
        public override void Edit(int id, RecipientsList item)
        {
            var db_item=GetById(id);
            if (db_item is null) return;
            db_item.Name = item.Name;
            db_item.Recipients = item.Recipients;

        }

    }
}
