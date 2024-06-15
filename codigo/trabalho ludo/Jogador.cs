using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace trabalho_ludo
{
    internal class Jogador
    {
        public Peao[] casa_jogador = new Peao[4];
        public string cor;
        public int[] jogadas = new int[3];
        
        public Jogador(string cor)
        {
            this.cor = cor;
        }

        public void Criar_peoes(string cor)
        {
            for(int i = 0; i < this.casa_jogador.Length; i++)
            {
                Peao peao = new Peao(cor,i+1);
                casa_jogador[i] = peao;
            }
        }
    }
}
