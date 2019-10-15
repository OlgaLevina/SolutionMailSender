namespace MailSender.ConsoleTest.Data.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<MailSender.ConsoleTest.Data.SongsDB>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true; //чтобы можно было удалять данные при миграциях (столбцы)
            MigrationsDirectory = @"Data\Migrations";
        }

        protected override void Seed(MailSender.ConsoleTest.Data.SongsDB db)
        {
            if (db.Trecks.Any()) return;//чтобы при наличии данных не запускался
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
            //db.SaveChanges();
        }
    }
}
