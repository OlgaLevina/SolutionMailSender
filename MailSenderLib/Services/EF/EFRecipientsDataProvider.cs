using MailSenderLib.Data.EF;
using MailSenderLib.Entities;
using MailSenderLib.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailSenderLib.Services.EF
{
    public class EFRecipientsDataProvider : EFDataProvider<Recipient>, IRecipientsDataProvider
    {
        //private readonly MailSenderDB _db;
        //private readonly ICollection<Recipient> _items; //DBSet
        //public EFRecipientsDataProvider(MailSenderDB db) { _db = db; _items = GetAll().ToList(); }


        //public EFRecipientsDataProvider(MailSenderDB db) : base(db) { }
        public EFRecipientsDataProvider(DataContextProvider db):base(db) { }
        //public int Create(Recipient item)
        //{
        //    if (item is null) throw new ArgumentNullException(nameof(item));
        //    //var _items = _db.Recipients;
        //    if (_items.Any(r=> r.Id==item.Id)) return item.Id;
        //    _items.Add(item); SaveChanges(); return item.Id;
        //}



        //public override void Edit(int id, Recipient item)
        //{
        //    base.Edit(id, item);
        //    GetById(id).Name = item.Name;
        //    GetById(id).Address = item.Address;
        //    SaveChanges(); 
        //}
        public override void Edit(int id, Recipient item)
        {
            base.Edit(id, item);
            using (var db = _db.CreateContext())
            {
                GetBuId(id, db).Name = item.Name;
                GetBuId(id, db).Address = item.Address;
                db.SaveChanges();
            }
        }

        //public IEnumerable<Recipient> GetAll() => _db.Recipients.AsEnumerable();
        //public Recipient GetById(int id) => _db.Recipients.FirstOrDefault(r=> r.Id==id);
        //public bool Remove(int id)
        //{
        //    var db_item = GetById(id);
        //    if (db_item is null) return false;
        //    _items.Remove(db_item); SaveChanges(); return true;
        //}
        //public void SaveChanges() => _db.SaveChanges();
    }

    public class EFSendersDataProvider : EFDataProvider<Sender>, ISendersDataProvider
    {
        public EFSendersDataProvider(DataContextProvider db) : base(db) { }
        public override void Edit(int id, Sender item)
        {
            base.Edit(id, item);
            using (var db = _db.CreateContext())
            {
                GetBuId(id, db).Name = item.Name;
                GetBuId(id, db).Address = item.Address;
                db.SaveChanges();
            }
        }
    }
    public class EFServersDataProvider : EFDataProvider<Server>, IServersDataProvider
    {
        public EFServersDataProvider(DataContextProvider db) : base(db) { }
        public override void Edit(int id, Server item)
        {
            base.Edit(id, item);
            using (var db = _db.CreateContext())
            {
                GetBuId(id, db).Name = item.Name;
                GetBuId(id, db).Host = item.Host;
                GetBuId(id, db).UserName = item.UserName;
                GetBuId(id, db).Password = item.Password;
                GetBuId(id, db).Port = item.Port;
                GetBuId(id, db).UserSSL = item.UserSSL;
                db.SaveChanges();
            }
        }
    }
    public class EFEmailsDataProvider : EFDataProvider<Email>, IEmailsDataProvider
    {
        public EFEmailsDataProvider(DataContextProvider db) : base(db) { }
        public override void Edit(int id, Email item)
        {
            base.Edit(id, item);
            using (var db = _db.CreateContext())
            {
                GetBuId(id,db).Subject = item.Subject;
                GetBuId(id, db).Body = item.Body;
                db.SaveChanges();
            }
        }
    }
    public class EFRecipientsListsDataProvider : EFDataProvider<RecipientsList>, IRecipientsListsDataProvider
    {
        public EFRecipientsListsDataProvider(DataContextProvider db) : base(db) { }
        public override void Edit(int id, RecipientsList item)
        {
            base.Edit(id, item);
            using (var db = _db.CreateContext())
            {
                //var db_item = GetById(id);//тонкий момент - т.к конекшин открылось и закрылось и объект функции не попадает в основное соединение
                //db.RecipientsLists.Attach(db_item);//присоединяем даные из внутреннего контекста к внешнему

                //var db_item= db.Set<RecipientsList>().FirstOrDefault(r => r.Id == id);//либо выходим на объект напрямую

                // либо попробовать мой вариант, с расширение метода поиска по индексу - вроде должен работать
                //if (!db.RecipientsLists.Any(r => r.Id == id)) return;
                GetBuId(id,db).Name = item.Name;
                GetBuId(id, db).Recipients = item.Recipients;
                db.SaveChanges();
            }
        }
    }
    public class EFSchedularTasksDataProvider : EFDataProvider<SchedularTask>, ISchedularTasksDataProvider
    {
        public EFSchedularTasksDataProvider(DataContextProvider db) : base(db) { }
        public override void Edit(int id, SchedularTask item)
        {
            base.Edit(id, item);
            using (var db = _db.CreateContext())
            {
                GetBuId(id, db).Server = item.Server;
                GetBuId(id, db).Time = item.Time;
                GetBuId(id, db).Sender = item.Sender;
                GetBuId(id, db).Recipients = item.Recipients;
                GetBuId(id, db).Email = item.Email;
                db.SaveChanges();
            }
        }
    }
}
