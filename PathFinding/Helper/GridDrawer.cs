using System;
using System.Collections.Generic;
using static PathFinding.Helper.GridBuilder;

namespace PathFinding.Helper
{
    public static class GridDrawer
    {
        public static void DrawGrid(int[,] grid)
        {
            for (int y = 0; y < GridBuilder.GridSize; y++)
            {
                for (int x = 0; x < GridBuilder.GridSize; x++)
                {
                    switch ((BoxState)grid[x, y])
                    {
                        case BoxState.Empty:
                            Console.Write("0 ", grid[x, y]);
                            break;
                        case BoxState.Start:
                            Console.Write("S ", grid[x, y]);
                            break;
                        case BoxState.Goal:
                            Console.Write("G ", grid[x, y]);
                            break;
                        case BoxState.Block:
                            Console.ForegroundColor = ConsoleColor.DarkRed;
                            Console.Write("B ", grid[x, y]);
                            Console.ForegroundColor = ConsoleColor.White;
                            break;
                        case BoxState.DeadEnd:
                            Console.ForegroundColor = ConsoleColor.DarkCyan;
                            Console.Write("D ", grid[x, y]);
                            Console.ForegroundColor = ConsoleColor.White;
                            break;
                        default:
                            throw new Exception();

                    }
                }

                Console.WriteLine();
            }
        }

        public static void DrawPath(int[,] grid, List<Position> path, GridNavigator nav)
        {
            var i = 0;

            foreach (var node in path)
            {

                Console.ForegroundColor = nav.Success ? ConsoleColor.Green : ConsoleColor.DarkYellow;
                Console.SetCursorPosition(node.X * 2, node.Y);
                Console.Write(i >= 10 ? "{0}" : "{0} ", i);
                Console.ForegroundColor = ConsoleColor.White;

                i++;
            }
        }

        public static void DrawSymbols(int[,] grid, List<Position> path)
        {
            Console.ForegroundColor = ConsoleColor.Green;


            for (int y = 0; y < GridBuilder.GridSize; y++)
            {
                for (int x = 0; x < GridBuilder.GridSize; x++)
                {
                    switch ((BoxState)grid[x, y])
                    {
                        case BoxState.Start:
                            Console.SetCursorPosition(x * 2, y);
                            Console.Write("S ", grid[x, y]);
                            break;
                        case BoxState.Goal:
                            Console.SetCursorPosition(x * 2, y);
                            Console.Write("G ", grid[x, y]);
                            break;
                        default:
                            break;
                    }
                }
            }

            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
