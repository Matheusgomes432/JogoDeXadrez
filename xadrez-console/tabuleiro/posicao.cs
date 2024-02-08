using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tabuleiro
{
    class posicao
    {
        public int Linha { get; set; }
        public int Coluna { get; set; }


        public posicao(int linha, int coluna)
        {
            Linha = linha;
            Coluna = coluna;
        }
    }
}
