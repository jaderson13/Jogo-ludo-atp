using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace trabalho_ludo
{
    internal class Tabuleiro
    {
        public string[,] tabuleiro;
        public string log_partida;
        public StreamWriter log_escrito = new StreamWriter("log.txt",false,Encoding.UTF8);
        public StreamReader log_leitura = new StreamReader("D:\\Usuário\\Documentos\\- Algoritmos C#\\- Trabalho ludo\\trabalho ludo\\bin\\Debug\\log.txt",Encoding.UTF8);
   
    }
}
