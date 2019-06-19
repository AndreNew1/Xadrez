using System.Collections.Generic;
using Tabuleiro;
using Xadrez;

namespace Xadrez
{
    class PartidaDeXadrez
    {
        public tabuleiro Tab { get; private set; }
        public int Turno { get; private set; }

        public Cor JogadorAtual { get; private set; }
        public bool Terminada { get; private set; }
        private HashSet<Peca> pecas;
        private HashSet<Peca> Capturadas;
        public bool Xeque { get; private set; }
        public Peca VulneravelEnPassant { get; private set; }
        public PartidaDeXadrez()
        {
            Tab = new tabuleiro(8, 8);
            Turno = 1;
            JogadorAtual = Cor.branca;
            Terminada = false;
            Xeque = false;
            VulneravelEnPassant = null;
            pecas = new HashSet<Peca>();
            Capturadas = new HashSet<Peca>();
            ColocarPecas();
        }
        public Peca ExecutarMovimento(Posicao origem,Posicao destino)
        {
            Peca p = Tab.RetirarPeca(origem);
            p.IncrementarQtdMovimentos();
            Peca PecaCap = Tab.RetirarPeca(destino);
            Tab.ColocarPeca(p, destino);
            if (PecaCap != null)
            {
                Capturadas.Add(PecaCap);
            }
            //Jogada Especial Roque Pequeno
            if(p is Rei&& destino.Coluna == origem.Coluna + 2)
            {
                Posicao origemT = new Posicao(origem.Linha, origem.Coluna + 3);
                Posicao destinoT = new Posicao(origem.Linha, origem.Coluna + 1);
                Peca T = Tab.RetirarPeca(origemT);
                T.IncrementarQtdMovimentos();
                Tab.ColocarPeca(T, destinoT);
            }
            //Roque Grande
            if (p is Rei && destino.Coluna == origem.Coluna -2)
            {
                Posicao origemT = new Posicao(origem.Linha, origem.Coluna - 4);
                Posicao destinoT = new Posicao(origem.Linha, origem.Coluna - 1);
                Peca T = Tab.RetirarPeca(origemT);
                T.IncrementarQtdMovimentos();
                Tab.ColocarPeca(T, destinoT);
            }


            if(p is Peao)
            {
                if (origem.Coluna != destino.Coluna&&PecaCap==null)
                {
                    Posicao posP;
                    if (p.Cor == Cor.branca)
                    {
                        posP = new Posicao(destino.Linha + 1, destino.Coluna);
                    }
                    else
                    {
                        posP = new Posicao(destino.Linha - 1, destino.Coluna);
                    }
                    PecaCap = Tab.RetirarPeca(posP);
                    Capturadas.Add(PecaCap);
                }
            }

            return PecaCap;
        }
        public void DesfazMovimento(Posicao origem,Posicao destino,Peca PecaCap)
        {
            Peca p = Tab.RetirarPeca(destino);
            p.DecrementarQtdMovimentos();
            if (PecaCap != null)
            {
                Tab.ColocarPeca(PecaCap, destino);
                Capturadas.Remove(PecaCap);
            }
            Tab.ColocarPeca(p, origem);

            //Roque Pequeno
            if (p is Rei && destino.Coluna == origem.Coluna + 2)
            {
                Posicao origemT = new Posicao(origem.Linha, origem.Coluna + 3);
                Posicao destinoT = new Posicao(origem.Linha, origem.Coluna + 1);
                Peca T = Tab.RetirarPeca(destinoT);
                T.DecrementarQtdMovimentos();
                Tab.ColocarPeca(T, origemT);
            }

            //Roque Grande
            if (p is Rei && destino.Coluna == origem.Coluna - 2)
            {
                Posicao origemT = new Posicao(origem.Linha, origem.Coluna - 4);
                Posicao destinoT = new Posicao(origem.Linha, origem.Coluna - 1);
                Peca T = Tab.RetirarPeca(origemT);
                T.IncrementarQtdMovimentos();
                Tab.ColocarPeca(T, destinoT);
            }

            //Jogada EnPassant
            if (p is Peao)
            {
                if (origem.Coluna != destino.Coluna && PecaCap == VulneravelEnPassant)
                {
                    Peca peao = Tab.RetirarPeca(destino);
                    Posicao posP;
                    if (p.Cor == Cor.branca)
                    {
                        posP = new Posicao(3, destino.Coluna);
                    }
                    else
                    {
                        posP = new Posicao(4, destino.Coluna);
                    }
                    Tab.ColocarPeca(peao, posP);
                }
            }
        
        }
        public void ColocarPecas(char coluna,int linha,Peca peca)
        {
            Tab.ColocarPeca(peca, new PosicaoXadrez(coluna, linha).ToPosicao());
            pecas.Add(peca);
        }
        private void ColocarPecas()
        {

            ColocarNovaPeca('a', 1, new Torre(Tab, Cor.branca));
            ColocarNovaPeca('b', 1, new Cavalo(Tab, Cor.branca));
            ColocarNovaPeca('c', 1, new Bispo(Tab, Cor.branca));
            ColocarNovaPeca('d', 1, new Rainha(Tab, Cor.branca));
            ColocarNovaPeca('e', 1, new Rei(Tab, Cor.branca,this));
            ColocarNovaPeca('f', 1, new Bispo(Tab, Cor.branca));
            ColocarNovaPeca('g', 1, new Cavalo(Tab, Cor.branca));
            ColocarNovaPeca('h', 1, new Torre(Tab, Cor.branca));
            ColocarNovaPeca('a', 2, new Peao(Tab, Cor.branca,this));
            ColocarNovaPeca('b', 2, new Peao(Tab, Cor.branca,this));
            ColocarNovaPeca('c', 2, new Peao(Tab, Cor.branca,this));
            ColocarNovaPeca('d', 2, new Peao(Tab, Cor.branca,this));
            ColocarNovaPeca('e', 2, new Peao(Tab, Cor.branca,this));
            ColocarNovaPeca('f', 2, new Peao(Tab, Cor.branca,this));
            ColocarNovaPeca('g', 2, new Peao(Tab, Cor.branca,this));
            ColocarNovaPeca('h', 2, new Peao(Tab, Cor.branca,this));

            ColocarNovaPeca('a', 8, new Torre(Tab, Cor.preto));
            ColocarNovaPeca('b', 8, new Cavalo(Tab, Cor.preto));
            ColocarNovaPeca('c', 8, new Bispo(Tab, Cor.preto));
            ColocarNovaPeca('d', 8, new Rainha(Tab, Cor.preto));
            ColocarNovaPeca('e', 8, new Rei(Tab, Cor.preto,this));
            ColocarNovaPeca('f', 8, new Bispo(Tab, Cor.preto));
            ColocarNovaPeca('g', 8, new Cavalo(Tab, Cor.preto));
            ColocarNovaPeca('h', 8, new Torre(Tab, Cor.preto));
            ColocarNovaPeca('a', 7, new Peao(Tab, Cor.preto,this));
            ColocarNovaPeca('b', 7, new Peao(Tab, Cor.preto,this));
            ColocarNovaPeca('c', 7, new Peao(Tab, Cor.preto,this));
            ColocarNovaPeca('d', 7, new Peao(Tab, Cor.preto,this));
            ColocarNovaPeca('e', 7, new Peao(Tab, Cor.preto,this));
            ColocarNovaPeca('f', 7, new Peao(Tab, Cor.preto,this));
            ColocarNovaPeca('g', 7, new Peao(Tab, Cor.preto,this));
            ColocarNovaPeca('h', 7, new Peao(Tab, Cor.preto,this));
        }
        public void RealizaJogada(Posicao origem,Posicao destino)
        {
            Peca PecaCap=ExecutarMovimento(origem, destino);

            if (EstaEmXeque(JogadorAtual))
            {
                DesfazMovimento(origem, destino, PecaCap);
                    throw new TabuleiroException("Você esta em xeque");
                
            }
            Peca p = Tab.Peca(destino);

            //Promoção

            if(p is Peao)
            {
                if ((p.Cor == Cor.branca && destino.Linha == 0) || (p.Cor == Cor.preto && destino.Linha == 7))
                {
                    p = Tab.RetirarPeca(destino);
                    pecas.Remove(p);
                    Peca Rainha = new Rainha(Tab, p.Cor);
                    Tab.ColocarPeca(Rainha, destino);
                    pecas.Add(Rainha);
                }
                
            }

            if (EstaEmXeque(Adversaria(JogadorAtual)))
            {
                Xeque = true;
            }
            else
            {
                Xeque = false;
            }
            if (TesteXequeMate(Adversaria(JogadorAtual)))
            {
                Terminada = true;
            }
            else
            {
                Turno++;
                MudaJogador();
            }

            Peca p = Tab.Peca(destino);
            //Jogada EnPassant
            if (p is Peao && (destino.Linha == origem.Linha - 2 || destino.Linha == origem.Linha + 2))
            {
                VulneravelEnPassant = p;
            }
            else
            {
                VulneravelEnPassant = null;
            }
        }
        public void ValidarPosicaoDeOrigem(Posicao pos)
        {
            if (Tab.Peca(pos) == null)
            {
                throw new TabuleiroException("Não existe a peca selecionada");
            }
            if (JogadorAtual != Tab.Peca(pos).Cor)
            {
                throw new TabuleiroException("A peça de origem escolhida não é sua");
            }
            if (!Tab.Peca(pos).ExisteMovimentosPossiveis())
            {
                throw new TabuleiroException("Não Existe Movimento Possivel");
            }
        }
        public void ValidarPosicaoDeDestino(Posicao origem,Posicao destino)
        {
            if (!Tab.Peca(origem).MovimentoPossivel(destino))
            {
                throw new TabuleiroException("posição de destino invalida");
            }

        }
        private void MudaJogador()
        {
            if (JogadorAtual == Cor.branca)
            {
                JogadorAtual = Cor.preto;
            }
            else
            {
                JogadorAtual = Cor.branca;
            }

        }
        public HashSet<Peca> PecasCapturadas(Cor cor)
        {
            HashSet<Peca> aux = new HashSet<Peca>();
            foreach(Peca x in Capturadas)
            {
                if (x.Cor == cor)
                {
                    aux.Add(x);
                }
            }
            return aux;
        }
        public HashSet<Peca> PecasEmJogo(Cor cor)
        {
            HashSet<Peca> aux = new HashSet<Peca>();
            foreach(Peca x in pecas)
            {
                if (x.Cor == cor)
                {
                    aux.Add(x);
                }
            }
             aux.ExceptWith(PecasCapturadas(cor));
            return aux;
        }
        private Cor Adversaria(Cor cor)
        {
            if (cor == Cor.branca)
            {
                return Cor.preto;
            }
            else return Cor.branca;
        }
        private Peca Rei(Cor cor)
        {
            foreach(Peca x in PecasEmJogo(cor))
            {
                if (x is Rei)
                {
                    return x;
                }
            }
            return null;
        }
        public bool EstaEmXeque(Cor cor)
        {
            Peca R = Rei(cor);
            if (R == null)
            {
                throw new TabuleiroException("Não tem rei da cor "+cor+" no tabuleiro");
            }

            foreach (Peca x in PecasEmJogo(Adversaria(cor)))
            {
                bool[,] mat = x.MovimentosPossiveis();
                if (mat[R.Posicao.Linha, R.Posicao.Coluna])
                {
                    return true;
                }

            }
            return false;

        }
        public bool TesteXequeMate(Cor cor)
        {
            if (!EstaEmXeque(cor))
            {
                return false;
            }
            foreach(Peca x in PecasEmJogo(cor))
            {
                bool[,] mat = x.MovimentosPossiveis();
                for(int i = 0; i < Tab.Linha; i++)
                {
                    for(int j = 0; i < Tab.Coluna; j++)
                    {
                        if (mat[i, j])
                        {
                            Posicao origem = x.Posicao;
                            Posicao destino = new Posicao(i, j);
                            Peca PecaCap = ExecutarMovimento(origem, destino);
                            bool TesteXeque = EstaEmXeque(cor);
                            DesfazMovimento(origem, destino, PecaCap);
                            if (!TesteXeque)
                            {
                                return false;
                            }
                        }
                    }
                }
            }
            return true;
        }
        public void ColocarNovaPeca(char coluna,int linha,Peca peca)
        {
            Tab.ColocarPeca(peca, new PosicaoXadrez(coluna, linha).ToPosicao());
            pecas.Add(peca);
        }

    }
}
