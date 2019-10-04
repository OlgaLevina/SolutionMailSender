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
   internal static class ThreadTests
    {

        public static void CheckThread()
        {
            Thread current_thread = Thread.CurrentThread;
            WriteLine($"Поток '{current_thread.Name}' (id: {current_thread.ManagedThreadId}) запущен.");
        }

         public static void Start()
        {
            Thread current_thread = Thread.CurrentThread;
            current_thread.Name = "Main thread";
            CheckThread();
            //ClockUpdater();
            //Thread clock_thread = new Thread(new ThreadStart(ClockUpdater));//все, что в скобках, за нас допишит компилятор
            Thread clock_thread = new Thread(ClockUpdater);//создан холодный поток
            clock_thread.Start();//запустили поток - сделали его горячим, !!!приложение будет работать, пока не закончит работать этот поток
            clock_thread.Priority = ThreadPriority.Highest;
            clock_thread.IsBackground = true;//лучше для прекращения потока вместе с основным
            clock_thread.Name = "Clock thread";
            string msg = "Hello, world";
            //Thread printer_thread = new Thread(new ParameterizedThreadStart(Printer));
            Thread printer_thread = new Thread(Printer);
            printer_thread.Name = "Printer thread";
            printer_thread.IsBackground = true;
            printer_thread.Start(msg);

            //еще вариант передачи параметра в поток
            Thread print2_thread = new Thread(() => Printer(msg));
            print2_thread.Name = "Printer thread 2";
            print2_thread.Start();

            //еще вариант передачи параметра в поток
            Printer3 printer3 = new Printer3(msg);
            Thread print3_thread = new Thread(printer3.Print);
            print3_thread.Name = "Printer thread 3";
            print3_thread.Start();
            IsClockEnable = false; //самый мягкий способ
            if (!clock_thread.Join(100)) clock_thread.Interrupt();//для комбинированного выхода - 100 - д.б. больше тайм-аута потока

            ReadLine();

            //clock_thread.Join();//основной поток не завершиться пока не завершиться Клок
            //clock_thread.Interrupt();//возможный вариант закрытия (прерывание - мягкое - ) потока но не очень 
            //clock_thread.Abort();//возможный вариант закрытия (прерывание - жесткое - на любом месте - может нарушить целостнось каких-то данных) потока но не очень 
        }

        private static bool IsClockEnable = true; // для совсем мягкого прерывания потока - через переменную

        public static void ClockUpdater()
        {
            CheckThread();
            while (IsClockEnable)
            {
                //Title = DateTime.Now.ToString(CultureInfo.InvariantCulture);
                Title = DateTime.Now.ToString("dd:MM:yyyy HH:mm:ss.ffff");
                Thread.Sleep(100);
            }
        }

        private static void Printer(object obj)
        {
            CheckThread();
            var msg = (string)obj;
            for (int i = 0; i < 20; i++)
            {
                WriteLine($"id: {Thread.CurrentThread.ManagedThreadId} - msg:\"{msg}\"");
                Thread.Sleep(200);
            }
            WriteLine("Finished");
        }

    }

    internal class Printer3
    {
        private string _msg;
        public Printer3(string msg) => _msg = msg;

        public void Print()
        {
            ThreadTests.CheckThread();
            for (int i = 0; i < 20; i++)
            {
                WriteLine($"id: {Thread.CurrentThread.ManagedThreadId} - msg:\"{_msg}\"");
                Thread.Sleep(200);
            }
            WriteLine("Finished");
        }

    }
}

