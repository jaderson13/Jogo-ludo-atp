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
        public Peao[] vet_peoes = new Peao[4];
        public int[] jogadas = new int[15];

        public void Criar_peoes()
        {
            for(int i = 0; i < this.vet_peoes.Length; i++)
            {
                Peao peao = new Peao(i,-1);
                this.vet_peoes[i] = peao;
            }
        }

        public bool Validar_jogada(int jogada_escolhida, int peao_escolhido_coluna, out bool sair_da_casa,ref bool peao_vitoria)
        {
            sair_da_casa = false;
            bool jogada_invalida = true, continuar = false;
            int index = 0;

            if (jogada_escolhida != 0)
            {
                for (int j = 0; j < this.jogadas.Length; j++)
                {
                    if (this.jogadas[j] == jogada_escolhida)
                    {
                        this.jogadas[j] = 0;
                        continuar = true;
                        index = j;
                        break;
                    }
                }
            }

            else
            {
                continuar = false;
            }

            if (continuar)
            {
                if (peao_escolhido_coluna == -1)
                {
                    if (jogada_escolhida == 6)
                    {
                        sair_da_casa = true;
                        jogada_invalida = false;
                    }
                }

                else if (peao_escolhido_coluna + jogada_escolhida <= 56)
                {
                    jogada_invalida = false;

                    if (peao_escolhido_coluna + jogada_escolhida == 56)
                    {
                        peao_vitoria = true;
                    }
                }


                if (jogada_invalida)
                {
                    this.jogadas[index] = jogada_escolhida;
                }
            }

            return jogada_invalida;

        }

        public void Mover_peao(int peao_escolhido, int num_dado,bool sair_da_casa,out bool checkpoint)
        {
            checkpoint = false;

            int[] vet_checkpoints = { -1, 0, 8, 13, 21, 26, 34, 39, 47, 51, 52, 53, 54, 55, 56, 57 };

            if (sair_da_casa)
            {
                for (int i = 0; i < this.vet_peoes.Length; i++)
                {
                    if (peao_escolhido == this.vet_peoes[i].numero)
                    {
                        this.vet_peoes[i].coluna = 0;
                        checkpoint = true;
                    }
                }
            }

            else if (!sair_da_casa)
            {
                for (int i = 0; i < this.vet_peoes.Length; i++)
                {
                    if (this.vet_peoes[i].numero == peao_escolhido)
                    {
                        this.vet_peoes[i].coluna += num_dado;

                        for (int j = 0; j < vet_checkpoints.Length; j++)
                        {
                            if (this.vet_peoes[i].coluna == vet_checkpoints[j])
                            {
                                checkpoint = true;
                                break;
                            }
                        }
                    }
                }
            }

        }

        public bool Validar_turno()
        {
  
            for(int i = 0; i < this.vet_peoes.Length; i++)
            {
                for(int j = 0; j < this.jogadas.Length; j++)
                {
                    if (this.jogadas[j] != 0)
                    {
                        if (this.vet_peoes[i].coluna == -1)
                        {
                            if (this.jogadas[j] == 6)
                            {
                                return false;
                            }
                        }

                        else if (this.vet_peoes[i].coluna + this.jogadas[j] <= 56)
                        {
                            return false;
                        }
                    }
                }

            }

            return true;

        }

        public bool Jogar_dado(ref bool jogada_extra)
        {
            int numero = 6, cont6 = 0, cont_jogadas = 0;
            bool passar_vez = false;
            Random dado = new Random();

            Console.WriteLine("\nJogar dado!");

            if (jogada_extra)
            {

                for (int i = 0; numero==6 && cont_jogadas<3; i++)
                {
                    if (this.jogadas[i] == 0)
                    {
                        numero = dado.Next(1, 10);

                        if (numero > 6)
                        {
                            numero = 6;
                        }

                        Console.WriteLine($"\nVocê tirou {numero}");
                        this.jogadas[i] = numero;
                        cont_jogadas++;

                        if (numero == 6)
                        {
                            cont6++;
                        }

                    }

                }
            }

            else
            {
                for (int i = 0; i < 3; i++)
                {
                    numero = dado.Next(1, 10);

                    if (numero > 6)
                    {
                        numero = 6;
                    }

                    this.jogadas[i] = numero;
                    Console.WriteLine($"\nVocê tirou {numero}");

                    if (numero == 6)
                    {
                        cont6++;
                    }

                    else
                    {
                        break;
                    }
                }

            }

            if (cont6 == 3)
            {
                passar_vez = true;
                Console.WriteLine("\nVocê perdeu a vez! Aperte enter para continuar: ");
                string op = Console.ReadLine();
                Console.Clear();

                for (int i = 0; i < this.jogadas.Length; i++)
                {
                    this.jogadas[i] = 0;
                }
            }

            return passar_vez;
        }

        public int Contar_jogadas_restantes()
        {
            int jogadas_restantes = 0;

            for (int j = 0; j < this.jogadas.Length; j++)
            {

                if (this.jogadas[j]!=0)
                {
                    jogadas_restantes++;
                }
            }

            return jogadas_restantes;
        }

        public void Verificar_jogadas_disponiveis(int peao_coluna,int[] jogadas) 
        {
            int contj = 0;

            for (int i = 0; i < this.jogadas.Length; i++)
            {
                if (this.jogadas[i] != 0)
                {
                    if (peao_coluna == -1)
                    {
                        if (this.jogadas[i] == 6)
                        {
                            jogadas[contj] = this.jogadas[i];
                            contj++;
                        }
                    }

                    else if (peao_coluna + this.jogadas[i] <= 56)
                    {
                        jogadas[contj] = this.jogadas[i];
                        contj++;
                    }
                }

            }
        }

        public void Verificar_peoes_disponiveis(int[] peoes)
        {
            int contp = 0;

            for (int i = 0; i < this.vet_peoes.Length; i++)
            {
                for (int j = 0; j < this.jogadas.Length; j++)
                {
                    if (this.vet_peoes[i].coluna != 56)
                    {
                        if (this.vet_peoes[i].coluna == -1)
                        {
                            if (this.jogadas[j] == 6)
                            {
                                peoes[contp] = this.vet_peoes[i].numero + 1;
                                contp++;
                                break;
                            }
                        }

                        else if (this.vet_peoes[i].coluna + this.jogadas[j] <= 56)
                        {
                            peoes[contp] = this.vet_peoes[i].numero + 1;
                            contp++;
                            break;
                        }
                    }

                }

            }
       
        }

    }
}
