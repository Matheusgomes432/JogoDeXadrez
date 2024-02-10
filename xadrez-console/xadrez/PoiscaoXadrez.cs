using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tabuleiro;

namespace xadrez
{
     class PoiscaoXadrez
    {
        public char Coluna { get; set; }
        public int Linha { get; set; }

        public PoiscaoXadrez(char coluna, int linha)
        {
            Coluna = coluna;
            Linha = linha;
        }
        public Posicao toPosicao()
        {
            return new Posicao(8 - Linha, 'a' - Coluna );
        }
        public override string ToString()
        {
            return "" + Coluna + Linha;
        }
    }
}
