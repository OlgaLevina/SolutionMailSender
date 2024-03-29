﻿//using MailSenderLib.Data.LinqToSQL;
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
    public class InMemorySchedularTasksDataProvider : ISchedularTasksDataProvider
    {

        private readonly MemoryDataContext _db;
        private readonly ICollection<SchedularTask> _items;
        public InMemorySchedularTasksDataProvider(MemoryDataContext db) { _db = db; _items = _db.SchedularTasks; }

        public int Create(SchedularTask item)
        {
            if (_items.Contains(item)) return item.Id;
            item.Id = _items.Count == 0 ? 1 : _items.Max(r => r.Id) + 1;
            _items.Add(item); return item.Id;
        }

        public void Edit(int id, SchedularTask item)
        {
            var db_item=GetById(id);
            if (db_item is null) return;
            db_item.Server = item.Server;
            db_item.Time = item.Time;
            db_item.Sender = item.Sender;
            db_item.Recipients = item.Recipients;
            db_item.Email = item.Email;
        }

        public IEnumerable<SchedularTask> GetAll() => _db.SchedularTasks;

        public SchedularTask GetById(int id) => GetAll().FirstOrDefault(e => e.Id == id);

        public bool Remove(int id) => _items.Remove(GetById(id));

        public void SaveChanges() { }
    }
}
