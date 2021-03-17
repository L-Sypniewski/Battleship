using System;
using System.Collections.Generic;
using ConsoleBattleships.Services;
using Core.Model;
using Core.Services;

namespace ConsoleBattleships
{
    internal class Program
    {
        private const int MaxAttempts = 20;


        private static void Main(string[] args)
        {
            var game = CreateGame();
            var gameConsole = new GameConsole(new BoardDrawer(), game, new InputToCellConverter());

            gameConsole.Play();
            Console.ReadKey();
        }


        private static Game CreateGame()
        {
            var shipConfiguration = new ShipConfiguration("Destroyer", 2, 2);
            var shipConfigurations = new HashSet<ShipConfiguration>(new[] {shipConfiguration});
            var boardSize = new BoardSize(3, 3);

            var cellVerifier = new CellVerifier();
            var shipPositioner = new ShipPositioner(MaxAttempts, new ShipOrientationRandomizer(),
                                                    new CellRandomizer(), new BoardVerifier(),
                                                    new StandardBattleshipGameRulesCellVerifier());
            var boardInitializer = new BoardInitializer(shipPositioner, cellVerifier, MaxAttempts);

            var game = new Game(boardSize, shipConfigurations, boardInitializer, new BoardVerifier());
            return game;
        }
    }
}