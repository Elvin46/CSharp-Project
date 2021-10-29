using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        public static void Print(string text, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(text);
            Console.ResetColor();
        }
    }
}
