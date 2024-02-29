using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tabuleiro;
using xadrez;
using xadrez_console.tabuleiro;

namespace Xadrez
{
     class PartidaDeXadrez
    {
        public Tabuleiro Tab { get; private set; }
        public int Turno { get; private set; }
        public Cor JogadorAtual { get; set; }
        public bool Terminada { get; private set; }
        private HashSet<Peca> pecas;
        private HashSet<Peca> capturadas;
        public bool Xeque { get; private set; }
        public PartidaDeXadrez()
        {
            Tab = new Tabuleiro(8, 8);
            Turno = 1;
            JogadorAtual = Cor.branca;
            Xeque= false;
            Terminada = false;
            pecas = new HashSet<Peca>();
            capturadas= new HashSet<Peca>();

            colocarPecas();  
        }
        public Peca executaMovimento(Posicao origem, Posicao destino)
        {
            Peca p = Tab.retirarPeca(origem);
            p.incrementarQuantidadeDeMovmentos();
            Peca pecaCapturada = Tab.retirarPeca(destino);
            Tab.colocarPeca(p, destino);
            if(pecaCapturada != null)
            {
                capturadas.Add(pecaCapturada);
            }
            return pecaCapturada;
        }
        public void desfazMovimento(Posicao origem,Posicao destino, Peca pecaCapturada)
        {
            Peca p = Tab.retirarPeca(destino);
            p.descrementarQuantidadeDeMovmentos();
            if(pecaCapturada!=null) {
                Tab.colocarPeca(pecaCapturada, destino);
                capturadas.Remove(pecaCapturada);
            }
            Tab.colocarPeca(p, origem);
        }

        public void realizaJogada(Posicao origem, Posicao destino)
        {
          Peca pecaCapturada =  executaMovimento(origem, destino);

            if (estaEmXeque(JogadorAtual)) {
                desfazMovimento(origem, destino, pecaCapturada);
                throw new TabulerioException("Você não pode se colcoar em xeque!");
            }
            if (estaEmXeque(adversaria(JogadorAtual))) {
                Xeque = true;
            }
            else { 
                Xeque = false;
            }
            if (xequeMate(adversaria(JogadorAtual)))
            {
                Terminada = true;
            }
            else
            {
                Turno++;
                mudaJogador();
            }

        }

        public void validarPosicaoDeOrigem(Posicao pos)
        {
            if (Tab.peca(pos) == null)
            {
                throw new TabulerioException("Não existe peça na posição de origem escolhida!");
            }
            if(JogadorAtual != Tab.peca(pos).cor)
            {
                throw new TabulerioException("A peça de origem escolhida não é sua!");
            }
            if (!Tab.peca(pos).existeMovimentosPossiveis())
            {
                throw new TabulerioException("Não há movimentos possiveis para a peça de origem escolhida!");
            }
        }
        
        public void validarPosicaoDeDestino(Posicao origem, Posicao destino)
        {
            if (!Tab.peca(origem).podeMoverPara(destino))
            {
                throw new TabulerioException("Posição de destino invalida!");
            }
        }

        private void mudaJogador()
        {
            if(JogadorAtual == Cor.branca) {
                JogadorAtual = Cor.preta;
            }
            else{
                JogadorAtual = Cor.branca;
            }
        }

        public HashSet<Peca> pecasCaputuras(Cor cor) { 
            HashSet<Peca> aux = new HashSet<Peca>();
            foreach(Peca x in capturadas) { 
                if(x.cor == cor)
                {
                    aux.Add(x);
                }
            }
            return aux;
        }
      
        public HashSet<Peca> pecaEmJogo(Cor cor)
        {
            HashSet<Peca> aux = new HashSet<Peca>();
            foreach (Peca x in pecas)
            {
                if (x.cor == cor)
                {
                    aux.Add(x);
                }
            }
            aux.ExceptWith(pecasCaputuras(cor));
            return aux;
        }

        private Cor adversaria(Cor cor)
        {
            if (cor == Cor.branca) {
                return Cor.preta;
            }
            else {
                return Cor.branca;
            }
        }

        private Peca rei(Cor cor)
        {
            foreach(Peca x in pecaEmJogo(cor))
            {
                if(x is Rei)
                {
                    return x;
                }
            }
            return null;
        }

        public bool estaEmXeque(Cor cor)
        {
            Peca R = rei(cor);
            if(R == null) {
                throw new TabulerioException("Não tem rei da cor " + cor + "no tabuleiro!");
            }

            foreach(Peca x in pecaEmJogo(adversaria(cor))) {
                bool[,] mat = x.movimentosPossiveis();
                if (mat[R.posicao.Linha, R.posicao.Coluna]) {
                    return true;
                }
            }
            return false;
        }

        public bool xequeMate(Cor cor)
        {
            if (!estaEmXeque(cor)) { 
                return false;
            }
            foreach (Peca x in pecaEmJogo(cor))
            {
                bool[,] mat = x.movimentosPossiveis();
                for(int i=0; i<Tab.Linhas; i++) { 
                    for(int j=0; j<Tab.Colunas; j++) {
                        if (mat[i,j]) {
                            Posicao origem = x.posicao;
                            Posicao destino = new Posicao(i, j);
                            Peca pecaCapturada = executaMovimento(origem, destino);
                            bool testeXeque = estaEmXeque(cor);
                            desfazMovimento(origem, destino, pecaCapturada);
                            if (!testeXeque) {
                                return false;
                            }
                        }
                    }
                }
            }
            return true;
        }


        public void colocarNovaPeca(char coluna, int linha, Peca peca)
        {
            Tab.colocarPeca(peca, new PosicaoXadrez(coluna, linha).toPosicao());
            pecas.Add(peca);
        }

        private void colocarPecas()
        {
           

            colocarNovaPeca('c', 1,new Torre(Tab, Cor.branca));
            colocarNovaPeca('h', 7, new Torre(Tab, Cor.branca));
           
            colocarNovaPeca('d', 1, new Rei(Tab, Cor.branca));

            colocarNovaPeca('b', 8, new Torre(Tab, Cor.preta));
           
            colocarNovaPeca('a', 8, new Rei(Tab, Cor.preta));

        }
     }
}
