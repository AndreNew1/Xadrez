using Tabuleiro;
using Xadrez;
namespace Xadrez
{
    class Rei:Peca
    {
        private PartidaDeXadrez partida;
        public Rei(tabuleiro tab,Cor cor,PartidaDeXadrez partida) : base(cor, tab)
        {
            this.partida = partida;
        }
        public override string ToString()
        {
            return "R";
        }
        private bool Podemover(Posicao pos)
        {
            Peca p = Tab.Peca(pos);
            return p == null || p.Cor != this.Cor;
        }

        private bool TesteTorreRoque(Posicao pos)
        {
            Peca p = Tab.Peca(pos);
            return p != null && p is Torre && p.Cor == Cor && p.QtdMovimentos == 0;
        }
        public override bool[,] MovimentosPossiveis()
        {
            bool[,] mat = new bool[Tab.Linha, Tab.Coluna];

            Posicao pos = new Posicao(0, 0);

            //acima
            pos.DefinirValores(Posicao.Linha - 1, Posicao.Coluna);
            if (Tab.PosicaoValida(pos) && Podemover(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;
            }

           //diagonal invertida acima
            pos.DefinirValores(Posicao.Linha - 1, Posicao.Coluna + 1);
            if (Tab.PosicaoValida(pos) && Podemover(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;
            }

            //Direita
            pos.DefinirValores(Posicao.Linha, Posicao.Coluna + 1);
            if (Tab.PosicaoValida(pos) && Podemover(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;
            }

            //Diagonal Principal baixo
            pos.DefinirValores(Posicao.Linha+1,Posicao.Coluna + 1);
            if (Tab.PosicaoValida(pos) && Podemover(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;
            }
            //baixo
            pos.DefinirValores(Posicao.Linha + 1, Posicao.Coluna);
            if (Tab.PosicaoValida(pos) && Podemover(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;
            }
            //Diagonal invertida baixo
            pos.DefinirValores(Posicao.Linha + 1, Posicao.Coluna - 1);
           if (Tab.PosicaoValida(pos) && Podemover(pos))
           {
                mat[pos.Linha, pos.Coluna] = true;
           }
            //Direita
            pos.DefinirValores(Posicao.Linha, Posicao.Coluna - 1);
            if (Tab.PosicaoValida(pos) && Podemover(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;
            }
            //diagonal Principal acima
            pos.DefinirValores(Posicao.Linha - 1, Posicao.Coluna - 1);
            if (Tab.PosicaoValida(pos) && Podemover(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;
            }

            //Jogada Especial
            if (QtdMovimentos == 0 && !partida.Xeque)
            {
                //Roque Pequeno
                Posicao posT1 = new Posicao(Posicao.Linha, Posicao.Coluna + 3);
                if (TesteTorreRoque(posT1))
                {
                    Posicao p1 = new Posicao(Posicao.Linha, Posicao.Coluna + 1);
                    Posicao p2 = new Posicao(Posicao.Linha, Posicao.Coluna + 2);
                    if (Tab.Peca(p1) == null && Tab.Peca(p2) == null)
                    {
                        mat[Posicao.Linha, Posicao.Coluna + 2] = true;
                    }
                }
                //Roque grade
                Posicao posT2 = new Posicao(Posicao.Linha, Posicao.Coluna - 4);
                if (TesteTorreRoque(posT2))
                {
                    Posicao p1 = new Posicao(Posicao.Linha, Posicao.Coluna - 1);
                    Posicao p2 = new Posicao(Posicao.Linha, Posicao.Coluna - 2);
                    Posicao p3 = new Posicao(Posicao.Linha, Posicao.Coluna - 3);
                    if (Tab.Peca(p1) == null && Tab.Peca(p2) == null&&Tab.Peca(p3)==null)
                    {
                        mat[Posicao.Linha, Posicao.Coluna - 2] = true;
                    }
                }
            }
            return mat;
        }

    }
}
