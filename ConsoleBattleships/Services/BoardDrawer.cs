using System;
using System.Linq;
using ConsoleBattleships.Services.Interfaces;
using Core.Model;
using Core.Utils;

namespace ConsoleBattleships.Services
{
    public class BoardDrawer : IBoardDrawer
    {
        public void Draw(Board board)
        {
            DrawColumnsNames(board);
            DrawUpperBorder(board);
            DrawGrid(board);
            DrawLowerBorder(board);
        }


        private static void DrawColumnsNames(Board board)
        {
            var columnsNumber = board.Size.XSize;
            var columnNames = Enumerable.Range(1, columnsNumber)
                                        .Select(x => x.ToColumnName());

            Console.Write("{0,-5}", "");
            foreach (var name in columnNames)
            {
                Console.Write("{0,-4}", name);
            }

            Console.WriteLine();
        }


        private static void DrawUpperBorder(Board board)
        {
            DrawBorder(board);
        }


        private static void DrawBorder(Board board)
        {
            Console.Write("{0,-3}", "");
            var columnsNumber = board.Size.XSize;
            var numberOfDashes = columnsNumber * 4 + 1;
            var dashes = new string('-', numberOfDashes);
            Console.WriteLine(dashes);
        }


        private static void DrawGrid(Board board)
        {
            var rowsNumber = board.Size.YSize;

            var rows = Enumerable.Range(1, rowsNumber);
            foreach (var row in rows)
            {
                DrawRow(board, row);
            }
        }


        private static void DrawRow(Board board, int rowNumber)
        {
            var allCellsInRow = board.AllCells
                                     .Where(cell => cell.YCoordinate == rowNumber - 1)
                                     .OrderBy(x => x.XCoordinate)
                                     .ThenBy(x => x.YCoordinate);


            Console.Write("{0,2}", rowNumber);
            Console.Write("{0,2}", "|");

            foreach (var cell in allCellsInRow)
            {
                var cellState = board.CellState(cell);
                var symbol = SymbolFor(cellState);
                Console.Write("{0,2}", symbol);
                Console.Write("{0,2}", "|");
            }

            Console.WriteLine();
        }


        private static void DrawLowerBorder(Board board)
        {
            DrawBorder(board);
        }


        private static char SymbolFor(CellState state)
        {
            return state switch
            {
                CellState.Clear => ' ',
                CellState.Hit => 'H',
                CellState.Sunk => 'S',
                CellState.Miss => 'M',
                _ => throw new ArgumentOutOfRangeException(nameof(state), state, null)
            };
        }
    }
}