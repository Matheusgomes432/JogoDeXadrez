﻿using System;
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

        public PartidaDeXadrez()
        {
            Tab = new Tabuleiro(8, 8);
            Turno = 1;
            JogadorAtual = Cor.branca;
            Terminada = false;
            pecas = new HashSet<Peca>();
            capturadas= new HashSet<Peca>();

            colocarPecas();  
        }
        public void executaMovimento(Posicao origem, Posicao destino)
        {
            Peca p = Tab.retirarPeca(origem);
            p.incrementarQuantidadeDeMovmentos();
            Peca pecaCapturada = Tab.retirarPeca(destino);
            Tab.colocarPeca(p, destino);
            if(pecaCapturada != null)
            {
                capturadas.Add(pecaCapturada);
            }
        }
        public void realizaJogada(Posicao origem, Posicao destino)
        {
            executaMovimento(origem, destino);
            Turno++;
            mudaJogador();

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


        public void colocarNovaPeca(char coluna, int linha, Peca peca)
        {
            Tab.colocarPeca(peca, new PosicaoXadrez(coluna, linha).toPosicao());
            pecas.Add(peca);
        }

        private void colocarPecas()
        {
           

            colocarNovaPeca('c', 1,new Torre(Tab, Cor.branca));
            colocarNovaPeca('c', 2, new Torre(Tab, Cor.branca));
            colocarNovaPeca('d', 2, new Torre(Tab, Cor.branca));
            colocarNovaPeca('e', 2, new Torre(Tab, Cor.branca));
            colocarNovaPeca('e', 1, new Torre(Tab, Cor.branca));
            colocarNovaPeca('d', 1, new Rei(Tab, Cor.branca));

            colocarNovaPeca('c', 7, new Torre(Tab, Cor.preta));
            colocarNovaPeca('c', 8, new Torre(Tab, Cor.preta));
            colocarNovaPeca('d', 7, new Torre(Tab, Cor.preta));
            colocarNovaPeca('e', 7, new Torre(Tab, Cor.preta));
            colocarNovaPeca('e', 8, new Torre(Tab, Cor.preta));
            colocarNovaPeca('d', 8, new Rei(Tab, Cor.preta));

        }
     }
}
