using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tabuleiro;

namespace xadrez_console.tabuleiro
{
     class peca
    {
        public posicao posicao { get; set; }
        public Cor cor { get; protected set; }

        public int  QteMovimentos { get; set; }
        public Tabuleiro Tab { get; set; }

        public peca(posicao posicao, Cor cor, int qteMovimentos, Tabuleiro tab)
        {
            this.posicao = posicao;
            this.cor = cor;
            qteMovimentos = 0;
            Tab = tab;
        }
    }
}
