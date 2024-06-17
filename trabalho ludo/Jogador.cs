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

        public void Criar_peoes(ConsoleColor cor)
        {
            for(int i = 0; i < this.percurso.GetLength(0); i++)
            {
                Peao peao = new Peao(cor,i);
                percurso[i,0] = peao;
            }
        }

        public bool Mover_peao(int peao_escolhido,int num_dado,ref bool checkpoint,ref bool casa_inicial)
        {
            //1 jogada por vez para não precisar chamar método dentro do outro

            peao_escolhido -= 1;
            int coluna_peao, casas_vazias = 0;
            int[] vet_checkpoints = { 1, 8, 21, 34, 47 };
            bool casas_cheias = true;

            for (int j = 0; j < this.jogadas.Length; j++)
            {
                if (this.jogadas[j] == num_dado)
                {
                    this.jogadas[j] = 0;
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
                    if (peao_escolhido == this.percurso[i,0].numero)
                    {
                        this.percurso[i, 1] = this.percurso[i,0];
                        this.percurso[i,0] = null;
                        casa_inicial = true;
                        checkpoint = true;
                        return true;
                    }
                }
            }
           
            else if(!casas_cheias)
            {
                for (int i = 1; i < this.percurso.GetLength(1); i++)
                {
                    if (this.percurso[peao_escolhido, i].numero == peao_escolhido)
                    {
                        this.percurso[peao_escolhido, i + num_dado] = this.percurso[peao_escolhido, i];
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

                        return true;
                    }

                }
            }

            return false;
        }
    }
}
