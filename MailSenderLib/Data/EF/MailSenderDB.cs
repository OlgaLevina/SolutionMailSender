using MailSenderLib.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailSenderLib.Data.EF
{
    public class MailSenderDB:DbContext
    {
        public MailSenderDB(string ConnectionString) : base(ConnectionString) { }
        public MailSenderDB() : this("name=MailSenderDB") { }
        public DbSet<Recipient> Artists { get; set; }
        public DbSet<Sender> Trecks { get; set; }
    }
}
