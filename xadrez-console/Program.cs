using System;
using System.Collections.Generic;
using tabuleiro;
using xadrez;
using xadrez_console;
using xadrez_console.tabuleiro;

namespace xadre_console
{
    class program
    {
        static void Main(string[] args)
        {
            try
            {
                Tabuleiro tab = new Tabuleiro(8, 8);

                tab.colocarPeca(new Torre(tab, Cor.preta), new Posicao(0, 0));
                tab.colocarPeca(new Torre(tab, Cor.preta), new Posicao(1, 3));
                tab.colocarPeca(new Rei(tab, Cor.preta), new Posicao(1, 4));
                tab.colocarPeca(new Torre(tab, Cor.branca), new Posicao(3, 5));

                Tela.imprimirTabuleiro(tab);
            }
            catch (TabulerioException e)
            {
                Console.WriteLine(e.Message);
            }
            Console.ReadLine();

        }
    }
}