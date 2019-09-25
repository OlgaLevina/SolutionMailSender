using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailSenderLib.Entities
{
    public class Server
    {
        public int Id { get; set; }
        public string Address { get; set; }
        public string Name { get; set; }
        public int Port { get; set; } = 25;
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Description { get; set; }

        public Server() { }
        public Server (Server server)
        {
            Id = server.Id;Address = server.Address; Name = server.Name;Port = server.Port;
            UserName = server.UserName;Password = server.Password;Description = server.Description;
        }
    }
}
