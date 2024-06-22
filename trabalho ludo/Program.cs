using System;
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

            int[] peoes_disponiveis = new int[4];
            int[] jogadas_disponiveis = new int[15];

            bool jogada_invalida, checkpoint, peao_vitoria;
            bool captura, passar_vez, jogada_extra = false;
            bool sair_da_casa;

            Console.WriteLine("Digite o número de jogadores (2) ou (4): ");
            n_jogadores = int.Parse(Console.ReadLine());

            Jogo ludo = new Jogo(n_jogadores,ConsoleColor.Green, ConsoleColor.Yellow, ConsoleColor.Red, ConsoleColor.Blue);

            do
            {
                for (int i = 0; i < ludo.vet_jogadores.Length; i++)
                {
                    passar_vez = ludo.vet_jogadores[i].Jogar_dado(ref jogada_extra); //passar a vez apenas se tirar 3 6 seguidos

                    if (!passar_vez) //testar se existe realmente uma jogada disponível
                    {
                        passar_vez = ludo.vet_jogadores[i].Validar_turno();
                    }

                    if (!passar_vez)
                    {
                        do
                        {
                            do
                            {
                                jogada_extra = false;

                                Console.WriteLine($"Qual peão deseja mover?\nPeões disponíveis no momento: ");
                                ludo.vet_jogadores[i].Verificar_peoes_disponiveis(peoes_disponiveis);
                                ludo.Exibir_informacoes(peoes_disponiveis);
                                peao_escolhido = int.Parse(Console.ReadLine());

                                Console.WriteLine($"\nQual jogada deseja gastar?\nJogadas disponíveis com o peão escolhido: ");
                                ludo.vet_jogadores[i].Verificar_jogadas_disponiveis(peao_escolhido,jogadas_disponiveis);
                                ludo.Exibir_informacoes(jogadas_disponiveis);
                                jogada_escolhida = int.Parse(Console.ReadLine());

                                jogada_invalida = ludo.vet_jogadores[i].Validar_jogada(jogada_escolhida, ludo.vet_jogadores[i].vet_peoes[peao_escolhido-1].coluna, out sair_da_casa);
                                //validar jogada antes de mover

                                if (jogada_invalida)
                                {
                                    Console.WriteLine("\nJogada inválida,tente novamente!");
                                }

                            } while (jogada_invalida);

                            ludo.vet_jogadores[i].Mover_peao(peao_escolhido, jogada_escolhida, out checkpoint, out peao_vitoria, sair_da_casa);
                            //E se o peão que for movido chegar no destino, o jogador pode jogar o dado mais uma vez

                            if (!checkpoint)
                            {//trabalhar jogada extra
                                captura = ludo.Capturar_peao(ludo.vet_jogadores[i].vet_peoes[peao_escolhido].coluna, i, out jogador_capturado, out peao_capturado);

                                if (captura)
                                { //jogar novamente e testar tudo dnv
                                    jogada_extra = true;
                                    Console.WriteLine($"Você capturou o peão {peao_capturado + 1} do jogador {jogador_capturado + 1}");
                                    ludo.vet_jogadores[i].Jogar_dado(ref jogada_extra);
                                    ludo.Retornar_casa(jogador_capturado, peao_capturado);
                                }
                            }


                        } while (jogadas_restantes == 0);
                    }
                    
                }
            } while (!ludo.Vitoria(out jogador_vencedor));

            return jogador_vencedor;
        }


        static void Main(string[] args)
        {
            
        }
    }
}
