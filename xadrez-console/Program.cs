﻿using System;
using System.Collections.Generic;
using xadrez_console.tabuleiro;

namespace xadre_console
{
    class program
    {
        static void Main(string[] args)
        {
           Tabuleiro tab = new Tabuleiro(8,8);

            Console.WriteLine(tab);

            Console.ReadLine();

        }
    }
}