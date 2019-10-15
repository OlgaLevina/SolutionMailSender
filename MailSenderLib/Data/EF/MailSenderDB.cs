using MailSenderLib.Data.EF.Migrations;
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
        static MailSenderDB()//вместо апдейта через консоль диспетчера проектов
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<MailSenderDB, Configuration>());//лучше всего - через миграции - при каждом запуске приложения будет удаляться вся структура базы данных и создваться заново
        }
    }
}
