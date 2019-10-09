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
        /// <summary>
        /// отсечка времени перед запуском потоков
        /// </summary>
        public static Dates Begin { get; set; }
        /// <summary>
        /// запуск потоков факториала и суммы
        /// </summary>
        /// <param name="number"></param>
        /// <param name="dates"></param>
        public static void FactorialSumStart(int number,Dates dates)
        {
            //Thread factorial_thread = new Thread(() => Printer(dates.Change(Factorial(number).ToString())));
            //factorial_thread.IsBackground = true;
            Begin = dates;
            ThreadCreate(Factorial,number);
            ThreadCreate(Sum, number);
        }
        /// <summary>
        /// создание потока
        /// </summary>
        /// <param name="function">обрабатываемая потоком фукнция</param>
        /// <param name="number"></param>
        public static void ThreadCreate(Func<int,double> function,int number)
        {
            Thread thread = new Thread(() => Printer(new Dates { time = DateTime.Now }.Change(function(number).ToString())));
            thread.Name = function.Method.Name;
            thread.IsBackground = true;//чтобы правильно вывелось время на консоли
            thread.Start();
        }
        /// <summary>
        /// рекурсия факториала
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public static double Factorial(int number)
        {
            if (number - 1 != 0) return number * Factorial(number - 1);
            else return number;
        }
        /// <summary>
        /// рукурсия суммы
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public static double Sum(int number)
        {
            if (number - 1 != 0) return number + Sum(number - 1);
            else return number;
        }
        /// <summary>
        /// подсчет милисекунд
        /// </summary>
        /// <param name="begin"></param>
        /// <returns></returns>
        public static int TimeCount( Dates begin)
        {

            int result = (int)(DateTime.Now - begin.time).Milliseconds;
            begin.time = DateTime.Now;
            return result;
        }
        /// <summary>
        /// блокатор
        /// </summary>
        private static readonly object _SyncRoot = new object();//4 байта памяти - главной сделать на чтение - и используем для группировки кода с т.зр. закрытост для прерывания разными потоками
        /// <summary>
        /// данные о сообщении и времени отсечки для передачи в функция потока
        /// </summary>
        public class Dates
        {
            public string message { get; set; } = "underfined";
            public DateTime time { get; set; }
            public Dates Change(string msg) { message = msg; return this; }
        }

        /// <summary>
        /// печать в потоке
        /// </summary>
        /// <param name="dates"></param>
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

        public static void CSVINText(string csvFile)
        {
            using (StreamReader sr = new StreamReader(csvFile))
            {
                //for(int i=0;)
            }
        }
        //public static void 
    }
}
