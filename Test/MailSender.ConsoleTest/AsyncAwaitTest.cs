using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static System.Console;
using System.IO;


namespace MailSender.ConsoleTest
{
    internal static class AsyncAwaitTest
    {
        public static void Start()
        {
            var messages = Enumerable.Range(0, 5).Select(i => $"{i}").ToArray();
            var process_task = new List<Task>();
            foreach (var msg in messages)
            {
                process_task.Add(Task.Run(() =>msgFunk(msg, $"{msg}.txt")));
            }
            WriteLine("all is ready. waiting tasks finish");
            Task.WaitAll(process_task.ToArray());//синхронный метод ожидания
            WriteLine("all is ready.tasks have finished ");
        }

        public static async void StartAsync()
        {
            var messages = Enumerable.Range(0, 5).Select(i => $"{i}").ToArray();
            var process_task = new List<Task>();
            foreach (var msg in messages)
            {
                //process_task.Add(Task.Run(() => msgFunk(msg, $"{msg}.txt")));
                //process_task.Add(Task.Run(() => msgFunkAsync(msg, $"{msg}.txt")));//вызываем асинхронный метод - асинхронный параллельный вызов - все задачи создаются параллельно и начинают выполняться
                process_task.Add(msgFunkAsync(msg, $"{msg}.txt"));//не тратим ресурсы на создание задач - асинхронный последовательный вызов
            }
            WriteLine("all is ready. waiting tasks finish");
            //Task.WaitAll(process_task.ToArray());
            var awaiting_task = Task.WhenAll(process_task);//асинхронное ожидание - новая задача, которая завершиться, когда выполнятся все задачи из списка
            await awaiting_task;
            WriteLine("all is ready.tasks have finished ");
        }

        private static int msgFunk(string msg, string filename)
        {
            WriteLine($"getting started {Thread.CurrentThread.ManagedThreadId}{msg}");
            Thread.Sleep(250); 
            File.WriteAllText(filename, msg);
            WriteLine($"made {Thread.CurrentThread.ManagedThreadId}{msg}");
            //throw new InvalidOperationException("Ups");//генерация искусственного исключения для первой задачи

            return msg.Length;
        }
        private static async Task<int> msgFunkAsync(string msg, string filename)
        {
            WriteLine($"getting started {Thread.CurrentThread.ManagedThreadId}{msg}");
            Thread.Sleep(250);
            //File.WriteAllText(filename, msg);если не переписать процесс работы в ассинхронном виде, но несмотря на изменения в заголовке будет работать все равно синхронно в одном потоке
            using (var writer = new StreamWriter(filename))
            {
                await writer.WriteAsync(msg);//развезали руки процессору на асинхронную работу с записью
            }
                WriteLine($"made {Thread.CurrentThread.ManagedThreadId}{msg}");
            //throw new InvalidOperationException("Ups");//генерация искусственного исключения для первой задачи

            return msg.Length;
        }

    }
}
