using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static System.Console;

namespace MailSender.ConsoleTest
{
    internal static class TaskTest
    {
        public static void Start()
        {
            //var firstTask = new Task(TaskMethod);//холодный запуск
            //firstTask.Start();
            ////firstTask.Wait();//чтобы главный поток ждал выполнения этой задачи
            var msg = "Hello world";
            //var get_length_task = new Task<int>(() => msgFunk(msg));
            //get_length_task.Start();
            //WriteLine($"something");
            //try //обработка склчений
            //{
            //    WriteLine($"{get_length_task.Result}");//как и вэйт приводит к блокировке потока, в котором они выполняются - могут привести к взаимной блокирвке
            //}
            //catch(AggregateException e)
            //{
            //    WriteLine("Ups!");
            //    foreach (var E in e.InnerExceptions) WriteLine(E.Message);
            //}
            var second_task = Task.Factory.StartNew(TaskMethod);//сразу горячая
            var third_task = Task.Run(() => msgFunk(msg));//рекомендуемый метод, сразу горячая
            //WriteLine(third_task.Result);
            third_task.ContinueWith(forth_task => WriteLine(third_task.Result));//дополнить задачу следующей
            var fifth_task =third_task.ContinueWith(sixth_task => WriteLine(third_task.Result),TaskContinuationOptions.OnlyOnRanToCompletion);//создать и выполнить только при удачном выполненнии исходной
            var seventh_task = third_task.ContinueWith(eighth_task => WriteLine(third_task.Result), TaskContinuationOptions.NotOnFaulted);//создать и выполнить только при неудачном выполненнии исходной
            ReadLine();
        }
        private static void TaskMethod()
        {
            WriteLine($"started thread {Thread.CurrentThread.ManagedThreadId} task {Task.CurrentId}");
            Thread.Sleep(250);
            WriteLine($"finished thread {Thread.CurrentThread.ManagedThreadId} task {Task.CurrentId}");
        }

        private static int msgFunk(string msg)
        {
            Thread.Sleep(250); WriteLine($"{Thread.CurrentThread.ManagedThreadId}{msg}");
            //throw new InvalidOperationException("Ups");//генерация искусственного исключения для первой задачи
            return msg.Length;
        }

    }
}
