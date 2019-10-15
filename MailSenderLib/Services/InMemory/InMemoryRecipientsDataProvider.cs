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
    public class InMemoryRecipientsDataProvider : IRecipientsDataProvider
    {
        private readonly MemoryDataContext _db;
        private readonly ICollection<Recipient> _items;

        public InMemoryRecipientsDataProvider(MemoryDataContext db) { _db = db; _items = _db.Recipients; }

        public int Create(Recipient item)
        {
            if (_items.Contains(item)) return item.Id;
            item.Id = _items.Count == 0 ? 1 : _items.Max(r => r.Id) + 1;
            _items.Add(item); return item.Id;
        }

        public  void Edit(int id, Recipient item)
        {
            var db_item = GetById(id);
            if (db_item is null) return;
            db_item.Name = item.Name;
            db_item.Address = item.Address;
            db_item.Description = item.Description;
        }

        public IEnumerable<Recipient> GetAll() => _db.Recipients;

        public Recipient GetById(int id) => GetAll().FirstOrDefault(e => e.Id == id);

        public bool Remove(int id) => _items.Remove(GetById(id));

        public void SaveChanges() { }
    }
}
