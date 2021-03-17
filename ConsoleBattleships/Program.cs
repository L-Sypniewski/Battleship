using System;
using System.Collections.Generic;
using ConsoleBattleships.Services;
using Core.Model;
using Core.Services;

namespace ConsoleBattleships
{
    internal static class Program
    {
        private const int MaxAttempts = 20000;
        private static BoardSize BoardSize => new(10,10);


        private static void Main(string[] args)
        {
            var game = CreateGame();
            var gameConsole = new ConsoleGame(new BoardDrawer(), game, new InputToCellConverter());

            gameConsole.Play();
            Console.ReadKey();
        }


        private static Game CreateGame() // DI container would be an overkill here
        {
            var shipConfigurations = CreateShipConfigurations();

            var boardVerifier = new BoardVerifier();
            var shipPositioner = new ShipPositioner(MaxAttempts, new ShipOrientationRandomizer(),
                                                    new CellRandomizer(), boardVerifier,
                                                    new StandardBattleshipGameRulesCellVerifier());
            var boardInitializer = new BoardInitializer(shipPositioner, new CellVerifier(), MaxAttempts);

            var game = new Game(BoardSize, shipConfigurations, boardInitializer, boardVerifier);
            return game;
        }


        private static HashSet<ShipConfiguration> CreateShipConfigurations() 
        {
            // Might be read from a file using for example IConfiguration with options pattern
            var battleshipConfiguration = new ShipConfiguration("Battleship", 5, 1);
            var destroyerConfiguration = new ShipConfiguration("Destroyer", 4, 2);
            var shipConfigurations = new HashSet<ShipConfiguration>(new[] {battleshipConfiguration, destroyerConfiguration});
            return shipConfigurations;
        }
    }
}