using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using static System.Console;
using System.Globalization;
using MailSender.ConsoleTest.Data;
using System.Data.Entity;
using DocumentFormat.OpenXml.Wordprocessing;
using MailSender.ConsoleTest.Documents;
using System.ComponentModel;

namespace MailSender.ConsoleTest
{
    public class Program
    {
        [Description("Entry point!")]
        static void Main(string[] args)
        {


            for (int i = 0; i < 10; i++)
            {
                WriteLine(i);
            }
            ReadLine();


            //var report = new Report {Data1="Test", TimeValue=DateTime.Now.Subtract(TimeSpan.FromDays(365*17)) };
            //const string report_file = "TestReport.docx";
            //report.CreatePackage(report_file);



            // using (var db = new SongsDB()) { db.Database.Log = msg => WriteLine($"EF: {msg}");  }
            //using (var db = new SongsDB())
            //{
            //    //db.Configuration.AutoDetectChangesEnabled = false;//отключить автообнаружение изменений для быстродействия
            //    db.Database.Log = msg => WriteLine($"EF: {msg}");
            //    var bad_artists = db.Artists.Where(a => a.Name.EndsWith("Bad")).Include(a => a.Trecks);//include -  для выгрузки не только данных по артистам, но и по трекам
            //    foreach (var bad_artist in bad_artists)//.Include(a => a.Trecks); - может быть и здесь
            //    {
            //        bad_artist.Name = $"{bad_artist.Name} Bad";
            //        for (int i = 0; i < 10; i++) bad_artist.Trecks.Add(new Treck
            //        {
            //            Name = $"Bad track {i+1}"
            //        });
            //    }
            //    ReadLine();
            //    Clear();
            //    //db.ChangeTracker.DetectChanges();//включение обнаружения изменений перед отправкой в бд - иначе изменения не появяться в бд
            //    db.SaveChanges();

            //}
            //using (var db = new SongsDB())
            //{
            //    db.Database.Log = msg => WriteLine($"EF: {msg}");
            //    var trecks_count = db.Trecks.Count();
            //    WriteLine($"There are {trecks_count} songs in db.");
            //    var long_trecks = db.Trecks.Where(treck => treck.Length > 500);
            //    var long_trecks_count = long_trecks.Count();
            //    WriteLine($"long treck count {long_trecks_count}");
            //    foreach (var info in long_trecks.Select(t => new
            //    {
            //        ArtistName = t.Name,
            //        t.Artist.BirthDay
            //    }))
            //    {
            //        WriteLine($"Artist: {info.ArtistName}, date: {info.BirthDay}");
            //    }
            //}
            //WriteLine();
            //ReadLine();
            //Clear();
            //using (var db = new SongsDB())
            //{
            //    db.Database.Log = msg => WriteLine($"EF: {msg}");
            //    for (int i = 0; i < 3; i++)
            //    {
            //        var artist=new Artist {Name=$"Artist Name {i}", BirthDay=DateTime.Now.Subtract(TimeSpan.FromDays(365*(i+20)))};
            //        artist.Trecks = new List<Treck>();
            //        for (int j = 0; j < 3; j++)
            //        {
            //            var treck = new Treck { Name = $"Treck {i + j}", Length = j * 456 };
            //            artist.Trecks.Add(treck);
            //        }
            //        db.Artists.Add(artist);
            //    }
            //    db.SaveChanges();
            //}
            //ReadLine();


        }


    }


}
