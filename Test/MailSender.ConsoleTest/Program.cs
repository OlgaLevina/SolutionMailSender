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
            //ThreadTests.Start();
            //SinchronizationTests.Start();
            //ThreadPoolTests.Start();
            int number = 100;
            HomeWork5.Dates dates = new HomeWork5.Dates { time = DateTime.Now };
            WriteLine($"Факториал {HomeWork5.Factorial(number)} - время выполнения {HomeWork5.TimeCount(dates)} млс");
            WriteLine($"Факториал {HomeWork5.Sum(number)} - время выполнения {HomeWork5.TimeCount(dates)} млс");
            HomeWork5.FactorialSumStart(number, dates);
            ReadLine();


        }


    }


}
