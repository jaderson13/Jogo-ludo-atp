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
        public int coluna;
        //atributo de validação
        public Peao(ConsoleColor cor,int numero,int coluna)
        {
            this.cor = cor;
            this.numero = numero;
            this.coluna = coluna;
        }
    }
}
