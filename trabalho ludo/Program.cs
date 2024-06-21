using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace trabalho_ludo
{
    internal class Program
    {
        static void Ludo_atp()
        {
            int jogador_vencedor,jogadas_restantes,jogador_capturado,jogada_escolhida;
            int peao_capturado, n_jogadores = 0,peao_escolhido;
            int[] peoes_disponiveis;
            bool jogada_invalida, checkpoint, peao_vitoria;
            bool captura, passar_vez, jogada_extra = false;

            Console.WriteLine("Digite o número de jogadores (2) ou (4): ");
            n_jogadores = int.Parse(Console.ReadLine());

            Jogo ludo = new Jogo(n_jogadores,ConsoleColor.Green, ConsoleColor.Yellow, ConsoleColor.Red, ConsoleColor.Blue);

            do
            {
                for (int i = 0; i < ludo.vet_jogadores.Length; i++)
                {
                    passar_vez = ludo.vet_jogadores[i].Jogar_dado(ref jogada_extra);
                    jogada_extra = false;

                    if (!passar_vez)
                    {
                        do
                        {
                            Console.WriteLine($"Qual peão deseja mover?\nPeões disponíveis: ");
                            peoes_disponiveis = ludo.vet_jogadores[i].Peoes_disponiveis(ludo.vet_jogadores[i].jogadas);

                            for (int k = 0; k < peoes_disponiveis.Length; k++)
                            {
                                Console.Write(peoes_disponiveis[i] + " ");
                            }

                            peao_escolhido = int.Parse(Console.ReadLine());
                            Console.WriteLine($"Qual jogada deseja gastar?\nJogadas disponíveis: ");
                            jogadas_restantes = ludo.vet_jogadores[i].Jogadas_disponiveis();

                            jogada_escolhida = int.Parse(Console.ReadLine());
                            ludo.vet_jogadores[i].Mover_peao(peao_escolhido, jogada_escolhida, out checkpoint, out peao_vitoria, out jogada_invalida);
                            //E se o peão que for movido chegar no destino, o jogador pode jogar o dado mais uma vez

                            if (jogada_invalida)
                            {
                                Console.WriteLine("\nJogada inválida,tente novamente!");
                            }

                            else
                            {
                                if (!checkpoint)
                                {
                                    captura = ludo.Capturar_peao(ludo.vet_jogadores[i].vet_peoes[peao_escolhido].coluna, i, out jogador_capturado, out peao_capturado);

                                    if (captura)
                                    { //jogar novamente e testar tudo dnv
                                        jogada_extra = true;
                                        Console.WriteLine($"Você capturou o peão {peao_capturado + 1} do jogador {jogador_capturado + 1}");
                                        ludo.vet_jogadores[i].Jogar_dado(ref jogada_extra);
                                        ludo.Retornar_casa(jogador_capturado, peao_capturado);
                                    }
                                }
                            }

                        } while (jogadas_restantes == 0);
                    }
                    
                }
            } while (!ludo.Vitoria(out jogador_vencedor));
        }


        static void Main(string[] args)
        {
            
        }
    }
}
