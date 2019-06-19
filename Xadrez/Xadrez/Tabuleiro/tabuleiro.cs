using System;
using System.Collections.Generic;
using System.Text;

namespace Tabuleiro
{
      class tabuleiro
    {
        public int Coluna { get; set; }
        public int Linha { get; set; }
        private Peca[,] pecas;

        public tabuleiro(int linhas,int colunas)
        {
            Linha = linhas;
            Coluna = colunas;
            pecas = new Peca[linhas, colunas];
        }
        public Peca Peca(int linha,int coluna)
        {
            return pecas[linha, coluna];
        }
        public Peca Peca(Posicao pos)
        {
            return pecas[pos.Linha, pos.Coluna];
        }
        public bool ExistePeca(Posicao pos)
        {
            ValidarPosicao(pos);
            return Peca(pos) != null;
        }
        public void ColocarPeca(Peca p,Posicao pos)
        {
            if (ExistePeca(pos))
            {
                throw new TabuleiroException("ja existe peça nessa posição");
            }
            pecas[pos.Linha, pos.Coluna] = p;
            p.Posicao = pos;
        }
        public Peca RetirarPeca(Posicao pos)
        {
            if (ExistePeca(pos)==false)
            {
                return null;
            }
            Peca aux = Peca(pos);
            pecas[pos.Linha, pos.Coluna]= null;
            return aux;

        }
        public bool PosicaoValida(Posicao pos)
        {
            if (pos.Linha < 0 || pos.Linha >= Linha || pos.Coluna < 0 || pos.Coluna >= Coluna)
                return false;
            else return true;
        }
        public void ValidarPosicao(Posicao pos)
        {
            if (!PosicaoValida(pos))
                throw new TabuleiroException("Posição Invalida");
        }
    }
}
