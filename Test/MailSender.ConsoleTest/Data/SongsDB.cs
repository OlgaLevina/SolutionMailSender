using MailSender.ConsoleTest.Data.Migrations;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailSender.ConsoleTest.Data
{
    public class SongsDB : DbContext
    {
        
        static SongsDB()//вместо апдейта через консоль диспетчера проектов
        {
           // Database.SetInitializer(new DropCreateDatabaseAlways<SongsDB>());//отладочный инициализатор - при каждом запуске приложения будет удаляться вся структура базы данных и создваться заново
           // Database.SetInitializer(new CreateDatabaseIfNotExists<SongsDB>());//создает бд, если отсутствует - при каждом запуске приложения будет удаляться вся структура базы данных и создваться заново
           // Database.SetInitializer(new DropCreateDatabaseIfModelChanges<SongsDB>());//пересоздает при изменениях - при каждом запуске приложения будет удаляться вся структура базы данных и создваться заново
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<SongsDB,Configuration>());//лучше всего - через миграции - при каждом запуске приложения будет удаляться вся структура базы данных и создваться заново
        }
        public SongsDB(string ConnectionString) : base(ConnectionString) { }
        public SongsDB() : this("name=SongsDB") { }
        public DbSet<Artist> Artists{get;set;}
        public DbSet<Treck> Trecks { get; set; }
    }
}
