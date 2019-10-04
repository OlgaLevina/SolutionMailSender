using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security;
using MailSenderLib.Entities.Base;

namespace MailSenderLib.Entities
{
    public class Server : NamedEntity
    {
        //public int Id { get; set; }
        public string Host { get; set; }
        //public string Name { get; set; }
        public int Port { get; set; } = 25;
        public string UserName { get; set; }
        public SecureString Password { get; set; }
        public string Description { get; set; }
        public bool UserSSL { get; set; }

        public Server() { }
        public Server (Server server)
        {
            Id = server.Id;Host = server.Host; Name = server.Name;Port = server.Port;
            UserName = server.UserName;Password = server.Password;Description = server.Description;
        }
    }
}
