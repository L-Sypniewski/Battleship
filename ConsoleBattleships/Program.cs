using System;
using System.Collections.Immutable;
using Core.Model;

namespace ConsoleBattleships
{
    class Program
    {
        static void Main(string[] args)
        {
            new BoardDrawer().Draw(GetBoard());
            Console.ReadKey();
        }


        private static Board GetBoard()
        {
            var boardSize = new BoardSize(5, 5);


            return new Board(boardSize, ImmutableArray<Ship>.Empty);
        }
    }
}