using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace trabalho_ludo
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ConsoleColor[] cores = new ConsoleColor[5];
            cores[0] = ConsoleColor.Yellow;
            Console.BackgroundColor = cores[0];
            Console.WriteLine("alou");
        }
    }
}
