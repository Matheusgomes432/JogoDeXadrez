using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tabuleiro;

namespace xadrez_console.tabuleiro
{
     class Peca
    {
        public Posicao posicao { get; set; }
        public Cor cor { get; protected set; }

        public int  QteMovimentos { get; set; }
        public Tabuleiro Tab { get; set; }

        public Peca(Cor cor, Tabuleiro tab)
        {
            this.posicao = null;
            this.cor = cor;
            QteMovimentos = 0;
            Tab = tab;
        }

        public Peca(Tabuleiro tab, Cor cor)
        {
            Tab = tab;
            this.cor = cor;
        }
    }
}
