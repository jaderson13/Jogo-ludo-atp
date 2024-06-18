using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace trabalho_ludo
{
    internal class Jogador
    {
        public Peao[] casa_vitoria = new Peao[4];
        public Peao[] vet_peoes = new Peao[4];
        public ConsoleColor cor;
        public int[] jogadas = new int[3];
        
        public Jogador(ConsoleColor cor)
        {
            this.cor = cor;
        }

        public void Criar_peoes()
        {
            for(int i = 0; i < this.vet_peoes.Length; i++)
            {
                Peao peao = new Peao(this.cor,i,0);
                this.vet_peoes[i] = peao;
            }
        }
        public bool Jogar_dado(ref bool jogada_extra)
        {
            int numero = 6, cont6 = 0;
            bool passar_vez = false;
            Random dado = new Random();

            if (jogada_extra)
            {
                numero = dado.Next(1, 7);

                Console.WriteLine($"\nVocê tirou {numero}");

                for (int i = 0; i < this.jogadas.Length; i++)
                {
                    if (this.jogadas[i] != 0)
                    {
                        this.jogadas[i] = numero;
                        break;
                    }
                }
            }

            else
            {
                for (int i = 0; numero == 6 && i < jogadas.Length; i++)
                {              
                    numero = dado.Next(1, 7);

                    if (this.jogadas[i] != 0)
                    {
                        this.jogadas[i] = numero;
                        Console.WriteLine($"\nVocê tirou {numero}");

                        if (numero == 6)
                        {
                            cont6++;
                        }
                    }
                    //jogada extra
                }

                if (cont6 == 3)
                {
                    passar_vez = true;
                    Console.WriteLine("\nPerdeu a vez!");

                    for (int i = 0; i < this.jogadas.Length; i++)
                    {
                        this.jogadas[i] = 0;
                    }
                }
            }

            return passar_vez;
        }

        public int Jogadas_disponiveis()
        {
            int jogadas_restantes = 0;

            for(int i = 0; i < this.jogadas.Length; i++)
            {
                if (this.jogadas[i] != 0)
                {
                    jogadas_restantes++;
                    Console.Write(this.jogadas[i] + " ");
                }
            }

            return jogadas_restantes;
        }

        public void Mover_peao(int peao_escolhido, int num_dado, out bool checkpoint, out bool peao_vitoria,out bool jogada_invalida)
        {
            //1 jogada por vez para não precisar chamar método dentro do outro
            peao_escolhido -= 1;
            int coluna_peao, casas_iniciais_vazias = 0, index_jogada = 0;
            int[] vet_checkpoints = { 1, 8, 21, 34, 47, 57 };
            bool casas_iniciais_cheias = true;
            peao_vitoria = checkpoint = false;
            jogada_invalida = true;

            //Gastar a jogada atual
            for (int j = 0; j < this.jogadas.Length; j++)
            {
                if (this.jogadas[j] == num_dado)
                {
                    jogada_invalida = false;
                    this.jogadas[j] = 0;
                    index_jogada = j;
                    break;
                }
            }

            if (!jogada_invalida)
            {
                //contar quantos peões ainda não saíram da casa principal

                for (int i = 0; i < this.vet_peoes.Length; i++)
                {
                    if (this.vet_peoes[i].coluna != 0)
                    {
                        casas_iniciais_cheias = false;
                        casas_iniciais_vazias += 1;
                    }
                }


                if (num_dado == 6 && casas_iniciais_vazias <= 3)
                {
                    for (int i = 0; i < this.vet_peoes.Length; i++)
                    {
                        if (this.vet_peoes[i].coluna != 57)
                        {
                            if (peao_escolhido == this.vet_peoes[i].numero && this.vet_peoes[i].coluna == 0)
                            {
                                this.vet_peoes[i].coluna = this.vet_peoes[i].coluna++;
                                checkpoint = true;
                            }
                        }
                    }
                }

                //jogada realizada se o peão em questão estiver fora da casa 0 ou se pelo menos 1 peão saiu da casa 0
                else if (!casas_iniciais_cheias)
                {
                    for (int i = 0; i < this.vet_peoes.Length; i++)
                    {
                        if (this.vet_peoes[i].numero == peao_escolhido)
                        {
                            if (this.vet_peoes[i].coluna + num_dado <= 57)
                            {
                                this.vet_peoes[i].coluna = i + num_dado;
                                coluna_peao = i + num_dado;
                                checkpoint = false;

                                for (int j = 0; j < vet_checkpoints.Length; j++)
                                {
                                    if (coluna_peao == vet_checkpoints[i] || coluna_peao >= 52)
                                    {
                                        checkpoint = true;
                                        break;
                                    }
                                }

                                if (coluna_peao == 57)
                                {
                                    for (int j = 0; j < this.casa_vitoria.Length; j++)
                                    {
                                        if (this.casa_vitoria[j] != null)
                                        {
                                            this.casa_vitoria[j] = this.vet_peoes[i];
                                            peao_vitoria = true;
                                            break;
                                        }
                                    }
                                }
        
                            }

                            else
                            {
                                this.jogadas[index_jogada] = num_dado;
                            }
                        }
                    }
                }
            }
        }

        //Mostrar quais peões estão disponíveis para o jogador
        public int[] Peoes_disponiveis(int[] jogadas)
        {
            int[] peoes = new int[4];
            bool dado6 = true;

            for (int j = 0; j < jogadas.Length; j++)
            {
                if (jogadas[j] == 6)
                {
                    dado6 = true;
                    break;
                }
            }

            for (int i = 0; i < this.vet_peoes.Length; i++)
            {
                if (this.vet_peoes[i].coluna == 0 && dado6)
                {
                    peoes[i] = this.vet_peoes[i].numero + 1;
                }

                else if (this.vet_peoes[i].coluna < 57)
                {
                    peoes[i] = this.vet_peoes[i].numero + 1;
                }
            }

            return peoes;
        }
    }
}
