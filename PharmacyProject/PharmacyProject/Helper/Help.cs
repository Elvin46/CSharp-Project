using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PharmacyProject.Helper
{
    class Help
    {
        public static int Parse()
        {
            string numstr;
            int num;
        Enterr:
            numstr = Console.ReadLine();
            bool result = int.TryParse(numstr, out num);
            if (result == false)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("Please Enter Number!");
                Console.ResetColor();
                goto Enterr;
            }
            return num;
        }
        public static void Print(string text, ConsoleColor color= ConsoleColor.White)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(text);
            Console.ResetColor();
        }
        public static void Blink()
        {
            Thread.Sleep(300);
            Console.Clear();
            for (int i = 0; i < 3; i++)
            {
                Console.WriteLine(".");
                Thread.Sleep(200);
                Console.Clear();
                Console.WriteLine(". .");
                Thread.Sleep(200);
                Console.Clear();
                Console.WriteLine(". . .");
                Thread.Sleep(150);
                Console.Clear();
            }
        }
        public static void Typing(string text,ConsoleColor color=ConsoleColor.White)
        {
            for (int i = 0; i < text.Length; i++)
            {
                Console.ForegroundColor = color;
                if (i==text.Length-1)
                {
                    Console.Write($"{text[i]}\n");
                    break;
                }
                Console.Write(text[i]);
                Thread.Sleep(25);
                Console.ResetColor();
            }
        }
    }
}
