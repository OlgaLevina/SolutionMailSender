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
    public class InMemorySendersDataProvider : InMemoryDataProvider<Sender>, ISendersDataProvider
    {
        public InMemorySendersDataProvider()
        {
            _Items.AddRange(Enumerable.Range(1, 20).Select(i => new Sender { Id = i, Name = $"Sender {i}", Address = $"sender{i}@server.com" }));
        }
        public override void Edit(int id, Sender item)
        {
            var db_item=GetById(id);
            if (db_item is null) return;
            db_item.Name = item.Name;
            db_item.Address = item.Address;

        }

    }
}
