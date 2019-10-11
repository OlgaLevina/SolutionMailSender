using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using static System.Console;
using System.Globalization;


namespace MailSender.ConsoleTest
{
    public class Program
    {

        static void Main(string[] args)
        {
            AsyncAwaitTest.Start();
            
            WriteLine("main thread is over");
            ReadLine();


        }


    }


}
