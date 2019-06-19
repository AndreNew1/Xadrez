
namespace Tabuleiro
{
    class Posicao
    {
        public int Coluna { get; set; }
        public int Linha { get; set; }

        public Posicao(int linha, int coluna)
        {
            Coluna = coluna;
            Linha = linha;
        }
        public void DefinirValores(int Linha,int Coluna)
        {
            this.Linha = Linha;
            this.Coluna = Coluna;

        }
        public override string ToString()
        {
            return Linha
                 + ", "
                 + Coluna;
        }
    }
}
