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

        public static void FactorialSumStart(int number)
        {
            Thread factorial_thread = new Thread(() => Printer(Factorial(number).ToString()));
            factorial_thread.IsBackground = true;
            Thread sum_thread = new Thread(() => Printer(Sum(number).ToString()));
            sum_thread.IsBackground = true;
        }

        public static int Factorial(int number)
        {
            if (number - 1 != 0) return number * Factorial(number - 1);
            else return number;
        }

        public static int Sum(int number)
        {
            if (number - 1 != 0) return number + Sum(number - 1);
            else return number;
        }

        private static readonly object _SyncRoot = new object();//4 байта памяти - главной сделать на чтение - и используем для группировки кода с т.зр. закрытост для прерывания разными потоками

        private static void Printer(string mesage)
        {
            ThreadTests.CheckThread();
            for (int i = 0; i < 20; i++)
            {
                lock (_SyncRoot)
                {
                    Write($"id {Thread.CurrentThread.ManagedThreadId}");
                    Write($" name {Thread.CurrentThread.Name=nameof(Thread.CurrentThread.Name)}: ");
                    WriteLine($" \"{mesage}\"");
                }
                Thread.Sleep(100);
            }
            WriteLine($"Finished  id:{Thread.CurrentThread.ManagedThreadId}");
        }
    }
}
