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
        public Jogador[] vet_jogadores;
        public StreamWriter log_partida = new StreamWriter("log.txt",false,Encoding.UTF8);

        public Jogo(int n_jogadores)
        {
            vet_jogadores = new Jogador[n_jogadores];

            for(int i = 0; i < vet_jogadores.Length; i++)
            {
                Jogador jogador = new Jogador();
                jogador.Criar_peoes();
                vet_jogadores[i] = jogador;
            }
        }

        public void Documentar_partida(int jogador_atual,int peao_atual,int jogada_atual,bool peao_vitoria,bool captura)
        {
            this.log_partida = new StreamWriter("log.txt", true, Encoding.UTF8);

            this.log_partida.WriteLine($"Jogador");

        }

        public bool Capturar_peao(int ultimo_peao_coluna,int ultimo_jogador,out int jogador_capturado,out int peao_capturado,out int peao_capturado_coluna)
        {
            int[] distancias = {13,26,39};
            int diferenca = 0, jogador_oponente = 0;
            int teste = 0, distancia = 0;
            jogador_capturado = peao_capturado = 0;
            peao_capturado_coluna = 0;

            if (this.vet_jogadores.Length == 2)
            {
                jogador_oponente = ultimo_jogador == 0 ? 1 : 0;

                for (int i = 0; i < this.vet_jogadores[jogador_oponente].vet_peoes.Length; i++)
                {
                    if (this.vet_jogadores[jogador_oponente].vet_peoes[i].coluna != -1)
                    {
                        diferenca = ultimo_peao_coluna - this.vet_jogadores[jogador_oponente].vet_peoes[i].coluna;
                        peao_capturado_coluna = this.vet_jogadores[jogador_oponente].vet_peoes[i].coluna;
                        diferenca = diferenca < 0 ? diferenca * -1 : diferenca;

                        Console.WriteLine($"Diferenca {diferenca}");

                        if (diferenca == distancias[1])
                        {
                            jogador_capturado = jogador_oponente;
                            peao_capturado = i;
                            return true;
                        }
                    }
                   
                }
            }

            else
            {
                for (int i = 0; i < this.vet_jogadores.Length; i++)
                {
                    if (i != ultimo_jogador)
                    {
                        for (int j = 0; j < this.vet_jogadores[i].vet_peoes.Length; j++)
                        {
                            if (this.vet_jogadores[i].vet_peoes[j].coluna != -1)
                            {
                                distancia = (ultimo_jogador - i) * 13;
                                distancia = distancia < 0 ? distancia * -1 : distancia;
                                teste = ultimo_jogador > i ? ultimo_peao_coluna + distancia : ultimo_peao_coluna - distancia;

                                peao_capturado_coluna = this.vet_jogadores[i].vet_peoes[j].coluna;

                                if (teste == this.vet_jogadores[i].vet_peoes[j].coluna)
                                {
                                    jogador_capturado = i;
                                    peao_capturado = j;
                                    Console.WriteLine($"\nteste {teste} Distancia {distancia} Peao testado coluna {peao_capturado_coluna}");
                                    return true;
                                }

                                else
                                {
                                    diferenca = ultimo_peao_coluna - this.vet_jogadores[i].vet_peoes[j].coluna;
                                    diferenca = diferenca > 0 ? diferenca : diferenca * -1;

                                    for (int k = 0; k < distancias.Length; k++)
                                    {
                                        if (diferenca == distancias[k])
                                        {
                                            jogador_capturado = i;
                                            peao_capturado = j;
                                            Console.WriteLine($"\nDiferenca {diferenca} Distancia {distancias[k]} Peao testado coluna {peao_capturado_coluna}");
                                            return true;
                                        }
                                    }
                                }
                            }
                        }
                    }
                    
                }
            }
                return false;
        }

        public void Retornar_casa(int jogador_capturado,int peao_capturado)
        {
            this.vet_jogadores[jogador_capturado].vet_peoes[peao_capturado].coluna = -1;
        }

        public bool Vitoria(ref int jogador_vencedor)
        {
            bool vitoria;

            for(int i = 0; i < this.vet_jogadores.Length; i++)
            {
                vitoria = true;

                for (int j = 0; j < this.vet_jogadores[i].vet_peoes.Length; j++)
                {
                    if (this.vet_jogadores[i].vet_peoes[j].coluna!=56)
                    {
                        vitoria = false;
                        break;
                    }
                }

                if (vitoria)
                {
                    jogador_vencedor = i;
                    return true;
                }
                
            }

            return false;
        }

        public void Exibir_informacoes(int[] vet)
        {
            for (int i = 0; i < vet.Length; i++)
            {
                if (vet[i] == 0)
                {
                    break;
                }

                Console.Write(vet[i] + " ");
            }
        }

        public bool Validar_peao(int peao_escolhido, int[] peoes_disponiveis)
        {
            if (peao_escolhido != 0)
            {
                for (int i = 0; i < peoes_disponiveis.Length; i++)
                {
                    if (peao_escolhido == peoes_disponiveis[i])
                    {
                        return false;
                    }
                }
            }

            return true;
        }

    }
}
