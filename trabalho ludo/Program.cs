﻿using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace trabalho_ludo
{
    internal class Program
    {
        static int Ludo_atp()
        {
            int jogador_vencedor, jogadas_restantes = 0, jogador_capturado, jogada_escolhida;

            int peao_capturado, n_jogadores = 0,peao_escolhido;

            string op;

            bool jogada_invalida, checkpoint, peao_vitoria = false;
            bool captura, passar_vez, jogada_extra = false;
            bool sair_da_casa,peao_invalido;

            do
            {
                Console.WriteLine("\nDigite o número de jogadores (2) ou (4): ");
                n_jogadores = int.Parse(Console.ReadLine());

                if(n_jogadores!=2 && n_jogadores != 4)
                {
                    Console.WriteLine("\nDigite apenas uma das 2 opções acima!");
                }

            } while (n_jogadores != 2 && n_jogadores != 4);

            Jogo ludo = new Jogo(n_jogadores,ConsoleColor.Green, ConsoleColor.Yellow, ConsoleColor.Red, ConsoleColor.Blue);

            do
            {
                for (int i = 0; i < ludo.vet_jogadores.Length; i++)
                {

                    Console.WriteLine($"\nTurno do jogador {i + 1}");

                    passar_vez = ludo.vet_jogadores[i].Jogar_dado(ref jogada_extra); //passar a vez apenas se tirar 3 6 seguidos

                    if(!passar_vez) //testar se existe realmente uma jogada disponível
                    {
                        passar_vez = ludo.vet_jogadores[i].Validar_turno();

                        if (passar_vez)
                        {
                            Console.WriteLine("\nVocê perdeu a vez!\n\nDigite algo para continuar: ");
                            op = Console.ReadLine();
                        }
                    }
                 
                    if(!passar_vez)
                    {
                        do
                        {
                            int[] peoes_disponiveis = new int[4], jogadas_disponiveis = new int[15];

                            jogada_extra = false;

                            Console.WriteLine($"\nJogador {i + 1}");

                            do
                            {
                                Console.Write("\nPeões disponíveis no momento: ");
                                ludo.vet_jogadores[i].Verificar_peoes_disponiveis(peoes_disponiveis);
                                ludo.Exibir_informacoes(peoes_disponiveis);
                                Console.WriteLine("\nQual peão deseja mover? ");
                                peao_escolhido = (int.Parse(Console.ReadLine())) - 1;
                                peao_invalido = ludo.Validar_peao(peao_escolhido, peoes_disponiveis);

                                if (peao_invalido)
                                {
                                    Console.WriteLine("\nDigite apenas um dos peões disponíveis! ");
                                }

                            } while (peao_invalido);
                            do
                            {
                                Console.Write("\nJogadas disponíveis com o peão escolhido: ");
                                ludo.vet_jogadores[i].Verificar_jogadas_disponiveis(ludo.vet_jogadores[i].vet_peoes[peao_escolhido].coluna, jogadas_disponiveis);
                                ludo.Exibir_informacoes(jogadas_disponiveis);

                                Console.WriteLine($"\nQual jogada deseja usar?");
                                jogada_escolhida = int.Parse(Console.ReadLine());

                                jogada_invalida = ludo.vet_jogadores[i].Validar_jogada(jogada_escolhida, ludo.vet_jogadores[i].vet_peoes[peao_escolhido].coluna, out sair_da_casa,ref peao_vitoria);
                                //validar jogada antes de mover

                                if (jogada_invalida)
                                {
                                    Console.WriteLine("\nJogada inválida,tente novamente!");
        
                                }

                            } while (jogada_invalida);

                            ludo.vet_jogadores[i].Mover_peao(peao_escolhido, jogada_escolhida, out checkpoint, sair_da_casa);
                            Console.WriteLine($"\nColuna do peão atual: {ludo.vet_jogadores[i].vet_peoes[peao_escolhido].coluna}");

                            if (peao_vitoria)
                            {
                                Console.WriteLine($"\nO jogador {i + 1} chegou com o peão {peao_escolhido+1} no destino");
                                jogada_extra = true;
                                ludo.vet_jogadores[i].Jogar_dado(ref jogada_extra);

                            }

                            else if (!checkpoint)
                            {
                                captura = ludo.Capturar_peao(ludo.vet_jogadores[i].vet_peoes[peao_escolhido].coluna, i, out jogador_capturado, out peao_capturado);

                                if (captura)
                                { //jogar novamente e testar tudo dnv
                                    jogada_extra = true;
                                    Console.WriteLine($"\nVocê capturou o peão {peao_capturado + 1} do jogador {jogador_capturado + 1}");
                                    Console.WriteLine($"Coluna do peão que foi capturado { ludo.vet_jogadores[jogador_capturado].vet_peoes[peao_capturado].coluna}");
                                    ludo.vet_jogadores[i].Jogar_dado(ref jogada_extra);
                                    ludo.Retornar_casa(jogador_capturado, peao_capturado);
                                }
                            }

                            jogadas_restantes = ludo.vet_jogadores[i].Contar_jogadas_restantes();
                            Console.WriteLine(jogadas_restantes);


                            Console.WriteLine("\nDigite algo para continuar: ");
                            op = Console.ReadLine();
                            Console.Clear();

                        } while (jogadas_restantes !=0);
                    }
                    
                }
            } while (!ludo.Vitoria(out jogador_vencedor));

            return jogador_vencedor;
        }


        static void Main(string[] args)
        {
            int jogador_vencedor = Ludo_atp();

            Console.WriteLine($"Jogador {jogador_vencedor} ganhou");
        }
    }
}
