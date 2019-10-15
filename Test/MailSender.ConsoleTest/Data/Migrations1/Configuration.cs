using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;


namespace MailSender.ConsoleTest.Data.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<SongsDB>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;//разрешает удалять данные из бд
            MigrationsDirectory = @"Data\Migrations";
        }
        protected override void Seed(SongsDB db)
        {
                db.Database.Log = msg => WriteLine($"EF: {msg}");
                for (int i = 0; i < 3; i++)
                {
                    var artist = new Artist { Name = $"Artist Name {i}", BirthDay = DateTime.Now.Subtract(TimeSpan.FromDays(365 * (i + 20))) };
                    artist.Trecks = new List<Treck>();
                    for (int j = 0; j < 3; j++)
                    {
                        var treck = new Treck { Name = $"Treck {i + j}", Length = j * 456 };
                        artist.Trecks.Add(treck);
                    }
                    db.Artists.Add(artist);
                }
                db.SaveChanges();
        }
    }
}
