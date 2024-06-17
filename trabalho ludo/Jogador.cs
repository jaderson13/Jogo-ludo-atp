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
        public Peao[,] percurso = new Peao[4, 58];
        public ConsoleColor cor;
        public int[] jogadas = new int[3];
    
        
        public Jogador(ConsoleColor cor)
        {
            this.cor = cor;
        }

        public void Criar_peoes()
        {
            for(int i = 0; i < this.percurso.GetLength(0); i++)
            {
                Peao peao = new Peao(this.cor,i,0);
                percurso[i,0] = peao;
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
            int coluna_peao, casas_vazias = 0,index_jogada=0;
            int[] vet_checkpoints = { 1, 8, 21, 34, 47, 57 };
            bool casas_cheias = true;
            casa_final = true;

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

            for (int i = 0; i < this.percurso.GetLength(0); i++)
            {
                if (this.percurso[i,0] == null)
                {
                    casas_cheias = false;
                    casas_vazias += 1;
                }
            }


            if (num_dado == 6 && casas_vazias <= 3)
            {
 
                for (int i = 0; i < this.percurso.GetLength(0); i++)
                {
                    if (this.percurso[i, 0] != null)
                    {
                        if (peao_escolhido == this.percurso[i, 0].numero)
                        {
                            this.percurso[i, 1] = this.percurso[i, 0];
                            this.percurso[i, 1].coluna = 1;
                            this.percurso[i, 0] = null;
                            casa_inicial = true;
                            checkpoint = true;
                            return true;
                        }
                    }

                }
            }
         
            //jogada realizada se o peão em questão estiver fora da casa 0 ou se pelo menos 1 peão saiu da casa 0
            if(!casas_cheias)
            {
                for (int i = 1; i < this.percurso.GetLength(1); i++)
                {
                    if (this.percurso[peao_escolhido, i] != null)
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
            }

            return false;
        }
    }
}
