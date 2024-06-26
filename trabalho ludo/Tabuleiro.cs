using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using trabalho_ludo;

namespace trabalho_ludo
{
    internal class Tabuleiro
    {
        public string[,] tabuleiro;
        public string log_partida;
        public StreamWriter log_escrito = new StreamWriter("log.txt", false, Encoding.UTF8);
        public StreamReader log_leitura = new StreamReader("D:\\Usuário\\Documentos\\- Algoritmos C#\\- Trabalho ludo\\trabalho ludo\\bin\\Debug\\log.txt", Encoding.UTF8);

        public Jogador jogador = new Jogador();

        //Matriz da posição 0 do tabuleiro de cada jogador.
        string[,] Coord_IndiceZero = {
            //J0 Jogador Amarelo Y1 Y2 quebra linha Y3 Y4
            {"1.2.4","2.2.6","3.3.4","4.3.6"}, //peao.posicao(linha.coluna)
            //J1 Jogador Azul B1 B2 quebra linha B3 B4
            {"1.2.22","2.2.24","3.3.22","4.3.24"},
            //J2 Jogador Vermelho R1 R2 quebra linha R3 R4
            {"1.11.4","2.11.6","3.12.4","4.12.6"},
            //J3 Jogador Verde G1 G2 quebra linha G3 G4
            {"1.11.22","2.11.24","3.12.22","4.12.24"}
        };

        //Matriz de coordenadas do tabuleiro conforme a trilha do jogador [4,57]
        string[,] Coord_TrilhaJogadores = {
        //Amarelo
            {"6.1","6.2","6.3","6.4","6.5","5.6","4.6","3.6","2.6","1.6","0.6","0.7","0.8","1.8","2.8","3.8","4.8","5.8","6.9","6.10","6.11","6.12","6.13","6.14","7.14","8.14","8.13","8.12","8.11","8.10","8.9","9.8","10.8","11.8","12.8","13.8","14.8","14.7","14.6","13.6","12.6","11.6","10.6","9.6","8.5","8.4","8.3","8.2","8.1","8.0","7.0","7.1","7.2","7.3","7.4","7.5","7.6"},

        //Azul
            {"1.8","2.8","3.8","4.8","5.8","6.9","6.10","6.11","6.12","6.13","6.14","7.14","8.14","8.13","8.12","8.11","8.10","8.9","9.8","10.8","11.8","12.8","13.8","14.8","14.7","14.6","13.6","12.6","11.6","10.6","9.6","8.5","8.4","8.3","8.2","8.1","8.0","7.0","6.0","6.1","6.2","6.3","6.4","6.5","5.6","4.6","3.6","2.6","1.6","0.6","0.7","1.7","2.7","3.7","4.7","5.7","6.7"},

        //Vermelho
            
            {},

        //Verde
            
            {}
        };


        const int NUM = 15;

        //Get an array with the values of ConsoleColor enumeration members.
        //vetor com as cores disponíveis
        ConsoleColor[] cores = (ConsoleColor[])ConsoleColor.GetValues(typeof(ConsoleColor));
        /*//Lista as cores disponíveis no vetor cores
        for(int i=0; i<15; i++){
            Console.WriteLine(cores[i]);
        }*/

        ConsoleColor backgroundCorrente = Console.BackgroundColor; //Preto
        ConsoleColor foregroundCorrente = Console.ForegroundColor; //Branco

        //Matriz para armazenar as posições dos peões, tridimensional, com 4 jogadores, 4 peões, 1 posição(valor do índice do peão no seu vetor de trilha)
        public int[,,] posicoesPeoes = new int[4, 4, 1]; //4*4*1 = 16 posições.

        ConsoleColor corPadrao = cores[0]; //Começa com a cor preta
        char[,] tabuleiro = new char[NUM, NUM * 2];

        //Exemplo vetor peão 1, amarelo, posição inicial 2,5
        //int[] J0_peao1 = new int[58]{}; //

        // Preenchendo o tabuleiro com 'P' e 'i' para identificar as casas pares e impares.
        Console.WriteLine();
        
