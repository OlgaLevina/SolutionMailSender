using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;
using System.Globalization;
using System.Threading;

namespace MailSender.ConsoleTest
{
    internal static class ThreadPoolTests
    {
        public static void Start()
        {
            var messages=Enumerable.Range(0,100).Select(i=> $"Message {i}").ToArray();
            //for (var i=0;i<messages.Length;i++)
            //{
            //    var msg = messages[i];
            //    new Thread(() => ProcessMessage(msg)).Start();
            //}
            foreach(var message in messages)
            {
                ThreadPool.SetMinThreads(5, 5);//сколько создается потоков изначально, первая цифра - количество потоков, 2е - сколько потоков может использоваться для ввода-вывода
                ThreadPool.SetMaxThreads(15, 15);//максимум до которого может раздуться
                //мин макс - обычно на вэб-серверах, т.к. система сама оптимизирует их количества, а мин-макс - связывает ей руки
                ThreadPool.QueueUserWorkItem(o=> ProcessMessage(message));//повторное использование потоков по мере их высвобождения

            }
        }

        private static readonly object _SyncRoot = new object();//4 байта памяти - главной сделать на чтение - и используем для группировки кода с т.зр. закрытост для прерывания разными потоками

        private static void ProcessMessage(string mesage)
        {
            ThreadTests.CheckThread();
            for (int i = 0; i < 3; i++)
            {
                lock (_SyncRoot)
                {
                    Write($"id: {Thread.CurrentThread.ManagedThreadId}");
                    Write($" msg{i}: ");
                    WriteLine($" \"{mesage}\"");
                }
                Thread.Sleep(100);
            }
            WriteLine("Finished");
        }
    }
}
