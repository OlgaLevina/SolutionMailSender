using MailSenderLib.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailSenderLib.Services.Interfaces
{
    interface IServersDataProvider
    {
        IEnumerable<Server> GetAll();
        int Create(Server server);
        void SaveChanges();
    }
}
