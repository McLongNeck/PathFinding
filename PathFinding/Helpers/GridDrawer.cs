using PathFinding.Models;
using PathFinding.ViewModels;
using System;
using System.Collections.Generic;

namespace PathFinding.Helpers
{
    public static class GridDrawer
    {
        public static void DrawGrid(short[,] grid)
        {
            for (short y = 0; y < GridBuilder.GridSize; y++)
            {
                for (short x = 0; x < GridBuilder.GridSize; x++)
                {
                    switch ((NodeState)grid[x, y])
                    {
                        case NodeState.Empty:
                            Console.Write("0 ", grid[x, y]);
                            break;
                        case NodeState.Start:
                            Console.Write("S ", grid[x, y]);
                            break;
                        case NodeState.Goal:
                            Console.Write("G ", grid[x, y]);
                            break;
                        case NodeState.Block:
                            Console.ForegroundColor = ConsoleColor.DarkRed;
                            Console.Write("B ", grid[x, y]);
                            Console.ForegroundColor = ConsoleColor.White;
                            break;
                        case NodeState.DeadEnd:
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

        public static void DrawPath(short[,] grid, List<Position> path, Navigator nav)
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

        public static void DrawSymbols(short[,] grid, List<Position> path)
        {
            Console.ForegroundColor = ConsoleColor.Green;


            for (short y = 0; y < GridBuilder.GridSize; y++)
            {
                for (short x = 0; x < GridBuilder.GridSize; x++)
                {
                    switch ((NodeState)grid[x, y])
                    {
                        case NodeState.Start:
                            Console.SetCursorPosition(x * 2, y);
                            Console.Write("S ", grid[x, y]);
                            break;
                        case NodeState.Goal:
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
