using System;
using System.Collections.Generic;
using tabuleiro;
using xadrez;
using Xadrez;
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
                PartidaDeXadrez partida = new PartidaDeXadrez();

                while (!partida.Terminada) { 

                    Console.Clear();
                    Tela.imprimirTabuleiro(partida.Tab);


                    Console.WriteLine();
                    Console.Write("Origem: ");
                    Posicao origem = Tela.lerPosicaoXadrez().toPosicao();
                    Console.Write("Destino: ");
                    Posicao destino = Tela.lerPosicaoXadrez().toPosicao();

                    partida.executaMovimento(origem, destino);
                }

                
            }
            catch (TabulerioException e)
            {
                Console.WriteLine(e.Message);
            }
            Console.ReadLine();

        }
    }
}