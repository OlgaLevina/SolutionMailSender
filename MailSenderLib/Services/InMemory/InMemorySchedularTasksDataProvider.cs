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
    public class InMemorySchedularTasksDataProvider : InMemoryDataProvider<SchedularTask>, ISchedularTasksDataProvider
    {
        public override void Edit(int id, SchedularTask item)
        {
            var db_item=GetById(id);
            if (db_item is null) return;
            db_item.Server = item.Server;
            db_item.Time = item.Time;
            db_item.Sender = item.Sender;
            db_item.Recipients = item.Recipients;
            db_item.Email = item.Email;
        }

    }
}
