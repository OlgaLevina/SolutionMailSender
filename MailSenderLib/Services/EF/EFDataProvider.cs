using MailSenderLib.Data.EF;
using System.Data.Entity;
using MailSenderLib.Services.Interfaces;
using System;
using System.Collections.Generic;
using MailSenderLib.Entities.Base;
using System.Linq;

namespace MailSenderLib.Services.EF
{
    public abstract class EFDataProvider<T> : IDataProvider<T> where T: BaseEntity
    {
        protected readonly DataContextProvider _db;
        protected EFDataProvider(DataContextProvider db) { _db = db; }
        public int Create(T item)
        {
            if (item is null) throw new ArgumentNullException(nameof(item));
            using (var db = _db.CreateContext())
            {
                if (db.Set<T>().Any(r => r.Id == item.Id)) return item.Id;
                db.Set<T>().Add(item); db.SaveChanges(); return item.Id;
            }
        }
        //public abstract void Edit(int id, T item);
        public virtual void Edit(int id, T item)
        {
            if (item is null) throw new ArgumentNullException(nameof(item));

        }
        public IEnumerable<T> GetAll(){ using (var db = _db.CreateContext()) return db.Set<T>().ToArray();}
        public T GetById(int id) { using (var db = _db.CreateContext()) return db.Set<T>().FirstOrDefault(r => r.Id == id); }

        public T GetBuId(int id, MailSenderDB db)
        {
            return db.Set<T>().FirstOrDefault(r => r.Id == id)??throw new InvalidOperationException($"id {id} not get from bd");
        }

        public bool Remove(int id)
        {
            using (var db = _db.CreateContext())
            {
                var db_item = GetById(id);
                if (db_item is null) return false;
                db.Set<T>().Remove(db_item); db.SaveChanges(); return true;
            }
        }
        public void SaveChanges() {  }
    }


    public abstract class EFDataProviderOneConnection<T> : IDataProvider<T> where T : BaseEntity
    {
        private readonly MailSenderDB _db;
        private readonly DbSet<T> _table;
        protected EFDataProviderOneConnection(MailSenderDB db) { _db = db; _table = db.Set<T>(); }
        public int Create(T item)
        {
            if (item is null) throw new ArgumentNullException(nameof(item));
            if (_table.Any(r => r.Id == item.Id)) return item.Id;
            _table.Add(item); SaveChanges(); return item.Id;
        }
        //public abstract void Edit(int id, T item);
        public virtual void Edit(int id, T item)
        {
            if (item is null) throw new ArgumentNullException(nameof(item));
        }
        public IEnumerable<T> GetAll() => _table.AsEnumerable();
        public T GetById(int id) => _table.FirstOrDefault(r => r.Id == id);
        public bool Remove(int id)
        {
            var db_item = GetById(id);
            if (db_item is null) return false;
            _table.Remove(db_item); SaveChanges(); return true;
        }
        public void SaveChanges() => _db.SaveChanges();
    }
}
