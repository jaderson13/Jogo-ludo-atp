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
            int n_jogadores=0;
            int jogador_vencedor,jogadas;
            int[] vet_peoes;

            Console.WriteLine("Digite o número de jogadores (2) ou (4): ");
            n_jogadores = int.Parse(Console.ReadLine());

            Jogo ludo = new Jogo(n_jogadores,ConsoleColor.Green, ConsoleColor.Yellow, ConsoleColor.Red, ConsoleColor.Blue);

            while (!ludo.Vitoria(out jogador_vencedor))
            {
                for(int i = 0; i < ludo.vet_jogadores.Length; i++)
                {
                    jogadas = 0;
                    ludo.vet_jogadores[i].Jogar_dado();

                    do
                    {
                        Console.WriteLine($"Qual peão deseja mover?\nPeões disponíveis: ");
                        vet_peoes = ludo.vet_jogadores[i].Peoes_disponiveis(ludo.vet_jogadores[i].jogadas);

                        //Contar as jogadas restantes
                        for (int j = 0; j < ludo.vet_jogadores[i].jogadas.Length; j++)
                        {
                            if (ludo.vet_jogadores[i].jogadas[j] != 0)
                            {
                                jogadas++;
                            }
                        }

                    } while (jogadas == 0);               
                }
            }
        }


        static void Main(string[] args)
        {

            /*testes destrutivos método mover peão
            bool checkpoint = false;
            bool casa_inicial = false;
            int jogador_capturado;
            int peao_capturado;
            Jogador j0 = new Jogador(ConsoleColor.Green);
            j0.Criar_peoes();
            j0.jogadas[0] = 6;
            j0.jogadas[1] = 6;
            j0.Mover_peao(1, 6, ref checkpoint, ref casa_inicial);
            Console.WriteLine("coluna " + j0.percurso[0, 1].coluna+" checkpoint "+checkpoint+" casa_inicial "+casa_inicial+" jogadas " + j0.jogadas[0]);
            j0.Mover_peao(1, 6, ref checkpoint, ref casa_inicial);
            Console.WriteLine("coluna " + j0.percurso[0, 7].coluna + " checkpoint " + checkpoint + " casa_inicial " + casa_inicial + " jogadas " + j0.jogadas[1]);
           

            Jogador j1 = new Jogador(ConsoleColor.Green);
            j1.Criar_peoes();
            j1.jogadas[0] = 6;
            j1.jogadas[1] = 31;
            j0.Mover_peao(4, 6, ref checkpoint, ref casa_inicial);
            Console.WriteLine("coluna " + j1.percurso[3, 1].coluna + " checkpoint " + checkpoint + " casa_inicial " + casa_inicial + " jogadas " + j1.jogadas[0]);
            j0.Mover_peao(4, 31, ref checkpoint, ref casa_inicial);
            Jogo.Capturar_peao(j1.percurso[3,1],1,out jogador_capturado,out peao_capturado);
            Jogo.Retornar_casa();
            Console.WriteLine("coluna " + j1.percurso[3, 32].coluna + " checkpoint " + checkpoint + " casa_inicial " + casa_inicial + " jogadas " + j1.jogadas[1]);
            Console.WriteLine("coluna " + j0.percurso[0, 0].coluna + " checkpoint " + checkpoint + " casa_inicial " + casa_inicial + " jogadas " + j0.jogadas[1]);
            */
        }
    }
}
