

namespace Tabuleiro
{
      
    abstract class Peca
    {
        public Posicao Posicao { get; set; }
        public Cor Cor { get; protected set; }
        public int QtdMovimentos { get; protected set;}
        public tabuleiro Tab { get; protected set; }


        public Peca(Cor cor,tabuleiro tab)
        {
            this.Cor = cor;
            this.Posicao = Posicao;
            this.QtdMovimentos = 0;
            this.Tab = tab;
        }
        public void IncrementarQtdMovimentos()
        {
            QtdMovimentos++;
        }
        public abstract bool[,] MovimentosPossiveis();
        public bool ExisteMovimentosPossiveis()
        {
            bool[,] mat = MovimentosPossiveis();
            for(int i = 0; i < Tab.Linha; i++)
            {
                for(int j = 0; j < Tab.Coluna; j++)
                {
                    if (mat[i, j])
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public void DecrementarQtdMovimentos()
        {
            QtdMovimentos--;
        }
        public bool MovimentoPossivel(Posicao pos)
        {
            return MovimentosPossiveis()[pos.Linha, pos.Coluna];
        }
    }
}
