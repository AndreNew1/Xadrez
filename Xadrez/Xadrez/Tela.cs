using System;
using Tabuleiro;
using System.Collections.Generic;
namespace Xadrez
{
    class Tela
    {
        public static void ImprimirPartida(PartidaDeXadrez Partida)
        {
            ImprimirTabuleiro(Partida.Tab);
            Console.WriteLine();
            ImprimirPecasCapturadas(Partida);
            Console.WriteLine();
            Console.WriteLine("Turno: " + Partida.Turno);
            if (!Partida.Terminada)
            {
                Console.WriteLine("Aquardando jogada: " + Partida.JogadorAtual);
                if (Partida.Xeque)
                {
                    Console.WriteLine("XEQUE!");
                }
            }
            else
            {
                Console.WriteLine("XEQUEMATE!");
                Console.WriteLine("Vencedor: " + Partida.JogadorAtual);
            }
        }
        public static void ImprimirPecasCapturadas(PartidaDeXadrez Partida)
        {
            Console.WriteLine("Peças Capturadas: ");
            Console.Write("Brancas: ");
            ImprimirConjunto(Partida.PecasCapturadas(Cor.branca));
            Console.WriteLine();
            Console.Write("Pretas: ");
            ConsoleColor aux = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Yellow;
            ImprimirConjunto(Partida.PecasCapturadas(Cor.preto));
            Console.ForegroundColor = aux;
            Console.WriteLine();
        }

        public static void ImprimirConjunto(HashSet<Peca> conjunto)
        {
            Console.Write("[");
            foreach(Peca x in conjunto)
            {
                Console.Write(x + " ");
            }
            Console.Write("]");
        }
        public static void ImprimirTabuleiro(tabuleiro tab)
        {
            for(int i = 0; i < tab.Linha; i++)
            {
                Console.Write(8 - i + " ");
                for (int j = 0; j < tab.Coluna; j++)
                {
                    if (tab.Peca(i, j) == null)
                    {
                        Console.Write("- ");
                    }
                    else
                    {

                        ImprimirPeca(tab.Peca(i, j));
                        Console.Write("");
                    }
                }
                Console.WriteLine();
            }
            Console.WriteLine("  a b c d e f g h");
        }
        public static void ImprimirTabuleiro(tabuleiro tab,bool[,] posicoesPossiveis)
        {
            ConsoleColor Original = Console.BackgroundColor;
            ConsoleColor fundoNew = ConsoleColor.DarkGray;

            for (int i = 0; i < tab.Linha; i++)
            {
                Console.Write(8 - i + "  ");
                for (int j = 0; j < tab.Coluna; j++)
                {
                    if (posicoesPossiveis[i, j])
                    {
                        Console.BackgroundColor = fundoNew;
                    }
                    else
                    {
                        Console.BackgroundColor = Original;
                    }
                    ImprimirPeca(tab.Peca(i, j));
                    Console.BackgroundColor = Original;
                }
                Console.WriteLine();
            }
            Console.WriteLine("  a b c d e f g h");
            Console.BackgroundColor = Original;
        }

        public static PosicaoXadrez LerPosicaoXadrez()
        {
            string s = Console.ReadLine();
            char linha = s[0];
            int coluna  = int.Parse(s[1] + "");
            return new PosicaoXadrez(linha,coluna);
        }
        public static void ImprimirPeca(Peca peca)
        {
            if (peca==null)
            {
                Console.Write("-");
            }
            else if (peca.Cor == Cor.branca)
                Console.Write(peca);
            else
            {
                ConsoleColor aux = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write(peca);
                Console.ForegroundColor = aux;
            }
            Console.Write(" ");
        }
    }
}
