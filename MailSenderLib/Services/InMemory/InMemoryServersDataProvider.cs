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
    public class InMemoryServersDataProvider : InMemoryDataProvider<Server>, IServersDataProvider
    {
        public override void Edit(int id, Server item)
        {
            var db_item=GetById(id);
            if (db_item is null) return;
            db_item.Name = item.Name;
            db_item.Host = item.Host;
            db_item.UserName = item.UserName;
            db_item.Password = item.Password;
            db_item.Port = item.Port;
            db_item.UserSSL = item.UserSSL;
        }

    }
}
