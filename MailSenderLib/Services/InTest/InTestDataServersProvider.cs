using MailSenderLib.Data;
using MailSenderLib.Entities;
using MailSenderLib.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailSenderLib.Services.InTest
{
    class InTestDataServersProvider : IServersDataProvider
    {
        private static List<Server> servers=TestData.Servers;

        public int Create(Server server)
        {
            if (server is null) throw new ArgumentNullException(nameof(server));
            servers.Add(server);
            return server.Id;
        }

        public IEnumerable<Server> GetAll()
        {
            return servers;
        }

        public void SaveChanges()
        {
            //!!!! добавить;
        }
    }
}
