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
    public class InMemoryServersDataProvider : IServersDataProvider
    {
        private readonly MemoryDataContext _db;
        private readonly ICollection<Server> _items;

        public InMemoryServersDataProvider(MemoryDataContext db) { _db = db; _items = _db.Servers; }
        public int Create(Server item)
        {
            if (_items.Contains(item)) return item.Id;
            item.Id = _items.Count == 0 ? 1 : _items.Max(r => r.Id) + 1;
            _items.Add(item); return item.Id;
        }

        public void Edit(int id, Server item)
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

        public IEnumerable<Server> GetAll() => _db.Servers;

        public Server GetById(int id) => GetAll().FirstOrDefault(e => e.Id == id);

        public bool Remove(int id) => _items.Remove(GetById(id));

        public void SaveChanges() { }
    }
}
