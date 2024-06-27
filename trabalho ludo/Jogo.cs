﻿using System;
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

        public bool Capturar_peao(int ultimo_peao_coluna,int ultimo_jogador,out int jogador_capturado,out int peao_capturado,out int peao_capturado_coluna)
        {
            int distancia = 0;
            int diferenca = 0, jogador_oponente = 0;  
            
            jogador_capturado = peao_capturado = 0;
            peao_capturado_coluna = 0;
            bool checkpoint;

            int[] vet_checkpoints = { -1,0, 8, 13, 21, 26, 34, 39, 47, 51, 52, 53, 54, 55, 56, 57 };

            if (this.vet_jogadores.Length == 2)
            {
                distancia = 26;
                jogador_oponente = ultimo_jogador == 0 ? 1 : 0;

                for (int i = 0; i < this.vet_jogadores[jogador_oponente].vet_peoes.Length; i++)
                {
                    checkpoint = false;

                    for (int j = 0; j < vet_checkpoints.Length; j++)
                    {
                        if (this.vet_jogadores[jogador_oponente].vet_peoes[i].coluna == vet_checkpoints[j])
                        {
                            checkpoint = true;
                            break;
                        }
                    }

                    if (!checkpoint)
                    {
                        diferenca = ultimo_peao_coluna - this.vet_jogadores[jogador_oponente].vet_peoes[i].coluna;
                        peao_capturado_coluna = this.vet_jogadores[jogador_oponente].vet_peoes[i].coluna;

                        diferenca = diferenca < 0 ? diferenca * -1 : diferenca;

                        Console.WriteLine($"Diferenca {diferenca} Distancia {distancia}");

                        if (diferenca == distancia)
                        {
                            jogador_capturado = jogador_oponente;
                            peao_capturado = i;
                            return true;
                        }
                    }                 
                }
            }

            /*subtrair índices dos jogadores multiplicar por 13 e somar na coluna do peão
                se o indice jogador atual for maior ou subtrair se for menor.*/

            else
            {
                for (int i = 0; i < this.vet_jogadores.Length; i++)
                {
                    if (i != ultimo_jogador)
                    {
                        for (int j = 0; j < this.vet_jogadores[i].vet_peoes.Length; j++)
                        {
                            checkpoint = false;

                            for (int k = 0; k < vet_checkpoints.Length; k++)
                            {
                                if (this.vet_jogadores[i].vet_peoes[j].coluna == vet_checkpoints[k])
                                {
                                    checkpoint = true;
                                    break;
                                }
                            }

                            if (!checkpoint)
                            {
                                distancia = ultimo_jogador - i;
                                distancia = distancia > 0 ? distancia * 13 : (distancia * -1) * 13;
                                diferenca = ultimo_jogador > i ? ultimo_peao_coluna + distancia : ultimo_peao_coluna - distancia;                                                                                    
                                peao_capturado_coluna = this.vet_jogadores[i].vet_peoes[j].coluna;

                                Console.WriteLine($"\ndiferenca {diferenca} Distancia {distancia} Peao testado coluna {peao_capturado_coluna}");

                                if (diferenca == this.vet_jogadores[i].vet_peoes[j].coluna)
                                {
                                    jogador_capturado = i;
                                    peao_capturado = j;
                                    return true;
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
            for(int i = 0; i < peoes_disponiveis.Length; i++)
            {
                if(peao_escolhido == peoes_disponiveis[i]-1)
                {
                    return false;
                }
            }

            return true;
        }

    }
}
