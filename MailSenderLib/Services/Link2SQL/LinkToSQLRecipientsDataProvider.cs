using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MailSenderLib.Entities;
//using MailSenderLib.Data.LinqToSQL;
using MailSenderLib.Services.Interfaces;

namespace MailSenderLib.Services
{
    public class LinkToSQLRecipientsDataProvider : IRecipientsDataProvider
    {
        public readonly Data.LinqToSQL.MailSenderDBDataContext _db;
        public LinkToSQLRecipientsDataProvider(Data.LinqToSQL.MailSenderDBDataContext db) { _db = db; }
        public IEnumerable<Recipient> GetAll()
        {
            _db.Refresh(System.Data.Linq.RefreshMode.OverwriteCurrentValues);//добавили,т.к. данный не обновлялись толком
            return _db.Recipient.ToArray().Select(r => new Recipient
            {
                Id = r.Id, Name = r.Name, Address = r.Address
            });
            
        }

        public int Create (Recipient item) // для сохранения данных из линка в бд
        {
            if (item is null) throw new ArgumentNullException(nameof(item));
            if (item.Id != 0) return item.Id;

            //_db.Recipient.InsertOnSubmit(recipient);
            var entity = new Data.LinqToSQL.Recipient
            {
                Name = item.Name,
                Address = item.Address
            };
            _db.Recipient.InsertOnSubmit(entity);
            SaveChanges();
            return entity.Id;
        }

        public void SaveChanges() => _db.SubmitChanges();

        public void Edit(int id, Recipient item)
        {
            var db_item = _db.Recipient.FirstOrDefault(r => r.Id == id); // метод линкту
            if (db_item is null) return;
            db_item.Name = item.Name; db_item.Address = item.Address;
            SaveChanges();
        }

        public bool Remove(int id)
        {
            var db_item = _db.Recipient.FirstOrDefault(r => r.Id == id); // метод линкту
            if (db_item is null) return false;
            _db.Recipient.DeleteOnSubmit(db_item);
            SaveChanges();
            return true;
        }

        public Recipient GetById(int id)
        {
            var db_item = _db.Recipient.FirstOrDefault(r=> r.Id==id); // метод линкту
            return db_item is null ? null : new Recipient { Id = db_item.Id, Name = db_item.Name, Address = db_item.Address }; 
        }
    }
}
