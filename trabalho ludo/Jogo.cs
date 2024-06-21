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
        public Jogador[] vet_jogadores;//57 casas no total, a partir da casa 52 é a victory zone

        public Jogo(int n_jogadores,params ConsoleColor[] cores)
        {
            vet_jogadores = new Jogador[n_jogadores];

            for(int i = 0; i < vet_jogadores.Length; i++)
            {
                Jogador jogador = new Jogador(cores[i]);
                jogador.Criar_peoes();
                vet_jogadores[i] = jogador;
            }
        }

        public bool Capturar_peao(int ultimo_peao_coluna,int ultimo_jogador,out int jogador_capturado,out int peao_capturado)
        {
            const int distancia = 26;
            int diferenca = 0, jogador_oponente = 0;
            jogador_capturado = peao_capturado = 0;
            Peao[] peoes_oponente = new Peao[4];

            if (this.vet_jogadores.Length == 2)
            {
                jogador_oponente = ultimo_jogador == 0 ? 1 : 0;

                for (int i = 0; i < this.vet_jogadores[jogador_oponente].vet_peoes.Length; i++)
                {
                    peoes_oponente[i] = this.vet_jogadores[jogador_oponente].vet_peoes[i];
                    diferenca = ultimo_peao_coluna - peoes_oponente[i].coluna;

                    if (diferenca == distancia)
                    {
                        jogador_capturado = jogador_oponente;
                        peao_capturado = i;
                        return true;
                    }
                }

            }

            /*else
            {
                //to be continue
            }*/
                return false;
        }

        public void Retornar_casa(int jogador_capturado,int peao_capturado)
        {
            this.vet_jogadores[jogador_capturado].vet_peoes[peao_capturado].coluna = 0;
        }

        public bool Vitoria(out int jogador_vencedor)
        {
            jogador_vencedor = 0;
            bool vitoria;

            for(int i = 0; i < this.vet_jogadores.Length; i++)
            {
                vitoria = true;

                for (int j = 0; j < this.vet_jogadores[i].casa_vitoria.Length; j++)
                {
                    if (this.vet_jogadores[i].casa_vitoria[j] == null)
                    {
                        vitoria = false;
                        break;
                    }
                }

                if (vitoria)
                {
                    jogador_vencedor = i + 1;
                    return true;
                }
                
            }

            return false;
        }
    }
}
