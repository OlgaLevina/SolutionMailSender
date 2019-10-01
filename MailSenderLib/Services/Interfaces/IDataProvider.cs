//using MailSenderLib.Data.LinqToSQL;
using MailSenderLib.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailSenderLib.Services.Interfaces
{
    public interface IDataProvider<T>  //интерфейс унифицируем, чтобы на его основе строить остальные. То же самое с классами провайдеров источников хранения данных (инМемори, линкту...  т.д.)
    {
        IEnumerable<T> GetAll(); //все ссылк меняем на ссылки на сущности вместо бд
        int Create(T item);
        void SaveChanges();
        void Edit(int id, T item);
        bool Remove(int id);
        T GetById(int id);

    }
}
