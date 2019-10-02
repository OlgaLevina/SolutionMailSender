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
    public class InMemoryRecipientsDataProvider : InMemoryDataProvider<Recipient>, IRecipientsDataProvider
    {
        public InMemoryRecipientsDataProvider()
        {
            _Items.AddRange(Enumerable.Range(1, 5).Select(i => new Recipient { Id = i, Name = $"Recipient{i}", Address = $"recipient{i}@server.com", Description = "descriptiion" }));
        }
        public override void Edit(int id, Recipient item)
        {
            var db_item = GetById(id);
            if (db_item is null) return;
            db_item.Name = item.Name;
            db_item.Address = item.Address;
            db_item.Description = item.Description;
        }
    }
}