        for (int i = 0; i<NUM; i++)
        {
            //Quebra de linha dupla pra separar as linhas
            Console.WriteLine("\n");
            Console.Write("\t\t");
            
            //Console.ResetColor();
            //Console.BackgroundColor = ConsoleColor.White; //Para cor branca, demais usa corAtual atribuida pelo vetor de cores do ConsoleColor
            //Console.BackgroundColor = corPadrao;
            Console.ForegroundColor = ConsoleColor.Black;

            int peao = -1;
        int linha = -1; int coluna = -1; //Inicializado com menos 1 para não iniciar com posição valida(0 a 57).
        bool caracter_Impresso = false; //Considera inicialmente que o caracter relativo ao peão não foi impresso.
            
            for (int j = 0; j<NUM* 2; j++)
            {
                //Recebe P ou i para indicar coluna para ou ímpar para referência de impressão
                if (j % 2 == 0)
                {
                    tabuleiro[i, j] = ' ';
                }
                else
                {
                    tabuleiro[i, j] = ' ';
                }


//yellow 6(darkyellow) e 14(yellow)
if (i == 0 && (j >= 0 && j <= 11))
{ //traço horizontal superior
    Console.BackgroundColor = cores[14];//yellow

}
else if ((i > 0 && i <= 5) && (j <= 1))
{ //traço vertical esquerdo
    Console.BackgroundColor = cores[14];//yellow

}
else if ((i > 0 && i <= 5) && (j >= 10 && j <= 11))
{ //traço vertical direito
    Console.BackgroundColor = cores[14];//yellow

}
else if (i == 5 && (j >= 0 && j <= 11))
{ //traço horizontal inferior
    Console.BackgroundColor = cores[14];//yellow

}
else if (i == 6 && (j >= 2 && j <= 3))
{ //saída, posição 1 do jogador amarelo
    Console.BackgroundColor = cores[14];//yellow

}
else if (i == 7 && (j >= 2 && j <= 13))
{ //curso final horizontal, jogador amarelo
    Console.BackgroundColor = cores[14];//yellow


    //Blue    
}
else if (i == 0 && (j >= 18 && j <= 29))
{ //traço horizontal superior
    Console.BackgroundColor = cores[1];//blue

}
else if ((i > 0 && i <= 5) && (j >= 18 && j <= 19))
{ //traço vertical esquerdo
    Console.BackgroundColor = cores[1];//blue

}
else if ((i > 0 && i <= 5) && (j >= 28 && j <= 29))
{ //traço vertical direito
    Console.BackgroundColor = cores[1];//blue;

}
else if (i == 5 && (j >= 18 && j <= 29))
{ //traço horizontal inferior
    Console.BackgroundColor = cores[1];//blue

}
else if (i == 1 && (j >= 16 && j <= 17))
{ //saída, posição 1 do jogador azul
    Console.BackgroundColor = cores[1];//blue

}
else if ((i >= 1 && i <= 6) && (j >= 14 && j <= 15))
{ //curso final vertical, jogador azul
    Console.BackgroundColor = cores[1];//blue


    //Red
}
else if (i == 9 && (j >= 18 && j <= 29))
{ //traço horizontal superior
    Console.BackgroundColor = cores[4];//red

}
else if ((i >= 10 && i <= 14) && (j >= 18 && j <= 19))
{ //traço vertical esquerdo
    Console.BackgroundColor = cores[4];//red

}
else if ((i >= 10 && i <= 14) && (j >= 28 && j <= 29))
{ //traço vertical direito
    Console.BackgroundColor = cores[4];//red

}
else if (i == 14 && (j >= 20 && j <= 27))
{ //traço horizontal inferior
    Console.BackgroundColor = cores[4];//red

}
else if (i == 8 && (j >= 26 && j <= 27))
{ //saída, posição 1 do jogador red
    Console.BackgroundColor = cores[4];//red

}
else if ((i == 7) && (j >= 16 && j <= 27))
{ //curso final vertical, jogador red
    Console.BackgroundColor = cores[4];//red


    //Green 2(darkgreen) e 10(green)
}
else if (i == 9 && (j >= 0 && j <= 11))
{ //traço horizontal superior
    Console.BackgroundColor = cores[2];//green

}
else if ((i >= 10 && i <= 13) && (j >= 0 && j <= 1))
{ //traço vertical esquerdo
    Console.BackgroundColor = cores[2];//green

}
else if ((i >= 10 && i <= 13) && (j >= 10 && j <= 11))
{ //traço vertical direito
    Console.BackgroundColor = cores[2];//green

}
else if (i == 14 && (j >= 0 && j <= 11))
{ //traço horizontal inferior
    Console.BackgroundColor = cores[2];//green

}
else if (i == 13 && (j >= 12 && j <= 13))
{ //saída, posição 1 do jogador green
    Console.BackgroundColor = cores[2];//green

}
else if ((i >= 8 && i <= 13) && (j >= 14 && j <= 15))
{ //curso final vertical, jogador green
    Console.BackgroundColor = cores[2];//green


    //Locais não percorridos black
}
else if ((i == 6) && (j >= 12 && j <= 13))
{ //Canto superior esquerdo do centro não circulável.
    Console.BackgroundColor = cores[8];//darkgray

}
else if ((i == 6) && (j >= 16 && j <= 17))
{ //Canto superior direito do centro não circulável.
    Console.BackgroundColor = cores[8];//darkgray

}
else if ((i == 8) && (j >= 12 && j <= 13))
{ //Canto inferior esquerdo do centro não circulável.
    Console.BackgroundColor = cores[8];//darkgray

}
else if ((i == 8) && (j >= 16 && j <= 17))
{ //Canto inferior direito do centro não circulável.
    Console.BackgroundColor = cores[8];//darkgray

}
else if ((i == 7) && (j >= 14 && j <= 15))
{ //Centro do tabuleiro não circulável.
    Console.BackgroundColor = cores[8];//darkgray


    //Checkpoints do percurso, exceto saídas casas de jogadores; 3(darkcyan) e 11(cyan)
}
else if ((i == 2) && (j >= 12 && j <= 13))
{ //Parte lateral do jogador amarelo
    Console.BackgroundColor = cores[11];//cyan
    tabuleiro[2, 13] = '*';

}
else if ((i == 6) && (j >= 24 && j <= 25))
{ //Parte inferior do jogador azul
    Console.BackgroundColor = cores[11];//cyan
    tabuleiro[6, 25] = '*';

}
else if ((i == 12) && (j >= 16 && j <= 17))
{ //Parte Lateral do jogador vermelho
    Console.BackgroundColor = cores[11];//cyan
    tabuleiro[12, 17] = '*'; //Locais com colunas impares sinalizando o checkpoint, o peão ficará na coluna par do checkpoint, ex. tabuleiro[12,16].

}
else if ((i == 8) && (j >= 4 && j <= 5))
{ //Parte Superior do jogador verde
    Console.BackgroundColor = cores[11];//cyan
    tabuleiro[8, 5] = '*'; //Locais com colunas impares sinalizando o checkpoint, o peão ficará na coluna par do checkpoint, ex. tabuleiro[8,4].

    //Locais possíveis para imprimir um peão ou um grupo de peões do mesmo jogador, ou de jogadores diferentes em checkpoints
}
else if (i == VerificarIJ(i, j, Coord_IndiceZero, Coord_TrilhaJogadores, out linha, out coluna, out peao))
{ //Verifica se a coordenada(i,j) do tabuleiro se trata de uma posição de possível local de um peão
    if (VerificarPeaoPosZero(posicoesPeoes, peao, linha, coluna))
    { //Retorno 1 = true. Se retornar true significa que a posição(linha,coluna) anterior se trata da posição de um peão na sua posição 0;
      //Cor do peao impresso conforme cor do jogador
        Console.ForegroundColor = jogador[linha].cor; //Console.ForegroundColor = ConsoleColor.${ };
        Console.BackgroundColor = ConsoleColor.White;
        Console.Write(peao);
        caracter_Impresso = true;

    }
    else if (VerificarPeaoPosTrilha())
    { // após verificado, verificar se terá algum peao sobrepondo, mesmo jogador ou jogadores diferentes

    }

}
else
{ //senão branco de fundo.
    Console.BackgroundColor = ConsoleColor.White;
}

//Posições dos peões
/*else if(i==VerificarLinha(i,Coord_IndiceZeroPeao) && j==VerificarColuna()){ //Apenas para verificar se na matriz de coord pode haver algum peão deste setor de impressão

   //Locais com posição de cada peão de cada jogador, ex. tabuleiro[2,12] = '1'; com foreground da cor do jogador amarelo. por exemplo. 
}*/ //

//criar if para verificar se o peão está no setor de impressão, se esta com a flag true (if(!flag) imprimir)
Console.Write(tabuleiro[i, j]);
//separa as colunas
if (j % 2 == 1 && j > 0)
{
    Console.Write("\t");
}
            }
            Console.ResetColor();
            //Quebra de linha dupla pra separar as linhas
            
        }
        Console.WriteLine("\n");

Console.ReadKey(); //Aguarda ler tecla sem entrada de dados.
    }


    
    //Validar linha do peão a ser impresso, retorna o i para confirmar que se trata do i,j a ser impresso com algum peão.
    static int VerificarIJ(int i_tabuleiro, int j-tabuleiro, string[,] Coord_IndiceZero, string[,] Coord_TrilhaJogadores, out int linha, out int coluna)
{

    //Verificar i,j Coord_IndiceZero
    for (int linhas = 0; linhas < Coord_IndiceZero.GetLength(0); linhas++)
    {
        for (int colunas = 0; colunas < Coord_IndiceZero.GetLength(1); colunas++)
        {
            string coord = Coord_IndiceZero;
            //Dividir a string de coordenada pelos pontos
            string[] partes = coordenada.Split('.');
            peao = partes[0];
            linha = partes[1];
            coluna = partes[2];

            //Condicional, se sim, significa que a posição i,j do tabuleiro a ser impressa pode ser de 1 peão na posição 0;
            if ((i_tabuleiro == linha) && (j_tabuleiro == coluna))
            {

                //Se retornar True, significa que a posição(linha,coluna) anterior, se trata da posição de um peão na sua posição 0(peao.coluna;
                if (VerificarPeaoPosZero(linha, coluna))
                {
                    return linha;
                }
                else
                {
                    return -1; //Caso ao se verificar peão e o mesmo não estiver na posição zero; invalidando o else if( VerificarIJ() )
                }
            }
        }
    }


    //*********** continuar, preparar função VerificarPeaoPosTrilha, de 1 a 57. *************//
    //Verificar i,j Coord_TrilhaJogadores
    for (int linhas = 0; linhas < Coord_TrilhaJogadores.GetLength(0); linhas++)
    {
        for (int colunas = 0; colunas < Coord_TrilhaJogadores.GetLength(1); colunas++)
        {
            string coord = Coord_TrilhaJogadores;

            //Dividir a string de coordenada pelo ponto
            string[] partes = coordenada.Split('.');
            //peao = partes[0];
            linha = partes[0];
            coluna = partes[1];

            //Condicional, se sim, significa que a posição i,j do tabuleiro a ser impressa pode ser de 1 peão na posição 0;
            if ((i_tabuleiro == linha) && (j_tabuleiro == coluna))
            {

                //Se retornar True, significa que a posição(linha,coluna) anterior, se trata da posição de um peão na sua posição 0(peao.coluna;
                if (VerificarPeaoPosTrilha(linha, coluna))
                {
                    return linha;
                }
                else
                {
                    return -1; //Caso ao se verificar peão e o mesmo não estiver na posição zero; invalidando o else if( VerificarIJ() )
                }
            }
        }
    }
}

static int VerificarPeaoPosZero(out int linha, out int coluna, int[,] posicoesPeoes)
{

    for (int i = 0; i < 4; i++)
    {
        for (int j = 0; j < 4; j++)
        {
            if (posicoesPeoes[i, j, 0] == 0)
            { //Se peão na posição(coluna) = 0; 
                if ((i == linha) && (j == coluna))
                { //Se i=linha, significa que se trata do mesmo peão;
                    return 1;
                }
                else
                {
                    return 0;
                }
            }
        }
    }
}
        

    }
}