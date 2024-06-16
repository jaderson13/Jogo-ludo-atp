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
        public Peao[] casa_jogador = new Peao[4];
        public Peao[] casa_vitoria = new Peao[4];
        public Peao[,] percurso = new Peao[4, 57];
        public ConsoleColor cor;
        public int[] jogadas = new int[3];
    
        
        public Jogador(ConsoleColor cor)
        {
            this.cor = cor;
        }

        public void Criar_peoes(ConsoleColor cor)
        {
            for(int i = 0; i < this.casa_jogador.Length; i++)
            {
                Peao peao = new Peao(cor,i);
                casa_jogador[i] = peao;
            }
        }

        public bool Mover_peao(int peao_escolhido,int num_dado,ref bool checkpoint,ref bool casa_inicial)
        {
            //1 jogada por vez para não precisar chamar método dentro do outro

            peao_escolhido -= 1;
            int coluna_peao;
            int[] vet_checkpoints = { 0, 8, 21, 34, 47 };
            bool vet_cheio = true;

            for (int j = 0; j < this.jogadas.Length; j++)
            {
                if (this.jogadas[j] == num_dado)
                {
                    this.jogadas[j] = 0;
                    break;
                }
            }

            //contar quantos peões ainda não saíram da casa principal

            for (int i = 0; i < this.casa_jogador.Length; i++)
            {
                if (this.casa_jogador[i] == null)
                {
                    vet_cheio = false;
                }
            }


            if (num_dado == 6)
            {
                for (int i = 0; i < this.casa_jogador.Length; i++)
                {
                    if (peao_escolhido == this.casa_jogador[i].numero)
                    {
                        this.percurso[peao_escolhido, 0] = this.casa_jogador[i];
                        this.casa_jogador[i] = null;
                        casa_inicial = true;
                        checkpoint = true;
                        return true;
                    }
                }
            }
           
            else if(!vet_cheio)
            {
                for (int i = 0; i < this.percurso.Length; i++)
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
