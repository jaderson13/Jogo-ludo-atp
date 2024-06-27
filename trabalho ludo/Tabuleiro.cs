using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using trabalho_ludo;

namespace trabalho_ludo
{
    internal class Tabuleiro
    {
        public string log_partida;
        public StreamWriter log_escrito = new StreamWriter("log.txt", false, Encoding.UTF8);
        
    }
}