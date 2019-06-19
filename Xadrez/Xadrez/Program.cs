using System;
using Tabuleiro;
namespace Xadrez
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                PartidaDeXadrez Partida = new PartidaDeXadrez();
                while (!Partida.Terminada)
                {
                    try
                    {
                        Console.Clear();
                        Tela.ImprimirPartida(Partida);
                        Console.WriteLine();
                        Console.Write("Origem: ");
                        Posicao origem = Tela.LerPosicaoXadrez().ToPosicao();
                        Partida.ValidarPosicaoDeOrigem(origem);
                        bool[,] posicoesPossiveis = Partida.Tab.Peca(origem).MovimentosPossiveis();

                        Console.Clear();
                        Tela.ImprimirTabuleiro(Partida.Tab, posicoesPossiveis);
                        Console.Write("Destino: ");
                        Posicao Destino = Tela.LerPosicaoXadrez().ToPosicao();
                        Partida.ValidarPosicaoDeDestino(origem, Destino);
                        Partida.RealizaJogada(origem, Destino);
                    }catch(TabuleiroException e)
                    {
                        Console.WriteLine(e.Message);
                        Console.ReadLine();
                    }
                }
            }
            catch (TabuleiroException e)
            {
                Console.WriteLine(e.Message);
            }
            Console.ReadLine();
        }
    }
}
