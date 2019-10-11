using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static System.Console;

namespace MailSender.ConsoleTest
{
    public class plinqTest
    {
        public static void Start()
        {
            //ThreadTests.Start();
            //SinchronizationTests.Start();
            //ThreadPoolTests.Start();
            //int number = 100;
            //HomeWork5.Dates dates = new HomeWork5.Dates { time = DateTime.Now };
            //WriteLine($"Факториал {HomeWork5.Factorial(number)} - время выполнения {HomeWork5.TimeCount(dates)} млс");
            //WriteLine($"Факториал {HomeWork5.Sum(number)} - время выполнения {HomeWork5.TimeCount(dates)} млс");
            //HomeWork5.FactorialSumStart(number, dates);
            //new Thread(() => WriteLine("Ассинхроный параллельный код")).Start();//создание и сразу же выполнение асинхронно потока
            //Action<string> printer = str => WriteLine(str);
            //printer("Hy");//неассинхронный вызов
            //printer.Invoke("Hy2");//неассинхронный вызов
            //printer.BeginInvoke("Hy3 - work accync",null, null);//ассинхронный вызов делегата
            //printer.BeginInvoke("Hy3 - work accync", result => WriteLine("completed"), null);//ассинхронный вызов делегата с обработкой завершения вызова

            //Parallel.For(0, 100, i => WriteLine(i));//современный подход асинъронного выполнения через библиотеку тпл
            var messages = Enumerable.Range(0, 100).Select(i => $"{i}").ToArray();

            //Parallel.For(0, messages.Length, new ParallelOptions {MaxDegreeOfParallelism=3}, i =>
            //{
            //    Thread.Sleep(250);
            //    WriteLine($"{Thread.CurrentThread.ManagedThreadId} {i}"); }
            //);//в опциях - сколько параллельных потоков 

            //Parallel.For(0, messages.Length, new ParallelOptions { MaxDegreeOfParallelism = 3 }, (i, state) =>//state - параметр - чтобы прервать
            //{
            //    if (messages[i].EndsWith("15")) state.Break(); //при этом стартовавшие параллельные операции будут доделаны
            //    Thread.Sleep(250);
            //    WriteLine($"{Thread.CurrentThread.ManagedThreadId} {i}");
            //}
            //);//в опциях - сколько параллельных потоков 

            //var for_result = Parallel.For(0, messages.Length, new ParallelOptions { MaxDegreeOfParallelism = 3 }, (i, state) =>//c сохранением результата в переменную. в качестве результата будут i-прерывания
            //{
            //    if (messages[i].EndsWith("15")) state.Break(); //при этом стартовавшие параллельные операции будут доделаны
            //    Thread.Sleep(250);
            //    WriteLine($"{Thread.CurrentThread.ManagedThreadId} {i}");
            //} );//в опциях - сколько параллельных потоков 
            //WriteLine(for_result.LowestBreakIteration);

            //Parallel.Invoke(
            //    ParallelInvokeMethod, ParallelInvokeMethod, ParallelInvokeMethod,
            //    () => WriteLine("!")
            //    ); // параллельный вызов нескольких операций через методы или лямбды

            Parallel.ForEach(messages, str => { msgFunk(str); });

            var msg_list = new List<int>(messages.Length);
            Parallel.ForEach(messages, str =>
            {
                var length = msgFunk(str);
                lock (msg_list) msg_list.Add(length);//возврат результата!
            });
            var total_lengt = msg_list.Sum();//linq
            total_lengt = messages.AsParallel().Select(msg => msgFunk(msg)).Sum();//то же, но сразу - через plinq
            total_lengt = messages.AsParallel()  // то же с опциями
                .WithDegreeOfParallelism(15)  // параллельно 15 потоков
                .WithExecutionMode(ParallelExecutionMode.ForceParallelism) //обязательно параллельно
                .Select(msg => msgFunk(msg))
                .AsSequential() //преобразовать параллельный запрос обратно в перечисление Ienumerable
                .Sum();//то же, но сразу - через plinq

            WriteLine(total_lengt);
            ReadLine();


        }

        private static void ParallelInvokeMethod()
        {
            WriteLine(Thread.CurrentThread.ManagedThreadId);
            Thread.Sleep(250);
            WriteLine(Thread.CurrentThread.ManagedThreadId);
        }

        private static int msgFunk(string msg)
        {
            Thread.Sleep(250); WriteLine($"{Thread.CurrentThread.ManagedThreadId}{msg}");
            return msg.Length;
        }
    }
}
