using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using  static System.Console;

namespace TestLib
{
   public class Printer
   {
       private string _Prefix;
       public Printer(string Prefix) => _Prefix = Prefix;
        public void Print(string msg)
        {
            WriteLine($"{_Prefix}{msg}");
        }

        private int GetPrefixLength(int Offset, string str) => _Prefix?.Length ?? -1+Offset + str?.Length??0;
   }


   internal class PrivatePrinter
   {
       private int _Data;

       private PrivatePrinter(int data)
       {
           _Data = data;
       }
       public void Print(string msg)
       {
           WriteLine($"{msg}{_Data}");
       }

   }
}
