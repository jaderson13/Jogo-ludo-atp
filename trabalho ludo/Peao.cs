using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace trabalho_ludo
{
    internal class Peao
    {
        public ConsoleColor cor;
        public int numero;
        public Coordenadas posicao;
        public Peao(ConsoleColor cor,int numero)
        {
            this.cor = cor;
            this.numero = numero;
        }
    }
}
