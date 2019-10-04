//using MailSenderLib.Data.LinqToSQL;
using MailSenderLib.Entities;
using MailSenderLib.Entities.Base;
using MailSenderLib.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailSenderLib.Services
{
    public abstract class InMemoryDataProvider<T> : IDataProvider<T> where  T : BaseEntity
    {
        protected readonly List<T> _Items = new List<T>();//чтобы могли заполнить источники текстовыми данными
        public int Create(T item)
        {
            if (_Items.Contains(item)) return item.Id;
            item.Id = _Items.Count == 0 ? 1 : _Items.Max(r => r.Id) + 1;
            _Items.Add(item);
            return item.Id;
        }

        public abstract void Edit(int id, T item);

        public IEnumerable<T> GetAll() => _Items;

        public T GetById(int id) => _Items.FirstOrDefault(r => r.Id == id);

        public bool Remove(int id)
        {
            var db_item=GetById(id);
            return _Items.Remove(db_item);
        }

        public void SaveChanges()
        {
        }

    }
}
