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
    internal static class SinchronizationTests
    {

        private static readonly List<string> _Messages = new List<string>();
        public static void Start2()
        {
            var concurent_dict = new ConcurrentDictionary<string, List<string>>();

        }
        public static void Start()
        {
            Thread[] threads = new Thread[10];
            for(int i=0; i<threads.Length;i++)
            {
                int i0 = i;//т.к. фактически i - это объект, меняющий свое значение и при выходе из цикла имеет значение 10, поэтму при печати сообщение будет выводить последнее значение i; чтобы избежать проблему  - сохраняем значене I  -  в локальную переменную и передаем ее значение в сообщение потока 
                threads[i] = new Thread(() => Printer($"Message {i0}"));
            }
            Array.ForEach(threads, thread => thread.Start());
        }

        private static readonly object _SyncRoot = new object();//4 байта памяти - главной сделать на чтение - и используем для группировки кода с т.зр. закрытост для прерывания разными потоками

        private static void Printer(string mesage)
        {
            ThreadTests.CheckThread();
            for (int i = 0; i < 20; i++)
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
    [Synchronization]
    internal class Logger: ContextBoundObject //3й вариант закрыть класс целиком
    {
        private string _FilePath;
        public string FilePath
        {
            get => _FilePath;
            set
            {
                if (!File.Exists(value)) throw new FileNotFoundException("File not exist", value);
                _FilePath = value;
            }
        }
        public Logger(string filePath) => _FilePath = filePath;

        //[MethodImpl(MethodImplOptions.Synchronized)]//второй вариат блокировки через атрибут
        public void Log(string message)
        {
            File.AppendAllText(_FilePath, message);
        }
    }
}
