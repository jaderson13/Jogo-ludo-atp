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
        //public Peao[,] percurso = new Peao[4, 58];
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
        public int Jogar_dado()
        {
            int numero = 0;
            int i = 0;

            do
            {
                Random dado = new Random();
                numero = dado.Next(1, 7);
                this.jogadas[i] = numero;
                i++;

            } while (numero == 6);


            return numero;
        }

        public bool Mover_peao(int peao_escolhido,int num_dado,ref bool checkpoint,ref bool casa_inicial,out bool casa_final)
        {
            //1 jogada por vez para não precisar chamar método dentro do outro

            peao_escolhido -= 1;
            int coluna_peao, casas_iniciais_vazias = 0,index_jogada=0;
            int[] vet_checkpoints = { 1, 8, 21, 34, 47, 57 };
            bool casas_iniciais_cheias = true;
            casa_final = true;

            //Gastar a jogada atual
            for (int j = 0; j < this.jogadas.Length; j++)
            {
                if (this.jogadas[j] == num_dado)
                {
                    this.jogadas[j] = 0;
                    index_jogada = j;
                    break;
                }
            }

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
                            casa_inicial = true;
                            checkpoint = true;
                            return true;
                        }
                    }
                }
            }
         
            //jogada realizada se o peão em questão estiver fora da casa 0 ou se pelo menos 1 peão saiu da casa 0
            if(!casas_iniciais_cheias)
            {
                for (int i = 1; i < this.vet_peoes.GetLength(1); i++)
                {
                  
                        if (this.percurso[peao_escolhido, i].numero == peao_escolhido)
                        {
                            if (i + num_dado < this.percurso.GetLength(1))
                            {
                                this.percurso[peao_escolhido, i + num_dado] = this.percurso[peao_escolhido, i];
                                this.percurso[peao_escolhido, i + num_dado].coluna = i + num_dado;
                                this.percurso[peao_escolhido, i] = null;
                                coluna_peao = i + num_dado;
                                casa_inicial = false;
                                checkpoint = false;

                                for (int j = 0; j < vet_checkpoints.Length; j++)
                                {
                                    if (coluna_peao == vet_checkpoints[i])
                                    {
                                        checkpoint = true;
                                    }
                                }

                                if (coluna_peao == 57)
                                {
                                    for(int j = 0; j < this.casa_vitoria.Length; j++)
                                    {
                                        if (this.casa_vitoria[j] != null)
                                        {
                                            this.casa_vitoria[j] = this.percurso[peao_escolhido, i + num_dado];
                                        }                               
                                    }
                                }
                                return true;
                            }

                            else
                            {
                                casa_final = true;
                                this.jogadas[index_jogada] = num_dado;
                            }
                        }
                    
                    
                }
            }

            return false;
        }

        //Mostrar quais peões estão disponíveis para o jogador
        public int[] Peoes_disponiveis(int[] jogadas)
        {
            int[] peoes = new int[4];

            for(int i = 0; i < this.percurso.GetLength(0); i++)
            {
                for(int j = 0; j < this.percurso.GetLength(1)-1; j++)
                {
                    if (this.percurso[i, j] != null)
                    {
                        if (j == 0)
                        {
                            for (int k = 0; k < jogadas.Length; k++)
                            {
                                if (jogadas[k] == 6)
                                {

                                }
                            }
                        }
                    }  
                }
            }

            return peoes;
        }
    }
}
