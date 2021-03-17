using System;
using ConsoleBattleships.Services.Interfaces;
using Core.Exceptions;
using Core.Model;
using Core.Services;
using Core.Utils;

namespace ConsoleBattleships.Services
{
    public class GameConsole
    {
        private readonly IBoardDrawer _boardDrawer;
        private readonly IGame _game;
        private readonly IInputToCellConverter _inputToCellConverter;


        public GameConsole(IBoardDrawer boardDrawer, IGame game, IInputToCellConverter inputToCellConverter)
        {
            _boardDrawer = boardDrawer;
            _game = game;
            _inputToCellConverter = inputToCellConverter;
        }


        public void Play()
        {
            var board = _game.StartGame();
            var counter = 1;
            do
            {
                DrawSeparator();
                Console.WriteLine($"Round no {counter}");

                _boardDrawer.Draw(board);

                var cellToShot = GetCellToShotAt();

                Console.WriteLine();

                var shotResult = MakeShot(cellToShot, board);
                board = shotResult.UpdatedBoard;

                PrintShotResult(shotResult, cellToShot);

                if (_game.IsFinished(board))
                {
                    Console.WriteLine("You sunk all the ships!!!");
                    break;
                }

                counter++;
            } while (true);
        }


        private static void DrawSeparator() => Console.WriteLine(new string('*', 10));


        private Cell GetCellToShotAt()
        {
            Console.WriteLine("Choose a cell to shoot");
            do
            {
                var cellToShotInput = Console.ReadLine();
                var cellToShot = _inputToCellConverter.ConvertedFrom(cellToShotInput ?? "");
                if (cellToShot != null)
                {
                    return cellToShot;
                }

                Console.WriteLine("Your input was invalid, select a cell again");
            } while (true);
        }


        private GameMoveResult MakeShot(Cell cellToShot, Board board)
        {
            try
            {
                var result = _game.ShootAt(board, cellToShot!);
                return result;
            }
            catch (Exception exception) when (exception is CannotMakeOutOfBoundsShotException ||
                                              exception is CannotShotAlreadyShotCellException)
            {
                var errorMessage = ErrorMessageFor(exception);
                var cellCoordinatesText = $"{( cellToShot.XCoordinate + 1 ).ToColumnName()}{cellToShot.YCoordinate + 1}";

                Console.WriteLine($"{errorMessage}\nInvalid cell: {cellCoordinatesText}\n");

                var newCellToShot = GetCellToShotAt();
                return MakeShot(newCellToShot, board);
            }
        }


        private static void PrintShotResult(GameMoveResult result, Cell shotCell)
        {
            var hitShip = result.HitShip;
            if (hitShip != null)
            {
                Console.WriteLine($"\nYou hit a Ship: {hitShip.Name}");
                if (hitShip.IsSunk)
                {
                    Console.WriteLine("The ship is sunk");
                }

                Console.WriteLine();
                return;
            }

            var cellState = result.UpdatedBoard.CellState(shotCell);
            if (cellState == CellState.Miss)
            {
                Console.WriteLine("You missed");
            }
        }


        private static string ErrorMessageFor(Exception exception)
        {
            return exception switch
            {
                CannotMakeOutOfBoundsShotException => "Cannot make a shot outside of board.",
                CannotShotAlreadyShotCellException => "Cannot shoot at already shot cell.",
                _ => "Unknown error"
            };
        }
    }
}