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
        public DbSet<Recipient> Recipients { get; set; }
        public DbSet<Sender> Senders { get; set; }
        public DbSet<Email> Emails { get; set; }
        public DbSet<RecipientsList> RecipientsLists { get; set; }
        public DbSet<Server> Servers { get; set; }
        public DbSet<SchedularTask> SchedularTasks { get; set; }
    }
}
