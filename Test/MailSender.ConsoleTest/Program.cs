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
using System.IO;
using System.Linq.Expressions;
using System.Reflection;
using System.Reflection.Emit;
//using DocumentFormat.OpenXml.Drawing;

namespace MailSender.ConsoleTest
{
    public class Program
    {
        [Description("Entry point!")]
        static void Main(string[] args)
        {


            //for (int i = 0; i < 10; i++)
            //{
            //    WriteLine(i);
            //}
            //ReadLine();
            //var prog=new Program();
            //Type t = typeof(Program);
            //Type t2 = prog.GetType();
            //MemberInfo:
            //MethodInfo m = t2.GetMethod("Mane");
            //ParameterInfo p = m.GetParameters().FirstOrDefault();
            //ConstructorInfo c = t2.GetConstructor(---);
            //PropertyInfo;
            //EventInfo;
            //FieldInfo;
            const string lib_file_name = "TestLib.dll";//C:\Users\Olga\Dropbox\GeekBrains\CS3\SolutionMailSender\Test\MailSender.ConsoleTest\TestLib.dll
            var lib_file_path = Path.GetFullPath(lib_file_name);

            var lib = Assembly.LoadFile(lib_file_path);//если не установить копирование в выходной каталог - выдаст ошибку!!

            //foreach (var type in lib.DefinedTypes) WriteLine(type.FullName);
            var printer_type = lib.GetType("TestLib.Printer");
            //foreach (var method in printer_type.GetMethods())
            //{
            //    WriteLine($"{method.ReturnType.Name} {method.Name} {string.Join(",",method.GetParameters().Select(p=>$"{p.ParameterType.Name} {p}"))}");

            //}

            var printer_ctor = printer_type.GetConstructor(new[] {typeof(string)});//если не сущесвует - вернут нул
            object printer_obj = printer_ctor.Invoke(new object[] {"PrinterPrefix"});
            var print_method = printer_type.GetMethod("Print");
            print_method.Invoke(printer_obj, new object[] {"Data to print"});
            var prefix_field = printer_type.GetField("_Prefix", BindingFlags.NonPublic | BindingFlags.Instance);//без бандбингов не будет читаться приватное поле
            string field_value = (string) prefix_field.GetValue((printer_obj)); //получение значения поля
            field_value += $"{DateTime.Now}";
            prefix_field.SetValue(printer_obj, field_value);//сохранение значения в поле
            var get_prefix_metod =
                printer_type.GetMethod(("GetPrefixLength"), BindingFlags.NonPublic | BindingFlags.Instance);//без бандбингов не достучаться
            var resul = (int) get_prefix_metod.Invoke(printer_obj, new object[]{42,"Hy!"});
            WriteLine(resul);

            dynamic printer = printer_obj; //работает только для публичных методов
            dynamic printer2 = Activator.CreateInstance(printer_type,new object[] {"Hy-ye"}, null);//упрощенное создание объекта
            printer.Print("Hy=hy!"); printer2.Print("!");

            var s1 = Sum("qwe", "123"); WriteLine(s1);
            WriteLine(Sum(42,52)); WriteLine(Sum(42,52.11224)); WriteLine(Sum(42,"Hy"));
            //var s2 = Sum(42, new List<int>());//выдаст ошибку из-за списка, тк. ищет у данных наличие операции +, соответственно

            var data=new object[]{"hy",42,32.456465767546546544,true};
            ProcessData(data);
            ProcessDataDLR(data);

            Expression<Func<string, int>> expression = str => str.Length;//дерево выражений
            Expression<Func<int, int, int>> sum = (x, y) => x + y;
            var function = sum.Compile();//перестройка выражения в обычную функцию
            WriteLine(function(2, 5));

            var parameter = Expression.Parameter(typeof(string), "Message");
            var body = Expression.Call(
                Expression.Constant(printer_obj),
                print_method, parameter);
            var print_expr=Expression.Lambda<Action<string>>(body,parameter);
            var print_action = print_expr.Compile();
            print_action("123");

            var list = new List<int>();
            var property_name = MemberName(list, l => l.Count); WriteLine(property_name);//получение имени метода Count

            ReadLine();
            //var report = new Report { Data1 = "Test", TimeValue = DateTime.Now.Subtract(TimeSpan.FromDays(365 * 17)) };
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

        private static dynamic Sum(dynamic X, dynamic Y)
        {
            return X + Y;
        }

        private static void ProcessDataDLR(object[] data)
        {
            foreach (var value in data)
            {
                dynamic v = value; Process(v);
            }
        }

        private static void ProcessData(object[] data)
        {
            foreach (var value in data)
                switch (value)
                {
                    case string v:Process(v);break;
                    case int v: Process(v); break;
                    case double v: Process(v); break;
                    case bool v: Process(v); break;
                }
        }
        private static void Process(string value) => WriteLine($"{value}");
        private static void Process(int value) => WriteLine($"{value}");
        private static void Process(double value) => WriteLine($"{value}");
        private static void Process(bool value) => WriteLine($"{value}");

        public static string MemberName<T, Q>(T item, Expression<Func<T, Q>> Expr)
        {
            var body = Expr.Body;
            switch ( body)
            {
                case MemberExpression member: return member.Member.Name;
            }
            return body.ToString();
        }
    }


}
