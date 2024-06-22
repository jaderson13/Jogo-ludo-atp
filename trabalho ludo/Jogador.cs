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
        public ConsoleColor cor;
        public int[] jogadas = new int[15];
        
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
              
                for (int i = 0; numero==6 && i < this.jogadas.Length; i++)
                {
                    if (this.jogadas[i] == 0)
                    {
                        numero = dado.Next(1, 7);
                        Console.WriteLine($"\nVocê tirou {numero}");
                        this.jogadas[i] = numero;
                        break;
                    }
                }
            }

            else
            {
                for (int i = 0; numero == 6 && i < jogadas.Length-2; i++)
                {              
                    numero = dado.Next(1, 7);

                    this.jogadas[i] = numero;
                    Console.WriteLine($"\nVocê tirou {numero}");

                    if (numero == 6)
                    {
                        cont6++;
                    }
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

        public void Mover_peao(int peao_escolhido, int num_dado, out bool checkpoint, out bool peao_vitoria,bool sair_da_casa)
        {

            int coluna_peao;
            int[] vet_checkpoints = { 1, 9, 14, 22, 27, 35, 40, 48, 52, 53, 54, 55, 56, 57 };                                          
            peao_vitoria = checkpoint = false;
            peao_escolhido -= 1; 

            if (sair_da_casa)
            {
                for (int i = 0; i < this.vet_peoes.Length; i++)
                {
                    if (this.vet_peoes[i].coluna == 0)
                    {
                        if (peao_escolhido == this.vet_peoes[i].numero)
                        {
                            this.vet_peoes[i].coluna = this.vet_peoes[i].coluna++;
                            checkpoint = true;
                        }
                    }
                }
            }

            //jogada realizada se o peão em questão estiver fora da casa 0 ou se pelo menos 1 peão saiu da casa 0
            else if (!sair_da_casa)
            {
                for (int i = 0; i < this.vet_peoes.Length; i++)
                {
                    if (this.vet_peoes[i].numero == peao_escolhido)
                    {

                        this.vet_peoes[i].coluna = this.vet_peoes[i].coluna + num_dado;
                        coluna_peao = this.vet_peoes[i].coluna + num_dado;

                        for (int j = 0; j < vet_checkpoints.Length; j++)
                        {
                            if (coluna_peao == vet_checkpoints[i])
                            {
                                checkpoint = true;
                                break;
                            }
                        }

                        if (coluna_peao == 57)
                        {
                            peao_vitoria = true;
                        }
                    }
                }
            }

        }

        public bool Validar_turno()// testar se pelo meno existe uma jogada válida no turno seguinte
        {
  
            for(int i = 0; i < this.jogadas.Length; i++)
            {
                if (this.jogadas[i] == 0)
                {
                    break;
                }

                for(int j = 0; j < this.vet_peoes.Length; j++)
                {
                    if (this.vet_peoes[i].coluna == 0)
                    {
                        if (this.jogadas[i] == 6)
                        {
                            return true;
                        }
                    }

                    else if (this.vet_peoes[j].coluna + this.jogadas[i] <= 57)
                    {
                        return true;
                    }
                }

            }

            return false;

        }

        public bool Validar_jogada(int jogada_escolhida,int peao_escolhido_coluna,out bool sair_da_casa)
        {
           sair_da_casa =  false;
           bool jogada_invalida = true;

            if (peao_escolhido_coluna == 0)
            {
                if (jogada_escolhida == 6)
                {
                    sair_da_casa = true;
                    jogada_invalida = false;
                }
            }

            else if (peao_escolhido_coluna + jogada_escolhida <= 57)
            {
               jogada_invalida =  false;
            }

            if (!jogada_invalida)
            {
                for (int j = 0; j < this.jogadas.Length; j++)
                {
                    if (this.jogadas[j] == jogada_escolhida)
                    {
                        this.jogadas[j] = 0;
                        break;
                    }
                }

            }

            return jogada_invalida;

        }

        public int Contar_jogadas_restantes()
        {
            int jogadas_restantes = 0;

            for (int j = 0; j < this.jogadas.Length; j++)
            {
                if (this.jogadas[j] != 0)
                {
                    jogadas_restantes++;
                }
            }

            return jogadas_restantes;
        }

        public void Verificar_jogadas_disponiveis(int peao_coluna,int[] jogadas) //Mostrar ao jogador as jogadas disponíveis para determinado peão
        {
            for (int i = 0; i < this.jogadas.Length; i++)
            {
                if (this.jogadas[i] == 0)
                {
                    break;
                }

                if (peao_coluna == 0)
                {
                    if (this.jogadas[i] == 6)
                    {
                        jogadas[i] = this.jogadas[i];
                    }
                }

                else if (peao_coluna + this.jogadas[i] <= 57)
                {
                    jogadas[i] = this.jogadas[i];
                }

            }
        }

        public void Verificar_peoes_disponiveis(int[] peoes)
        {
            for (int i = 0; i < this.vet_peoes.Length; i++)
            {
                for (int j = 0; j < this.jogadas.Length; j++)
                {
                    if (this.vet_peoes[i].coluna == 0)
                    {
                        if (this.jogadas[j] == 6)
                        {
                            peoes[i] = this.vet_peoes[i].numero;
                            break;
                        }
                    }

                    else if (this.vet_peoes[i].coluna + this.jogadas[j] <= 57)
                    {
                        peoes[i] = this.vet_peoes[i].numero;
                        break;
                    }
                }

            }
       
        }

    }
}
