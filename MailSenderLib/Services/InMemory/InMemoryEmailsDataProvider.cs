//using MailSenderLib.Data.LinqToSQL;
using MailSenderLib.Entities;
using MailSenderLib.Services.InMemory;
using MailSenderLib.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailSenderLib.Services
{
    public class InMemoryEmailsDataProvider : IEmailsDataProvider
    {
        private readonly MemoryDataContext _db;
        private readonly ICollection<Email> _items;

        public InMemoryEmailsDataProvider(MemoryDataContext db){ _db = db; _items = _db.Emails; }

        public int Create(Email item)
        {
            if(_items.Contains(item)) return item.Id;
            item.Id = _items.Count == 0 ? 1 : _items.Max(r => r.Id) + 1;
            _items.Add(item); return item.Id;
        }

        public void Edit(int id, Email item)
        {
            var db_item=GetById(id);
            if (db_item is null) return;
            db_item.Subject = item.Subject;
            db_item.Body = item.Body;

        }

        public IEnumerable<Email> GetAll() => _db.Emails;

        public Email GetById(int id) => GetAll().FirstOrDefault(e => e.Id == id);

        public bool Remove(int id) => _items.Remove(GetById(id));

        public void SaveChanges() {}
    }
}
