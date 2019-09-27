using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MailSenderLib.Data.LinqToSQL;

namespace MailSenderLib.Services
{
    public class RecipientsDataProvider
    {
        public readonly MailSenderDBDataContext _db;
        public RecipientsDataProvider(MailSenderDBDataContext db) { _db = db; }
        public IEnumerable<Recipient> GetAll()
        {
            _db.Refresh(System.Data.Linq.RefreshMode.OverwriteCurrentValues);//добавили,т.к. данный не обновлялись толком
            return _db.Recipient.ToArray();
            
        }

        public int Create (Recipient recipient) // для сохранения данных из линка в бд
        {
            if (recipient is null) throw new ArgumentNullException(nameof(recipient));


            _db.Recipient.InsertOnSubmit(recipient);
            SaveChanges();
            return recipient.Id;
        }

        public void SaveChanges()
        {
            _db.SubmitChanges();
        }
    }
}
