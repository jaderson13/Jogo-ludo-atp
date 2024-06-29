using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;
using System.Runtime.CompilerServices;

namespace trabalho_ludo
{
    internal class Program
    {
        static int Ludo_atp(Jogo ludo)
        {
            int jogador_vencedor=-1, jogadas_restantes = 0, jogador_capturado, jogada_escolhida;
            int peao_capturado,peao_escolhido,peao_capturado_coluna;
            int[] peoes_posicoes;
            string op;
            bool jogada_invalida,checkpoint, peao_vitoria;
            bool captura, passar_vez, jogada_extra = false;
            bool sair_da_casa, peao_invalido, vitoria = false;

            do
            {
                for (int i = 0; i < ludo.vet_jogadores.Length && !vitoria; i++)
                {
                    ludo.log_partida.WriteLine("\n---------------------------------------------------");
                    Console.WriteLine($"\nTurno do jogador {i}");
                    ludo.log_partida.WriteLine($"\nTurno do jogador {i}\n");

                    passar_vez = ludo.vet_jogadores[i].Jogar_dado(ref jogada_extra,ludo); 

                    if(!passar_vez) 
                    {
                        passar_vez = ludo.vet_jogadores[i].Validar_turno();

                        if (passar_vez)
                        {
                            Console.WriteLine("\nVocê perdeu a vez!\n\nAperte enter para continuar: ");
                            ludo.log_partida.WriteLine($"Perdeu a vez");
                            op = Console.ReadLine();
                            Console.Clear();
                        }
                    }
                 
                    if(!passar_vez)
                    {
                        do
                        {
                            int[] peoes_disponiveis = new int[4], jogadas_disponiveis = new int[15];
                            jogada_extra = false;
                            Console.WriteLine($"\nJogador {i}\n");
                            Console.Write("Posição dos peões: ");
                            ludo.log_partida.WriteLine("Posição dos peões: ");
                            peoes_posicoes = ludo.vet_jogadores[i].Exibir_posicoes();

                            for(int j = 0; j < peoes_posicoes.Length; j++)
                            {
                                Console.Write($"Peão {j + 1} pos {peoes_posicoes[j]} |");
                                ludo.log_partida.WriteLine($"Peão {j + 1} pos {peoes_posicoes[j]} |");
                            }

                            do
                            {
                                Console.Write("\n\nPeões disponíveis no momento: ");
                                ludo.vet_jogadores[i].Verificar_peoes_disponiveis(peoes_disponiveis);
                                ludo.Exibir_informacoes(peoes_disponiveis);
                                Console.WriteLine("\nQual peão deseja mover? ");
                                peao_escolhido = int.Parse(Console.ReadLine());
                                peao_invalido = ludo.Validar_peao(peao_escolhido, peoes_disponiveis);

                                if (peao_invalido) { Console.WriteLine("\nDigite apenas um dos peões disponíveis! "); }

                            } while (peao_invalido);

                            peao_escolhido -= 1;

                            do
                            {
                                Console.Write("\nJogadas disponíveis com o peão escolhido: ");
                                ludo.vet_jogadores[i].Verificar_jogadas_disponiveis(ludo.vet_jogadores[i].vet_peoes[peao_escolhido].coluna, jogadas_disponiveis);
                                ludo.Exibir_informacoes(jogadas_disponiveis);

                                Console.WriteLine($"\nQual jogada deseja usar?");
                                jogada_escolhida = int.Parse(Console.ReadLine());

                                jogada_invalida = ludo.vet_jogadores[i].Validar_jogada(jogada_escolhida, ludo.vet_jogadores[i].vet_peoes[peao_escolhido].coluna, out sair_da_casa, out peao_vitoria);
      
                                if (jogada_invalida) { Console.WriteLine("\nJogada inválida,tente novamente!");}
             
                            } while (jogada_invalida);

                            ludo.vet_jogadores[i].Mover_peao(peao_escolhido, jogada_escolhida, sair_da_casa,out checkpoint);
                            ludo.log_partida.WriteLine($"\nJogador {i} moveu o peao {peao_escolhido+1}, {jogada_escolhida} casas para frente");

                            Console.WriteLine($"\nColuna do peão movido: {ludo.vet_jogadores[i].vet_peoes[peao_escolhido].coluna}");
                            ludo.log_partida.WriteLine($"\nColuna do peão movido: {ludo.vet_jogadores[i].vet_peoes[peao_escolhido].coluna}");

                            if (peao_vitoria)
                            {
                                Console.WriteLine($"\nO jogador {i} chegou com o peão {peao_escolhido + 1} no destino");
                                ludo.log_partida.WriteLine($"\nO jogador {i} chegou com o peão {peao_escolhido + 1} no destino");
                                jogada_extra = true;
                                ludo.vet_jogadores[i].Jogar_dado(ref jogada_extra,ludo);
                            }

                            if (!checkpoint)
                            {
                                captura = ludo.Capturar_peao(ludo.vet_jogadores[i].vet_peoes[peao_escolhido].coluna, i, out jogador_capturado, out peao_capturado, out peao_capturado_coluna);

                                if (captura)
                                {
                                    jogada_extra = true;
                                    Console.WriteLine($"\nVocê capturou o peão {peao_capturado + 1} do jogador {jogador_capturado}");
                                    ludo.log_partida.WriteLine($"\nVocê capturou o peão {peao_capturado + 1} do jogador {jogador_capturado}");
                                    Console.WriteLine($"Coluna do peão que foi capturado {peao_capturado_coluna}");
                                    passar_vez = ludo.vet_jogadores[i].Jogar_dado(ref jogada_extra,ludo);
                                    ludo.Retornar_casa(jogador_capturado, peao_capturado);
                                }
                            }

                            if (!passar_vez)
                            {
                                Console.WriteLine("\nAperte enter para continuar: ");
                                op = Console.ReadLine();
                                Console.Clear();
                            }

                            jogadas_restantes = ludo.vet_jogadores[i].Contar_jogadas_restantes();
                            vitoria = ludo.Vitoria(ref jogador_vencedor);

                        } while (jogadas_restantes != 0 && !vitoria);
                    
                    }
                }

            } while (!vitoria);

            return jogador_vencedor;
        }

        static void Main(string[] args)
        {
            int n_jogadores = 0;

            Console.WriteLine("\n--Jogo Ludo--\n");

            do
            {
                Console.WriteLine("\nDigite o número de jogadores (2) ou (4): ");
                n_jogadores = int.Parse(Console.ReadLine());

                if (n_jogadores != 2 && n_jogadores != 4)
                {
                    Console.WriteLine("\nDigite apenas uma das 2 opções acima!");
                }

            } while (n_jogadores != 2 && n_jogadores != 4);


            Jogo ludo = new Jogo(n_jogadores);

            ludo.log_partida = new StreamWriter("log.txt", false, Encoding.UTF8);
            ludo.log_partida.WriteLine($"--Ludo--\n--Começo--\n\nNúmero de jogadores: {n_jogadores}");

            int jogador_vencedor = Ludo_atp(ludo);
            Console.WriteLine($"\nJogador {jogador_vencedor} ganhou\nFim de jogo");
            ludo.log_partida.WriteLine($"\n---------------------------------------------------\nJogador {jogador_vencedor} ganhou\nFim de jogo");
            ludo.log_partida.Close();
        }
    }
}
