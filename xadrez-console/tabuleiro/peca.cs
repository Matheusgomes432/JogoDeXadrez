using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tabuleiro;

namespace xadrez_console.tabuleiro
{
    abstract class Peca
    {
        public Posicao posicao { get; set; }
        public Cor cor { get; protected set; }

        public int  QteMovimentos { get; protected set; }
        public Tabuleiro Tab { get; protected set; }

        public Peca(Cor cor, Tabuleiro tab)
        {
            this.posicao = null;
            this.cor = cor;
            this.QteMovimentos = 0;
            Tab = tab;
        }

        public Peca(Tabuleiro tab, Cor cor)
        {
            Tab = tab;
            this.cor = cor;
        }
        public void incrementarQuantidadeDeMovmentos()
        {
            QteMovimentos++;
        }
        public void descrementarQuantidadeDeMovmentos()
        {
            QteMovimentos--;
        }


        public bool existeMovimentosPossiveis()
        {
            bool[,] mat = movimentosPossiveis();
            for(int i = 0; i<Tab.Linhas; i ++) {
                for(int j = 0; j<Tab.Colunas; j++) {
                    if (mat[i, j])
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        public bool movimentoPossivel(Posicao pos)
        {
            return movimentosPossiveis()[pos.Linha, pos.Coluna];
        }

        public abstract bool[,] movimentosPossiveis();
        
    }
}
