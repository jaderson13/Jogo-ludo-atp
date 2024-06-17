using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace trabalho_ludo
{
    internal class Jogo
    {
        public Tabuleiro tabuleiro;
        public Jogador[] vet_jogadores;

        public int Jogar_dado(int n_jogador)
        {
            int numero = 0;
            int i=0;

            do
            {
                Random dado = new Random();
                numero = dado.Next(1, 7);
                vet_jogadores[n_jogador].jogadas[i] = numero;
                i++;
            } while (numero == 6);

            
            return numero;
        }

        public Jogo(int n_jogadores,params ConsoleColor[] cores)
        {
            vet_jogadores = new Jogador[n_jogadores];

            for(int i = 0; i < vet_jogadores.Length; i++)
            {
                Jogador jogador = new Jogador(cores[i]);
                jogador.Criar_peoes(cores[i]);
                vet_jogadores[i] = jogador;
            }
        }

        public bool Capturar_peao(Peao ultimo_peao,int ultimo_jogador,ref int jogador_capturado,ref int peao_capturado)
        {
            const int distancia = 27;
            int diferenca,jogador_oponente = 0;
            Peao[] peoes_oponente = new Peao[4];

            if (this.vet_jogadores.Length == 2)
            {
                jogador_oponente = ultimo_jogador == 0 ? 1 : 0;

                for (int i = 0; i < peoes_oponente.Length; i++)
                {
                    peoes_oponente[i] = this.vet_jogadores[jogador_oponente].casa_jogador[i];
                    diferenca = ultimo_peao.posicao.coluna - peoes_oponente[i].posicao.coluna;

                    if (diferenca == distancia)
                    {
                        jogador_capturado = jogador_oponente;
                        peao_capturado = i;
                        return true;
                    }
                }

            }

            else
            {
                //to be continue
            }
                return false;
        }
    }
}
