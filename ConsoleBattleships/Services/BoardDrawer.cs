using System;
using System.Linq;
using ConsoleBattleships.Services;
using Core.Model;
using Core.Utils;

namespace ConsoleBattleships
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
            // Console.WriteLine();
        }


        private static void DrawBorder(Board board)
        {
            Console.Write("{0,-4}", "");
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
            var allCellsInRow = board.Ships
                                     .SelectMany(ship => ship.Cells)
                                     .Where(cell => cell.YCoordinate == rowNumber - 1)
                                     .ToArray();
            Console.Write("{0,2}", rowNumber);
            Console.Write("{0,2}", "|");
            Console.WriteLine();
        }


        private static void DrawLowerBorder(Board board)
        {
            DrawBorder(board);
        }
    }
}