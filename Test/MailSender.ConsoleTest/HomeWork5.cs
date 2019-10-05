using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;
using System.Globalization;
using System.Threading;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.Remoting.Contexts;
using System.Collections.Concurrent;//потокобезопасные коллекции

namespace MailSender.ConsoleTest
{
    internal static class HomeWork5
    {
        public static Dates Begin { get; set; }

        public static void FactorialSumStart(int number,Dates dates)
        {
            //Thread factorial_thread = new Thread(() => Printer(dates.Change(Factorial(number).ToString())));
            //factorial_thread.IsBackground = true;
            Begin = dates;
            ThreadCreate(Factorial,number);
            ThreadCreate(Sum, number);
        }

        public static void ThreadCreate(Func<int,double> function,int number)
        {
            Thread thread = new Thread(() => Printer(new Dates { time = DateTime.Now }.Change(function(number).ToString())));
            thread.Name = function.Method.Name;
            thread.IsBackground = true;//чтобы правильно вывелось время на консоли
            thread.Start();
        }

        public static double Factorial(int number)
        {
            if (number - 1 != 0) return number * Factorial(number - 1);
            else return number;
        }

        public static double Sum(int number)
        {
            if (number - 1 != 0) return number + Sum(number - 1);
            else return number;
        }

        public static int TimeCount( Dates begin)
        {

            int result = (int)(DateTime.Now - begin.time).Milliseconds;
            begin.time = DateTime.Now;
            return result;
        }

        private static readonly object _SyncRoot = new object();//4 байта памяти - главной сделать на чтение - и используем для группировки кода с т.зр. закрытост для прерывания разными потоками

        public class Dates
        {
            public string message { get; set; } = "underfined";
            public DateTime time { get; set; }
            public Dates Change(string msg) { message = msg; return this; }
        }


        private static void Printer(object dates)
        {
            lock (_SyncRoot)
            {
                Dates Data = (Dates)dates;
                Write($"id {Thread.CurrentThread.ManagedThreadId}");
                Write($" name {Thread.CurrentThread.Name}: ");
                Write($" результат: \"{Data.message}\"");
                WriteLine($" (время выполнения - {HomeWork5.TimeCount( Data)} млс)");
                WriteLine($"Текущее общепоточное время выполнения - {(int)(DateTime.Now - Begin.time).Milliseconds} млс");
            }
            WriteLine($"Finished  id:{Thread.CurrentThread.ManagedThreadId}");
        }
    }
}
